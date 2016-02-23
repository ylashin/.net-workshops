(function () {
    'use strict';

    angular.module('app')
        .factory('appService', appService);

    /* @ngInject */
    function appService(toastr, httpBusyService) {
        return {
            checkTweet: checkTweet,
            getPhrases: getPhrases,
            getGoalTypes: getGoalTypes,
            delete: deleteGoal
        };

        function checkTweet(tweet) {
            var action = 'checking tweet';
            return httpBusyService
                .post(action, 'api/app/tweet/check',tweet);
        }

        function getPhrases() {
            var action = 'getting all phrases';
            return httpBusyService
                .get(action, 'api/app/phrases/getall')
                .catch(function () {
                    notifyError(action);
                });
        }

        function getGoalTypes() {
            var action = 'loading goal types';
            return httpBusyService
                .get(action, 'admin/goal/types/')
                .catch(function () {
                    notifyError(action);
                });
        }

        function deleteGoal(ypId, goalId) {
            var action = 'deleting young person goal';
            return httpBusyService
                .delete(action, 'admin/goal/' + ypId + '/' + goalId)
                .catch(function () {
                    notifyError(action);
                });
        }

        function notifyError(action) {
            toastr.error(errorString(action, 'Error'));
        }

        function errorString(action) {
            return 'An error occurred ' + action + '. Please try again.';
        }
    }
})();