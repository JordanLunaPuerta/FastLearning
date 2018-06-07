$(".btnInscripcionClase").click(function (eve) {
    $("#modal-content").load("/Clase/InscribirseEnClase/" + $(this).data("id"));
});