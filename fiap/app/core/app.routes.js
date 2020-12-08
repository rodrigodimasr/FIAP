(function () {
    "use strict";
    debugger
    angular.module('fiap').config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider', '$locationProvider']

    function config($stateProvider, $urlRouterProvider, $locationProvider) {

        $urlRouterProvider.otherwise('/home');

        $stateProvider
            .state('state-home', { url: '/home', views: { 'body': { templateUrl: '/home/index', controller: 'homeController' } } })

            .state('state-cliente', { url: '/cliente', views: { 'body': { templateUrl: '/cliente/index', controller: 'clienteController' } } })
            .state('state-cliente-add', { url: '/cliente/add', views: { 'body': { templateUrl: '/cliente/add', controller: 'clienteAddController' } } })
            .state('state-cliente-detail', { url: '/cliente/detail/:id', views: { 'body': { templateUrl: '/cliente/detail', controller: 'clienteDetailController' } } })
            .state('state-cliente-edit', { url: '/cliente/edit/:id', views: { 'body': { templateUrl: '/cliente/edit', controller: 'clienteDetailController' } } })
            ;
        $locationProvider.html5Mode(false);
    }

})();