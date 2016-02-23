var Bootstrapper = (function (window, document) {

  var initialize = function (angular, angularApplicationName) {

      

      var bootstrapAngular = function () {

        angular.bootstrap(window.document, [angularApplicationName]);

        
    };

      angular.element(document).ready(bootstrapAngular);

  };

  return {
    initialize: initialize
  };

})(window, window.document);
