angular.module('starterKit').controller('subscriptionController', ['$state', 'notif', 'plans', function ($state, notif, plans) {
    var vm = this;

    var _init = function () {
        vm.plans = plans.data;
    };

    
    _init();
}]);
