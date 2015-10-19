(function () {

    function fetchData(filtering) {
        return $.getJSON("/Products/List", filtering);
    }

    function generateAddToCartButton(rowData) {
        return $("<td />").append(
            $("<a />").text("הוסף לסל הקניות").click(window.addToCart.bind(null, rowData.ID))
        );
    }

    function generateRows(data) {
        return data.map(function (rowData) {
            return $("<tr />").append([
                $("<td />").text(rowData.ID),
                $("<td />").text(rowData.Name),
                $("<td />").text(rowData.ManufactorName),
                $("<td />").text(rowData.PriceInNIS),
                $("<td />").text(rowData.AmountInStore),
                window.isLoggedIn ? (
                    generateAddToCartButton(rowData)
                ) : $("<td />")
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
    $("#maxprice-filter").keyup(loadPage);
    $("#available-filter").change(loadPage);

})();