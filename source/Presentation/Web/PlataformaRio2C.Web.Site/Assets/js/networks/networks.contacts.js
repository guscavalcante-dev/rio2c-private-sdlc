// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 11-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-12-2020
// ***********************************************************************
// <copyright file="networks.contacts.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

var NetworksContacts = function () {

    var showModalId = '#ShowContactsListModal';

    // Search -------------------------------------------------------------------------------------
    var search = function () {
        if (typeof (NetworksContactsListWidget) !== 'undefined') {
            NetworksContactsListWidget.search();
        }
    };

    var enableSearchEvent = function () {
        $('#ContactsSearchKeywords').not('.search-event-enabled').on('search', function () {
            search();
        });

        $('#ContactsSearchKeywords').addClass('search-event-enabled');
    };

    var enableRoleChangeEvent = function() {
        $('#CollaboratorRoleUid').not('.change-event-enabled').on('change', function () {
            search();
        });

        $('#CollaboratorRoleUid').addClass('change-event-enabled');
    };

    var enableIndustryChangeEvent = function () {
        $('#CollaboratorIndustryUid').not('.change-event-enabled').on('change', function () {
            search();
        });

        $('#CollaboratorIndustryUid').addClass('change-event-enabled');
    };

    // Plugins ------------------------------------------------------------------------------------
    var enableListPlugins = function () {
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '#CollaboratorIndustryUid', allowClear: true, placeholder: translations.selectPlaceholderAn.replace('{0}', translations.industry) + '...' });
        MyRio2cCommon.enableSelect2({ inputIdOrClass: '#CollaboratorRoleUid', allowClear: true, placeholder: translations.selectPlaceholderA.replace('{0}', translations.role) + '...' });
        enableSearchEvent();
        enableRoleChangeEvent();
        enableIndustryChangeEvent();
        MyRio2cCommon.enablePaginationBlockUi();
    };

    // Show List Modal ----------------------------------------------------------------------------
    var enableShowListModalPlugins = function () {
        enableListPlugins();
    };

    var showListModal = function () {
        MyRio2cCommon.block({ isModal: true });

        var jsonParameters = new Object();

        $.get(MyRio2cCommon.getUrlWithCultureAndEdition('/Networks/ShowContactsListModal'), jsonParameters, function (data) {
            MyRio2cCommon.handleAjaxReturn({
                data: data,
                // Success
                onSuccess: function () {
                    enableShowListModalPlugins();
                    $(showModalId).modal();
                    NetworksContactsListWidget.init();
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

    var sendMessage = function (otherUserId) {
        if (typeof (NetworksMessagesConversationsWidget) !== 'undefined') {
            NetworksMessagesConversationsWidget.show(otherUserId);
            $(showModalId).modal('hide');
        }

        return false;
    };

    return {
        init: function () {
            enableListPlugins();
        },
        search: function () {
            search();
        },
        showListModal: function() {
            showListModal();
        },
        sendMessage: function (otherUserId) {
            return sendMessage(otherUserId);
        }
    };
}();