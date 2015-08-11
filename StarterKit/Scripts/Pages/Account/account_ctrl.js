angular.module('starterKit').controller('accountController', ['viewModel', 'accountResource', function (viewModel, accountResource) {
    var vm = this;

    var _init = function () {
        vm.viewModel = viewModel.data;
    };

    vm.submit = function () {
        accountResource.update(vm.viewModel);
    };
    
    _init();
}]);
