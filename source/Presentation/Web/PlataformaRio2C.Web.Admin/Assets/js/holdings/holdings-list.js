// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-07-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-09-2019
// ***********************************************************************
// <copyright file="holdings-list.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var HoldingsList = function () {

    var initiListTable = function (searchAction) {
        var tableElementId = $('#holdings-list-table');

        var table = tableElementId.DataTable({
            "language": {
                "url": "/Assets/components/datatables/datatables." + userInterfaceLanguage + ".js"
            },
            lengthMenu: [[1, 10, 25, 50, 100], [1, 10, 25, 50, 100]],
            pageLength: 10,
            responsive: true,
            searchDelay: 2000,
            processing: true,
            serverSide: true,
            order: [[0, "asc"]],
            sDom: 'lt<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>r',
            //ajax: 'https://keenthemes.com/metronic/themes/themes/metronic/dist/preview/inc/api/datatables/demos/server.php',
            ajax: {
                url: searchAction,
                data: function (d) {
                    d.showAllEditions = $('#ShowAllEditions').prop('checked');
                }
            },
            columns: [
                { data: 'name' },
                { data: 'createDate' },
                { data: 'uid' },
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
                    targets: [2],
                    className: "dt-hide-column",
                    orderable: false,
                    searchable: false
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

        $('#Search').keyup(function() {
            table.search($(this).val()).draw();
        });

        $('#ShowAllEditions').click(function (e) {
            table.ajax.reload();
        });
    };

    return {
        init: function (searchAction) {
            initiListTable(searchAction);
        }
    };
}();