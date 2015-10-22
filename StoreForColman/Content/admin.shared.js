(function () {
    var DOM = window.DOM;
    var Admin = window.Admin = {};

    Admin.generateManipulationOptions = function generateManipulationOptions(id) {
        return [
            DOM.a({ href: "/AdminProducts/Details/" + id }, "פרטים"),
            " / ",
            DOM.a({ href: "/AdminProducts/Edit/" + id }, "ערוך"),
            " / ",
            DOM.a({ href: "/AdminProducts/Delete/" + id }, "מחק")
        ];
    };
})();