(function () {
    angular
        .module('app')
        .controller('AdminEditController',
        ['$state', '$scope', '$stateParams', 'toastr', 'appService', AdminEditController]);

    /* @ngInject */
    function AdminEditController($state, $scope,$stateParams, toastr, appService) {
        /* jshint validthis: true */
        var vm = this;
        vm.savePhrase = savePhrase;
        vm.deletePhrase = deletePhrase;
        activate();
        
        console.log($stateParams.phraseId);

        function activate() {
            vm.phrase = {};
            if ($stateParams.phraseId)
            {
                appService.getPhrase($stateParams.phraseId).then(function (response) {
                    console.log(response);
                    vm.phrase = response;
                }, function (error) {
                    console.error(error);
                });
            }
            else
            {
                vm.phrase.phrase = '';
            }
        }

        function savePhrase() {

            if (!vm.phrase.phrase)
            {
                toastr.warning('Please fill phrase text');
                return;
            }

            appService.savePhrase(vm.phrase).then(function (response) {
                vm.phrase = response;
                toastr.success("Phrase saved successfully");
            }, function (error) {
                console.error(error);
                toastr.error("Error saving phrase");
            });
        }


        function deletePhrase() {
            appService.deletePhrase(vm.phrase.id).then(function (response) {                
                toastr.success("Phrase deleted successfully");
                $state.go("admin");
            }, function (error) {
                console.error(error);
                toastr.error("Error deleting phrase");
            });
        }
    }
}());
