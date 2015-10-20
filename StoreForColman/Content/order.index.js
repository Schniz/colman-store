(function () {
    var td = DOM.td;
    var tr = DOM.tr;
    var input = DOM.input;
    var a = DOM.a;
    var span = DOM.span;
    
    var blankRow = tr(
        td({ colspan: 5 }, [
            "אין מוצרים ברשימה. ",
            a({ href: "/Products" }, "צא למסע קניות!")
        ])
    );

    function generateRows(cartData) {
        return !cartData.length ? blankRow : cartData.map(function (item) {
            return DOM.tr([
                DOM.td(item.ID),
                DOM.td(item.Name),
                DOM.td(item.ManufactorName),
                DOM.td(item.PriceInNIS),
                DOM.td(
                    DOM.div({ class: 'input-group'}, [
                        DOM.input({ class: 'form-control', type: 'number', min: 0 }, item.Quantity),
                        a({ href: "javascript: void(0)", class: 'input-group-addon' }, [
                            span({ class: 'glyphicon glyphicon-trash'})
                        ]).click(removeItemFromCart.bind(null, item.ID))
                    ])
                ).css({ width: '11em' })
            ]);
        });
    }

    function refreshTable(rows) {
        $("#order-index tr:not(.table-head)").remove();
        $("#order-index").append(rows);
        $("#products-loading").hide();
    }

    var removeItemFromCart = compose(refreshTable, generateRows, window.removeFromCart);
    var loadPage = compose(refreshTable, trace('rows'), generateRows, window.fetchCartData);

    $(loadPage);
})();