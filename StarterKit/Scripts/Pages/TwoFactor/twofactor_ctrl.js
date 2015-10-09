angular.module('starterKit').controller('twofactorController', ['Authentication', '$state', 'notif', function (Authentication, $state, notif) {
    var vm = this;

    var _init = function () {
        vm.user = {
            code: null
        };
    };
    
    vm.submit = function () {
        var result = Authentication.confirmTwoFactor(vm.user.code);
        
        result.then(function (resp) {
            if (resp.success == true) {
                $state.go('dashboard');
            }
        });
    };

    vm.resend = function () {
        Authentication.resendTwoFactor();
    };

    _init();
}]);
