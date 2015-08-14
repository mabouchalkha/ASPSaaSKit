var StarterKit;
(function (StarterKit) {
    var Home;
    (function (Home) {
        var AppController = (function () {
            function AppController($scope, $modal, $location, Authentication) {
                this.$scope = $scope;
                this.$modal = $modal;
                this.$location = $location;
                this.Authentication = Authentication;
                this.title = 'StarterKit';
                this.user = {};
                this.brand = 'StarterKit';
                this.isProfileOpen = false;
                this.service = this.Authentication;
                this.initEvents();
            }
            AppController.prototype.logout = function () {
                var _this = this;
                var result = this.Authentication.logout();
                result.then(function () {
                    _this.$location.path('#/login');
                });
            };
            AppController.prototype.initEvents = function () {
                this.$scope.$on('user:change', function (e, user) {
                    this.user = user;
                });
            };
            AppController.prototype.resetPassword = function () {
                var result = this.Authentication.resetPassword(this.user.Email);
            };
            AppController.$inject = ["$scope", "$location", "$modal", "Authentication"];
            return AppController;
        })();
        angular.module("starterKit").controller('appController', AppController);
    })(Home = StarterKit.Home || (StarterKit.Home = {}));
})(StarterKit || (StarterKit = {}));
//# sourceMappingURL=home_ctrl.js.map