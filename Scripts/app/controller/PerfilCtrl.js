app.controller("PerfilCtrl", function ($rootScope, $scope, crudService, ControlError, Validaciones, notify) {


    $scope.formulario = {
        metodo:1,
        consulta:0,
        nombre: '',
        apellido: '',
        cedula: '',
        edad: 0,
        idsexo: null,
        fecha_nac: null,
        telofono: '',
        celular: '',
        email: '',
        direccion: '',
        idpais: null,
        iduniversidades: ''
    }
    console.log($scope.formulario)
   
    function Consulta(idconsulta) {

       
        $scope.formulario.consulta = idconsulta;

        var post = crudService.post($scope.formulario, '../api/api');
        post.then(function (pl) {

            if (pl.data[0].exito) {
                $scope.formulario.consulta = 0;
                $scope.formulario.nombre = nonull(pl.data[0].data[0].nombre);
                $scope.formulario.apellido = nonull(pl.data[0].data[0].apellido);
                $scope.formulario.cedula = nonull(pl.data[0].data[0].cedula);
                $scope.formulario.edad = nonull(pl.data[0].data[0].edad);
                $scope.formulario.idsexo = pl.data[0].data[0].idsexo;
                $scope.formulario.fecha =new Date(nonull(Validaciones.ordenfecha(pl.data[0].data[0].fecha_nac)));
                $scope.formulario.fecha_nac = nonull(Validaciones.ordenfecha(pl.data[0].data[0].fecha_nac));
                $scope.formulario.telofono = nonull(pl.data[0].data[0].telofono);
                $scope.formulario.celular = nonull(pl.data[0].data[0].celular);
                $scope.formulario.email = nonull(pl.data[0].data[0].email);
                $scope.formulario.direccion = nonull(pl.data[0].data[0].direccion);
                $scope.formulario.idpais = pl.data[0].data[0].idpais;
                $scope.formulario.iduniversidades = pl.data[0].data[0].iduniversidades;
                $scope.Consulta_uni(pl.data[0].data[0].idpais);
                if (idconsulta == 4) {

                    notify({ message: 'Registro Exitos ya puede entrar a su portafolio. ', position: 'right', classes: 'alert-success' });
                }
            }
            else {
                if (pl.data[0].numero === '0') {
                    $scope.formularios = [];
                }
                else {
                    ControlError.error(pl.data[0].numero)
                }
            }

        },
        function (errorPl) {
        });
    };

    function nonull(a) {
        
        if (a == null || a == undefined) {
            return '';
        }
        else {
            return a;
        }
    }

    function Pais() {
        var parametros = {
            metodo: 2,
            consulta:1
        }

        var post = crudService.post(parametros, '../api/api');
        post.then(function (pl) {

            if (pl.data[0].exito) {
                $scope.pais = pl.data[0].data;

                console.log(pl.data[0].data)
            }
            else {
                if (pl.data[0].numero === '0') {
                    $scope.pais = [];
                }
                else {
                    ControlError.error(pl.data[0].numero)
                }
            }

        },
        function (errorPl) {
        });
    }

    function Sexo() {
        var parametros = {
            metodo: 2,
            consulta: 3
        }

        var post = crudService.post(parametros, '../api/api');
        post.then(function (pl) {

            if (pl.data[0].exito) {
                $scope.sexo = pl.data[0].data;

                console.log(pl.data[0].data)
            }
            else {
                if (pl.data[0].numero === '0') {
                    $scope.sexo = [];
                }
                else {
                    ControlError.error(pl.data[0].numero)
                }
            }

        },
        function (errorPl) {
        });
    }

    $scope.Consulta_uni = function (idpais) {
        var parametros = {
            metodo: 2,
            consulta: 2,
            idpais:idpais
        }

        var post = crudService.post(parametros, '../api/api');
        post.then(function (pl) {

            if (pl.data[0].exito) {
                $scope.universidades = pl.data[0].data;

                console.log(pl.data[0].data)
            }
            else {
                if (pl.data[0].numero === '0') {
                    $scope.universidades = [];
                }
                else {
                    ControlError.error(pl.data[0].numero)
                }
            }

        },
        function (errorPl) {
        });
    }

    $scope.fecha = function (fecha) {
        var  f = new Date(fecha);
        var month = f.getMonth() + 1;
        var day = f.getDate();
        var year = f.getFullYear();

        $scope.formulario.fecha_nac = month + "/" + day + "/" + year;
    }

    $scope.guardar = function (a) {

        console.log(a)
        if (a.nombre.length > 1) {
            if (a.apellido.length > 1) {
                if (a.cedula.length > 1) {
                    if (a.edad > 0) {
                        if (a.idsexo != null || a.idsexo != undefined) {
                            if (true){//(Validaciones.fecha(a.fecha_nac)) {
                                if (a.idpais != null || a.idpais != undefined) {
                                    if (a.direccion.length > 1) {
                                        if (a.telofono.length > 1 || a.celular.length > 1) {
                                            if (a.telofono.length > 1 || a.celular.length > 1) {
                                                if (a.iduniversidades != null || a.iduniversidades != undefined) {
                                                    Consulta(4);
                                                }
                                                else {
                                                    notify({ message: 'seleccione su Universidad por favor', position: 'right', classes: 'alert-warning' });
                                                }
                                            }
                                            else {
                                                notify({ message: 'Intgrese un correo donde pueda ser contactado por favor', position: 'right', classes: 'alert-warning' });
                                            }
                                        }
                                        else {
                                            notify({ message: 'Intgrese un numero donde pueda ser contactado por favor', position: 'right', classes: 'alert-warning' });
                                        }
                                    }
                                    else {
                                        notify({ message: 'Intgrese su Direccion de residencia por favor', position: 'right', classes: 'alert-warning' });
                                    }
                                }
                                else {
                                    notify({ message: 'seleccione su país por favor', position: 'right', classes: 'alert-warning' });
                                }
                        }
                        }
                        else {
                            notify({ message: 'seleccione su sexo por favor', position: 'right', classes: 'alert-warning' });
                        }
                    }
                    else {
                        notify({ message: 'Ingrese su edad por favor', position: 'right', classes: 'alert-warning' });
                    }
                }
                else {
                    notify({ message: 'Ingrese su cedula por favor', position: 'right', classes: 'alert-warning' });
                }
            }
            else {
                notify({ message: 'Ingrese su apellido por favor', position: 'right', classes: 'alert-warning' });
            }
        }
        else {
            notify({ message: 'Ingrese su nombre por favor', position: 'right', classes: 'alert-warning' });
        }
    }
    Consulta(3);
    Pais();
    Sexo();
});