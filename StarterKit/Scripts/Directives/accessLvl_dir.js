angular.module("starterKit").directive('accessLevel', ['Authentication', function (Authentication) {
    return {
        restrict: 'A',
        link: function($scope, element, attrs) {
            // need to be refactored
        }
    };
}]);