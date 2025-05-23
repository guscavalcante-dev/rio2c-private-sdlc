﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 07-03-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-19-2023
// ***********************************************************************
// <copyright file="innovation.trackoptions.groups.datatable.widget" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var InnovationTrackOptionGroupsDataTableWidget = function () {

    var widgetElementId = '#InnovationTrackOptionGroupsDataTableWidget';
    var tableElementId = '#innovation-trackoption-groups-list-table';
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
            //buttons: [
            //{
            //    extend: 'collection',
            //    text: labels.actions,
            //    buttons: [
            //        {
            //            text: labels.selectAll,
            //            action: function (e, dt, node, config) {
            //                $('.dt-button-background').remove();
            //                table.rows().select();
            //            }
            //        },
            //        {
            //            text: labels.unselectAll,
            //            action: function (e, dt, node, config) {
            //                $('.dt-button-background').remove();
            //                table.rows().deselect();
            //            }
            //        }]
            //}],
            order: [[0, "asc"]],
            sDom: '<"row"<"col-sm-6"l><"col-sm-6 text-right"B>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            oSearch: {
                sSearch: $('#Search').val()
            },
            ajax: {
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/TrackOptionGroups/Search'),
                data: function (d) {
                    //d.showAllEditions = $('#ShowAllEditions').prop('checked');
                    //d.showAllParticipants = $('#ShowAllParticipants').prop('checked');
                    //d.innovationOrganizationTrackOptionGroupUid = $('#InnovationOrganizationTrackOptionGroupUid').val();
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
                    data: 'GroupName',
                },
                {
                    data: 'InnovationOrganizationTracksNames',
                    render: function (data, type, row, meta) {
                        var html = '<ul class="m-0 pl-4">';

                        //loop through all the row details to build output string
                        for (var rowIndex in row.InnovationOrganizationTrackOptionNames) {
                            if (row.InnovationOrganizationTrackOptionNames.hasOwnProperty(rowIndex)) {
                                html += '<li>' + row.InnovationOrganizationTrackOptionNames[rowIndex] + '</li>';
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
                        var html = '\
                                        <span class="dropdown">\
                                            <a href="#" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown" aria-expanded="true">\
                                              <i class="la la-ellipsis-h"></i>\
                                            </a>\
                                            <div class="dropdown-menu dropdown-menu-right">';
                        
                        html += '<button class="dropdown-item" onclick="InnovationTrackOptionGroupsDataTableWidget.showDetails(\'' + full.Uid + '\');"><i class="la la-eye"></i> ' + labels.view + '</button>';
                        
                        html += '<button class="dropdown-item" onclick="InnovationTrackOptionGroupsDelete.showModal(\'' + full.Uid + '\');"><i class="la la-remove"></i> ' + labels.remove + '</button>';

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
                    width: "50%",
                    orderable: true
                },
                {
                    targets: [1],
                    width: "25%",
                    orderable: false
                },
                {
                    targets: [2, 3],
                    className: "dt-center"
                },
                {
                    targets: [4],
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

    var showDetails = function (innovationOrganizationTrackOptionGroupUid) {
        if (MyRio2cCommon.isNullOrEmpty(innovationOrganizationTrackOptionGroupUid)) {
            return;
        }

        window.location.href = MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/TrackOptionGroups/Details/' + innovationOrganizationTrackOptionGroupUid);
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            initiListTable();
        },
        refreshData: function () {
            refreshData();
        },
        showDetails: function (innovationOrganizationTrackOptionGroupUid) {
            showDetails(innovationOrganizationTrackOptionGroupUid);
        }
    };
}();