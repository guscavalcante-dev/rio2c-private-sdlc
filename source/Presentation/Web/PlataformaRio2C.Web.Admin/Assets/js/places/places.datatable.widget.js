﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-17-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="places.datatable.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var PlacesDataTableWidget = function () {

    var widgetElementId = '#PlacesDataTableWidget';
    var tableElementId = '#places-list-table';
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
            searchDelay: 2000,
            processing: true,
            serverSide: true,
            buttons: [],
            order: [[2, "asc"]],
            sDom: '<"row"<"col-sm-6"l><"col-sm-6 text-right"B>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            oSearch: {
                sSearch: $('#Search').val()
            },
            ajax: {
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Places/Search'),
                data: function (d) {
                    d.showAllEditions = $('#ShowAllEditions').prop('checked');
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
                    data: 'Name',
                    render: function (data, type, full, meta) {
	                    var html = data;

	                    if (!full.IsInCurrentEdition) {
		                    html += ' <span class="kt-badge kt-badge--inline kt-badge--info mt-2">' + labels.notInEdition + '</span>';
	                    }

	                    return html;
                    }
                },
                {
                    data: 'Type',
                    render: function (data, type, full, meta) {
	                    var html = '';
                        if (full.IsHotel) {
                            html += '<span class="kt-badge kt-badge--inline kt-badge--info">' + translate.hotel + '</span>';
                        }
                        else if (full.IsAirport) {
                            html += '<span class="kt-badge kt-badge--inline kt-badge--success">' + translate.airport + '</span>';
                        }
                        else {
                            html += '<span class="kt-badge kt-badge--inline kt-badge--dark">' + translate.others + '</span>';
                        }       
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
	                    var html = '\
                                        <span class="dropdown">\
                                            <a href="#" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown" aria-expanded="true">\
                                              <i class="la la-ellipsis-h"></i>\
                                            </a>\
                                            <div class="dropdown-menu dropdown-menu-right">';

                        if (!full.IsInCurrentEdition) {
                            html += '<button class="dropdown-item" onclick="PlacesCreate.showModal(\'' + full.Uid + '\', true);"><i class="la la-plus"></i> ' + addToEdition + '</button>';
                        }
                        else {
	                        html += '<button class="dropdown-item" onclick="PlacesDataTableWidget.showDetails(\'' + full.Uid + '\', false);"><i class="la la-edit"></i> ' + labels.edit + '</button>';

	                        if (full.IsInCurrentEdition && full.IsInOtherEdition) {
		                        html += '<button class="dropdown-item" onclick="PlacesDelete.showModal(\'' + full.Uid + '\', true);"><i class="la la-plus"></i> ' + removeFromEdition + '</button>';
	                        }
	                        else {
		                        html += '<button class="dropdown-item" onclick="PlacesDelete.showModal(\'' + full.Uid + '\', false);"><i class="la la-remove"></i> ' + labels.remove + '</button>';
	                        }
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
                    width: "50%"
                },
                {
                    targets: [1],
                    className: "dt-center",
                    orderable: false
                },
                {
                    targets: [2, 3],
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

    var showDetails = function (placeUid) {
        if (MyRio2cCommon.isNullOrEmpty(placeUid)) {
            return;
        }

        window.location.href = MyRio2cCommon.getUrlWithCultureAndEdition('/Places/Details/' + placeUid);
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            initiListTable();
        },
        refreshData: function () {
            refreshData();
        },
        showDetails: function (placeUid) {
            showDetails(placeUid);
        }
    };
}();