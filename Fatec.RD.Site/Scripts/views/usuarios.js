var api = "";
var formulario = $("#form-usuarios");
var body = $("#modal-generic");
var bodyModal = $("#modal-generic .modal-body");
var titleModal = $("#modal-title");
var table = $(".m_datatable");

listarTabela();

function novoUsuario() {
    titleModal.html("Adicionar Usuário");
    bodyModal.load("Usuarios/Criar");
    body.modal('show');
}

function inserir() {

}

function alterar() {

}

function excluir() {

}

function selecionarPorId() {
    return $.ajax({
        type: "GET",
        url: api + "/"
    });
}

function selecionarTodos() {

}




function listarTabela() {
    var t = table.mDatatable({
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
                    url: "inc/api/datatables/demos/default.php",
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
                field: "RecordID",
                title: "#",
                sortable: !1,
                width: 40,
                selector: !1,
                textAlign: "center"
            },
            {
                field: "OrderID",
                title: "Order ID",
                filterable: !1,
                width: 150,
                template: "{{OrderID}} - {{ShipCountry}}"
            },
            {
                field: "ShipCountry",
                title: "Ship Country",
                width: 150,
                template: function (t) {
                    return t.ShipCountry + " - " + t.ShipCity
                }
            },
            {
                field: "ShipCity",
                title: "Ship City"
            },
            {
                field: "Currency",
                title: "Currency", width: 100
            },
            {
                field: "ShipDate",
                title: "Ship Date",
                sortable: "asc",
                type: "date",
                format: "MM/DD/YYYY"
            },
            {
                field: "Latitude",
                title: "Latitude",
                type: "number"
            },
            {
                field: "Status",
                title: "Status",
                template: function (t) {
                    var e = {
                        1: { title: "Pending", class: "m-badge--brand" },
                        2: { title: "Delivered", class: " m-badge--metal" },
                        3: { title: "Canceled", class: " m-badge--primary" },
                        4: { title: "Success", class: " m-badge--success" },
                        5: { title: "Info", class: " m-badge--info" },
                        6: { title: "Danger", class: " m-badge--danger" },
                        7: { title: "Warning", class: " m-badge--warning" }
                    };
                    return '<span class="m-badge ' + e[t.Status].class + ' m-badge--wide">' + e[t.Status].title + "</span>"
                }
            },
            {
                field: "Type",
                title: "Type",
                template: function (t) {
                    var e = {
                        1: { title: "Online", state: "danger" },
                        2: { title: "Retail", state: "primary" },
                        3: { title: "Direct", state: "accent" }
                    };
                    return '<span class="m-badge m-badge--' + e[t.Type].state + ' m-badge--dot"></span>&nbsp;<span class="m--font-bold m--font-' + e[t.Type].state + '">' + e[t.Type].title + "</span>"
                }
            },
            {
                field: "Actions",
                width: 110,
                title: "Actions",
                sortable: !1,
                overflow: "visible",
                template: function (t, e, a) {
                    return '\
                            <div class="dropdown ' + (a.getPageSize() - e <= 4 ? "dropup" : "") + '">\
                                <a href="#" class="btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown">\
                                    <i class="la la-ellipsis-h"></i>\
                                </a>\
                                <div class="dropdown-menu dropdown-menu-right">\
                                    <a class="dropdown-item" href="#">\
                                        <i class="la la-edit"></i> Edit Details\
                                    </a>\
                                    <a class="dropdown-item" href="#">\
                                        <i class="la la-leaf"></i> Update Status\
                                    </a>\
                                    <a class="dropdown-item" href="#">\
                                        <i class="la la-print"></i> Generate Report\
                                    </a>\
                                </div>\
                                <a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" title="Edit details">\
                                    <i class="la la-edit"></i>\
                                </a>\
                                <a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Delete">\
                                    <i class="la la-trash"></i>\
                                </a>\
                            </div>';
                }
            }]
    }),
    e = t.getDataSourceQuery();
    $("#m_form_status").on("change", function () {
        var e = t.getDataSourceQuery();
        e.Status = $(this).val().toLowerCase(),
        t.setDataSourceQuery(e),
        t.load()
    }).val(void 0 !== e.Status ? e.Status : ""),
    $("#m_form_type").on("change", function () {
        var e = t.getDataSourceQuery();
        e.Type = $(this).val().toLowerCase(),
        t.setDataSourceQuery(e),
        t.load()
    }).val(void 0 !== e.Type ? e.Type : ""),
    $("#m_form_status, #m_form_type").selectpicker();
}
