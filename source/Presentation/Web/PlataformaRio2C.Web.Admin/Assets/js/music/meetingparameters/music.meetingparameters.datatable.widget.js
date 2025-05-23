﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-05-2020
// ***********************************************************************
// <copyright file="Music.meetingparameters.datatable.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicMeetingParametersDataTableWidget = function () {

    var widgetElementId = '#MusicMeetingParametersDataTableWidget';
    var tableElementId = '#Music-meetingparameters-list-table';

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
            order: [[0, "asc"]],
            sDom: '<"row"<"col-sm-6"l><"col-sm-6 text-right"B>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            oSearch: {
                sSearch: $('#Search').val()
            },
            ajax: {
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Music/MeetingParameters/Search'),
                data: function (d) {
                    //d.showAllEditions = $('#ShowAllEditions').prop('checked');
                    //d.showAllParticipants = $('#ShowAllParticipants').prop('checked');
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
                    data: 'StartDate',
                    render: function (data) {
	                    return moment(data).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('L');
                    }
	            },
                {
                    data: 'StartDate',
                    render: function (data) {
	                    return moment(data).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('LT');
                    }
                },
	            {
                    data: 'EndDate',
                    render: function (data) {
	                    return moment(data).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('LT');
                    }
                },
                {
                    data: 'RoundFirstTurn'
                },
                {
                    data: 'RoundSecondTurn'
                },
                {
                    data: 'TimeIntervalBetweenTurn',
                    render: function (data) {
	                    return moment.utc(moment.duration(data).as('milliseconds')).format('HH:mm');
	                }
                },
                {
                    data: 'TimeOfEachRound',
                    render: function (data) {
	                    return moment.utc(moment.duration(data).as('milliseconds')).format('HH:mm');
                    }
                },
                {
                    data: 'TimeIntervalBetweenRound',
                    render: function (data) {
	                    return moment.utc(moment.duration(data).as('milliseconds')).format('HH:mm');
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

                        html += '               <button class="dropdown-item" onclick="MusicMeetingParametersDataTableWidget.showDetails(\'' + full.NegotiationConfigUid + '\');"><i class="la la-edit"></i> ' + labels.edit + '</button>';
                        html += '               <button class="dropdown-item" onclick="MusicMeetingParametersDelete.showModal(\'' + full.NegotiationConfigUid + '\');"><i class="la la-remove"></i> ' + labels.remove + '</button>';

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
                    className: "dt-center"
                },
	            {
		            targets: [1, 2, 3, 4, 5, 6, 7],
                    className: "dt-center",
		            orderable: false
	            },
                {
                    targets: [8, 9],
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

        //$('#Search').keyup(function (e) {
        //    if (e.keyCode === 13) {
        //        table.search($(this).val()).draw();
        //    }
        //});

        $('.enable-datatable-reload').click(function (e) {
            table.ajax.reload();
        });

        MyRio2cCommon.unblock({ idOrClass: widgetElementId });
    };

    var refreshData = function () {
        table.ajax.reload();
    };

    var showDetails = function (negotiationConfigUid) {
        if (MyRio2cCommon.isNullOrEmpty(negotiationConfigUid)) {
            return;
        }

        window.location.href = MyRio2cCommon.getUrlWithCultureAndEdition('/Music/MeetingParameters/Details/' + negotiationConfigUid);
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            initiListTable();
        },
        refreshData: function () {
            refreshData();
        },
        exportEventbriteCsv: function() {
            exportEventbriteCsv();
        },
        showDetails: function (negotiationConfigUid) {
            showDetails(negotiationConfigUid);
        }
    };
}();