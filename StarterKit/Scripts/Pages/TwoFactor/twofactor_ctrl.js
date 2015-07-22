angular.module('starterKit').controller('twofactorController', ['Authentication', '$location', function (Authentication, $location) {
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
        var result = Authentication.resendTwoFactor();

        result.then(function (resp) {
            if (resp.success == true) {
                alert('new code sent');
            }
        });
    };

    _init();
}]);
