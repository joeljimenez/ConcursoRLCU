
app.service('crudService', function ($http) {

    this.post = function (datos, url) {
        var request = $http({
            method: "post",
            url: url,
            data: datos
        });
        return request;
    };

    this.get = function (url, id) {
        return $http.get(url + id);
    };

    this.consulta = function (url) {
        return $http.get(url);
    };

    this.delete = function (id, datos, url) {

        var request = $http({
            method: "delete",
            url: url + id,
            data: datos,
            headers: { 'contene-type': 'application/json' }
        });
        return request;
    };

    this.put = function (id, datos, url) {
        var request = $http({
            method: "put",
            url: url + id,
            data: datos,
            headers: { 'contene-type': 'application/json' }
        });
        return request;
    };

});

app.service('Validaciones', function (notify) {

    //Create new record
    this.monto = function (monto, decimales) {
        var respuesta = '';
      
        if (monto != null && monto != 0 && monto != '' && monto != undefined) {


            var sep;
            if (monto.toString().lastIndexOf('.') >= 1) {
                sep = monto.split('.');
            } else {
                sep = [monto.toString()]
            }


            
            var number = sep[0].split('');

            for (var i in number) {

                if (!isNaN(number[i])) {
                    respuesta = respuesta + '' + number[i];
                }
            }
            if (decimales > 0) {
                if (sep.length == 2) {
                    respuesta = respuesta + ".";
                    var decimal = sep[1].split('');

                    for (var i in decimal) {

                        if (!isNaN(decimal[i]) && i <= decimales - 1) {
                            respuesta = respuesta + '' + decimal[i];
                        }
                    }
                }
            }

        }
        return respuesta;
    };

    this.fecha = function (fecha) {
        var res = false;
        if (fecha != null && fecha != '') {
            

            if (fecha.length > 0) {
                var cadenas = fecha.split('/');

                if (cadenas.length == 3) {
                    if (cadenas[0] <= 12 && cadenas[1] <= 31 && cadenas[2].length == 4) {
                        var res = true;
                    }
                    else {
                        notify({ message: 'Fecha Incorrecta asegure que tenga este formato Mes/Dia/Año', position: 'right', classes: 'alert-warning' });
                        
                    }
                } else {
                    notify({ message: 'Digite la fecha con el formato solicitado  Mes/Dia/Año', position: 'right', classes: 'alert-warning' });
                    
                }
            }
            else {
                notify({ message: 'Digite la fecha con el formato solicitado  Mes/Dia/Año', position: 'right', classes: 'alert-warning' });
                
            }

        } else {
            notify({ message: 'Digite la fecha con el formato solicitado  Mes/Dia/Año', position: 'right', classes: 'alert-warning' });
        }
        return res;

    };

    this.ordenfecha = function (fecha) {
       
        if (fecha != null && fecha != '') {

            var dato = fecha.split("T");

            if (dato.length == 2) {
                 fecha = dato[0].split("-");

                 fecha = fecha[1] + '/' + fecha[2] + '/' + fecha[0];  
            }
        }
        return fecha;

    };

    this.checkbox = function (estado) {
       

        var retorno = 0;
        if (estado == true) {
            retorno = 1;
        }

       
        return retorno;

    };

    this.ceros_izquierda = function (valor, cantidad) {
        /*concatena ceros a la izquierda*/
        var respuesta = valor.toString();

        cantidad = cantidad - respuesta.length;

        for (var i = 0; i < cantidad; i++) {
            respuesta = '0' + respuesta;
        }

        return respuesta;

    };

    this.diseño = function (val) {
        return retorno = '';
        if (val != null && val != 0 && val != '') {

        }
        return respuesta;

    };

});

app.service('ControlError', function (crudService, notify, $timeout) {
    this.error = function (numero) {
        if (numero == 1062) {
            notify({ message: 'Este Registro ya existe.', position: 'right', classes: 'alert-warning' });
        }
        else if (numero == 1451) {
            notify({ message: 'Este Registro no puedo ser eliminado porque tiene dependencias.', position: 'right', classes: 'alert-warning' });       
        }
        else if (numero == 1136) {
            notify({ message: 'La cantidad de columnas no concuerdan en el registro. Notifique al departamento de tecnologia.', position: 'right', classes: 'alert-danger' });          
        }
        else if (numero == -1) {
            notify({ message: 'Lo sentimos su sesion a expirado.', position: 'left', classes: 'alert-info' });
            $timeout(function () {

                window.location.href = "/login";

            }, 3000);
           
        }
        else if (numero == -2) {
            notify({ message: 'Error al subir Archivo', position: 'right', classes: 'alert-warning' });
        }
        else if (numero == -3) {
            notify({ message: 'Error al eliminar Archivo', position: 'right', classes: 'alert-warning' });
        }
        else if (numero == -4) {
            notify({ message: 'Error al reenombrar Archivo', position: 'right', classes: 'alert-warning' });
        }
        else if (numero == -5) {
            notify({ message: 'El archivo a renombrar no existe', position: 'right', classes: 'alert-warning' });
        }
        else if (numero == -6) {
            notify({ message: 'Este archivo ya existe. no se puede reenombrar', position: 'right', classes: 'alert-warning' });
        }
        else if (numero == -7) {
            notify({ message: 'Este archivo ya existe en esta pieza. no se puede guardar', position: 'right', classes: 'alert-warning' });
        }
        else {
            notify({ message: 'Evento no registrado', position: 'right', classes: 'alert-danger' });
            
        }
    };
});