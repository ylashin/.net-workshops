(function () {
    angular
        .module('app')
        .controller('HomeController', ['$state', '$scope', 'toastr', 'appService',HomeController]);

    /* @ngInject */
    function HomeController($state, $scope, toastr, appService) {
        /* jshint validthis: true */
        var vm = this;
        activate();



        function activate() {
            vm.tweet = {};
            vm.tweet.text = '';
            vm.activate = activate;
            vm.checkTweet = checkTweet;
            vm.generalError = '';
        }

        function checkTweet() {

            if (!vm.tweet.text)
            {
                toastr.warning('Please fill tweet text');
                return;
            }

            appService.checkTweet(vm.tweet).then(function (response) {
                console.log(response)
                vm.generalError = '';
                vm.verbotenCheckPassed = response.verbotenCheckPassed;
                vm.violations = response.violations;
            }, function (error) {
                console.error(error);
                vm.generalError = 'Sorry, some error has occurred. Please try again';
            });
        }

        

    }
}());
