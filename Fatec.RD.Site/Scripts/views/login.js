console.log("teste");
var formulario = $("#FormLogin");

var login = $("#login-email");
var senha = $("#login-password");

var loginValido = "admin";
var senhaValida = "Admin";

formulario.validate({
    rules: {
        login: { required: true },
        senha: { required: true },
    },
    messages: {
        login: { required: "Campo obrigatório." },
        senha: { required: "Campo obrigatório." }
    },
    submitHandler: function (form) {
        fazerLogin();
    }
});

function fazerLogin() {
    if (login.val().toLowerCase() == loginValido && senha.val() == senhaValida) {
        window.location = "/Despesas";
    }
    else
        alert("Login ou senha inválido");
}