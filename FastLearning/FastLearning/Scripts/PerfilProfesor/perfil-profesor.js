$(".perfilProfesor").click(function (eve) {
    $("#modal-content-2").load("/Usuario/PerfilProfesor/" + $(this).data("id"));
});