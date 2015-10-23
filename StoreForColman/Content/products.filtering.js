(function () {
    var ProductFiltering = window.ProductFiltering = {};
    var currency = undefined;

    function fetchCurrency() {
        return $.getJSON("/Currency");
    }

    function getCurrency() {
        var c = currency || fetchCurrency();
        currency = c;
        return currency;
    }

    ProductFiltering.applyCurrency = function applyCurrency(data) {
        return new Promise(function (resolve, reject) {
            getCurrency().then(function (currency) {
                var selected = $("#choose-currency").val();
                var curr = currency[selected] || 1;
                var newData = data.map(function (item) {
                    return Object.assign({}, item, {
                        PriceInNIS: Math.round(item.PriceInNIS / curr * 100) / 100
                    });
                });
                resolve(newData);
            });
        });
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
        $("#choose-currency").change(callback);
    }
})();