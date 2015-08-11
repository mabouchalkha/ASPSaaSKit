angular.module('starterKit').constant('accountResolver', {
    resolve: {
        viewModel: ['accountResource', 'Authentication', function (accountResource, Authentication) {
            return accountResource.read().$promise.then()
        }]
    }
});