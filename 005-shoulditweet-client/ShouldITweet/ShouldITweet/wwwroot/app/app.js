var myapp = angular.module('app', ["ui.router", 'toastr', 'ngSanitize', 'cgBusy']);


myapp.value('cgBusyDefaults', {
    message: 'please wait...',
    backdrop: true,
    delay: 300
})
    .constant('_', window._)
    .constant('moment', window.moment);


myapp.config(function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise("/home");

    $stateProvider
      .state('home', {
          url: "/home",
          templateUrl: "app/home/home.html",
          controller: 'HomeController',
          controllerAs: 'vm',
      })
      .state('admin', {
          url: "/admin",
          templateUrl: "app/admin/admin.html",
          controller: 'AdminController',
          controllerAs: 'vm',
      })
    .state('admin-edit', {
        url: "/admin/edit/:phraseId",
        templateUrl: "app/admin/admin-edit.html",
        controller: 'AdminEditController',
        controllerAs: 'vm',
    });

});


