(function () {
    angular
        .module('app')
        .controller('AdminEditController', AdminEditController);

    /* @ngInject */
    function AdminEditController($state, $scope,$stateParams, toastr, appService) {
        /* jshint validthis: true */
        var vm = this;
        activate();

        var phraseId = $stateParams.phraseId;
        console.log(phraseId);

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
