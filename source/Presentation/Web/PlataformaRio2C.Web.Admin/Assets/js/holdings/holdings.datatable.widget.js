// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-07-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-12-2019
// ***********************************************************************
// <copyright file="holdings.datatable.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var HoldingsDataTableWidget = function () {

    var widgetElementId = '#HoldingDataTableWidget';
    var widgetElement = $(widgetElementId);

    var initiListTable = function (searchUrl) {

        var tableElementId = $('#holdings-list-table');

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

        var table = tableElementId.DataTable({
            "language": {
                "url": "/Assets/components/datatables/datatables." + userInterfaceLanguage + ".js"
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
                url: searchUrl,
                data: function (d) {
                    d.showAllEditions = $('#ShowAllEditions').prop('checked');
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('#holdings-list-table_processing').hide();
            },
            createdRow: function (row, data, dataIndex) {
                $(row).attr('data-id', data.uid);
            },
            columns: [
                { data: 'name' },
                { data: 'createDate' },
                //{
                //    data: null,
                //    render: function (data, type, row, meta) {
                //        var descriptionsDtos = '';
                //        //loop through all the row details to build output string
                //        for (var item in row.descriptionsDtos) {
                //            if (row.descriptionsDtos.hasOwnProperty(item)) {
                //                var r = row.descriptionsDtos[item];
                //                descriptionsDtos += r.value + ' (' + r.languageDto.name + ')' + '</br>';
                //            }
                //        }
                //        return descriptionsDtos;
                //    }
                //},
                { data: 'Actions', responsivePriority: -1 }
            ],
            columnDefs: [
                {
                    targets: [1],
                    width: "20%",
                    className: "dt-center",
                    render: function (data) {
                        return moment(data).locale(userInterfaceLanguage).format('L LTS');
                    }
                },
                {
                    targets: -1,
                    title: 'Actions',
                    width: "10%",
                    orderable: false,
                    searchable: false,
                    className: "dt-center",
                    render: function (data, type, full, meta) {
                        return '\
                            <span class="dropdown">\
                                <a href="#" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown" aria-expanded="true">\
                                  <i class="la la-ellipsis-h"></i>\
                                </a>\
                                <div class="dropdown-menu dropdown-menu-right">\
                                    <a class="dropdown-item" href="#"><i class="la la-edit"></i> Edit Details</a>\
                                    <a class="dropdown-item" href="#"><i class="la la-leaf"></i> Update Status</a>\
                                    <a class="dropdown-item" href="#"><i class="la la-print"></i> Generate Report</a>\
                                </div>\
                            </span>';
                    }
                }
            ]
        });

        $('#Search').keyup(function (e) {
            if (e.keyCode === 13) {
                table.search($(this).val()).draw();
            }
        });

        $('#ShowAllEditions').click(function (e) {
            table.ajax.reload();
        });

        MyRio2cCommon.unblock({ idOrClass: widgetElementId });
    };

    return {
        init: function (searchUrl) {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            initiListTable(searchUrl);
        }
    };
}();