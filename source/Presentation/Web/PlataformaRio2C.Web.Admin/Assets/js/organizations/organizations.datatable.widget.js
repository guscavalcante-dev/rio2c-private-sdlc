// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-21-2019
// ***********************************************************************
// <copyright file="organizations.datatable.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var OrganizationsDataTableWidget = function () {

    var widgetElementId = '#PlayerCompanyDataTableWidget';
    var widgetElement = $(widgetElementId);
    var table;

    var initiListTable = function () {

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

        var globalVariables = MyRio2cCommon.getGlobalVariables();

        table = tableElementId.DataTable({
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
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Players/Search'),
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
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('#holdings-list-table_processing').hide();
            },
            createdRow: function (row, data, dataIndex) {
                $(row).attr('data-id', data.Uid);
            },
            columns: [
                { data: 'Name' },
                { data: 'HoldingBaseDto.Name' },
                { data: 'Document' },
                { data: 'Website' },
                { data: 'PhoneNumber' },
                { data: 'CreateDate' },
                { data: 'UpdateDate' },
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
                    targets: [0],
                    width: "25%",
                    className: "dt-center",
                    render: function (data, type, full, meta) {
                        var html = '\
                                <table class="image-side-text text-left">\
                                    <tr>\
                                        <td>';

                        if (!MyRio2cCommon.isNullOrEmpty(full.ImageUploadDate)) {
                            html += '<img src="https://dev.assets.my.rio2c.com/img/organizations/' + full.Uid + '_thumbnail.png?v=' + moment(full.ImageUploadDate).locale(globalVariables.userInterfaceLanguage).format('YYYYMMDDHHmmss') + '" /> ';
                        }
                        else {
                            html += '<img src="https://dev.assets.my.rio2c.com/img/organizations/no-image.png?v=20190818200849" /> ';
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
                    targets: [1],
                    width: "25%"
                },
                {
                    targets: [4],
                    className: "dt-center"
                },
                {
                    targets: [5,6],
                    className: "dt-center",
                    render: function (data) {
                        return moment(data).locale(globalVariables.userInterfaceLanguage).format('L LTS');
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
                        var html = '\
                                        <span class="dropdown">\
                                            <a href="#" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown" aria-expanded="true">\
                                              <i class="la la-ellipsis-h"></i>\
                                            </a>\
                                            <div class="dropdown-menu dropdown-menu-right">';

                        if (!full.IsInCurrentEdition) {
                            html += '<button class="dropdown-item" onclick="OrganizationsUpdate.showModal(\'' + full.Uid + '\', true);"><i class="la la-plus"></i> ' + addToEdition +'</button>';
                        }

                        html += '<button class="dropdown-item" onclick="OrganizationsUpdate.showModal(\'' + full.Uid + '\', false);"><i class="la la-edit"></i> ' + labels.edit + '</button>';

                        if (full.IsInCurrentEdition && full.IsInOtherEdition) {
                            html += '<button class="dropdown-item" onclick="OrganizationsDelete.showModal(\'' + full.Uid + '\', true);"><i class="la la-plus"></i> ' + removeFromEdition + '</button>';
                        }
                        else {
                            html += '<button class="dropdown-item" onclick="OrganizationsDelete.showModal(\'' + full.Uid + '\', false);"><i class="la la-remove"></i> ' + labels.remove + '</button>';
                        }

                        html += '\
                                            </div>\
                                        </span>';

                        return html;
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