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

    function refreshCart(cartData) {
        var itemsCount = cartData.reduce(function (sum, item) {
            return sum + item.Quantity;
        }, 0);
        $("#cart-items-number").text(itemsCount);
        $("#cart-items-link").attr("title", cartDataToString(cartData)).tooltip('fixTitle');
        console.log(cartDataToString(cartData));
    }

    function createTooltip() {
        $("#cart-items-link").tooltip({
            placement: 'bottom'
        });
    }

    window.addToCart = compose(refreshCart, sendAddToCart);

    $(compose(refreshCart, fetchCartData, createTooltip));
})();