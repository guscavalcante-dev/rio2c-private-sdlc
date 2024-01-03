// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Elton Assunção
// Created          : 12-22-2023
//
// Last Modified By : Elton Assunção
// Last Modified On : 12-22-2023
// ***********************************************************************
// <copyright file="innovation.players.executives.datatable.widget" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var InnovationPlayersExecutivesDataTableWidget = function () {

    var widgetElementId = '#PlayersExecutivesDataTableWidget';
    var tableElementId = '#playersexecutives-list-table';
    var table;

    // Invitation email ---------------------------------------------------------------------------
    var sendInvitationEmails = function () {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.selectedCollaboratorsUids = $('#playersexecutives-list-table_wrapper tr.selected').map(function () { return $(this).data('id'); }).get().join(',');

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/PlayersExecutives/SendInvitationEmails'), jsonParameters, function (data) {
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

            if (typeof (InnovationPlayersExecutivesDataTableWidget) !== 'undefined') {
                InnovationPlayersExecutivesDataTableWidget.refreshData();
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
    var initiListTable = function () {

        var tableElement = $(tableElementId);

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
                    //{
                    //    text: sendInvitationEmail,
                    //    action: function (e, dt, node, config) {
                    //        $('.dt-button-background').remove();
                    //        showSendInvitationEmailsModal();
                    //    }
                    //},
                    //{
                    //    text: exportToEventbrite,
                    //    action: function (e, dt, node, config) {
                    //        $('.dt-button-background').remove();
                    //        var eventbriteCsvExport = dt.ajax.params();
                    //        eventbriteCsvExport.selectedCollaboratorsUids = $('#playersexecutives-list-table_wrapper tr.selected').map(function () { return $(this).data('id'); }).get().join(',');
                    //        eventbriteCsvExport.showAllEditions = $('#ShowAllEditions').prop('checked');
                    //        eventbriteCsvExport.showAllParticipants = $('#ShowAllParticipants').prop('checked');
                    //        eventbriteCsvExport.collaboratorTypeName = collaboratorTypeName;

                    //        SalesPlatformsExport.showExportEventbriteCsvModal(eventbriteCsvExport);
                    //    }
                    //},
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
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/PlayersExecutives/Search'),
                data: function (d) {
                    d.showAllEditions = $('#ShowAllEditions').prop('checked');
                    d.showAllParticipants = $('#ShowAllParticipants').prop('checked');
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

                        if (!full.Active) {
                            html += '<span class="kt-badge kt-badge--inline kt-badge--info mt-2" style="color: #ffffff; background: #E66E72">' + labels.blocked + '</span><br>';
                        }

                        if (!full.IsInCurrentEdition) {
                            html += '<span class="kt-badge kt-badge--inline kt-badge--info mt-2">' + labels.notInEdition + '</span>';
                        }

                        return html;
                    }
                },
                { data: 'Email' },
                {
                    data: 'Player',
                    render: function (data, type, row, meta) {
                        var html = '<ul class="m-0 pl-4">';

                        //loop through all the row details to build output string
                        for (var item in row.AttendeeOrganizationBasesDtos) {
                            if (row.AttendeeOrganizationBasesDtos.hasOwnProperty(item)) {
                                var r = row.AttendeeOrganizationBasesDtos[item];
                                html += '<li>' + r.OrganizationBaseDto.DisplayName + '</li>';
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

                        return '';
                    }
                },
                {
                    data: 'Actions',
                    responsivePriority: -1,
                    render: function (data, type, full, meta) {
                        //var html = '\
                        //                <span class="dropdown">\
                        //                    <a href="#" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown" aria-expanded="true">\
                        //                      <i class="la la-ellipsis-h"></i>\
                        //                    </a>\
                        //                    <div class="dropdown-menu dropdown-menu-right">';

                        //if (!full.IsInCurrentEdition) {
                        //    html += '<button class="dropdown-item" onclick="InnovationPlayersExecutivesUpdate.showModal(\'' + full.Uid + '\', true);"><i class="la la-plus"></i> ' + addToEdition + '</button>';
                        //}

                        //if (full.IsInCurrentEdition) {
                        //    html += '<button class="dropdown-item" onclick="InnovationPlayersExecutivesDataTableWidget.showDetails(\'' + full.Uid + '\');"><i class="la la-eye"></i> ' + labels.view + '</button>';
                        //}

                        //html += '<button class="dropdown-item" onclick="AccountsUpdateUserStatus.showModal(\'' + full.UserBaseDto.Uid + '\',\'' + !full.Active + '\');"><i class="la la-lock"></i> ' + ((full.Active) ? labels.block : labels.unblock) + '</button>';

                        //if (full.IsInCurrentEdition && full.IsInOtherEdition) {
                        //    html += '<button class="dropdown-item" onclick="InnovationPlayersExecutivesDelete.showModal(\'' + full.Uid + '\', true);"><i class="la la-remove"></i> ' + removeFromEdition + '</button>';
                        //}
                        //else {
                        //    html += '<button class="dropdown-item" onclick="InnovationPlayersExecutivesDelete.showModal(\'' + full.Uid + '\', false);"><i class="la la-remove"></i> ' + labels.remove + '</button>';
                        //}

                        //html += '\
                        //                    </div>\
                        //                </span>';

                        //return html;
                        return "";
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
                    targets: [2],
                    orderable: false
                },
                {
                    targets: [3, 4],
                    className: "dt-center"
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

        window.location.href = MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/PlayersExecutives/Details/' + commissionUid);
    };

    // Export -------------------------------------------------------------------------------------
    var exportPlayersExecutivesReportToExcel = function () {

        var jsonParameters = new Object();
        jsonParameters.search = $('#Search').val();
        jsonParameters.showAllEditions = $('#ShowAllEditions').prop('checked');
        jsonParameters.showAllParticipants = $('#ShowAllParticipants').prop('checked');

        location.href = '/Innovation/PlayersExecutives/ExportPlayersExecutivesReportToExcel?' + jQuery.param(jsonParameters);
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
        },
        exportPlayersExecutivesReportToExcel: function () {
            exportPlayersExecutivesReportToExcel();
        }
    };
}();