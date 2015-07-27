angular.module('starterKit').controller('twofactorController', ['Authentication', '$location', 'notif', function (Authentication, $location, notif) {
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
               $location.path('/');
            }
        });
    };

    vm.resend = function () {
        Authentication.resendTwoFactor();
    };

    _init();
}]);
