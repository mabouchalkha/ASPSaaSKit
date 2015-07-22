angular.module('starterKit').controller('twoController', ['$http', function ($http) {
    var vm = this;

    $http.get('/RoutesDemo/two').then(function (resp) {
        vm.data = resp;
    });
}]);
