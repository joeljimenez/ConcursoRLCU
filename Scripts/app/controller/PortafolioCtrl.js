app.controller("PortafolioCtrl", function ($rootScope, $scope, crudService, ControlError, Validaciones, notify, $timeout) {

    $scope.carga = 0;
    $scope.info = {
        consulta: 0,
        metodo:3,
        idcategoria: null,
        nombre: '',
        descripcion: '',
        extencion:''
    }

    $scope.edit_foto = {
        consulta: 0,
        metodo: 4,
        idgaleria: null,
        nombre: '',
        nombre2: '',
        descripcion: '',
        extencion: ''
    }

    function consulta(idconsulta) {
      
        $scope.info.consulta = idconsulta;

        var post = crudService.post($scope.info, '../api/api');
        post.then(function (pl) {

            if (pl.data[0].exito) {
                $scope.galeria = encapsula(pl.data[0].data);
            }
            else {
                if (pl.data[0].numero === '0') {
                    $scope.galeria = [];
                }
                else {
                    ControlError.error(pl.data[0].numero)
                }
            }

        },
        function (errorPl) {
        });
    }

    function encapsula(a) {

        var data = [];
        for (var i in $scope.categorias) {
            $scope.categorias[i].data = [];

            for (var j in a) {
                if (a[j].idcategoria == $scope.categorias[i].idcategoria) {
                    $scope.categorias[i].data.push(a[j]);
                }
            }

            if ($scope.categorias[i].data.length > 0) {
                data.push($scope.categorias[i]);
            }

         
        }

        return data;
    }

    function Categoria() {
        var parametros = {
            metodo: 2,
            consulta: 4
        }

        var post = crudService.post(parametros, '../api/api');
        post.then(function (pl) {

            if (pl.data[0].exito) {
                $scope.categorias = pl.data[0].data;
            }
            else {
                if (pl.data[0].numero === '0') {
                    $scope.categorias = [];
                }
                else {
                    ControlError.error(pl.data[0].numero)
                }
            }

        },
        function (errorPl) {
        });
    }

    $scope.update_file = function () {

        var files = $("#inputFile").get(0).files;
        $scope.lista_archivo = [];

        if (files.length > 0) {
            time = true;
            $scope.info.nombre = files[0].name.split('.')[0]
            $scope.info.extencion = (files[0].name.substring(files[0].name.lastIndexOf('.'))).toLowerCase();
            console.log($scope.info)
        } else {
            time = false;
        }

        update();

    }

    var update = function () {
        $timeout(function () {

            $scope.info;
            $scope.carga;
        }, 100);
    }

    $scope.loadarchivo = function () {


        var files = $("#inputFile").get(0).files;



        // captura archivos seleccionasdos
        if ($scope.info.extencion == '.jpg') { //|| $scope.info.extencion == '.jpeg' || $scope.info.extencion == '.png' || $scope.info.extencion == '.rar'
            if ($scope.info.idcategoria != null || $scope.info.idcategoria != undefined) {
                if (files.length > 0) {

                    $rootScope.loading_progress = true;

                    $scope.cant_arch = 0;
                    $scope.cant_arch_env = files.length;

                    for (i = 0; i < files.length; i++) {


                        var data = new FormData();

                        //var nombre = $scope.lista_archivo[i].nombre;
                        //var extencion = (files[i].name.substring(files[i].name.lastIndexOf('.'))).toLowerCase();

                        var nombre_archivo = $scope.info.nombre + '°' + $scope.info.extencion + '°' + $scope.info.descripcion + '°' + $scope.info.idcategoria;

                        data.append("file", files[i], nombre_archivo);


                        var megas = files[i].size / (1024 * 1024);

                        if (megas <= 50) {

                            $.ajax({
                                type: "POST",
                                url: "../api/apifilerecolector",
                                contentType: false,
                                processData: false,
                                data: data,
                                xhr: function () {
                                    var xhr = $.ajaxSettings.xhr();
                                    xhr.upload.onprogress = function (e) {
                                        $scope.carga = Math.floor(e.loaded / e.total * 100);

                                        update();
                                    };
                                    return xhr;
                                },

                                success: function (pl) {

                                    if (pl[0].exito) {
                                        $scope.carga = 0;
                                        $scope.galeria = encapsula(pl[0].data);
                                        notify({ message: "Carga completa", position: 'right', classes: 'alert-warning' });

                                    }
                                    else {
                                        if (pl[0].numero == 0) {
                                            notify({ message: pl[0].mensaje, position: 'right', classes: 'alert-warning' });
                                            $scope.carga = 0;
                                        }
                                        else {

                                            ControlError.error(pl[0].numero)
                                        }
                                    }
                                    update();
                                },

                                error: function (errorThrown) {

                                    notify({ message: "Se presentó un error  desconocido al subir el archivo, consulte a tecnología", position: 'right', classes: 'alert-warning' });
                                    console.log(errorThrown)
                                }
                            });
                        } else {
                            notify({ message: "El archivo es muy pesado para subirlo, máximo permitido 50Mb. Contacte al departamento sistemas para que le ayuden con el tema.", position: 'right', classes: 'alert-danger' });


                        }

                    }

                    $("#inputFile").val('');
                    limpia_info();

                } else {
                    notify({ message: "Seleccione el archivo de la evidencia", position: 'right', classes: 'alert-warning' });
                }
            }
            else {
                notify({ message: "Seleccione la categoria de la imagen", position: 'right', classes: 'alert-warning' });
            }
        }
        else {
            notify({ message: "Solo se aceptan los sigientes formatos: .jpg, .jpeg, .png", position: 'right', classes: 'alert-warning' });
        }
        //fin ajax
    };

    $scope.editar = function (a) {
        $scope.edit_foto.consulta = 4;
        $scope.edit_foto.nombre = a.nombre;
        $scope.edit_foto.nombre2 = a.nombre;
        $scope.edit_foto.idgaleria = a.idgaleria;
        $scope.edit_foto.descripcion = a.descripcion;
        $scope.edit_foto.extencion = a.extencion;
    }

    $scope.eliminar = function (a) {
        $scope.edit_foto.consulta = 5;
        $scope.edit_foto.nombre = a.nombre;
        $scope.edit_foto.nombre2 = a.nombre;
        $scope.edit_foto.idgaleria = a.idgaleria;
        $scope.edit_foto.descripcion = a.descripcion;
        $scope.edit_foto.extencion = a.extencion;
    }

    $scope.guardar = function (a) {
       

        var post = crudService.post(a, '../api/api');

        post.then(function (pl) {

            if (pl.data[0].exito) {
                $scope.galeria = encapsula(pl.data[0].data);
            }
            else {
                if (pl.data[0].numero === '0') {
                    $scope.galeria = [];
                }
                else {
                    ControlError.error(pl.data[0].numero)
                }
            }

            if (a.consulta == 4) { document.getElementById('edit').style.display = 'none' }
            if (a.consulta == 5) { document.getElementById('delete').style.display = 'none' }

        },
        function (errorPl) {
        });
    }
    function limpia_info() {

        $scope.info = {
            consulta: 0,
            metodo: 3,
            idcategoria: null,
            nombre: '',
            descripcion: '',
            extencion: ''
        }
    }


    Categoria();
    consulta(1);
});