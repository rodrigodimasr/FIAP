(function () {
    "use strict";

    angular.module('fiap').controller('clienteDetailController', controller);
    controller.$inject = ['$scope', '$state', '$stateParams', '$window', 'clienteService', '$confirm', 'toastr'];

    function controller($scope, $state, $stateParams, $window, clienteService, $confirm, toastr) {
        //[Inicializa objetos]
        $scope.toastr = toastr;
        $scope.items = {
            auxiliar: [{ ID: 0, Nome: "Celular" },
            { ID: 1, Nome: "Comercial" },
            { ID: 2, Nome: "Residencial" },
            { ID: 3, Nome: "Recado" }],
            data: { Codigo: decodeURIComponent($stateParams.id), CNPJMascara: '' },
            carregando: false,
            vazio: false,
            erro: {
                ativo: false,
                mensagem: ''
            }
        };

        //[Inicializa funcões]
        $scope.initialize = function () {
            $scope.refresh();
        };
        $scope.historyBack = function () {
            $window.history.back();
        }
        $scope.get = function () {
            debugger;
            $scope.items.carregando = true;
            $scope.items.vazio = false;
            var codigo = decodeURIComponent($scope.items.data.Codigo);
            clienteService.get(codigo)
                .then(
                    function (response) {
                        debugger;
                        if (response.Status < 0)
                            $scope.toastr.error(response.Message, 'Serviço');
                        else {
                            $scope.items.data = response.Data;
                            if (response.Data.CPF && response.Data.CPF != "")
                                response.Data.CPF = response.Data.CPF.trim().replace(".", "").replace(".", "").replace("-", "");

                        }

                        $scope.items.carregando = false;
                        $scope.items.vazio = ($scope.items.data == 0);
                    },
                    function (err) {
                        $scope.toastr.error(err);
                        $scope.items.carregando = false;
                        $scope.items.vazio = true;
                    }
                );
        };

        $scope.save = function () {


            clienteService
                .save($scope.items.data)
                .then(
                    function (response) {
                        if (response.Status < 0) {
                            $scope.toastr.error(response.Message, 'Serviço');
                        }
                        else {
                            $scope.toastr.success("Cliente salvo com sucesso");
                        }

                    },
                    function (error) {
                        $scope.toastr.error('Ao salvar o cliente', 'Comunicação com o servidor');
                        console.error('Ao salvar o cliente', error);
                    }
                );

        }

        $scope.refresh = function () {
            $scope.get();
        };


        $scope.submit = function () {
            ;
            $scope.redirect = true;
            $scope.save();
        };

        $scope.validaCpf = function (cpf) {
            if ($state.current.name == "state-cliente-detail") {
                return;
            }
            clienteService.validaCpf($scope.items.data.CPFMascara).then(
                function (response) {
                    $scope.items.data.CpfInvalido = !response.Data;

                    if (!response.Data) {
                        $scope.toastr.error("CPF inválido");
                    }
                });
        }

        $scope.update = function () {
            clienteService
                .update($scope.items.data)
                .then(
                    function (response) {
                        if (response.Status < 0) {
                            $scope.toastr.error(response.Message, 'Serviço');
                        }
                        else {
                            $scope.toastr.success("Cliente alterado com sucesso!");
                            $state.go("state-home");
                        }

                    },
                    function (error) {
                        $scope.toastr.error('Ao salvar o cliente', 'Comunicação com o servidor');
                        console.error('Ao salvar o cliente', error);
                    }
                );
        };
        $scope.delete = function () {
            debugger;
            clienteService
                .delete($scope.items.data.Codigo)
                .then(
                    function (response) {
                        if (response.Status < 0) {
                            $scope.toastr.error(response.Message, 'Serviço');
                        }
                        else {
                            $scope.toastr.success("Cliente deletadp com sucesso!");
                            $state.go("state-home");
                        }

                    },
                    function (error) {
                        $scope.toastr.error('Ao salvar o cliente', 'Comunicação com o servidor');
                        console.error('Ao salvar o cliente', error);
                    }
                );
        };

        /*Inicializa funções*/
        $scope.initialize();
    }

})();