// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : William Sergio Almado Junior
// Created          : 12-13-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 12-13-2019
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

    // Invitation email ---------------------------------------------------------------------------
    var downloadProjects = function (projectData) {
        MyRio2cCommon.block();

        var jsonParameters = new Object();
        jsonParameters.selectedProjectsUids = $('#projectpitching-list-table_wrapper tr.selected').map(function () { return $(this).data('id'); }).get().join(',');
        jsonParameters.keyword = $('#Search').val();

        window.location = '/Projects/DownloadProjectDocument?' + 'keyword=' + jsonParameters.keyword + '&' + 'selectedProjectsUids=' + jsonParameters.selectedProjectsUids;

        MyRio2cCommon.unblock();
    };

    var showDownloadModal = function (projectData) {
        var selectedProjectsUids = $('#projectpitching-list-table_wrapper tr.selected').map(function () { return $(this).data('id'); }).get().join(',');
        var message = selectedProjectsUids === '' ? translations.confirmDownloadAll : translations.confirmDownloadSelected;
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

                    label: labels.download,
                    className: "btn btn-brand btn-elevate",
                    callback: function () {
                        downloadProjects(projectData);
                    }
                }
            }
        });
    };

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
        //var imageDirectory = 'https://' + globalVariables.bucket + '/img/users/';

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
                            text: translations.downloadListProjects,
                            action: function (e, dt, node, config) {
                                showDownloadModal(projectData);
                            }
                        }]
                }],
            order: [[0, "asc"]],
            sDom: '<"row"<"col-sm-6"l><"col-sm-6 text-right"B>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            oSearch: {
                sSearch: $('#Search').val()
            },
            ajax: {
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Projects/ShowPitchingListWidget'),
                data: function (d) {
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
                { data: 'ProducerName' },
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
                    className: "dt-left"
                },
                {
                    targets: [1],
                    orderable: false
                },
                {
                    targets: [2, 3],
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
        },
    };
}();