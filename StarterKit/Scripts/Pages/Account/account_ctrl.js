angular.module('starterKit').controller('accountController', ['viewModel', '$state', 'accountResource', 'notif', function (viewModel, $state, accountResource, notif) {
    var vm = this;

    var _init = function () {
        vm.viewModel = viewModel.data;
    };

    vm.submit = function () {
        notif.wait();

        accountResource.update(vm.viewModel).$promise.then(function (resp) {
            $state.reload('account');
        }).finally(function () {
            notif.clear();
        });
    };
    
    _init();
}]);
