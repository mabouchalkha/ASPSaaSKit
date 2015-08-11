angular.module('starterKit').factory('accountResource', ['$resource', function ($resource) {
    return $resource('', {}, {
        read: {
            method: 'GET', url: '/tenant/read/'
        },
        update: {
            method: 'PUT', url: '/tenant/update'
        },
    });
}]);