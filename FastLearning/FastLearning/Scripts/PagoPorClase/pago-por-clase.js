$(".btnPago").click(function (eve) {
    $("#modal-content-1").load("/Pago/PagoPorClase/" + $(this).data("id"));
});