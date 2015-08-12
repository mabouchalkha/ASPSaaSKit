angular.module('starterKit').controller('inviteController', ['userResource', function (userResource) {
    var vm = this;

    var _init = function () {
        vm.emails = '';
    };

    vm.submit = function () {
        userResource.invite({ emails: vm.emails });
    };

    _init();
}]);
