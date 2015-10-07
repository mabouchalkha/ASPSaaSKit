angular.module('starterKit').controller('confirmEmailController', ['Authentication', '$state', '$stateParams', function (Authentication, $state, $stateParams) {
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
