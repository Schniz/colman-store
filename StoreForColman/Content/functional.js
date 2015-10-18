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