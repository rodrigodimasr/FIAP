(function () {
    "use strict";

    angular.module('fiap').factory('clienteService', factory);
    factory.$inject = ['$http', '$q'];

    function factory($http, $q) {

        var _getAll = function (filtro) {
            var deferred = $q.defer();
            var args = {
                'skip': filtro.skip,
                'take': filtro.take
            };

            $http.post('/cliente/GetAll', args)
                .success(function (data) { deferred.resolve(data); })
                .error(function (data, status, headers, config) { deferred.reject(data); });
            return deferred.promise;
        };

        var _get = function (codigo) {
            debugger;
            var deferred = $q.defer();
            var args = { 'codigocliente': codigo }
            $http.post('/cliente/get', args)
                .success(function (data) { deferred.resolve(data); })
                .error(function (data, status, headers, config) { deferred.reject(data); });
            return deferred.promise;
        };

        var _validaCpf = function (cpf) {
            var deferred = $q.defer();
            var args = { 'cpf': cpf };
            $http.post('/cliente/ValidaCpf', args)
                .success(function (data) { deferred.resolve(data); })
                .error(function (data, status, headers, config) { deferred.reject(data); });
            return deferred.promise;
        };

        var _save = function (item) {
            var deferred = $q.defer();
            var args = {
                item: item
            };
            $http.post('cliente/save', args)
                .success(function (data) { deferred.resolve(data); })
                .error(function (data, status, headers, config) { deferred.reject(data); });
            return deferred.promise;
        }
        var _update = function (item) {
            var deferred = $q.defer();
            var args = {
                item: item
            };
            $http.post('cliente/update', args)
                .success(function (data) { deferred.resolve(data); })
                .error(function (data, status, headers, config) { deferred.reject(data); });
            return deferred.promise;
        };

        var _delete = function (item) {
            var deferred = $q.defer();
            var args = {
                codigo_cliente: item
            };
            $http.post('cliente/delete', args)
                .success(function (data) { deferred.resolve(data); })
                .error(function (data, status, headers, config) { deferred.reject(data); });
            return deferred.promise;
        };

        return {
            getAll: _getAll,
            get: _get,
            validaCpf: _validaCpf,
            save: _save,
            update: _update,
            delete: _delete
        };
    }

})();