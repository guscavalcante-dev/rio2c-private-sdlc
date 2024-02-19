// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 02-14-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-14-2024
// ***********************************************************************
// <copyright file="creator.commissions.datatable.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var CreatorCommissionsDataTableWidget = function () {

    var widgetElementId = '#CreatorCommissionsDataTableWidget';
    var tableElementId = '#creator-commissions-list-table';
    var modalId = '#ExportEventbriteCsvModal';
    var table;
    var eventbriteCsvExport;

    // Invitation email ---------------------------------------------------------------------------
    var sendInvitationEmails = function () {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.selectedCollaboratorsUids = $('#creator-commissions-list-table_wrapper tr.selected').map(function () { return $(this).data('id'); }).get().join(',');

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Creator/Commissions/SendInvitationEmails'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                },
                // Error
                onError: function () {
                }
            });
        })
        .fail(function () {
        })
        .always(function () {
            MyRio2cCommon.unblock();

            if (typeof (CreatorCommissionsDataTableWidget) !== 'undefined') {
                CreatorCommissionsDataTableWidget.refreshData();
            }
        });
    };

    var showSendInvitationEmailsModal = function () {
        bootbox.dialog({
            message: confirmToSendInvitationEmails,
            buttons: {
                cancel: {
                    label: labels.cancel,
                    className: "btn btn-secondary btn-elevate mr-auto",
                    callback: function () {
                    }
                },
                confirm: {
                    label: labels.send,
                    className: "btn btn-brand btn-elevate",
                    callback: function () {
                        sendInvitationEmails();
                    }
                }
            }
        });
    };

    // Init datatable -----------------------------------------------------------------------------
    var enableFormPlugins = function () {
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '.enable-select2' });
    };

    var initiListTable = function () {
        var tableElement = $(tableElementId);

        enableFormPlugins();

        // Disable datatable alert
        $.fn.dataTable.ext.errMode = 'none';

        // Set initial page size
        var pageLengthOptions = [1, 10, 25, 50, 100];
        var pageLength = 10;
        if (!MyRio2cCommon.isNullOrEmpty(initialPageSize) && pageLengthOptions.includes(initialPageSize)) {
            pageLength = initialPageSize;
        }

        // Set initial page
        var displayStart = 0;
        if (!MyRio2cCommon.isNullOrEmpty(initialPage)) {
            displayStart = (initialPage - 1) * pageLength;
        }

        var globalVariables = MyRio2cCommon.getGlobalVariables();
        var imageDirectory = 'https://' + globalVariables.bucket + '/img/users/';

        // Initiate datatable
        table = tableElement.DataTable({
            "language": {
                "url": "/Assets/components/datatables/datatables." + globalVariables.userInterfaceLanguage + ".js"
            },
            select: {
                style: 'multi'
            },
            lengthMenu: [pageLengthOptions, pageLengthOptions],
            displayStart: displayStart,
            pageLength: pageLength,
            responsive: true,
            sScrollY: "520",
            //bScrollCollapse: false,
            searchDelay: 2000,
            processing: true,
            serverSide: true,
            buttons: [
            {
                extend: 'collection',
                text: labels.actions,
                buttons: [
                    {
                        text: sendInvitationEmail,
                        action: function (e, dt, node, config) {
                            $('.dt-button-background').remove();
                            showSendInvitationEmailsModal();
                        }
                    },
                    {
                        text: labels.selectAll,
                        action: function (e, dt, node, config) {
                            $('.dt-button-background').remove();
                            table.rows().select();
                        }
                    },
                    {
                        text: labels.unselectAll,
                        action: function (e, dt, node, config) {
                            $('.dt-button-background').remove();
                            table.rows().deselect();
                        }
                    }]
            }],
            order: [[0, "asc"]],
            sDom: '<"row"<"col-sm-6"l><"col-sm-6 text-right"B>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            oSearch: {
                sSearch: $('#Search').val()
            },
            ajax: {
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Creator/Commissions/Search'),
                data: function (d) {
                    d.showAllEditions = $('#ShowAllEditions').prop('checked');
                    d.showAllParticipants = $('#ShowAllParticipants').prop('checked');
                    d.creatorOrganizationTrackOptionGroupUid = $('#CreatorOrganizationTrackOptionGroupUid').val();
                },
                dataFilter: function (data) {
                    var jsonReturned = jQuery.parseJSON(data);

                    return MyRio2cCommon.handleAjaxReturn({
                        data: jsonReturned,
                        // Success
                        onSuccess: function () {

                            // Parameters returned with capital letter
                            var json = new Object();
                            json.draw = jsonReturned.dataTable.Draw;
                            json.error = jsonReturned.dataTable.Error;
                            json.recordsTotal = jsonReturned.dataTable.TotalRecords;
                            json.recordsFiltered = jsonReturned.dataTable.TotalRecordsFiltered;
                            json.data = jsonReturned.dataTable.Data;

                            return JSON.stringify(json); // return JSON string
                        },
                        // Error
                        onError: function () {
                        }
                    });
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $(tableElementId + '_processing').hide();
                    MyRio2cCommon.showAlert();
                }
            },
            createdRow: function (row, data, dataIndex) {
                $(row).attr('data-id', data.Uid);
            },
            columns: [
                {
                    data: 'FullName',
                    render: function (data, type, full, meta) {
                        var html = '\
                                <table class="image-side-text text-left">\
                                    <tr>\
                                        <td>';

                        if (!MyRio2cCommon.isNullOrEmpty(full.ImageUploadDate)) {
                            html += '<img src="' + imageDirectory + full.Uid + '_thumbnail.png?v=' + moment(full.ImageUploadDate).locale(globalVariables.userInterfaceLanguage).format('YYYYMMDDHHmmss') + '" /> ';
                        }
                        else {
                            html += '<img src="' + imageDirectory + 'no-image.png?v=20190818200849" /> ';
                        }

                        html += '       <td> ' + full.FullName + '</td>\
                                    </tr>\
                                </table>';

                        if (!full.IsInCurrentEdition) {
                            html += '<span class="kt-badge kt-badge--inline kt-badge--info mt-2">' + labels.notInEdition + '</span>';
                        }

                        return html;
                    }
                },
                {
                     data: 'Email'
                },
                {
                    data: 'Company',
                    render: function (data, type, row, meta) {
                        var html = '<ul class="m-0 pl-4">';

                        //loop through all the row details to build output string
                        for (var item in row.AttendeeOrganizationBasesDtos) {
                            if (row.AttendeeOrganizationBasesDtos.hasOwnProperty(item)) {
                                var r = row.AttendeeOrganizationBasesDtos[item];
                                html += '<li>' + r.DisplayName + '</li>';
                            }
                        }

                        html += '</ul>';

                        return html;
                    }
                },
                {
                    data: 'CreateDate',
                    render: function (data) {
	                    return moment(data).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('L LTS');
                    }
                },
                {
                    data: 'WelcomeEmailSentDate',
                    render: function (data) {
                        if (data !== null) {
                            return moment(data).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('L LTS');
                        }

                        return '-';
                    }
                },
                {
                    data: 'CurrentEditionOnboardingFinishDate',
                    render: function (data) {
                        if (data !== null) {
                            return moment(data).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('L LTS');
                        }

                        return '-';
                    }
                },
                {
                    data: 'Actions',
                    responsivePriority: -1,
                    render: function (data, type, full, meta) {
                        var html = '\
                                        <span class="dropdown">\
                                            <a href="#" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown" aria-expanded="true">\
                                              <i class="la la-ellipsis-h"></i>\
                                            </a>\
                                            <div class="dropdown-menu dropdown-menu-right">';

                        if (!full.IsInCurrentEdition) {
                            html += '<button class="dropdown-item" onclick="CreatorCommissionsUpdate.showModal(\'' + full.Uid + '\', true);"><i class="la la-plus"></i> ' + addToEdition + '</button>';
                        }
                        else {
                            html += '<button class="dropdown-item" onclick="CreatorCommissionsDataTableWidget.showDetails(\'' + full.Uid + '\', false);"><i class="la la-eye"></i> ' + labels.view + '</button>';
                        }

                        if (full.IsInCurrentEdition && full.IsInOtherEdition) {
                            html += '<button class="dropdown-item" onclick="CreatorCommissionsDelete.showModal(\'' + full.Uid + '\', true);"><i class="la la-minus"></i> ' + removeFromEdition + '</button>';
                        }
                        else {
                            html += '<button class="dropdown-item" onclick="CreatorCommissionsDelete.showModal(\'' + full.Uid + '\', false);"><i class="la la-remove"></i> ' + labels.remove + '</button>';
                        }

                        html += '\
                                            </div>\
                                        </span>';

                        return html;
                    }
                }
            ],
            columnDefs: [
                {
                    targets: [0],
                    width: "25%",
                    className: "dt-center"
                },
                {
                    targets: [2, 3],
                    orderable: false
                },
                {
                    targets: [4],
                    className: "dt-center"
                },
                {
                    targets: [5, 6],
                    className: "dt-center",
                    orderable: false
                },
                {
                    targets: -1,
                    width: "10%",
                    orderable: false,
                    searchable: false,
                    className: "dt-center"
                }
            ],
            initComplete: function() {
                $('button.buttons-collection').attr('data-toggle', 'dropdown');
            }
        });

        $('#Search').keyup(function (e) {
            if (e.keyCode === 13) {
                table.search($(this).val()).draw();
            }
        });

        $('#CreatorOrganizationTrackOptionGroupUid').on('change', function (e) {
            table.ajax.reload();
        });

        $('.enable-datatable-reload').click(function (e) {
            table.ajax.reload();
        });

        MyRio2cCommon.unblock({ idOrClass: widgetElementId });
    };

    var refreshData = function () {
        table.ajax.reload();
    };

    var showDetails = function (commissionUid) {
        if (MyRio2cCommon.isNullOrEmpty(commissionUid)) {
            return;
        }

        window.location.href = MyRio2cCommon.getUrlWithCultureAndEdition('/Creator/Commissions/Details/' + commissionUid);
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            initiListTable();
        },
        refreshData: function () {
            refreshData();
        },
        exportEventbriteCsv: function() {
            exportEventbriteCsv();
        },
        showDetails: function (commissionUid) {
            showDetails(commissionUid);
        }
    };
}();