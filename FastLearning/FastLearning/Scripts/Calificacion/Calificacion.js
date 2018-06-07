$(".btnCalificarProfesor").click(function (eve) {
    $("#modal-content").load("/Clase/CalificarProfesor/" + $(this).data("id"));
});

$(".btnCalificarAlumnos").click(function (eve) {
    $("#modal-content").load("/Clase/CalificarAlumnos/" + $(this).data("id"));
});