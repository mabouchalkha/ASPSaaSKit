angular.module('starterKit').controller('confirmPasswordController', ['Authentication', '$location', function (Authentication, $location) {
    var vm = this;

    var _init = function () {
        vm.user = {
            id: null,
            code: null
        };

        var success = _retrieveUserParams();

        if (success == true) {
            _confirmPassword();
        }
        else {
            // need a notif here
            alert('something went wrong');
        }
    };
    
    var _confirmPassword = function () {
        var result = Authentication.confirmPassword(vm.user.id, vm.user.code);
        
        result.then(function (resp) {
            if (resp.success == true) {
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
