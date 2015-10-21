(function () {
	var loadPage = compose(trace('this is the filter'), ProductFiltering.getFilter);
	$(loadPage);
})();