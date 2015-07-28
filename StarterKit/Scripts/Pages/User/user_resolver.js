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
                resource: userResource,
                columnDefs: [
                { field: 'FirstName' },
                { field: 'LastName' },
                { field: 'Email' },
                { field: 'EmailConfirmed'}]
            }
        }]
    },
    resolve: {
        viewModel: ['userResource', '$route', function (userResource, $route) {
            var id = $route.current.params.id;

            if (id == null || id == 'create') {
                return { data: {} };
            }
            else {
                return userResource.read({ id: id }).$promise.then();
            }
        }]
    }
});