angular.module('starterKit').controller('confirmEmailController', ['Authentication', '$location', function (Authentication, $location) {
    var vm = this;

    var _init = function () {
        vm.user = {
            id: null,
            code: null
        };

        var success = _retrieveUserParams();

        if (success == true) {
            _confirmEmail();
        }
        else {
            // need a notif here
            alert('something went wrong');
        }
    };
    
    var _confirmEmail = function () {
        var result = Authentication.confirmEmail(vm.user.id, vm.user.code);
        
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
