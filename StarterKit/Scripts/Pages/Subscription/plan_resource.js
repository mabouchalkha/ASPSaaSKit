﻿angular.module('starterKit').factory('planResource', ['$resource', function ($resource) {
    return $resource('', {}, {
        index: {
            method: 'GET', url: '/SubscriptionPlan'
        }
        //read: {
        //    method: 'GET', url: '/user/read/:id'
        //}
    });
}]);