// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : William Sergio Almado Junior
// Created          : 12-13-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-17-2023
// ***********************************************************************
// <copyright file="audiovisual.projects.datatable.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var AudiovisualProjectsDataTableWidget = function () {

    var widgetElementId = '#AudiovisualProjectDataTableWidget';
    var tableElementId = '#audiovisualprojects-list-table';
    var projectData;
    var table;

    // Download PDFs ------------------------------------------------------------------------------
    var downloadProjects = function () {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.selectedProjectsUids = $('#audiovisualprojects-list-table_wrapper tr.selected').map(function () { return $(this).data('id'); }).get().join(',');
        jsonParameters.keyword = $('#Search').val();
        jsonParameters.showPitchings = $('#ShowPitchings').prop('checked');
        jsonParameters.interestUid = $('#InterestUid').val();

        window.open(MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Projects/DownloadPdfs') + '?keyword=' + jsonParameters.keyword + '&showPitchings=' + jsonParameters.showPitchings + '&interestUid=' + jsonParameters.interestUid + '&selectedProjectsUids=' + jsonParameters.selectedProjectsUids);

        MyRio2cCommon.unblock();
    };

    var showDownloadModal = function () {
        var selectedProjectsUids = $('#audiovisualprojects-list-table_wrapper tr.selected').map(function () { return $(this).data('id'); }).get().join(',');
        var message = selectedProjectsUids === '' ? translations.confirmDownloadAll :
                                                    translations.confirmDownloadSelected;

        bootbox.dialog({
            message: message,
            buttons: {
                cancel: {
                    label: labels.cancel,
                    className: "btn btn-secondary btn-elevate mr-auto",
                    callback: function () {
                    }
                },
                confirm: {

                    label: labels.confirm,
                    className: "btn btn-brand btn-elevate",
                    callback: function () {
                        downloadProjects();
                    }
                }
            }
        });
    };

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
            buttons: [
                {
                    extend: 'collection',
                    text: labels.actions,
                    buttons: [
                        {
                            text: translations.downloadPdfs,
                            action: function (e, dt, node, config) {
                                $('.dt-button-background').remove();
                                showDownloadModal();
                            }
                        },
                        {
                            text: labels.selectAll,
                            action: function (e, dt, node, config) {
                                $('.dt-button-background').remove();
                                table.rows().select();
                            }
                        },
                        {
                            text: labels.unselectAll,
                            action: function (e, dt, node, config) {
                                $('.dt-button-background').remove();
                                table.rows().deselect();
                            }
                        }
                    ]
                }],
            order: [[5, "asc"]],
            sDom: '<"row"<"col-sm-6"l><"col-sm-6 text-right"B>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            oSearch: {
                sSearch: $('#Search').val()
            },
            ajax: {
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Projects/Search'),
                data: function (d) {
                    d.showPitchings = $('#ShowPitchings').prop('checked');
                    d.interestUid = $('#InterestUid').val();
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

                            searchKeywords = jsonReturned.searchKeywords;
                            interestUid = jsonReturned.interestUid;
                            evaluationStatusUid = jsonReturned.evaluationStatusUid;
                            showPitchings = jsonReturned.showPitchings;
                            initialPage = jsonReturned.page;
                            initialPageSize = jsonReturned.pageSize;

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
                    data: 'ProjectName',
                    render: function (data, type, full, meta) {
                        var html = '\
                                <table class="image-side-text text-left">\
                                    <tr>\
                                        <td> ' + full.ProjectName + '</td>\
                                    </tr>\
                                </table>';

                        if (full.IsPitching) {
                            html += '<span class="kt-badge kt-badge--inline kt-badge--info mt-2">' + translations.pitching + '</span>';
                        }

                        return html;
                    }
                },
                {
                    data: 'ProducerName',
                    render: function (data, type, full, meta) {
                        var html = '\
                                <table class="image-side-text text-left">\
                                    <tr>\
                                        <td>';

                        if (!MyRio2cCommon.isNullOrEmpty(full.ProducerImageUploadDate)) {
                            html += '<img src="' + imageDirectory + full.ProducerUid + '_thumbnail.png?v=' + moment(full.ProducerImageUploadDate).locale(globalVariables.userInterfaceLanguage).format('YYYYMMDDHHmmss') + '" /> ';
                        }
                        else {
                            html += '<img src="' + imageDirectory + 'no-image.png?v=20190818200849" /> ';
                        }

                        html += '       </td>\
                                        <td> ' + full.ProducerName + '</td>\
                                    </tr>\
                                </table>';

                        return html;
                    }
                },
                {
                    data: 'Genre',
                    render: function (data, type, row, meta) {
                        var html = '<ul class="m-0 pl-4">';

                        //loop through all the row details to build output string
                        for (var item in row.Genre) {
                            if (row.Genre.hasOwnProperty(item)) {
                                html += '<li>' + row.Genre[item] + '</li>';
                            }
                        }

                        html += '</ul>';

                        return html;
                    }
                },
                {
                    data: 'Evaluation',
                    render: function (data, type, row, meta) {
                        var icon = "fa fa-diagnoses";
                        var color = "warning";
                        var text = translations.underEvaluation;

                        if (row.IsPitching == false) {
                            icon = "la la-remove";
                            color = "dark";
                            text = translations.notCheckedForPitching;
                        } else if (isProjectEvaluationClosed) {
                            if (row.IsApproved) {
                                icon = "fa fa-thumbs-up";
                                color = "success";
                                text = translations.projectAccepted;
                            }
                            else {
                                icon = "fa fa-thumbs-down";
                                color = "danger";
                                text = translations.projectRefused;
                            }
                        }

                        var html = '<table>';
                        html += '       <tr>';
                        html += '           <td>';
                        html += '               <div class="col-md-12 justify-content-center">';
                        html += '                   <span class="kt-widget__button" data-toggle="tooltip" title="' + text + '">';
                        html += '                       <label class="btn btn-label-' + color + ' btn-sm m-1">';
                        html += '                           <i class="' + icon + ' p-0"></i>';
                        html += '                       </label>';
                        html += '                   </span>';

                        if (row.IsPitching == true) {
                            if (isProjectEvaluationClosed) {
                                html += '           <div class="row justify-content-center">';
                                html += '               <span>';
                                html += '                   <b>' + (!MyRio2cCommon.isNullOrEmpty(row.CommissionGrade) ? parseFloat(row.CommissionGrade).toFixed(2).replace('.', ',') : '-') + '</b>';
                                html += '               </span>';
                                html += '           </div>';
                            }

                            html += '               <div class="row justify-content-center">';
                            html += '                   <span>';
                            html += '                       (' + row.CommissionEvaluationsCount + ' ' + (row.CommissionEvaluationsCount == 1 ? translations.vote : translations.votes) + ')';
                            html += '                   </span>';
                            html += '               </div>';
                        }
                       
                        html += '           </div>';
                        html += '       </td>';
                        html += '   </tr>';
                        html += '</table>';

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
                    data: 'FinishDate',
                    render: function (data) {
                        return moment(data).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('L LTS');
                    }
                },
                {
                    data: 'Actions',
                    responsivePriority: -1,
                    render: function (data, type, row, meta) {
                        var html = '\<span class="dropdown">';
                        html += '\      <a href="#" class="btn btn-sm btn-clean btn-icon btn-icon-md" data-toggle="dropdown" aria-expanded="true">';
                        html += '\          <i class="la la-ellipsis-h"></i>';
                        html += '\      </a>';
                        html += '\      <div class="dropdown-menu dropdown-menu-right">';
                        html += '\          <button class="dropdown-item" onclick="AudiovisualProjectsDataTableWidget.showDetails(\'' + row.Id + '\', \'' + searchKeywords + '\', \'' + interestUid + '\', \'' + evaluationStatusUid + '\', \'' + showPitchings + '\', \'' + initialPage + '\', \'' + initialPageSize + '\');">';
                        html += '\              <i class="la la-eye"></i>' + labels.view + '';
                        html += '\          </button>';
                        html += '\          <button class="dropdown-item" onclick="AudiovisualProjectsDelete.showModal(\'' + row.Uid + '\');">';
                        html += '\              <i class="la la-remove"></i>' + labels.remove + '';
                        html += '\          </button>';
                        html += '\      </div>';
                        html += '\  </span>';

                        return html;
                    }
                }
            ],
            columnDefs: [
                {
                    targets: [0],
                    width: "25%",
                    className: "dt-center",
                    orderable: false
                },
                {
                    targets: [1],
                    orderable: false
                },
                {
                    targets: [2],
                    className: "dt-left",
                    orderable: false
                },
                {
                    targets: [4, 5],
                    className: "dt-center"
                },
                {
                    targets: [3],
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

        $('#InterestUid').on('change', function (e) {
            table.ajax.reload();
        });

        $('#EvaluationStatusUid').on('change', function (e) {
            table.ajax.reload();
        });

        $('.enable-datatable-reload').click(function (e) {
            table.ajax.reload();
        });

        MyRio2cCommon.unblock({ idOrClass: widgetElementId });
    };

    var refreshData = function () {
        table.ajax.reload();
    };

    var showDetails = function (projectId, searchKeywords, interestUid, evaluationStatusUid, showPitchings, page, pageSize) {
        if (MyRio2cCommon.isNullOrEmpty(projectId)) {
            return;
        }

        window.location.href = MyRio2cCommon.getUrlWithCultureAndEdition('/Audiovisual/Projects/Details/' + projectId
            + '?searchKeywords=' + searchKeywords
            + '&interestUid=' + interestUid
            + '&evaluationStatusUid=' + evaluationStatusUid
            + '&showPitchings=' + showPitchings
            + '&page=' + page
            + '&pageSize=' + pageSize
        );
    };

    // Export -------------------------------------------------------------------------------------
    var exportEvaluatorsReportToExcel = function () {
        var jsonParameters = new Object();
        jsonParameters.searchKeywords = $('#Search').val();
        jsonParameters.showPitchings = $('#ShowPitchings').prop('checked');
        jsonParameters.interestUid = $('#InterestUid').val();
        jsonParameters.evaluationStatusUid = $('#EvaluationStatusUid').val();

        location.href = '/Audiovisual/Projects/ExportEvaluatorsReportToExcel?' + jQuery.param(jsonParameters);
    };

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            initiListTable();
        },
        refreshData: function () {
            refreshData();
        },
        showDetails: function (projectId, searchKeywords, interestUid, evaluationStatusUid, showPitchings, page, pageSize) {
            showDetails(projectId, searchKeywords, interestUid, evaluationStatusUid, showPitchings, page, pageSize);
        },
        exportEvaluatorsReportToExcel: function () {
            exportEvaluatorsReportToExcel();
        }
    };
}();