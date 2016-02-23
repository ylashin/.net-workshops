var myapp = angular.module('app',
    ["ui.router", 'toastr', 'ngSanitize', 'cgBusy'])
    .value('cgBusyDefaults', {
        message: 'please wait...',
        backdrop: true,
        delay: 300
    })
    .constant('_', window._)
    .constant('moment', window.moment);


myapp.config(function ($stateProvider, $urlRouterProvider) {

    // For any unmatched url, send to /route1
    $urlRouterProvider.otherwise("/home");

    $stateProvider
      .state('home', {
          url: "/home",
          templateUrl: "/wwwroot/app/home/home.html",
          controller: 'HomeController',
          controllerAs: 'vm',
      })
      .state('admin', {
          url: "/admin",
          templateUrl: "/wwwroot/app/admin/admin.html",
          controller: 'AdminController',
          controllerAs: 'vm',
      })
    .state('admin-edit', {
        url: "/admin/edit/:phraseId",
        templateUrl: "/wwwroot/app/admin/admin-edit.html",
        controller: 'AdminEditController',
        controllerAs: 'vm',
    });

});


/*
(function () {
    'use strict';

    angular.module('app', [
            //'ngAnimate',
            'toastr',
            'ngSanitize',
            'cgBusy',
            'ui.router'
    ])
        .value('cgBusyDefaults', {
            message: 'please wait...',
            backdrop: true,
            delay: 300
        })
        .constant('_', window._)
        .constant('moment', window.moment)
        .run(['$rootScope', runApp]);



    function runApp($rootScope) {


        $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
            console.log("State change from :");
            console.log(fromState);
            console.log("State change to : ");
            console.log(toState);
            document.body.scrollTop = document.documentElement.scrollTop = 0;
        });


        $rootScope.$on('$stateChangeError', function (event, toState, toParams, fromState, fromParams, error) {
            console.log("State change from :");
            console.log(fromState);
            console.log("State change to : ");
            console.log(toState);
            document.body.scrollTop = document.documentElement.scrollTop = 0;
        });



        $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
            console.log("State change from :");
            console.log(fromState);
            console.log("State change to : ");
            console.log(toState);
            document.body.scrollTop = document.documentElement.scrollTop = 0;
        });


        $rootScope.$on('$stateNotFound', function (event, unfoundState, fromState, fromParams) {
            console.log(unfoundState.to); // "lazy.state"
            console.log(unfoundState.toParams); // {a:1, b:2}
            console.log(unfoundState.options); // {inherit:false} + default options
        });

        //$rootScope.$broadcast("$stateNotFound", 1, 2, 3, 4);
    }

    //angular.module('app').controller('ctrl', function ($scope, $state) {
    //    console.log("This is controller");
    //    //$stateProvider.go
    //});
})
();

*/