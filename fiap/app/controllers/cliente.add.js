(function () {
    "use strict";

    angular.module('fiap').controller('clienteAddController', controller);

    controller.$inject = ['$scope', '$state', 'toastr', 'clienteService'];

    function controller($scope, $state, toastr, clienteService) {

        $scope.toastr = toastr;
        //[Inicializa objetos]
        $scope.items = {
            auxiliar: [{ ID: 0, Nome: "Celular" },
            { ID: 1, Nome: "Comercial" },
            { ID: 2, Nome: "Residencial" },
            { ID: 3, Nome: "Recado" }],
            data: {},
            carregando: false,
            vazio: false,
            erro: {
                ativo: false,
                mensagem: ''
            },
        };
        $scope.campoInvalido = function (campo) {

            var isInvalid = function (value) {
                var invalid = (value == null || value == "");
                return invalid;
            }

            switch (campo) {
                case 'email':
                    var value = clienteForm.email.value;
                    return isInvalid(value);
                    break;
                //case 'telefoneDDD':
                //    var value = clienteForm.telefoneDDD.value;
                //    return isInvalid(value);
                //    break;
                //case 'telefoneNumero':
                //    var value = clienteForm.telefoneNumero.value;
                //    return isInvalid(value);
                //    break;
                default:
                    return false;
            }
        }

        $scope.validateEmail = function (email) {
            debugger;
            var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
            if (reg.test(email)) {
                return true;
            }
            else {
                $scope.toastr.error("E-mail inválido!");
                var value = clienteForm.email.value;
                return isInvalid(value);
            }
        } 

        $scope.initialize = function () {
            $scope.refresh();
        };

        $scope.submit = function () {
            $scope.redirect = true;
            $scope.save();
        };

        $scope.save = function () {
            if (!$scope.validateEmail($scope.items.data.Email))
                return;
            debugger;
            clienteService.save($scope.items.data)
                .then(
                    function (response) {
                        debugger;
                        if (response.Status < 0)
                            $scope.toastr.error(response.Message, 'Serviço');
                        else {
                            $scope.toastr.success("Cliente salvo com sucesso");
                            $state.go("state-home");
                        }

                    },
                    function (err) { $scope.toastr.error(err); }
                );
        }


        $scope.validaCpf = function (cpf) {
            clienteService.validaCpf(cpf).then(
                function (response) {
                    $scope.items.data.CpfInvalido = !response.Data;
                    if (!response.Data) {
                        $scope.toastr.error("CPF inválido");
                    }
                });
        }



        $scope.refresh = function () {

        };

        /*Inicializa funções*/
        $scope.initialize();
    }
})();