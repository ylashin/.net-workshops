(function () {
    'use strict';

    angular.module('app')
        .factory('appService', ['toastr', 'httpBusyService', appService]);




    /* @ngInject */
    function appService(toastr, httpBusyService) {
        return {
            checkTweet: checkTweet,
            getPhrases: getPhrases,
            getPhrase: getPhrase,
            savePhrase: savePhrase,
            deletePhrase: deletePhrase

        };

        function checkTweet(tweet) {
            var action = 'checking tweet';
            return httpBusyService
                .post(action, 'api/app/tweet/check', tweet);
        }

        function savePhrase(phrase) {
            var action = 'saving phrase';
            if (phrase.id) {
                return httpBusyService
                .put(action, 'api/app/phrases/update', phrase);
            }
            else {
                return httpBusyService
                .post(action, 'api/app/phrases/add', phrase);
            }

        }

        function deletePhrase(id) {
            var action = 'deleting phrase';

            return httpBusyService
            .delete(action, 'api/app/phrases/delete/' + id);
        }


        function getPhrases() {
            var action = 'getting all phrases';
            return httpBusyService
                .get(action, 'api/app/phrases/getall')
                .catch(function () {
                    notifyError(action);
                });
        }

        function getPhrase(id) {
            var action = 'loading...';
            return httpBusyService
                .get(action, 'api/app/phrases/' + id)
                .catch(function (error) {
                    console.error(error);
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