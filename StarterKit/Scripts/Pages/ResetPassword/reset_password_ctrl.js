angular.module('starterKit').controller('resetPasswordController', ['Authentication', '$state', 'notif', function (Authentication, $state, notif) {
    var vm = this;

    var _init = function () {
        vm.user = {
            password: '',
            confirmPassword: '',
            id: null,
            code: null
        };

        var success = _retrieveUserParams();

        if (success != true) {
            // TOASTR here need a notif
            alert('something went wrong');
        }
    };
    
    vm.resetPassword = function () {
        var result = Authentication.confirmResetPassword(vm.user.password, vm.user.confirmPassword, vm.user.id, vm.user.code);
        
        result.then(function (resp) {
            if (resp.success == true) {
               $state.go('login');
            }
        });
    };

    var _retrieveUserParams = function () {
        if ($stateParams.userid != null && $stateParams.code != null) {
            vm.user.id = $stateParams.userid;
            vm.user.code = $stateParams.code;
            return true;
        }

        return false;
    };

    _init();
}]);
