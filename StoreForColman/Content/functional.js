function compose(/* arguments */) {
    var args = Array.prototype.slice.apply(arguments);
    return function (x) {
        return args.reduceRight(function (a, b) {
            if (a && typeof a.then === 'function') return a.then(b);
            return b(a);
        }, x);
    }
}