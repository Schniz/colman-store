(function () {
    var ProductFiltering = window.ProductFiltering = {};
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

    ProductFiltering.getFilter = function getFilter() {
        return {
            manufactor: getManufactorFilter(),
            maxPrice: getMaxPrice(),
            available: getAvailableFilter()
        };
    }
    
    ProductFiltering.onChange = function onChange(callback) {
        $("#manufactors-filter").change(callback);
        $("#maxprice-filter").keyup(callback).change(callback);
        $("#available-filter").change(callback);
    }
})();