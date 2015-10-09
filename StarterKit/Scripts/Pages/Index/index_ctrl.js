angular.module('starterKit').controller('indexController', ['viewModel', 'config', '$scope', 'notif', '$location', '$modal', '$state', function (viewModel, config, $scope, notif, $location, $modal, $state) {
    var vm = this;

    var _init = function () {
        vm.viewModel = viewModel.data;
        vm.config = config;
        vm.gridOptions = _generateGridOptions();
        _registerGridApi();

        vm.iconClass = _generateIconClass(vm.config.icon);

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
            columnDefs: vm.config.columnDefs,
            enableRowSelection: true,
            enableRowHeaderSelection: false,
            multiSelect: false,
        }
    };

    var _registerGridApi = function () {
        vm.gridOptions.onRegisterApi = function (api) {
            api.selection.on.rowSelectionChanged($scope, function (row) {
                vm.selectedLine = row.isSelected == true ? row : null;
            });
        };
    };

    var _getPrimaryKey = function (entity) {
        return entity ? entity[config.id] : null;
    }

    var _getSelectedEntity = function () {
        return vm.selectedLine ? vm.selectedLine.entity : null;
    }

    var _validateRequiredConfig = function (config) {
        if (!config.resource) {
            throw 'Cannot print index page without a resource passed to config object';
        }

        if (!config.id) {
            throw 'Cannot print index page without the entity id';
        }

        if (!config.columnDefs) {
            throw 'Cannot print index page without the columns definition';
        }
    };

    var _doDeleteEntity = function (id) {
        notif.wait();

        config.resource.delete({id: id}).$promise.then(function (resp) {
            $state.reload($state.current.name);
        }).finally(function () {
            notif.clear();
        });
    }

    vm.addEntity = function () {
        //$location.path($location.path() + '/create');
        $state.go('user.create');
    }

    vm.editSelectedEntity = function () {
        var entityId = _getPrimaryKey(_getSelectedEntity());

        if (entityId) {
            $location.path($location.path() + '/' + entityId);
        }
    }

    vm.deleteSelectedEntity = function () {
        var entityId = _getPrimaryKey(_getSelectedEntity());

        if (entityId) {
            var instance = $modal.open({ templateUrl: '/Scripts/Pages/Index/deletemodal.html', $scope: vm });
            instance.result.then(function () {
                _doDeleteEntity(entityId);
            });
        }
    };


    _init();
}]);