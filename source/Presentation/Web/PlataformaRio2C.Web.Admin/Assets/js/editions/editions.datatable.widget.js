﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 04-04-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-04-2021
// ***********************************************************************
// <copyright file="editions.datatable.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var EditionsDataTableWidget = function () {

    var widgetElementId = '#EditionsDataTableWidget';
    var tableElementId = '#editions-list-table';
    var table;

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
            buttons: [],
            order: [[3, "desc"]],
            sDom: '<"row"<"col-sm-6"l><"col-sm-6 text-right"B>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            oSearch: {
                sSearch: $('#Search').val()
            },
            ajax: {
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Editions/Search'),
                data: function (d) {
                    //d.showAllEditions = $('#ShowAllEditions').prop('checked');
                    //d.showAllParticipants = $('#ShowAllParticipants').prop('checked');
                    //d.showHighlights = $('#ShowHighlights').prop('checked');
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
                    data: 'Name'
                },
                {
                    data: 'IsCurrent',
                    render: function (data) {
                        return (data === true) ? translate.yes : translate.no;
                    }
                },
                {
                    data: 'IsActive',
                    render: function (data) {
                        return (data === true) ? translate.yes : translate.no;
                    }
                },
                {
                    data: 'StartDate',
                    render: function (data) {
                        return moment(data).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('L');
                    }
                },
                {
                    data: 'EndDate',
                    render: function (data) {
                        return moment(data).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('L');
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
                        var html = '\
                                        <span class="dropdown">\
                                            <a href="#" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown" aria-expanded="true">\
                                              <i class="la la-ellipsis-h"></i>\
                                            </a>\
                                            <div class="dropdown-menu dropdown-menu-right">';

                        html += '               <button class="dropdown-item" onclick="EditionsDataTableWidget.showDetails(\'' + full.Uid + '\', false);"><i class="la la-eye"></i> ' + labels.view + '</button>';
                        html += '               <button class="dropdown-item" onclick="EditionsDelete.showModal(\'' + full.Uid + '\', false);"><i class="la la-remove"></i> ' + labels.remove + '</button>';

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
                    width: "35%"
                },
                {
                    targets: [1, 2, 3, 4],
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

    var showDetails = function (editionUid) {
        if (MyRio2cCommon.isNullOrEmpty(editionUid)) {
            return;
        }

        window.location.href = MyRio2cCommon.getUrlWithCultureAndEdition('/Editions/Details/' + editionUid);
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            initiListTable();
        },
        refreshData: function () {
            refreshData();
        },
        showDetails: function (editionUid) {
            showDetails(editionUid);
        }
    };
}();