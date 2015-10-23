(function () {
    var DOM = window.DOM;

    function fetchData(filters) {
        return getJSON("/Products/List", filters);
    }

    function generateAddToCartButton(id) {
        return DOM.a({
            href: 'javascript: void(0);'
        }, "הוסף לסל הקניות").click(
            window.addToCart.bind(null, id)
        );
    }

    function generateRows(data) {
        return data.map(function (rowData) {
            return DOM.tr([
                DOM.td(rowData.ID),
                DOM.td(rowData.Name),
                DOM.td(rowData.ManufactorName),
                DOM.td(rowData.PriceInNIS),
                DOM.td(rowData.AmountInStore),
                DOM.td(generateAddToCartButton(rowData.ID))
            ]);
        });
    }

    function refreshTable(rows) {
        $("#products-index tr:not(.table-head)").remove();
        $("#products-index").append(rows);
    }

    function x() {
        return Promise.resolve('wat');
    }

    // Loading a page is just a composition of all the functions
    // below
    var loadPage = compose(
        refreshTable,
        generateRows,
        ProductFiltering.applyCurrency,
        memoize(fetchData),
        ProductFiltering.getFilter
    );

    $(loadPage);
    ProductFiltering.onChange(loadPage);

})();