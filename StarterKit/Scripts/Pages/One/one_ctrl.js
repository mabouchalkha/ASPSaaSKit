angular.module('starterKit').controller('oneController', ['$http', 'notif', function ($http, notif) {
    var vm = this;

   $http.get('/RoutesDemo/one').then(function (resp) {
       vm.data = resp;
   });

   notif.wait();

   setTimeout(function () {
       notif.clear();
   }, 3000);
}]);