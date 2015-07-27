angular.module('starterKit').constant('userResolver', {
    resolveIndex: {
        viewModel: ['userResource', function (userResource) {
            return userResource.index().$promise.then()
        }],
        config: ['userResource', function (userResource) {
            return {
                name: 'User',
                icon: 'users',
                id: 'Id',
                inline: true,
                resource: userResource
            }
        }]
    },
    resolve: {
        viewModel: ['entityResource', '$route', function (entityResource, $route) {
            var id = $route.current.params.id;

            if (id == null) {
                return {};
            }
            else {
                return entityResource.read({ id: id }).$promise.then();
            }
        }]
    }
});