function compose(/* arguments */) {
    var args = Array.prototype.slice.apply(arguments);
    return function (x) {
        return args.reduceRight(function (a, b) {
            if (a && typeof a.then === 'function') return a.then(b);
            return b(a);
        }, x);
    }
}

function trace(tag) {
    return function (x) {
        console.log(tag, x);
        return x;
    };
}

var memoizationTable = {};
function memoize(func) {
    var funcString = func.toString();
    memoizationTable[funcString] = {};
    return function () {
        var argsString = JSON.stringify(arguments);
        if (!memoizationTable[funcString][argsString]) {
            memoizationTable[funcString][argsString] = func.apply(this, arguments);
        }
        return memoizationTable[funcString][argsString];
    };
}

// For creating dom elements with syntatic sugar:
// $("<a />").attr('href', 'http://').text('yay') becomes
// dom('a', { href: 'http://' }, 'yay'), or even better:
// a({ href: 'http://' }, 'yay') just by binding it.
function dom(type, attr, children) {
    if (!children && (
        Array.isArray(attr) ||
        attr instanceof $ ||
        typeof attr !== 'object'
    )) {
        children = attr;
        attr = undefined;
    }
    var $el = $("<" + type + " />");
    if (attr) {
        $el.attr(attr);
    }
    if (children !== null && children !== undefined) {
        $el.val(children).append(children);
    }
    return $el;
}

function createDOM(/* arguments */) {
    return Array.prototype.slice.apply(arguments).reduce(function (obj, type) {
        obj[type] = dom.bind(null, type);
        return obj;
    }, {});
}

window.DOM = createDOM(
    'td', 'tr', 'input', 'a', 'span', 'div'
);

// Alerts
function growl(type, text, data) {
    $.bootstrapGrowl(text, {
        ele: 'body', // which element to append to
        type: type, // (null, 'info', 'danger', 'success')
        offset: { from: 'bottom', amount: 20 }, // 'top', or 'bottom'
        align: 'right', // ('left', 'right', or 'center')
        width: 250, // (integer, or 'auto')
        delay: 4000, // Time while the message will be displayed. It's not equivalent to the *demo* timeOut!
        allow_dismiss: true, // If true then will display a cross to close the popup.
        stackup_spacing: 10 // spacing between consecutively stacked growls.
    });

    return data;
}

var successGrowl = growl.bind(null, "success");

function getJSON(/* arguments */) {
    var args = Array.prototype.slice.apply(arguments);
    return new Promise(function (resolve, reject) {
        $.getJSON.apply($, args).then(resolve).fail(reject);
    });
}