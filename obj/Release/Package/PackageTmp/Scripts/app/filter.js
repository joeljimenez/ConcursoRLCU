app.filter("traductor", function () {
    return function (text) {
        var retorno = text;
        if (text != null && localStorage.idioma == 'es') {

            var DT = text.toUpperCase();


            switch (DT) {
                case "E-MAIL":
                    retorno = "Correo";
                    break;
                case "PASSWORD":
                    retorno = "Contraseña";
                    break;
                case "LOGIN":
                    retorno = "Entrar";
                    break;
                case "SAVE":
                    retorno = "Guardar";
                    break;
                default:
                    retorno = text;
            }

        }
        return retorno;

    }
});

app.filter("ordenfecha", function (LetraMes) {
    return function (text) {
        if (text != null) {
            var dato = text.split("T");

            
            if (dato.length == 2) {
                var fecha = dato[0].split("-");

                if (fecha.length == 3) {
                    return LetraMes.convert(fecha[1]) + ' ' + fecha[2] + ' ' + fecha[0];
                }
                else {
                    return text;
                }
            }
            else {
                return text;
            }
            
        }


       
    }
});

app.service('LetraMes', function () {
    this.convert = function (text) {
        var retorno;
        switch (text) {
            case "01":
                retorno = "ENE";
                break;
            case "02":
                retorno = "FBR";
                break;
            case "03":
                retorno = "MAR";
                break;
            case "04":
                retorno = "ABR";
                break;
            case "05":
                retorno = "MAY";
                break;
            case "06":
                retorno = "JUN";
                break;
            case "07":
                retorno = "JUL";
                break;
            case "08":
                retorno = "AGO";
                break;
            case "09":
                retorno = "SEP";
                break;
            case "1":
                retorno = "ENE";
                break;
            case "2":
                retorno = "FBR";
                break;
            case "3":
                retorno = "MAR";
                break;
            case "4":
                retorno = "ABR";
                break;
            case "5":
                retorno = "MAY";
                break;
            case "6":
                retorno = "JUN";
                break;
            case "7":
                retorno = "JUL";
                break;
            case "8":
                retorno = "AGO";
                break;
            case "9":
                retorno = "SEP";
                break;
            case "10":
                retorno = "OCT";
                break;
            case "11":
                retorno = "NOV";
                break;
            case "12":
                retorno = "DIC";
                break;
            default:
                retorno = text;
        }
        return retorno;
    }
});

app.filter("maxLength", function () {
    return function (text, max) {
        if (text != null) {
            if (text.length > max) {
                return text.substring(0, max) + "";
            } else {
                return text;
            }
        }
    }
});

app.filter("monto", function () {
    return function (text, decimal) {
        if (text != null) {
            
            var sep = text.split('.');

            var number = sep[0].split('');
            text = '';
            for (var i in number) {
                console.log(isNaN(number[i]) + '  ' + number[i])
                if (!isNaN(number[i])) {
                    text = text + '' + number[i];
                }
            }

            return text;

        }
    }
});

app.filter("htmlconvert", ['$sce', function ($sce) {
    return function(htmlCode){
        return $sce.trustAsHtml(htmlCode);
    }
}])

app.filter("promedio", function () {
    return function (array, campo) {
        var suma = 0;
        var cantida = array.length;

        for (var i in array) {
            suma = suma + array[i][campo];
        }

        return Math.round(suma / cantida);
    }
});
