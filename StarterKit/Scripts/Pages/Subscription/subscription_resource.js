angular.module('starterKit').factory('subscriptionResource', ['$resource', function ($resource) {
    return $resource('', {}, {
        index: {
            method: 'GET', url: '/Subscription'
        }
    });
}]);