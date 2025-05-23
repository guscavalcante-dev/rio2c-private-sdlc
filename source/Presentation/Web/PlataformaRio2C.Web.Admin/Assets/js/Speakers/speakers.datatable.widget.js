﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 12-16-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-31-2023
// ***********************************************************************
// <copyright file="speakers.datatable.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var SpeakersDataTableWidget = function () {

    var widgetElementId = '#SpeakersDataTableWidget';
    var tableElementId = '#speakers-list-table';
    var table;

    // Invitation email ---------------------------------------------------------------------------
    var sendInvitationEmails = function () {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.selectedCollaboratorsUids = $('#speakers-list-table_wrapper tr.selected').map(function () { return $(this).data('id'); }).get().join(',');

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Speakers/SendInvitationEmails'), jsonParameters, function (data) {
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
                        {
                            name: 'btnExportToExcel',
                            text: exportToExcelText,
                            action: function (e, dt, node, config) {
                                $('.dt-button-background').remove();
                                exportToExcel();
                            }
                        },
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
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Speakers/Search'),
                data: function (d) {
                    d.showAllEditions = $('#ShowAllEditions').prop('checked');
                    d.showAllParticipants = $('#ShowAllParticipants').prop('checked');
                    d.showHighlights = $('#ShowHighlights').prop('checked');
                    d.showNotPublishableToApi = $('#ShowNotPublishableToApi').prop('checked');
                    d.roomsUids = $('#RoomsUids').val().join(',');
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
                            html += '   <div class="text-center w-100">'
                                + '             <div class="kt-userpic kt-userpic--md kt-userpic--brand">'
                                + '                 <span>' + full.NameAbbreviation + '</span>'
                                + '             </div>'
                                + '     </div>';
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
                    data: 'UpdateDate',
                    render: function (data) {
                        return moment(data).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('L LTS');
                    }
                },
                {
                    data: 'SpeakerCurrentEditionOnboardingFinishDate',
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
                    data: 'RequiredFieldsToPublish',
                    render: function (data) {
                        let tooltip = '';
                        let isValid = true;
                        const keys = Object.keys(data);
                        for (let i = 0; i < keys.length; i++) {
                            const _key = keys[i];
                            if (!data[_key].IsValid) {
                                isValid = false;
                                tooltip = `${tooltip}${data[_key].Message}\n`;
                            }
                        }
                        if (isValid == true)
                            return '<span class="kt-pricing-1__icon kt-font-success" data-toggle="tooltip" data-placement="right" style="cursor: pointer;" title="' + publishable + '"><i class="fa flaticon2-check-mark"></i></span>';
                        else
                            return '<span class="kt-pricing-1__icon kt-font-danger" data-toggle="tooltip" data-placement="right" style="cursor: pointer;" title="' + tooltip + '"><i class="fa flaticon2-cross"></i></span>';
                    }
                },
                {
                    data: 'IsApiDisplayEnabled',
                    render: function (data) {
                        if (data == true)
                            return '<span class="kt-pricing-1__icon kt-font-success" data-toggle="tooltip" data-placement="right" style="cursor: pointer;" title="' + showingOnSiteEdition + '"><i class="fa flaticon2-check-mark"></i></span>';
                        else
                            return '<span class="kt-pricing-1__icon kt-font-danger" data-toggle="tooltip" data-placement="right" style="cursor: pointer;" title="' + notShowingOnSiteEdition + '"><i class="fa flaticon2-cross"></i></span>';
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
                            html += '<button class="dropdown-item" onclick="SpeakersUpdate.showModal(\'' + full.Uid + '\', true);"><i class="la la-plus"></i> ' + addToEdition + '</button>';
                        }
                        else {
                            html += '<button class="dropdown-item" onclick="SpeakersDataTableWidget.showDetails(\'' + full.Uid + '\', false);"><i class="la la-eye"></i> ' + labels.view + '</button>';
                        }

                        if (full.IsInCurrentEdition && full.IsInOtherEdition) {
                            html += '<button class="dropdown-item" onclick="SpeakersDelete.showModal(\'' + full.Uid + '\', true);"><i class="la la-remove"></i> ' + removeFromEdition + '</button>';
                        }
                        else {
                            html += '<button class="dropdown-item" onclick="SpeakersDelete.showModal(\'' + full.Uid + '\', false);"><i class="la la-remove"></i> ' + labels.remove + '</button>';
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
                    targets: [2],
                    className: "dt-center"
                },
                {
                    targets: [3, 4],
                    className: "dt-center",
                    orderable: false
                },
                {
                    targets: [5, 6],
                    width: "5%",
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
            initComplete: function () {
                $('button.buttons-collection').attr('data-toggle', 'dropdown');

                if (!isAdminFull) {
                    table.buttons(0, 'btnExportToExcel:name').remove();
                }
            }
        });

        $('#Search').keyup(function (e) {
            if (e.keyCode === 13) {
                table.search($(this).val()).draw();
            }
        });

        $('#RoomsUids').change(function () {
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

    var showDetails = function (collaboratorUid) {
        if (MyRio2cCommon.isNullOrEmpty(collaboratorUid)) {
            return;
        }

        window.location.href = MyRio2cCommon.getUrlWithCultureAndEdition('/Speakers/Details/' + collaboratorUid);
    };

    // Export to Excel ----------------------------------------------------------------------------
    var exportToExcel = function () {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.searchKeywords = $('#Search').val();
        jsonParameters.showAllEditions = $('#ShowAllEditions').prop('checked');
        jsonParameters.showAllParticipants = $('#ShowAllParticipants').prop('checked');
        jsonParameters.showHighlights = $('#ShowHighlights').prop('checked');
        jsonParameters.showNotPublishableToApi = $('#ShowNotPublishableToApi').prop('checked');
        jsonParameters.roomsUids = $('#RoomsUids').val().join(',');

        //TODO: Needs to upgrade DataTables from current v1.10.19 to v2 to get columns names and send 'jsonParameters.sortColumns'
        //var table = $(tableElementId).DataTable();
        //var order = table.order();
        //var column = table.column(order[0][0]).header();
        //var columnName = $(column).html();
        //jsonParameters.sortColumns = {
        //    columnName: columnName,
        //    order: order[0][1]
        //};

        location.href = '/Speakers/ExportToExcel?' + jQuery.param(jsonParameters);

        MyRio2cCommon.unblock();
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            initiListTable();
            MyRio2cCommon.enableSelect2({ inputIdOrClass: widgetElementId + ' .enable-select2', allowClear: true });
        },
        refreshData: function () {
            refreshData();
        },
        exportEventbriteCsv: function () {
            exportEventbriteCsv();
        },
        exportToExcel: function () {
            exportToExcel();
        },
        showDetails: function (collaboratorUid) {
            showDetails(collaboratorUid);
        }
    };
}();