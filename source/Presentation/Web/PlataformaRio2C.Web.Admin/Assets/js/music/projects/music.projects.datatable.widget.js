// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 03-01-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-02-2020
// ***********************************************************************
// <copyright file="music.projects.datatable.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicProjectsDataTableWidget = function () {

    var widgetElementId = '#MusicProjectDataTableWidget';
    var tableElementId = '#musicprojects-list-table';
    var projectData;
    var table;

    // Enable form plugins ------------------------------------------------------------------------
    var enableFormPlugins = function () {
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '.enable-select2' });
    };

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
        var imageDirectory = 'https://' + globalVariables.bucket + '/img/organizations/';

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
            order: [[6, "asc"]],
            sDom: '<"row"<"col-sm-6"l><"col-sm-6 text-right"B>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            oSearch: {
                sSearch: $('#Search').val()
            },
            ajax: {
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Projects/ShowListWidget'),
                data: function (d) {
                    d.musicGenreUid = $('#MusicGenreUid').val();
                    d.evaluationStatusUid = $('#EvaluationStatusUid').val();
                },
                dataFilter: function (data) {
                    var jsonReturned = JSON.parse(data);

                    projectData = jsonReturned.dataTable.Data;

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
                    data: 'MusicBandName',
                    render: function (data, type, full, meta) {
                        var html = '\
                                <table class="image-side-text text-left">\
                                    <tr>\
                                        <td>';

                        if (!MyRio2cCommon.isNullOrEmpty(full.MusicBandImageUrl)) {
                            html += '<img src="' + full.MusicBandImageUrl + '" /> ';
                        }
                        else {
                            html += '<img src="' + imageDirectory + 'no-image.png?v=20190818200849" /> ';
                        }

                        html += '       <td> ' + data + '</td>\
                                    </tr>\
                                </table>';

                        return html;
                    }
                },
	            { data: 'MusicBandTypeName' },
                {
                    data: 'MusicGenreNames',
                    render: function (data, type, row, meta) {
                        var html = '<ul class="m-0 pl-4">';

                        //loop through all the row details to build output string
                        for (var item in row.MusicGenreNames) {
                            if (row.MusicGenreNames.hasOwnProperty(item)) {
                                html += '<li>' + row.MusicGenreNames[item] + '</li>';
                            }
                        }

                        html += '</ul>';

                        return html;
                    }
                },
                {
                    data: 'MusicTargetAudiencesNames',
	                render: function (data, type, row, meta) {
		                var html = '<ul class="m-0 pl-4">';

		                //loop through all the row details to build output string
                        for (var item in row.MusicTargetAudiencesNames) {
                            if (row.MusicTargetAudiencesNames.hasOwnProperty(item)) {
                                html += '<li>' + row.MusicTargetAudiencesNames[item] + '</li>';
			                }
		                }

		                html += '</ul>';

		                return html;
	                }
                },
                {
                    data: 'Evaluation',
                    render: function (data, type, row, meta) {
                        return row.EvaluationHtmlString;
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

                        html += '               <button class="dropdown-item" onclick="MusicProjectsDataTableWidget.showDetails(\'' + full.MusicProjectUid + '\', false);"><i class="la la-edit"></i> ' + labels.edit + '</button>';
                        html += '               <button class="dropdown-item" onclick="MusicProjectsDelete.showModal(\'' + full.MusicProjectUid + '\', false);"><i class="la la-remove"></i> ' + labels.remove + '</button>';

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
                    width: "20%",
                    className: "dt-left",
                    orderable: false
                },
                {
                    targets: [1, 2, 3, 5],
                    className: "dt-left",
                    orderable: false
                },
                {
	                targets: [4],
                    className: "dt-center",
	                orderable: false
                },
                {
                    targets: [6],
                    width: "8%",
                    className: "dt-center"
                },
                {
	                targets: -1,
	                width: "6%",
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

        $('#MusicGenreUid').not('.change-event-enabled').on('change', function (e) {
            table.ajax.reload();
        });
        $('#MusicGenreUid').addClass('change-event-enabled');

        $('#EvaluationStatusUid').not('.change-event-enabled').on('change', function (e) {
	        table.ajax.reload();
        });
        $('#EvaluationStatusUid').addClass('change-event-enabled');

        $('.enable-datatable-reload').click(function (e) {
            table.ajax.reload();
        });

        MyRio2cCommon.unblock({ idOrClass: widgetElementId });
    };

    var refreshData = function () {
        table.ajax.reload();
    };

    // Details ------------------------------------------------------------------------------------
    var showDetails = function (musicProjectUid) {
	    if (MyRio2cCommon.isNullOrEmpty(musicProjectUid)) {
		    return;
	    }

	    window.location.href = MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Projects/Details/' + musicProjectUid);
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            initiListTable();
        },
        refreshData: function () {
            refreshData();
        },
        showDetails: function (musicProjectUid) {
	        showDetails(musicProjectUid);
        }
    };
}();