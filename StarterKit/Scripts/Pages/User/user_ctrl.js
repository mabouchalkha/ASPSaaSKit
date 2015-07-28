angular.module('starterKit').controller('userController', ['viewModel', 'userResource', '$location', 'notif', function (viewModel, userResource, $location, notif) {
    var vm = this;

    var _init = function () {
        vm.viewModel = viewModel.data;
        vm.meta = viewModel.meta;
        vm.isNew = vm.viewModel.Id == null;
    };

    vm.submit = function () {
        if (vm.viewModel.Email) {
            notif.wait();

            if (vm.isNew) {
                userResource.create(vm.viewModel).$promise.then(_reloadIndex).finally(_clearNotif);
            }
            else {
                userResource.update(vm.viewModel).$promise.then(_reloadIndex).finally(_clearNotif);
            }
        }
    };

    var _reloadIndex = function () {
        $location.path('/user');
    };

    var _clearNotif = function () {
        notif.clear();
    }
    _init();
}]);
