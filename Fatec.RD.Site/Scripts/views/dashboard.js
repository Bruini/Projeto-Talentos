/*********** Declarando variáveis para facilitar ***************/

var api = "http://localhost:63649/api/Despesa/";

var valorTotal = $("#valorTotal");
var qtdDespesas = $("#qtdDespesas");
var valorTotalRelatorio = $("#valorTotalRelatorio");
var qtdDespesasRelatorio = $("#qtdDespesasRelatorio");

var selectRelatorio = $("#listaRelatorios");

somaTotal();
getRelatorios();
/***************************************************************/


/*********** Funções da API ***************/
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

function getRelatorios() {
    var api = "http://localhost:63649/api/Relatorio/";
    $.ajaxSetup({ async: false });
    var retorno;
    $.ajax({
        type: 'GET',
        url: api,
        success: function (relatorios) {
            retorno = relatorios;
            $.each(retorno, function (i, item) {


                selectRelatorio.append($('<option>', {
                    value: item.Id,
                    text: item.Descricao
                }));

            });
        },
        error: function (error) {
            console.log(error);
        }
    });
    $.ajaxSetup({ async: true });

}

function somaTotalRelatorio(id) {

    $.ajax({
        type: "GET",
        url: api + "SomaTotal/" + id,
        success: function (data) {

            prencheSomaDespesasRelatorio(data);

        },
        error: function (error) {
            console.log(error);
        }
    });
}

/********************************************/



/*********** Funções Internas ***************/

function prencheSomaDespesas(data) {
    valorTotal.text(data.ValorTotal);
    qtdDespesas.text(data.Despesas);
}

function prencheSomaDespesasRelatorio(data) {
    valorTotalRelatorio.text(data.ValorTotal);
    qtdDespesasRelatorio.text(data.Despesas);   
}



selectRelatorio.on('change', function () {
    somaTotalRelatorio(this.value);
})

/********************************************/