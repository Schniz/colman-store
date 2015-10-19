(function () {
    window.sendAddToCart = function sendAddToCart(id) {
        return $.post("/Order/Add/" + id);
    };

    window.fetchCartData = function fetchCartData() {
        return $.getJSON("/Order/Cart");
    };

    function refreshCart(cartData) {
        var itemsCount = cartData.reduce(function (sum, item) {
            return sum + item.Quantity;
        }, 0);
        $("#cart-items-number").text(itemsCount);
    }

    window.addToCart = compose(refreshCart, sendAddToCart);

    $(compose(refreshCart, fetchCartData));
})();