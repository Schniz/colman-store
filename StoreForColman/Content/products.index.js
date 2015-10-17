function fetchData(filtering) {
    return $.getJSON("/Products/List", filtering);
}

function generateRows(data) {
    return data.map(function (rowData) {
        return $("<tr />").append([
            $("<td />").text(rowData.ID),
            $("<td />").text(rowData.Name),
            $("<td />").text(rowData.ManufactorName),
            $("<td />").text(rowData.PriceInNIS),
            $("<td />").text(rowData.AmountInStore)
        ])
    });
}

function refreshTable(rows) {
    $("#products-index tr:not(.table-head)").remove();
    $("#products-index").append(rows);
}

var loadPage = compose(refreshTable, generateRows, fetchData);

$("#get-pokemons").click(function () {
    loadPage({ manufactor: 'pokemon' });
});

$(function () { loadPage() });