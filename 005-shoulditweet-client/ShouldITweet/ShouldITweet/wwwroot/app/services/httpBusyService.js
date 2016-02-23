(function() {
    'use strict';

    angular.module('app')
        .factory('httpBusyService', ['$http', '$q','globalBusyService',httpBusyService]);

    /* @ngInject */
    function httpBusyService($http, $q, globalBusyService) {
        return {
            get: get,
            post: post,
            put: put,
            delete: deleteThing
        };

        function get(message, url){
            var deferred = $q.defer();
            var callUrl =  url;

            $http.get(callUrl).success(function(data, status) {
                deferred.resolve(data);
            }).error(function(data, status) {
                deferred.reject({data: data, status:status});
            });

            globalBusyService.showBusy(deferred.promise, message);

            return deferred.promise;
        }

        function post(message, url, data){
            var deferred = $q.defer();
            var callUrl =  url;

            $http.post(callUrl, data).success(function(data, status) {
                deferred.resolve(data);
            }).error(function(data, status) {
                deferred.reject({data: data, status:status});
            });

            globalBusyService.showBusy(deferred.promise, message);

            return deferred.promise;
        }

        function put(message, url, data){
            var deferred = $q.defer();
            var callUrl =  url;

            $http.put(callUrl, data).success(function(data, status) {
                deferred.resolve(data);
            }).error(function(data, status) {
                deferred.reject({data: data, status:status});
            });

            globalBusyService.showBusy(deferred.promise, message);

            return deferred.promise;
        }

        function deleteThing(message, url){
            var deferred = $q.defer();
            var callUrl =  url;

            $http.delete(callUrl).success(function(data, status) {
                deferred.resolve(data);
            }).error(function(data, status) {
                deferred.reject({data: data, status:status});
            });

            globalBusyService.showBusy(deferred.promise, message);

            return deferred.promise;
        }
    }
})();