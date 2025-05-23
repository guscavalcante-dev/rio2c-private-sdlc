﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 05/05/2025
// ***********************************************************************
// <copyright file="audiovisual.players.datatable.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualPlayersDataTableWidget = function () {

    var widgetElementId = '#AudiovisualPlayersDataTableWidget';
    var tableElementId = '#audiovisualplayers-list-table';
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
        var imageDirectory = 'https://' + globalVariables.bucket + '/img/organizations/';

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
                        }]
                }],
            order: [[0, "asc"]],
            sDom: '<"row"<"col-sm-6"l><"col-sm-6 text-right"B>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            oSearch: {
                sSearch: $('#Search').val()
            },
            ajax: {
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Players/Search'),
                data: function (d) {
                    d.showAllEditions = $('#ShowAllEditions').prop('checked');
                    d.showAllOrganizations = $('#ShowAllOrganizations').prop('checked');
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

                        html += '       </td>\
                                        <td> ' + full.Name + '</td>\
                                    </tr>\
                                </table>';

                        if (!MyRio2cCommon.isNullOrEmpty(full.IsVirtualMeeting)) {
                            var virtualOrPresentialText = (full.IsVirtualMeeting === true) ? virtual : presential;
                            html += '<span class="kt-badge kt-badge--inline kt-badge--warning kt-font-boldest mt-2 w-50">' + virtualOrPresentialText + '</span>';
                        }

                        if (!full.IsInCurrentEdition) {
                            html += '<span class="kt-badge kt-badge--inline kt-badge--info mt-2">' + labels.notInEdition + '</span>';
                        }

                        return html;
                    }
                },
                { data: 'HoldingBaseDto.Name' },
                { data: 'Document' },
                { data: 'Website' },
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
                    data: 'OnboardingFinishDate',
                    render: function (data) {
                        return data
                            ? moment(data).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('L')
                            : '';
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
                            html += '<button class="dropdown-item" onclick="AudiovisualPlayersUpdate.showModal(\'' + full.Uid + '\', true);"><i class="la la-plus"></i> ' + addToEdition + '</button>';
                        }
                        else {
                            html += '<button class="dropdown-item" onclick="AudiovisualPlayersDataTableWidget.showDetails(\'' + full.Uid + '\', false);"><i class="la la-eye"></i> ' + labels.view + '</button>';
                        }

                        if (full.IsInCurrentEdition && full.IsInOtherEdition) {
                            html += '<button class="dropdown-item" onclick="AudiovisualPlayersDelete.showModal(\'' + full.Uid + '\', true);"><i class="la la-minus"></i> ' + removeFromEdition + '</button>';
                        }
                        else {
                            html += '<button class="dropdown-item" onclick="AudiovisualPlayersDelete.showModal(\'' + full.Uid + '\', false);"><i class="la la-remove"></i> ' + labels.remove + '</button>';
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
                    targets: [1],
                    width: "25%"
                },
                {
                    targets: [4, 5, 6],
                    className: "dt-center"
                },
                {
                    targets: [7],
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

    var showDetails = function (playerUid) {
        if (MyRio2cCommon.isNullOrEmpty(playerUid)) {
            return;
        }

        window.location.href = MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Players/Details/' + playerUid);
    };

    // Export to Excel ----------------------------------------------------------------------------
    var exportToExcel = function () {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.searchKeywords = $('#Search').val();
        jsonParameters.showAllEditions = $('#ShowAllEditions').prop('checked');
        jsonParameters.showAllOrganizations = $('#ShowAllOrganizations').prop('checked');

        //TODO: Needs to upgrade DataTables from current v1.10.19 to v2 to get columns names and send 'jsonParameters.sortColumns'
        //var table = $(tableElementId).DataTable();
        //var order = table.order();
        //var column = table.column(order[0][0]).header();
        //var columnName = $(column).html();
        //jsonParameters.sortColumns = {
        //    columnName: columnName,
        //    order: order[0][1]
        //};

        location.href = '/Audiovisual/Players/ExportToExcel?' + jQuery.param(jsonParameters);

        MyRio2cCommon.unblock();
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            initiListTable();
        },
        refreshData: function() {
            refreshData();
        },
        showDetails: function (playerUid) {
            showDetails(playerUid);
        },
        exportToExcel: function () {
            exportToExcel();
        }
    };
}();