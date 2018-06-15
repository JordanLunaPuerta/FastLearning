window.onload = inicializar;
var fichero;
var storageRef;
var tokenImagenURL;

function inicializar() {
    fichero = document.getElementById("fichero");
    fichero.addEventListener("change", subirImagenAFirebase, false);

    storageRef = firebase.storage().ref();

}

function subirImagenAFirebase() {
    var imagenASubir = fichero.files[0];
    function random() {
        return Math.random().toString(36).substr(2); 
    };

    function token() {
        return random() + random();
    };
    tokenImagenURL = token();
    var uploadTask = storageRef.child('imagenes/' + tokenImagenURL + imagenASubir.name).put(imagenASubir);
    uploadTask.on(firebase.storage.TaskEvent.STATE_CHANGED, 
        function (snapshot) {           
        }, function (error) {           
        }, function() {
            uploadTask.snapshot.ref.getDownloadURL().then(function (downloadURL) {
                //console.log('File available at', downloadURL);
                storageRef.child('imagenes/' + tokenImagenURL + imagenASubir.name).getDownloadURL().then(function (url) {

                    var xhr = new XMLHttpRequest();
                    xhr.responseType = 'blob';
                    xhr.onload = function (event) {
                        var blob = xhr.response;
                    };
                    xhr.open('GET', url);
                    xhr.send();
                    var img = document.getElementById('img');
                    img.src = url;
                    //img.name = url;
                    //var fotoPerfil = document.getElementById('foto');
                    $("#foto").val(img.src);
                }).catch(function (error) {
                });
            });
        });
}

    
