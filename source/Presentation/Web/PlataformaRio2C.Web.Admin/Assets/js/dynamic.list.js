// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 08-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-28-2019
// ***********************************************************************
// <copyright file="dynamic.list.js" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary>
// Dynamic list functions to add, remove and recalculate indexes
// Pre-requisites:
//      1) The row template in html
//          Ex:
//              var templates = {
//                  Installment: $('#installment-template').html()
//              };
//              $('#installment-template').remove();
//      2) Specific id for the list to append and remove items
//      3) Specific class for all rows of the list
//      4) All input parameters must have a prefix for (Id and Name)
//      5) Specific class for all remove buttons or links
//</summary>

// ***********************************************************************

var DynamicList = function () {

    // Add item to the list -------------------------------------------------------
    //  templateHtml: item that will be added in html
    //  listId: id of the list
    //  itemClass: class of all rows
    //  prefix: prefix of the Id and the Name attributes
    var addItem = function (templateHtml, listId, itemClass, prefix) {
        if (MyRio2cCommon.isNullOrEmpty(templateHtml) || MyRio2cCommon.isNullOrEmpty(listId) || MyRio2cCommon.isNullOrEmpty(itemClass) || MyRio2cCommon.isNullOrEmpty(prefix)) {
            console.error('dynamic.list.js::addItem(): templateHtml, listId, itemClass and prefix are mandatory.');
            return;
        }

        var regExprId = new RegExp(prefix + '_\\d', 'g');
        var regExprId2 = new RegExp(prefix + '\\d', 'g');
        var regExprName = new RegExp(prefix + '\\[\\d\\]', 'g');
        $('#' + listId)
            .append(templateHtml
                .replace(regExprId, prefix + '_' + $('#' + listId + ' .' + itemClass).length)
                .replace(regExprName, prefix + '[' + $('#' + listId + ' .' + itemClass).length + ']')
                .replace(regExprId2, prefix + $('#' + listId + ' .' + itemClass).length)
        );
    };

    // Remove item of the list ----------------------------------------------------
    //  element: must be "this"
    //  itemClass: class of all rows
    //  prefix: prefix of the Id and the Name attributes
    var removeItem = function (element, itemClass, prefix) {
        if (MyRio2cCommon.isNullOrEmpty(element) || MyRio2cCommon.isNullOrEmpty(itemClass) || MyRio2cCommon.isNullOrEmpty(prefix)) { 
            console.error('dynamic.list.js::removeItem(): element, itemClass and prefix are mandatory.');
            return;
        }

        $(element).parents('.' + itemClass).remove();
        recalculateIndexes(itemClass, prefix);
    };

    // Disable/enable remove button of the first item -----------------------------
    //  removeClass: class of all remove buttons or links
    //  itemClass: class of all rows
    var toggleItemRemoveButton = function (removeClass, itemClass) {
        if (MyRio2cCommon.isNullOrEmpty(removeClass) || MyRio2cCommon.isNullOrEmpty(itemClass)) {
            console.error('dynamic.list.js::toggleItemRemoveButton(): removeClass and itemClass are mandatory.');
            return;
        }

        if ($('.' + itemClass).length === 1) {
            $('.' + removeClass).addClass('d-none');
        } else {
            $('.' + removeClass).removeClass('d-none');
        }
    };

    // Recalculate indexes --------------------------------------------------------
    //  itemClass: class of all rows
    //  prefix: prefix of the Id and the Name attributes
    var recalculateIndexes = function (itemClass, prefix) {
        var regExprId = new RegExp(prefix + '_\\d', 'g');
        var regExprId2 = new RegExp(prefix + '\\d', 'g');
        var regExprName = new RegExp(prefix + '\\[\\d\\]', 'g');

        $('.' + itemClass).each(function (index) {
            $(this)
                .html($(this).html()
                    .replace(regExprId, prefix + '_' + index)
                    .replace(regExprName, prefix + '[' + index + ']')
                    .replace(regExprId2, prefix + index)
                );
        });
    };

    return {
        init: function () {
        },
        addItem: function (templateHtml, listId, itemClass, prefix) {
            addItem(templateHtml, listId, itemClass, prefix);
        },
        removeItem: function (element, itemClass, prefix) {
            removeItem(element, itemClass, prefix);
        },
        toggleItemRemoveButton: function (removeClass, itemClass) {
            toggleItemRemoveButton(removeClass, itemClass);
        }
    };
}();