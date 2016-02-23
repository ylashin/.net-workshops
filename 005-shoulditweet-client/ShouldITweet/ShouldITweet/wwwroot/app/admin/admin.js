(function () {
    angular
        .module('app')
        .controller('AdminController', AdminController);

    /* @ngInject */
    function AdminController($state, $scope, toastr, appService) {
        /* jshint validthis: true */
        var vm = this;
        activate();



        function activate() {
            vm.phrases = [];
            
            appService.getPhrases().then(function (response) {
                console.log(response);
                vm.phrases = response;
            }, function (error) {
                console.error(error);                
            });

            
        }

        function checkTweet() {

            if (!vm.tweet.text)
            {
                toastr.warning('Please fill tweet text');
                return;
            }

            appService.checkTweet(vm.tweet).then(function (response,error) {
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
