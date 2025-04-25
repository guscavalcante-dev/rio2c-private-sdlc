// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Daniel Giese
// Created          : 03-17-2025
//
// Last Modified By : Daniel Giese
// Last Modified On : 03-17-2025
// ***********************************************************************
// <copyright file="music.meetings.sendemailtoproducers.datatable.widget.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var MusicMeetingsSendEmailToProducersDataTableWidget = function () {
    var widgetElementId = '#MusicMeetingsSendEmailToProducersDataTableWidget';
    var tableElementId = '#musicmeetingssendemailtoproducers-list-table';
    var table;

    // Invitation email ---------------------------------------------------------------------------
    var sendEmails = function (sendEmailParameters) {
        MyRio2cCommon.block();
        $.post(MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Meetings/SendProducersEmails'), sendEmailParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                },
                // Error
                onError: function () {
                }
            });
        })
        .fail(function () {
        })
        .always(function () {
            MyRio2cCommon.unblock();
        });
    };

    var showSendEmailsModal = function () {

        var jsonParameters = new Object();
        jsonParameters.selectedAttendeeCollaboratorsUids = $('#musicmeetingssendemailtoproducers-list-table_wrapper tr.selected').map(function () { return $(this).data('id'); }).get().join(',');
        jsonParameters.keywords = $('#Search').val();
        
        
        var message = jsonParameters.selectedAttendeeCollaboratorsUids === '' ? translations.confirmSendEmailAll : translations.confirmSendEmailSelected;

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
                    label: labels.send,
                    className: "btn btn-brand btn-elevate",
                    callback: function () {
                        sendEmails(jsonParameters);
                    }
                }
            }
        });
    };

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
            buttons: [
                {
                    extend: 'collection',
                    text: labels.actions,
                    buttons: [
                        {
                            text: translations.sendEmailToProducers,
                            action: function (e, dt, node, config) {
                                $('.dt-button-background').remove();
                                showSendEmailsModal();
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
                        }]
                }],
            order: [[0, "asc"]],
            sDom: '<"row"<"col-sm-6"l><"col-sm-6 text-right"B>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            oSearch: {
                sSearch: $('#Search').val()
            },
            ajax: {
                url: MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Meetings/SendEmailToProducersSearch'),
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
                    data: 'Name',
                    render: function (data, type, row, meta) {
                        var html = '\
                                <table class="image-side-text text-left">\
                                    <tr>\
                                        <td>';

                        if (!MyRio2cCommon.isNullOrEmpty(row.CollaboratorDto.ImageUploadDate)) {
                            html += '<img src="' + imageDirectory + row.CollaboratorDto.Uid + '_thumbnail.png?v=' + moment(row.CollaboratorDto.ImageUploadDate).locale(globalVariables.userInterfaceLanguage).format('YYYYMMDDHHmmss') + '" /> ';
                        }
                        else {
                            html += '<img src="' + imageDirectory + 'no-image.png?v=20190818200849" /> ';
                        }

                        html += '       </td>\
                                        <td> ' + row.CollaboratorDto.FullName + '</td>\
                                    </tr>\
                                </table>';

                        return html;
                    }
                },
                {
                    data: 'OneToOneMeetings',
                    render: function (data, type, row, meta) {
                        var html = '\
                                <table class="w-100">\
                                    <tr>\
                                        <th style="width: 8%;">' + translations.date + '</th>\
                                        <th style="width: 20%;">' + translations.room + '</th>\
                                        <th style="width: 20%;">' + translations.round + '</th>\
                                        <th style="width: 6%;">' + translations.table + '</th>\
                                        <th style="width: 26%;">' + translations.player + '</th>\
                                    </tr>';

                        //loop through all the row details to build output string
                        var sortedNegotiationBaseDtos = row.MusicBusinessRoundNegotiationBaseDtos.sortBy('StartDate');
                        for (var item in sortedNegotiationBaseDtos) {
                            if (sortedNegotiationBaseDtos.hasOwnProperty(item)) {
                                var r = sortedNegotiationBaseDtos[item];
                                html += '\
                                    <tr style="font-size: 10px; border-top: 1px solid #ebedf2;;">\
                                        <td class="text-center">' + moment(r.StartDate).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('L') + '</td>\
                                        <td class="text-center">' + r.RoomJsonDto.Name;

                                if (!MyRio2cCommon.isNullOrEmpty(r.RoomJsonDto.IsVirtualMeeting)) {
                                    var virtualOrPresentialText = (r.RoomJsonDto.IsVirtualMeeting === true) ? translations.virtual : translations.presential;
                                    html += '<span class="kt-badge kt-badge--inline kt-badge--warning kt-font-boldest">' + virtualOrPresentialText + '</span>';
                                }

                                html += '\
                                        </td>\
                                        <td>' + translations.round + ' ' + r.RoundNumber + ' (' + moment(r.StartDate).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('LT') + ' - ' + moment(r.EndDate).tz(globalVariables.momentTimeZone).locale(globalVariables.userInterfaceLanguage).format('LT') + ')</td>\
                                        <td class="text-center">' + r.TableNumber + '</td>\
                                        <td>';

                                html += '\
                                            <table class="image-side-text text-left">\
                                                <tr>\
                                                    <td>';

                                if (!MyRio2cCommon.isNullOrEmpty(r.MusicBusinessRoundProjectBuyerEvaluationBaseDto.BuyerAttendeeOrganizationBaseDto.OrganizationBaseDto.ImageUploadDate)) {
                                    html += '<img src="' + imageDirectory + r.MusicBusinessRoundProjectBuyerEvaluationBaseDto.BuyerAttendeeOrganizationBaseDto.OrganizationBaseDto.Uid + '_thumbnail.png?v=' + moment(r.MusicBusinessRoundProjectBuyerEvaluationBaseDto.BuyerAttendeeOrganizationBaseDto.OrganizationBaseDto.ImageUploadDate).locale(globalVariables.userInterfaceLanguage).format('YYYYMMDDHHmmss') + '" /> ';
                                }
                                else {
                                    html += '<img src="' + imageDirectory + 'no-image.png?v=20190818200849" /> ';
                                }

                                html += '           </td>\
                                                    <td> ' + r.MusicBusinessRoundProjectBuyerEvaluationBaseDto.BuyerAttendeeOrganizationBaseDto.OrganizationBaseDto.DisplayName + '</td>\
                                                </tr>\
                                            </table>\
                                        </td>\
                                    </tr>';
                            }
                        }

                        html += '</table>';

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
                    width: "75%",
                    orderable: false,
                    searchable: false
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

    //var showDetails = function (playerUid) {
    //    if (MyRio2cCommon.isNullOrEmpty(playerUid)) {
    //        return;
    //    }

    //    window.location.href = MyRio2cCommon.getUrlWithCultureAndEdition('/Music/Producers/Details/' + playerUid);
    //};

    return {
        init: function () {
            MyRio2cCommon.block({ idOrClass: widgetElementId });
            initiListTable();
        },
        refreshData: function() {
            refreshData();
        },
        //showDetails: function (playerUid) {
        //    showDetails(playerUid);
        //}
    };
}();