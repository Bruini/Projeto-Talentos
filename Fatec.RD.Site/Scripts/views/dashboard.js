/*********** Declarando variáveis para facilitar ***************/

var api = "http://localhost:63649/api/Despesa/";

var valorTotal = $("#valorTotal");
var qtdDespesas = $("#qtdDespesas");


somaTotal();

/***************************************************************/

function somaTotal() {
   
    $.ajax({
        type: "GET",
        url: api + "SomaTotal",
        success: function (data) {

            prencheSomaDespesas(data);

        },
        error: function (error) {
            console.log(error);
        }
    });
}

function prencheSomaDespesas(data) {
    valorTotal.text(data.ValorTotal);
    qtdDespesas.text(data.Despesas);
}