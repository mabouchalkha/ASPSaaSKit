﻿angular.module("starterKit").controller('appController', ['$scope', '$state', 'Authentication', '$modal', function ($scope, $state, Authentication, $modal) {
    var vm = this;

    var _init = function () {
        vm.user = {};
        vm.service = Authentication;
        vm.title = 'StarterKit',
        vm.brand = 'StarterKit'
        vm.isProfileOpen = false;
    };

    $scope.$on('user:change', function (e, user) {
        vm.user = user;
    });

    vm.logout = function () {
        var result = Authentication.logout();

        result.then(function () {
            $state.go('login');
        });
    };

    vm.resetPassword = function () {
        var result = Authentication.resetPassword(vm.user.Email);
    }

    _init();

}]);

