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
    if (children) {
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