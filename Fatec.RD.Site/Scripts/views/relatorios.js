/*********** Declarando variáveis para facilitar ***************/
var api = "http://localhost:63649/api/Relatorio";
var formulario = $("#form-relatorios");
var body = $("#modal-generic");
var body = $("#modal-relatorio");
var bodyDespesa = $("#modal-despesa-relatorio");
var modalFooter = $("#modal-relatorio .modal-footer");
var titleModal = $("#modal-title");
var botaoSalvar = $("#salvar-relatorio");
var botaoSalvarDespesaRelatorio = $("#salvarDespesaRelatorio");
var tabela = $("#tabela-relatorios");
var tabelaRelatorioDespesa = $("#tabela-relatoriosDespesa");
var tabelaRelatorioDespesaAdicionada = $("#tabela-relatoriosDespesaAdicionada");
var dadosVinculados = $("#dadosVinculados");


var tipoRelatorio = $("#tipoRelatorio");
var descricao = $("#descricao");
var comentario = $("#comentario");
var dataVisualizacao = $("#data-visualizacao");
var data = $("#data");

var date = new Date();

var elemento = '<button type="button" class="btn btn-info left" id="btn-add-despesa" onclick="adicionarDespesaRelatorio()">Adicionar Despesa</button>';

selecionarTipoRelatorio();

/***************************************************************/

/************* Utilizando componentes em campos ****************/
$(".selecpicker").selectpicker();
/***************************************************************/

/************* Funções disparadas nos cliques ******************/
function novoRelatorio() {
    dataVisualizacao.val(date.toLocaleDateString());
    data.val(date.toJSON());
    formValidation();
    titleModal.html("Adicionar Relatório");
    body.modal('show');
}
function selecionarTipoRelatorio() {
    var api = "http://localhost:63649/api/TipoRelatorio/";
    $.ajaxSetup({ async: false });
    var retorno;
    $.ajax({
        type: 'GET',
        url: api,
        success: function (tipoRelatorio) {
            retorno = tipoRelatorio;

            $.each(retorno, function (i, item) {


                $('#tipoRelatorio').append($('<option>', {
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

function fechar() {
    formulario.validate().destroy();
    limparCampos();
   
    $("#btn-add-despesa").remove();
    botaoSalvar.removeAttr("disabled");

    
}

function adicionarDespesaRelatorio() {
    tRelatorioDespesa.reload();
    body.modal('hide');
    bodyDespesa.modal('show');
}

function SalvarDespesaRelatorio() {

    /******************DESVINCULADAS******************/
    var despesasSelecionadas = $('#tabela-relatoriosDespesa table tbody .m-checkbox input:checked').toArray().map(function (check) {
        return $(check).val();
    });
    var obj = {};
    var objChave = [];
    $.each(despesasSelecionadas, function (key, value) {
        objChave.push({
            IdDespesa: value
        });
    });
    obj.Chave = objChave;

    VincularDespesa(JSON.stringify(obj));
    /***********************************************/

    /******************VINCULADAS******************/
    var despesasVinculadas = $('#tabela-relatoriosDespesaAdicionada table tbody .m-checkbox input:checked').toArray().map(function (check) {
        return $(check).val();
    });
    obj = {};
    objChave = [];
    $.each(despesasVinculadas, function (key, value) {
        objChave.push({
            IdDespesa: value
        });
    });
    obj.Chave = objChave;
    DesvincularDespesa(JSON.stringify(obj));
    /***********************************************/
}

function abrirModalExcluir(id) {
    if (confirm("Tem certeza que deseja excluir?")) {
        excluir(id)
    }
}

function abrirModalAlterar(id) {
    dadosVinculados.empty();
    formValidation();
    botaoSalvar.attr("data-id", id);
    var relatorio = (selecionarPorId(id));
    preencherCampos(relatorio);
    titleModal.html("Alterar Relatório");
    modalFooter.append(elemento);
    var dados = listarTabelaRelatorioDespesaAdicionada(id);
    
    prencherVinculadas(dados);

    body.modal('show');
    
}
/***************************************************************/

/************** Funções com requisições para a API *************/
function inserir(relatorio) {

    console.log(relatorio);
    $.ajax({
        type: 'POST',
        url: api,
        data: JSON.stringify(relatorio),
        contentType: 'application/json',
        success: function () {
            modalFooter.append(elemento);
            botaoSalvar.attr("disabled", true);
            alert("Inserido com sucesso!");
            tRelatorio.reload();
        },
        error: function (error) {
            console.log(error.responseJSON.error);
        }
    })
}

function alterar(id, relatorio) {
    $.ajax({
        type: 'PUT',
        url: api + '/' + id,
        data: JSON.stringify(relatorio),
        contentType: 'application/json',
        success: function () {
            alert("Alterado com sucesso!");
            tRelatorio.reload();
            body.modal('hide');
            limparCampos();
        },
        error: function (error) {
            alert(error.responseJSON.error);
        }
    })
}

function excluir(id) {
    $.ajax({
        type: 'DELETE',
        url: api + '/' + id,
        success: function () {
            alert("Deletado com sucesso!");
            tRelatorio.reload();
        },
        error: function (error) {
            console.log(error);
        }
    })
}

function selecionarPorId(id) {
    $.ajaxSetup({ async: false }); // Força com que ela espere o retorno para prosseguir - Assim consigo pegar o resultado antes dele abrir o modal
    var retorno;
    $.ajax({
        type: 'GET',
        url: api + '/' + id,
        success: function (relatorio) {
            retorno = relatorio;

        },
        error: function (error) {
            console.log(error);
        }
    });
    $.ajaxSetup({ async: true });
    return retorno;
}

function VincularDespesa(despesas) {
  
    var id = botaoSalvar.attr("data-id");
    $.ajax({
        type: 'POST',
        url: api + '/' + id + "/Despesas",
        data: despesas,
        contentType: "application/json",
        success: function () {
            alert("Inserido com sucesso!");
            limparCampos();
            $("#btn-add-despesa").remove();
            botaoSalvar.removeAttr("disabled");
            bodyDespesa.modal('hide');
            tRelatorioDespesa.reload();
        },
        error: function (error) {
            console.log(error);
        }
    })
}

function DesvincularDespesa(despesas) {
    var id = botaoSalvar.attr("data-id");
    $.ajax({
        type: 'DELETE',
        url: api + '/' + id + "/Despesas",
        data: despesas,
        contentType: "application/json",
        success: function () {
            alert("Deletado com sucesso!");
            limparCampos();
            $("#btn-add-despesa").remove();
            botaoSalvar.removeAttr("disabled");
            bodyDespesa.modal('hide');
            tRelatorioDespesa.reload();
        },
        error: function (error) {
            console.log(error);
        }
    })

}


/***************************************************************/

/*********************** Funções internas **********************/

function salvarRelatorio() {
    var id = botaoSalvar.attr("data-id");

    var relatorio = {
        IdTipoRelatorio: tipoRelatorio.val(),
        Descricao: descricao.val(),
        Data: data.val(),
        Comentario: comentario.val()
    };
    if (id != undefined)
        alterar(id, relatorio);
    else
        //console.log(relatorio);
        inserir(relatorio);
}

function preencherCampos(relatorio) {
  

    var dataSub = relatorio.DataCriacao.substring(0, 10);
    var dataSplit = dataSub.split("-");
    var date = dataSplit[2] + "/" + dataSplit[1] + "/" + dataSplit[0];


    tipoRelatorio.val(relatorio.IdTipoRelatorio);
    tipoRelatorio.selectpicker('refresh');
    descricao.val(relatorio.Descricao);
    comentario.val(relatorio.Comentario);
    dataVisualizacao.val(date);
    data.val(date);

    //Remask
    dataVisualizacao.unmask().mask('00/00/0000');
    data.unmask().mask('00/00/0000');
}

function limparCampos() {
    formulario.each(function () {
        this.reset();
    });
    tipoRelatorio.selectpicker('refresh');
    botaoSalvar.removeAttr('data-id');
}

/***************************************************************/

/************************* Validações **************************/
function formValidation() {
    formulario.validate({
        errorClass: "errorClass",
        rules: {
            tipoRelatorio: { required: true },
            data: { required: true },
            descricao: { required: true },
            comentario: { required: true }
        },
        messages: {
            tipoRelatorio: { required: "Campo obrigatório." },
            data: { required: "Campo obrigatório." },
            descricao: { required: "Campo obrigatório." },
            comentario: { required: "Campo obrigatório." /*, minlength: "Campo deve possuir no mínimo {0} caracteres", maxlength: "Campo deve possuir no máximo {0} caracteres"*/ }
        },
        submitHandler: function (form) {
            salvarRelatorio();
        }
    });
}

/***************** RelatórioDespesa Vinculada *****************/


function listarTabelaRelatorioDespesaAdicionada(id) {

    
    $.ajaxSetup({ async: false }); // Força com que ela espere o retorno para prosseguir - Assim consigo pegar o resultado antes dele abrir o modal
    var retorno;
    $.ajax({
        type: "GET",
        url: api + "/" + id + "/Despesas",
        success: function (data) {
           
            retorno = data;
           
        },error: function (error) {
            console.log(error);
        }


    });
    $.ajaxSetup({ async: true });
    return retorno;
    



}

function prencherVinculadas(dados) {

    var html;
    
    console.log(dados);
    $.each(dados, function (i, item) {

        html += '<tr data-row="0" class="m-datatable__row" style="height: 0px;">';
        html += '<td data-field="Id" class="m-datatable__cell--center m-datatable__cell m-datatable__cell--check"><span style="width: 50px;"><label class="m-checkbox m-checkbox--single m-checkbox--solid m-checkbox--brand"><input type="checkbox" value="' + item.Id + '"><span></span></label></span></td>'; 
        html += '<td data-field="Data" class="m-datatable__cell"><span style="width: 110px;">' + item.Data + '</span></td>';
        html += '<td data-field="Valor" class="m-datatable__cell"><span style="width: 110px;">' + item.Valor + '</span></td>';
        html += '<td data-field="Comentario" class="m-datatable__cell"><span style="width: 110px;">' + item.Comentario + '</span></td>';

    });

    dadosVinculados.html(html);
}
/***************************************************************/

/*********************** RelatórioDespesa Desvinculada **********************/

var tRelatorioDespesa = tabelaRelatorioDespesa.mDatatable({
    translate: {
        records: {
            noRecords: "Nenhum resultado encontrado.",
            processing: "Processando..."
        },
        toolbar: {
            pagination: {
                items: {
                    default: {
                        first: "Primeira",
                        prev: "Anterior",
                        next: "Próxima",
                        last: "Última",
                        more: "Mais",
                        input: "Número da página",
                        select: "Selecionar tamanho da página"
                    },
                    info: 'Exibindo' + ' {{start}} - {{end}} ' + 'de' + ' {{total}} ' + 'resultados'
                },
            }
        }
    },
    data: {
        type: "remote",
        source: {
            read: {
                method: "GET",
                url: api + "/" + "Despesas",
                map: function (t) {
                    var e = t;
                    return void 0 !== t.data && (e = t.data), e
                }
            }
        },
        pageSize: 10,
        serverPaging: !0,
        serverFiltering: !0,
        serverSorting: !0
    },
    layout: {
        theme: "default",
        class: "",
        scroll: !1,
        footer: !1
    },
    sortable: !0,
    pagination: !0,
    toolbar: {
        items: {
            pagination: {
                pageSizeSelect: [10, 20, 30, 50, 100]
            }
        }
    },
    search: {
        input: $("#generalSearch")
    },
    columns: [
        {

            field: "Id",
            title: "#",
            width: 50,
            sortable: !1,
            textAlign: "center",
            selector: { class: "m-checkbox--solid m-checkbox--brand" }
        },
        {
            field: "Data",
            title: "Data",
        },
        {
            field: "Valor",
            title: "Valor",
        },
        {
            field: "Comentario",
            title: "Comentario",
        }],
    extensions: { checkbox: { vars: { selectedAllRows: 'selectedAllRows', requestIds: 'requestIds', rowIds: 'meta.Id', }, }, }

});


/***************************************************************************/


/*********************** Exibir lista de relatórios **********************/

var tRelatorio = tabela.mDatatable({
    translate: {
        records: {
            noRecords: "Nenhum resultado encontrado.",
            processing: "Processando..."
        },
        toolbar: {
            pagination: {
                items: {
                    default: {
                        first: "Primeira",
                        prev: "Anterior",
                        next: "Próxima",
                        last: "Última",
                        more: "Mais",
                        input: "Número da página",
                        select: "Selecionar tamanho da página"
                    },
                    info: 'Exibindo' + ' {{start}} - {{end}} ' + 'de' + ' {{total}} ' + 'resultados'
                },
            }
        }
    },
    data: {
        type: "remote",
        source: {
            read: {
                method: "GET",
                url: api ,
                map: function (t) {
                    var e = t;
                    return void 0 !== t.data && (e = t.data), e
                }
            }
        },
        pageSize: 10,
        serverPaging: !0,
        serverFiltering: !0,
        serverSorting: !0
    },
    layout: {
        theme: "default",
        class: "",
        scroll: !1,
        footer: !1
    },
    sortable: !0,
    pagination: !0,
    toolbar: {
        items: {
            pagination: {
                pageSizeSelect: [10, 20, 30, 50, 100]
            }
        }
    },
    search: {
        input: $("#generalSearch")
    },
    columns: [
        {

            field: "Id",
            title: "#",
            width: 50,
            sortable: !1,
            textAlign: "center",
            selector: { class: "m-checkbox--solid m-checkbox--brand" }
        },
        {
            field: "TipoRelatorio",
            title: "Tipo Relatorio",
        },
        {
            field: "Descricao",
            title: "Descricao",
        },
        {
            field: "Comentario",
            title: "Comentario",
        },
        {
            field: "Acoes",
            title: "Ações",
            width: 50,
            sortable: false,
            overflow: "visible",
            template: function (t, e, a) {
                return '\
                            <div class="dropdown">\
                                <a href="#" class="btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown">\
                                    <i class="la la-ellipsis-h"></i>\
                                </a>\
                                <div class="dropdown-menu dropdown-menu-right">\
                                    <a class="dropdown-item" href="#" onclick="abrirModalAlterar(' + t.Id + ')">\
                                        <i class="la la-edit"></i> Editar\
                                    </a>\
                                    <a class="dropdown-item" href="#" onclick="abrirModalExcluir('+ t.Id + ')">\
                                        <i class="la la-leaf"></i> Excluir\
                                    </a>\
                                </div>\
                            </div>';
            }
        }],
    extensions: {
        checkbox: {
            vars: {
                selectedAllRows: 'selectedAllRows',
                requestIds: 'requestIds',
                rowIds: 'meta.Id',
            },
        },
    }

});
/***************************************************************/