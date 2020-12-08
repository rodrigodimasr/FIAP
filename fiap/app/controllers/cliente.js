(function () {
    "use strict";
    debugger;
    angular.module('fiap').controller('clienteController', controller);
    controller.$inject = ['$scope', '$state', 'clienteService', 'toastr'];

    function controller($scope, $state, clienteService, toastr) {
        debugger;
        //[Inicializa objetos]
        $scope.toastr = toastr;
        $scope.state = {
            data: [],
            carregando: false,
            filtro: {
                skip: 0,
                take: 20
            }
        };

        $scope.initialize = function () {
            $scope.refresh();
        };

        $scope.refresh = function () {
            $scope.getAll();
        };

        $scope.getAll = function () {
            debugger;
            $scope.state.carregando = !$scope.state.carregando;
            $scope.state.filtro.skip = 0;
            clienteService.getAll($scope.state.filtro)
                .then(
                    function (response) {
                        debugger;
                        if (response.Status < 0) {
                            $scope.toastr.error(response.Message, "Serviço");
                            $scope.state.carregando = !$scope.state.carregando;
                        }else {
                            angular.copy(angular.fromJson(response.Data), $scope.state.data);
                            $scope.state.carregando = !$scope.state.carregando;
                            $scope.state.vazio = ($scope.state.data.length == 0);
                        }
                    },
                    function (err) { $scope.toastr.error(err); }
                );

        };

        $scope.AddCliente = function () {
            debugger;
            console.log('teste');
        }

        $scope.initialize();
    }

})();