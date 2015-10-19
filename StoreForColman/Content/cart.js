(function () {
    window.sendAddToCart = function sendAddToCart(id) {
        return $.post("/Order/Add/" + id);
    };

    window.fetchCartData = function fetchCartData() {
        return $.getJSON("/Order/Cart");
    };

    function cartDataToString(cartData) {
        return cartData.reduce(function(str, item) {
            return str + "\n" + item.Name + ": " + item.Quantity;
        }, "").trim();
    }

    function updateItemsNumber(cartData) {
        var itemsCount = cartData.reduce(function (sum, item) {
            return sum + item.Quantity;
        }, 0);
        $("#cart-items-number").text(itemsCount);
        return cartData;
    }

    function updateLinkTooltip(cartData) {
        $("#cart-items-link").attr("title", cartDataToString(cartData)).tooltip('fixTitle');
        return cartData;
    }

    var refreshCart = compose(updateLinkTooltip, updateItemsNumber);

    function createTooltip() {
        $("#cart-items-link").tooltip({
            placement: 'bottom'
        });
    }

    window.addToCart = compose(refreshCart, sendAddToCart);

    $(compose(refreshCart, fetchCartData, createTooltip));
})();