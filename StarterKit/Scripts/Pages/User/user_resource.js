angular.module('starterKit').factory('userResource', ['$resource', function ($resource) {
    return $resource('', {}, {
        index: {
            method: 'GET', url: '/user'
        },
        create: {
            method: 'POST', url: '/user/create'
        },
        read: {
            method: 'GET', url: '/user/read/:id'
        },
        update: {
            method: 'POST', url: '/user/update'
        },
        delete: {
            method: 'DELETE', url: '/user/delete/:id'
        }
    });
}]);