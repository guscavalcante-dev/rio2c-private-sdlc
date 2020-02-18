// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-07-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-16-2020
// ***********************************************************************
// <copyright file="holdings.datatable.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var HoldingsDataTableWidget = function () {

    var widgetElementId = '#HoldingDataTableWidget';
    var tableElementId = '#holdings-list-table';
    var table;

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
        var imageDirectory = 'https://' + globalVariables.bucket + '/img/holdings/';

        table = tableElement.DataTable({
            "language": {
                "url": "/Assets/components/datatables/datatables." + globalVariables.userInterfaceLanguage + ".js"
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
            order: [[0, "asc"]],
            sDom: '<"row"<"col-sm-6"l>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            oSearch: {
                sSearch: $('#Search').val()
            },
            ajax: {
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Holdings/Search'),
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

                        html += '       <td> ' + full.Name + '</td>\
                                    </tr>\
                                </table>';

                        if (!full.IsInCurrentEdition) {
                            html += '<span class="kt-badge kt-badge--inline kt-badge--info mt-2">' + labels.notInEdition + '</span>';
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
                        return '\
                            <span class="dropdown">\
                                <a href="#" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown" aria-expanded="true">\
                                  <i class="la la-ellipsis-h"></i>\
                                </a>\
                                <div class="dropdown-menu dropdown-menu-right">\
                                    <button class="dropdown-item" onclick="HoldingsUpdate.showModal(\'' + full.Uid + '\');"><i class="la la-edit"></i> ' + labels.edit + '</button>\
                                    <button class="dropdown-item" onclick="HoldingsDelete.showModal(\'' + full.Uid + '\');"><i class="la la-remove"></i> ' + labels.remove + '</button>\
                                </div>\
                            </span>';
                    }
                }
            ],
            columnDefs: [
                {
                    targets: [1,2],
                    width: "12%",
                    className: "dt-center"
                },
                {
                    targets: -1,
                    width: "10%",
                    orderable: false,
                    searchable: false,
                    className: "dt-center"
                }
            ]
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

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            initiListTable();
        },
        refreshData: function() {
            refreshData();
        }
    };
}();