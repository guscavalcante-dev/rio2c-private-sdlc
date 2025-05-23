﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-23-2021
// ***********************************************************************
// <copyright file="administrators.datatable.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AdministratorsDataTableWidget = function () {

    var widgetElementId = '#AdministratorsDataTableWidget';
    var tableElementId = '#administrators-list-table';
    var table;

    // Enable form plugins ------------------------------------------------------------------------
    var enableFormPlugins = function () {
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '.enable-select2' });
    };

    // Init datatable -----------------------------------------------------------------------------
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
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Administrators/Search'),
                data: function (d) {
                    d.showAllEditions = $('#ShowAllEditions').prop('checked');
                    d.showAllParticipants = $('#ShowAllParticipants').prop('checked');
                    d.collaboratorType = $('#CollaboratorType').val();
                    d.roleName = $('#Role').val();
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

                        html += '       <td> ' + full.FullName + '</td>';
                        html += '    </tr>';
                        html += ' </table>';

                        if (!full.Active) {
                            html += '<span class="kt-badge kt-badge--inline kt-badge--info mt-2" style="color: #ffffff; background: #E66E72">' + labels.blocked + '</span><br>';
                        }

                        if (!full.IsInCurrentEdition && !full.IsAdminFull) {
                            html += '<span class="kt-badge kt-badge--inline kt-badge--info mt-2">' + labels.notInEdition + '</span>';
                        }

                        return html;
                    }
                },
                {
                    data: 'Email'
                },
                {
                    data: 'TranslatedCollaboratorTypes',
                    render: function (data, type, row, meta) {
                        var html = '<ul class="m-0 pl-4">';

                        //loop through all the row details to build output string
                        for (var item in row.TranslatedCollaboratorTypes) {
                            if (row.TranslatedCollaboratorTypes.hasOwnProperty(item)) {
                                var r = row.TranslatedCollaboratorTypes[item];
                                html += '<li>' + r + '</li>';
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
                    data: 'UpdateDate',
                    render: function (data) {
                        return moment(data).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('L LTS');
                    }
                },
                {
                    data: 'Actions',
                    responsivePriority: -1,
                    render: function (data, type, full, meta) {
                        var unblock = 'Desbloquear'
                        var html = '\<span class="dropdown">\
                                            <a href="#" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown" aria-expanded="true">\
                                              <i class="la la-ellipsis-h"></i>\
                                            </a>\
                                            <div class="dropdown-menu dropdown-menu-right">';

                        if (!full.IsInCurrentEdition && !full.IsAdminFull) {
                            html += '<button class="dropdown-item" onclick="AdministratorsUpdate.showModal(\'' + full.Uid + '\', true);"><i class="la la-plus"></i> ' + addToEdition + '</button>';
                        }

                        html += '<button class="dropdown-item" onclick="AdministratorsDataTableWidget.showDetails(\'' + full.Uid + '\');"><i class="la la-eye"></i> ' + labels.view + '</button>';
                        html += '<button class="dropdown-item" onclick="AccountsPassword.showResetModal(\'' + full.Id + '\');"><i class="la la-key"></i> ' + changePassword + '</button>';
                        html += '<button class="dropdown-item" onclick="AccountsUpdateUserStatus.showModal(\'' + full.UserBaseDto.Uid + '\',\'' + !full.Active + '\');"><i class="la la-lock"></i> ' + ((full.Active) ? block : unblock) + '</button>';

                        if (full.IsInCurrentEdition && full.IsInOtherEdition) {
                            html += '<button class="dropdown-item" onclick="AdministratorsDelete.showModal(\'' + full.Uid + '\', true);"><i class="la la-minus"></i> ' + removeFromEdition + '</button>';
                        }
                        else {
                            html += '<button class="dropdown-item" onclick="AdministratorsDelete.showModal(\'' + full.Uid + '\', false);"><i class="la la-remove"></i> ' + labels.remove + '</button>';
                        }

                        html += '\</div>\</span>';

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
                    orderable: false
                },
                {
                    targets: [3, 4],
                    className: "dt-center",
                    width: "10%",
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
            }
        });

        $('#Search').keyup(function (e) {
            if (e.keyCode === 13) {
                table.search($(this).val()).draw();
            }
        });

        $('#Role').not('.change-event-enabled').on('change', function (e) {
            table.ajax.reload();
        });
        $('#Role').addClass('change-event-enabled');

        $('#CollaboratorType').not('.change-event-enabled').on('change', function (e) {
            table.ajax.reload();
        });
        $('#CollaboratorType').addClass('change-event-enabled');

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

        window.location.href = MyRio2cCommon.getUrlWithCultureAndEdition('/Administrators/Details/' + collaboratorUid);
    };

    var updateUserStatus = function (userUid, active) {
        var jsonParameters = new Object();
        jsonParameters.userUid = userUid;
        jsonParameters.active = active;

        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Account/UpdateUserStatus'), jsonParameters, function (data) {
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
                if (typeof (AdministratorsDataTableWidget) !== 'undefined') {
                    AdministratorsDataTableWidget.refreshData();
                }
                MyRio2cCommon.unblock();
            });
    }

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            initiListTable();
        },
        refreshData: function () {
            refreshData();
        },
        exportEventbriteCsv: function () {
            exportEventbriteCsv();
        },
        showDetails: function (collaboratorUid) {
            showDetails(collaboratorUid);
        },
        updateUserStatus: function (userUid, active) {
            updateUserStatus(userUid, active);
        }
    };
}();