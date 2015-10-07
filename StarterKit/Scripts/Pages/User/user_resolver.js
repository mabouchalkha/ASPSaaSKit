angular.module('starterKit').constant('userResolver', {
    resolveIndex: {
        viewModel: ['userResource', function (userResource) {
            return userResource.index().$promise.then()
        }],
        config: ['userResource', '$state', function (userResource, $state) {
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
                    { field: 'EmailConfirmed' }],
                customActions: [{ name: 'Invite User', fn: function () { $state.go('invite'); } }]
            }
        }]
    },
    resolve: {
        viewModel: ['userResource', '$stateParams', function (userResource, $stateParams) {
            var id = $stateParams.id;

            if (id == null || id == 'create') {
                return { data: {} };
            }
            else {
                return userResource.read({ id: id }).$promise.then();
            }
        }]
    }
});