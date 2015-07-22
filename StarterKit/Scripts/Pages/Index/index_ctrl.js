angular.module('starterKit').controller('indexController', ['viewModel', 'config', '$scope', 'notif', function (viewModel, config, $scope, notif) {
    var vm = this;

    var _init = function () {
        vm.viewModel = viewModel.data;
        vm.config = config;
        vm.gridOptions = _generateGridOptions();

        vm.iconClass = _generateIconClass(vm.config.icon);

        _registerGridApi();

        _validateRequiredConfig(vm.config);
    };


    var _generateIconClass = function (icon) {
        if (icon) {
            return 'fa fa-' + icon;
        }

        return 'fa fa-database';
    };

    var _generateGridOptions = function () {
        return {
            appScopeProvider: vm,
            data: vm.viewModel.entities,
            columnDefs: [
                { field: 'Id', enableCellEdit: false },
                { field: 'FirstName' },
                { field: 'LastName' },
                { field: 'Email' },
                { name: ' ', width: '5%', enableCellEdit: false, enableColumnMenu: false, enableSorting: false, cellTemplate: '<button sng-show="false" ng-click="grid.appScope.saveEntity(row.entity)" class="btn btn-link info"><i class="fa fa-save"></i></button><button ng-click="grid.appScope.deleteEntity(row.entity)" class="btn btn-link danger pull-right"><i class="fa fa-remove"></i></button>' }]
        }
    };

    var _registerGridApi = function () {
        vm.gridOptions.onRegisterApi = function (api) {
            api.edit.on.afterCellEdit($scope, function (rowEntity, col, newValue, oldValue) {
                console.log(rowEntity);
            });
        };
    };

    var _validateRequiredConfig = function (config) {
        if (!config.resource) {
            throw 'Cannot print index page without a resource passed to config object';
        }
    };

    vm.saveEntity = function (entity) {
        if (entity != null) {
            if (entity._isNew == true) {
                vm.config.resource.create({entity: entity});
            }
            else {
                vm.config.resource.update({ entity: entity }).$promise.then(function (resp) {
                    console.log('updated');
                });
            }
        }
    };

    vm.deleteEntity = function (entity) {
        var index = -1;

        for (var i = 0; i < vm.viewModel.entities.length; i++) {
            if (vm.viewModel.entities[i].Id == entity.Id) {
                index = i;
            }
        }

        if (index > -1) {
            vm.viewModel.entities.splice(index, 1);
        }
    }

    _init();
}]);