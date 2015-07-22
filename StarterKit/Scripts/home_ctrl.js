angular.module("starterKit").controller('appController', ['$scope', '$location', 'Authentication', '$modal', function ($scope, $location, Authentication, $modal) {
    var vm = this;

    var _init = function () {
        vm.user = {};
        vm.service = Authentication;
        vm.title = 'starterKit',
        vm.brand = 'starterKit'
        vm.isProfileOpen = false;
    };

    $scope.$on('user:change', function (e, user) {
        vm.user = user;
    });

    vm.logout = function () {
        var result = Authentication.logout();

        result.then(function () {
            $location.path('#/login');
        });
    };

    _init();

}]);

