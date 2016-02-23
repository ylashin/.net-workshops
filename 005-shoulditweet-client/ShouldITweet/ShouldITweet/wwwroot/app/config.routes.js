(function() {
    'use strict';

    angular
        .module('app')
        .config(['$stateProvider', '$urlRouterProvider',  configFunction]);

    /* @ngInject */
    function configFunction($stateProvider, $urlRouterProvider, configuration) {

        //$urlRouterProvider.otherwise(function($injector) {
        //    var state = $injector.get('$state');
        //    state.go('app.home');
        //});

        console.log("config routes");


        $urlRouterProvider.otherwise("/home");
        
        $stateProvider
            .state('app.home', {
                url: '/home',
                //templateUrl: 'wwwroot/app/layout/shell.html',
                template: '<h1>Hello Route</h1>',
                controller: 'HomeController',
                controllerAs: 'vm',
                onEnter: function () {
                    console.log("entered home");
                }
            })            
            .state('app.manage', {
                url: '/manage',
                templateUrl: 'wwwroot/app/manage/manage.html',
                controller: 'ManageController',
                controllerAs: 'vm',
                onEnter: function () {
                    console.log("entered admin");
                }
            });       
    }
})();