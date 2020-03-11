// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-11-2020
// ***********************************************************************
// <copyright file="logisticsponsors.datatable.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var LogisticsDataTableWidget = function () {
    var widgetElementId = '#LogisticRequestsDataTableWidget';
    var tableElementId = '#logisticsponsors-list-table';
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
        var imageDirectory = 'https://' + globalVariables.bucket + '/img/users/';

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
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Logistics/Search'),
                data: function (d) {
                    d.ShowAllParticipants = $('#ShowAllParticipants').prop('checked');
                    d.showAllSponsors = $('#ShowAllSponsors').prop('checked');
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

	                    if (!MyRio2cCommon.isNullOrEmpty(full.CollaboratorImageUploadDate)) {
                            html += '<img src="' + imageDirectory + full.Uid + '_thumbnail.png?v=' + moment(full.CollaboratorImageUploadDate).locale(globalVariables.userInterfaceLanguage).format('YYYYMMDDHHmmss') + '" /> ';
	                    }
	                    else {
		                    html += '<img src="' + imageDirectory + 'no-image.png?v=20190818200849" /> ';
	                    }

                        html += '       <td> ' + full.Name + '</td>\
                                    </tr>\
                                </table>';

	                    //if (!full.IsInCurrentEdition) {
		                   // html += '<span class="kt-badge kt-badge--inline kt-badge--info mt-2">' + labels.notInEdition + '</span>';
	                    //}

	                    return html;
                    }
                },
				{
                    data: 'AirfareSponsor',
                    render: function (data, type, full, meta) {
                        return data;
                    }
                },
                {
                    data: 'AccommodationSponsor',
                    render: function (data, type, full, meta) {
                        return data;
                    }
                },
                {
                    data: 'AirportTransferSponsor',
                    render: function (data, type, full, meta) {
                        return data;
                    }
                },
	            {
                    data: 'TransferCity',
                    render: function (data, type, full, meta) {
                        return data === true ? yes : no;
		            }
                },
	            {
                    data: 'IsVehicleDisposalRequired',
		            render: function (data, type, full, meta) {
			            return data === true ? yes : no;
		            }
	            },
                {
                    data: 'CreateDate',
                    render: function (data) {
                        return moment(data).locale(globalVariables.userInterfaceLanguage).format('L LTS');
                    }

                },
                {
                    data: 'UpdateDate',
                    render: function (data) {
                        return moment(data).locale(globalVariables.userInterfaceLanguage).format('L LTS');
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
                        
                        if (full.HasRequest) {
	                        // View details
	                        html += '<button class="dropdown-item" onclick="LogisticsDataTableWidget.showDetails(\'' + full.Uid + '\');"><i class="la la-edit"></i> ' + view + '</button>';
                        }

                        if (!full.HasRequest) {
                            // Create request
                            html += '<button class="dropdown-item" onclick="LogisticsCreate.showModal(\'' + full.Uid + '\');"><i class="la la-edit"></i> ' + addRequest + '</button>';
                        }
                        else if (!full.HasLogistics) {
	                        html += '<button class="dropdown-item" onclick="LogisticsDelete.showModal(\'' + full.Uid + '\', false);"><i class="la la-remove"></i> ' + labels.remove + '</button>';
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
                    width: '25%'

	            },
                {
                    targets: [1, 2, 3, 4, 5],
                    className: "dt-center",
                    orderable: false,
                    width: '10%'
                },
	            {
		            targets: [6, 7],
                    className: "dt-center",
		            width: '9%'
	            },
                {
                    targets: -1,
                    width: "7%",
                    orderable: false,
                    searchable: false,
                    className: "dt-center"
                }
            ],
            initComplete: function() {
                //$('button.buttons-collection').attr('data-toggle', 'dropdown');
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
        
    var showDetails = function (logisticsUid) {
        if (MyRio2cCommon.isNullOrEmpty(logisticsUid)) {
            return;
        }

        window.location.href = MyRio2cCommon.getUrlWithCultureAndEdition('/Logistics/Details/' + logisticsUid);
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
        showDetails: function (logisticsUid) {
            showDetails(logisticsUid);
        }
    };
}();