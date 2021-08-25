// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Renan Valentim
// Created          : 07-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-24-2021
// ***********************************************************************
// <copyright file="innovation.projects.datatable.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var InnovationProjectsDataTableWidget = function () {

    var widgetElementId = '#InnovationProjectDataTableWidget';
    var tableElementId = '#innovationprojects-list-table';
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
            order: [[5, "asc"]],
            sDom: '<"row"<"col-sm-6"l><"col-sm-6 text-right"B>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            oSearch: {
                sSearch: $('#Search').val()
            },
            ajax: {
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/Projects/ShowListWidget'),
                data: function (d) {
                    d.innovationOrganizationTrackUid = $('#InnovationOrganizationTrackUid').val();
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

                            if (jsonReturned.dataTable.TotalRecords == 0 && !MyRio2cCommon.isNullOrEmpty(jsonReturned.dataTable.AdditionalParameters.noRecordsFoundMessage)) {
                                table.context[0].oLanguage.sEmptyTable = jsonReturned.dataTable.AdditionalParameters.noRecordsFoundMessage;
                            }
                            else {
                                table.context[0].oLanguage.sEmptyTable = null;
                            }

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
                    data: 'InnovationOrganizationName',
                    render: function (data, type, row, meta) {
                        var html = '\
                                <table class="image-side-text text-left">\
                                    <tr>\
                                        <td>';
                        
                        if (!MyRio2cCommon.isNullOrEmpty(row.ImageUploadDate)) {
                            html += '<img src="' + imageDirectory + row.InnovationOrganizationUid + '_thumbnail.png?v=' + moment(row.ImageUploadDate).locale(globalVariables.userInterfaceLanguage).format('YYYYMMDDHHmmss') + '" /> ';
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
                { data: 'InnovationOrganizationServiceName' },
                {
                    data: 'InnovationOrganizationTracksNames',
                    render: function (data, type, row, meta) {
                        var html = '<ul class="m-0 pl-4">';

                        //loop through all the row details to build output string
                        for (var item in row.InnovationOrganizationTracksNames) {
                            if (row.InnovationOrganizationTracksNames.hasOwnProperty(item)) {
                                html += '<li>' + row.InnovationOrganizationTracksNames[item] + '</li>';
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
	                render: function (data, type, row, meta) {
                        return row.MenuActionsHtmlString;
	                }
                }
            ],
            columnDefs: [
                {
                    targets: [0],
                    //width: "20%",
                    className: "dt-left",
                    orderable: false
                },
                {
                    targets: [1, 2],
                    className: "dt-left",
                    orderable: false
                },
                {
                    targets: [3, 6],
                    //width: "15%",
                    className: "dt-center",
                    orderable: false
                },
                {
                    targets: [4, 5],
                    //width: "8%",
                    className: "dt-center",
                    orderable: true,
                },
                {
	                targets: -1,
	                //width: "6%",
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

        $('#InnovationOrganizationTrackUid').not('.change-event-enabled').on('change', function (e) {
            table.ajax.reload();
        });
        $('#InnovationOrganizationTrackUid').addClass('change-event-enabled');

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
    var showDetails = function (attendeeInnovationOrganizationId, searchKeywords, innovationOrganizationTrackUid, evaluationStatusUid, page, pageSize) {
        if (MyRio2cCommon.isNullOrEmpty(attendeeInnovationOrganizationId)) {
		    return;
	    }

        window.location.href = MyRio2cCommon.getUrlWithCultureAndEdition('/Innovation/Projects/EvaluationDetails/' + attendeeInnovationOrganizationId
            + '?searchKeywords=' + searchKeywords
            + '&innovationOrganizationTrackUid=' + innovationOrganizationTrackUid
            + '&evaluationStatusUid=' + evaluationStatusUid
            + '&page=' + page
            + '&pageSize=' + pageSize
        );
    };

    // Export -------------------------------------------------------------------------------------
    var exportExcel = function (url) {
        var jsonParameters = new Object();


        $.get(MyRio2cCommon.getUrlWithCultureAndEdition(url), jsonParameters, function (resp) {

            var hiddenElement = document.createElement('a');
            hiddenElement.href = 'data:text/csv;charset=utf-8,%EF%BB%BF' + encodeURI(resp.fileContent);
            hiddenElement.download = resp.fileName;
            hiddenElement.click();
        })
        .fail(function () {
        })
        .always(function () {
            MyRio2cCommon.unblock();
        });
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            initiListTable();
        },
        refreshData: function () {
            refreshData();
        },
        showDetails: function (attendeeInnovationOrganizationId, searchKeywords, innovationOrganizationTrackUid, evaluationStatusUid, page, pageSize) {
            showDetails(attendeeInnovationOrganizationId, searchKeywords, innovationOrganizationTrackUid, evaluationStatusUid, page, pageSize);
        },
        exportExcelReportByProject: function () {
            exportExcel('/Innovation/Projects/ExportEvaluationListWidget');
        },
        exportExcelReportByEvaluators: function () {
            exportExcel('/Innovation/Projects/ExportEvaluatorsListWidget');
        }
    };
}();