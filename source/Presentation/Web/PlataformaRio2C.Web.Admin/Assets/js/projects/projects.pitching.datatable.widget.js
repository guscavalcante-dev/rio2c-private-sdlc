// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : William Sergio Almado Junior
// Created          : 12-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-03-2019
// ***********************************************************************
// <copyright file="projects.pitching.datatable.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var ProjectsPitchingDataTableWidget = function () {

    var widgetElementId = '#ProjectPitchingDataTableWidget';
    var tableElementId = '#projectpitching-list-table';
    var projectData;
    var table;

    // Download PDFs ------------------------------------------------------------------------------
    var downloadProjects = function () {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.selectedProjectsUids = $('#projectpitching-list-table_wrapper tr.selected').map(function () { return $(this).data('id'); }).get().join(',');
        jsonParameters.keyword = $('#Search').val();
        jsonParameters.interestUid = $('#InterestUid').val();

        window.location = MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/DownloadPdfs') + '?keyword=' + jsonParameters.keyword + '&interestUid=' + jsonParameters.interestUid + '&selectedProjectsUids=' + jsonParameters.selectedProjectsUids;

        MyRio2cCommon.unblock();
    };

    var showDownloadModal = function () {
        var selectedProjectsUids = $('#projectpitching-list-table_wrapper tr.selected').map(function () { return $(this).data('id'); }).get().join(',');
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
                                showDownloadModal();
                            }
                        }]
                }],
            order: [[4, "asc"]],
            sDom: '<"row"<"col-sm-6"l><"col-sm-6 text-right"B>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            oSearch: {
                sSearch: $('#Search').val()
            },
            ajax: {
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/ShowPitchingListWidget'),
                data: function (d) {
                    d.interestUid = $('#InterestUid').val();
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
                { data: 'ProjectName' },
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

                        html += '       <td> ' + full.ProducerName + '</td>\
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
                    data: 'CreateDate',
                    render: function (data) {
                        return moment(data).locale(globalVariables.userInterfaceLanguage).format('L LTS');
                    }
                },
                {
                    data: 'FinishDate',
                    render: function (data) {
                        return moment(data).locale(globalVariables.userInterfaceLanguage).format('L LTS');
                    }
                },
            ],
            columnDefs: [
                {
                    targets: [0],
                    width: "25%",
                    className: "dt-left",
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
                    targets: [3, 4],
                    className: "dt-center"
                },
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
        refreshData: function () {
            refreshData();
        }
    };
}();