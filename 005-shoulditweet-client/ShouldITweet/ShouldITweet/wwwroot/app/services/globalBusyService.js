(function(){
    'use strict';

    angular.module('app')
        .factory('globalBusyService',['$rootScope', globalBusyService]);

    /* @ngInject */
    function globalBusyService($rootScope) {

        return { showBusy: showBusy };

        function showBusy(promise,busyMessage){
            if ($rootScope.busyPromise && $rootScope.busyPromise.$$state.status === 0){ // Only chain promises if the existing promise is still pending
                $rootScope.busyPromise.finally(function () {
                    setBusy(promise,busyMessage);
                });
            } else {
                if(!busyMessage){
                    busyMessage = 'loading...';
                }
                setBusy(promise,busyMessage);
            }
        }

        function setBusy(promise, busyMessage){
            setTimeout(function(){
                $rootScope.busyPromise = promise;
                $rootScope.busyMessage = busyMessage;
                $rootScope.$apply();
            });
        }
    }
})();