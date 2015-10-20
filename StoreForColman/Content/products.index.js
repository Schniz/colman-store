(function () {
    var DOM = window.DOM;

    function fetchData(filtering) {
        return $.getJSON("/Products/List", filtering);
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

    function getManufactorFilter() {
        var $element = $("#manufactors-filter option:selected");
        return $element.attr("data-is-all") ? undefined : $element.val();
    }

    function getMaxPrice() {
        return $("#maxprice-filter").val() || undefined;
    }

    function getAvailableFilter() {
        return $("#available-filter").is(':checked') || undefined;
    }

    function getFilter() {
        return {
            manufactor: getManufactorFilter(),
            maxPrice: getMaxPrice(),
            available: getAvailableFilter()
        };
    }

    // Loading a page is just a composition of all the functions
    // below
    var loadPage = compose(
        refreshTable,
        generateRows,
        memoize(fetchData),
        getFilter
    );

    $(loadPage);
    $("#manufactors-filter").change(loadPage);
    $("#maxprice-filter").keyup(loadPage).change(loadPage);
    $("#available-filter").change(loadPage);

})();