app.controller("JuradoCtrl", function ($rootScope, $scope, crudService, ControlError, Validaciones, notify, $timeout) {

    $scope.info = {
        consulta: 0,
        metodo: 3,
        idgaleria: 0,
        nombre: '',
        descripcion: '',
        nivel: 0,
    }
    $scope.c_muestra = 12;
    $scope.paginacionNav = [];
    $scope.imagenes = [];
    if (localStorage.nivel == undefined) {
        localStorage.nivel = 1;
    }
    if (localStorage.pagina == undefined) {
        localStorage.pagina = 1;
    }
    function consulta(idconsulta) {

        $scope.info.consulta = idconsulta;
        var post = crudService.post($scope.info, '../api/api');
        post.then(function (pl) {

            if (pl.data[0].exito) {
                $scope.galeria = encapsula(pl.data[0].data,localStorage.nivel);
                

                var img = new Image();
                for (var i in $scope.galeria) {    
                    for (var e in $scope.galeria[i].data)
                        var a = $scope.galeria[i].data[e];
                    img.src = '../GALERIA/' + a.idconcurso + '/' + a.idparticipante + '/' + a.nombre + a.extencion;
                }
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

    function encapsula(datos, nivel_activo) {
        var retorno = [];
        var existe = false;
        var index = 0;
        for (var i in datos) {

            if (retorno.length == 0) {
                retorno.push({
                    id: datos[i].nivel,
                    data: [datos[i]]
                })
            }
            else {
                existe = false;
                index = 0;
                for (var e in retorno) {
                    if (retorno[e].id == datos[i].nivel) {
                        existe = true; index = e;
                    }
                }

                if (existe) {
                    retorno[index].data.push(datos[i]);
                }
                else {
                    retorno.push({
                        id: datos[i].nivel,
                        data: [datos[i]]
                    })
                }
            }
        }
        var entro = false;
        for (var g in retorno) {
            if (retorno[g].id == localStorage.nivel) {
                retorno[g].color = 'w3-green';
                entro = true;
                $scope.cambio_nivel(retorno[g])

            }
        }
        if (!entro) {
            retorno[retorno.length - 1].color = 'w3-green';
            $scope.cambio_nivel(retorno[retorno.length - 1])
        }
        return retorno;
    }

    $scope.cambio_nivel = function (datos) {
        var cantidad = datos.data.length;
        var sobra = cantidad;
        if (datos.id != localStorage.nivel) { localStorage.pagina = 1 }
        localStorage.nivel = datos.id;
        $scope.paginacionNav = [];
        var contador = 1;
        while (sobra > 0) {
            $scope.paginacionNav.push({ num: contador, estado: '' });
            contador++;
            sobra = sobra - $scope.c_muestra;
        }

        for (var i in $scope.galeria) {
            if ($scope.galeria[i].id == datos.id) {
                $scope.galeria[i].color = 'w3-green';
            }
            else {
                $scope.galeria[i].color = '';
            }
        }
        $scope.imagenes = datos.data;
        
        $scope.vista_imagen(localStorage.pagina);
    }

    $scope.edita_nivel = function (nivel,datos) {
        $scope.info.nivel = nivel;
        $scope.info.idgaleria = datos.idgaleria;

        consulta(7)
    }
   
    consulta(6);

    $scope.paginacion = [];

    $scope.vista_imagen = function (a) {
        localStorage.pagina = a;
        //coloreo pa posicion en donde estoy actalmente
        for (var i in $scope.paginacionNav) {
            $scope.paginacionNav[i].estado = '';
            if ($scope.paginacionNav[i].num == a) {
                $scope.paginacionNav[i].estado = ' w3-green';
            }
        }
        // indico cual es el punto de partida para contar las imagenes
        var inicio = ((a * $scope.c_muestra) - $scope.c_muestra);

        $scope.paginacion = [];

        for (var i = 0; i < $scope.c_muestra; i++) {
            if (($scope.imagenes.length - 1) >= inicio)
            $scope.paginacion.push($scope.imagenes[inicio]);
            inicio++;
        }
    }

    $scope.next = function () {
        var long = $scope.paginacionNav.length;
        var a = 0;

        for (var i in $scope.paginacionNav) {
            
            if ($scope.paginacionNav[i].estado.length > 2) {
                a = $scope.paginacionNav[i].num;
            }
        }


        if (long == a) {
            $scope.vista_imagen(1);
        }
        else {
            $scope.vista_imagen(a + 1);
        }

    }

    $scope.back = function () {
        var long = $scope.paginacionNav.length;
        var a = 0;

        for (var i in $scope.paginacionNav) {

            if ($scope.paginacionNav[i].estado.length > 2) {
                a = $scope.paginacionNav[i].num;
            }
        }


        if (a == 1) {
            $scope.vista_imagen(long);
        }
        else {
            $scope.vista_imagen(a - 1);
        }

    }


    $scope.img_modal = '';
    $scope.modal_puntero = 0;
    $scope.modal = function (a) {
        $scope.modal_puntero = a;
        $scope.img_modal = $scope.paginacion[a].idconcurso + '/' + $scope.paginacion[a].idparticipante + '/' + $scope.paginacion[a].nombre + $scope.paginacion[a].extencion;
        document.getElementById('id01').style.display = "block";
    }

    $scope.modal_mov = function (a) {
        a = $scope.modal_puntero + (a);
        $scope.modal_puntero = a;

        if (a == -1) {
            a = $scope.paginacion.length - 1;
            $scope.modal_puntero = a;
        }
        else if (a > ($scope.paginacion.length - 1)) {
            a = 0;
            $scope.modal_puntero = a;
        }

        console.log(a)
        $scope.img_modal = $scope.paginacion[a].idconcurso + '/' + $scope.paginacion[a].idparticipante + '/' + $scope.paginacion[a].nombre + $scope.paginacion[a].extencion;
    }
    $scope.imgInfo = [];
    $scope.info_imagen = function (a) {
        $scope.imgInfo = [];
        var img = new Image();        
        img.src = '../GALERIA/' + a.idconcurso + '/' + a.idparticipante + '/' + a.nombre + a.extencion;
       
        a.w = img.width;
        a.h = img.height;
        a.img = '../GALERIA/' + a.idconcurso + '/' + a.idparticipante + '/' + a.nombre + a.extencion;
        $scope.imgInfo = a;
        
        document.getElementById('info').style.display = 'block'
    }
});
