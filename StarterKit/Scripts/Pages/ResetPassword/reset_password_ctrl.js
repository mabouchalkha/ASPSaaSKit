angular.module('starterKit').controller('resetPasswordController', ['Authentication', '$location', 'notif', function (Authentication, $location, notif) {
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
               notif.log('Password reseted', 'Your new password has been sent through email.');
               $location.search({userid: null, code: null});
               $location.path('/login');
            }
        });
    };

    var _retrieveUserParams = function () {
        var qs = $location.search();

        if (qs != null) {
            if (qs.userid != null && qs.code != null) {
                vm.user.id = qs.userid;
                vm.user.code = qs.code;

                return true;
            }
        }
        
        return false;
    };

    _init();
}]);
