angular.module('starterKit').controller('accountController', ['viewModel', '$route', 'accountResource', 'notif', function (viewModel, $route, accountResource, notif) {
    var vm = this;

    var _init = function () {
        vm.viewModel = viewModel.data;
    };

    vm.submit = function () {
        notif.wait();

        accountResource.update(vm.viewModel).$promise.then(function (resp) {
            $route.reload();
        }).finally(function () {
            notif.clear();
        });
    };
    
    _init();
}]);
