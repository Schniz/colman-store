(function () {
    var DOM = window.DOM;
    
    var blankRow = DOM.tr(
        DOM.td({ colspan: 5 }, [
            "אין מוצרים ברשימה. ",
            DOM.a({ href: "/Products" }, "צא למסע קניות!")
        ])
    );

    function removeItemButton(id) {
        return DOM.a({
            href: "javascript: void(0)",
            class: 'input-group-addon'
        }, DOM.span({
            class: 'glyphicon glyphicon-trash'
        })).click(removeItemFromCart.bind(null, id));
    }

    function sumRow(cartData) {
        var sum = cartData.reduce(function (sum, item) {
            return sum + item.PriceInNIS * item.Quantity;
        }, 0);
        return DOM.tr([
            DOM.td(),
            DOM.td(),
            DOM.td(),
            DOM.td(),
            DOM.td('סה"כ: ' + sum + ' ש"ח')
        ]);
    }

    function getQuantityInForm(id) {
        var val = Math.floor(parseInt($("[data-item-id=" + id + "]").val()));
        if (isNaN(val)) throw Error("not gute");
        return { id: id, quantity: val };
    }

    function quantityInput(item) {
        var onChange = changedQuantity.bind(null, item.ID);
        return DOM.input({
            class: 'form-control',
            type: 'number',
            min: 0,
            'data-item-id': item.ID
        }, item.Quantity).change(onChange);
    }

    function generateRows(cartData) {
        return !cartData.length ? blankRow : cartData.map(function (item) {
            return DOM.tr([
                DOM.td(item.ID),
                DOM.td(item.Name),
                DOM.td(item.ManufactorName),
                DOM.td(item.PriceInNIS),
                DOM.td(
                    DOM.div({ class: 'input-group'}, [
                        quantityInput(item),
                        removeItemButton(item.ID)
                    ])
                ).css({ width: '11em' })
            ]);
        }).concat(sumRow(cartData));
    }

    function toggleLoad(x) {
        $("#order-index tr:not(.table-head)").hide();
        $("#products-loading").show();
        return x;
    }

    function refreshTable(rows) {
        $("#order-index tr:not(.table-head)").remove();
        $("#order-index").append(rows);
        $("#products-loading").hide();
    }

    function sendCreateOrder() {
        return $.post("/Order/Create");
    }

    function afterSave(result) {
        if (result.error) throw Error(result.error);
        return result;
    }

    function showSuccessMessage(result) {
        $.bootstrapGrowl("הצלחנו! זה נשלח בהצלחה", {
            ele: 'body', // which element to append to
            type: 'success', // (null, 'info', 'danger', 'success')
            offset: { from: 'bottom', amount: 20 }, // 'top', or 'bottom'
            align: 'right', // ('left', 'right', or 'center')
            width: 250, // (integer, or 'auto')
            delay: 4000, // Time while the message will be displayed. It's not equivalent to the *demo* timeOut!
            allow_dismiss: true, // If true then will display a cross to close the popup.
            stackup_spacing: 10 // spacing between consecutively stacked growls.
        });

        return result;
    }

    var removeItemFromCart = compose(refreshTable, generateRows, window.removeFromCart, toggleLoad);
    var changedQuantity = compose(refreshTable, generateRows, window.editFromCart, trace('quantity is'), getQuantityInForm, toggleLoad);
    var loadPage = compose(refreshTable, trace('rows'), generateRows, window.fetchAndRefreshCart, toggleLoad);
    var createOrder = window.createOrder = compose(loadPage, trace('after save'), showSuccessMessage, afterSave, sendCreateOrder);

    $(loadPage);
})();