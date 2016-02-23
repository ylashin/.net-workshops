var app = angular.module('app', ["ui.router", 'toastr', 'ngSanitize', 'cgBusy']);


app.value('cgBusyDefaults', {
    message: 'please wait...',
    backdrop: true,
    delay: 300
})
    .constant('_', window._)
    .constant('moment', window.moment);

app.run(function ($rootScope) {
    jQuery(".navbar .container a").click(function () {
        if (jQuery(".navbar-collapse").hasClass("in"))
            jQuery("button.navbar-toggle").click();
    });
    
    $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
        
    });
});





app.config(function ($stateProvider, $urlRouterProvider) {

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


