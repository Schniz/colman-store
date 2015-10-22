(function () {
    var DOM = window.DOM;

    function fetchData(filter) {
        return $.getJSON("/Products/List", filter);
    }

    function generateRows(data) {
        return data.map(function (item) {
            return DOM.tr([
                DOM.td(item.ID),
                DOM.td(item.Name),
                DOM.td(item.ManufactorName),
                DOM.td(item.PriceInNIS),
                DOM.td(item.AmountInStore),
                DOM.td(window.Admin.generateManipulationOptions(item.ID))
            ]);
        });
    }

    function refreshTable(rows) {
        $("#products-index tr:not(.table-head)").remove();
        $("#products-index").append(rows);
        return rows;
    }

	var loadPage = compose(trace('this is the filter'), refreshTable, generateRows, memoize(fetchData), ProductFiltering.getFilter);
	ProductFiltering.onChange(loadPage);
	$(loadPage);
})();