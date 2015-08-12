angular.module('starterKit').controller('loginController', ['$routeParams', 'Authentication', '$location', 'notif', function ($routeParams, Authentication, $location, notif) {
    var vm = this;

    var _init = function () {
        vm.user = {
            email: '',
            password: '',
            remeberMe: true,
            confirmPassword: '',
            firstName: '',
            lastName: '',
            returnUrl: $routeParams.returnUrl,
            loginFailure: false
        };

        vm.isLogin = true;
    };

    vm.register = function () {
        notif.wait();
        var result = Authentication.register(vm.user.email, vm.user.firstName, vm.user.lastName, vm.user.password, vm.user.confirmPassword);

        result.then(function (res) {
            if (res.success) {
                vm.goTo('isLogin');
            }
        }).finally(function () {
            notif.clear();
        });
    };

    vm.login = function () {
        notif.wait();
        var result = Authentication.login(vm.user.email, vm.user.password, vm.user.remeberMe);

        result.then(function (resp) {
            if (resp.success == true) {
                if (resp.meta != null) {
                    if (resp.meta.needTwoFactor == true) {
                        $location.path('/twofactor');
                    }
                    else if (resp.meta.needEmailConfirmation == true) {
                        return;
                    }
                }
                else if (vm.user.returnUrl != null) {
                    var returnUrl = vm.user.returnUrl;
                    $location.search('returnUrl', null);
                    $location.path(returnUrl);
                }
                else {
                    $location.path('/');
                }
            }
        }).finally(function () {
            notif.clear();
        });
    };

    vm.resetPassword = function () {
        notif.wait();
        Authentication.resetPassword(vm.user.email);
        vm.goTo('isLogin');
        notif.clear();
    };

    vm.goTo = function (section) {
        if (section == 'isForgot') {
            vm.isLogin = false;
            vm.isForgot = true;
            vm.isSignup = false
        }
        else if (section == 'isSignup') {
            vm.isLogin = false;
            vm.isForgot = false;
            vm.isSignup = true;
        }
        else if (section == 'isLogin') {
            vm.isLogin = true;
            vm.isForgot = false;
            vm.isSignup = false;
        }
    };

    _init();
}]);
