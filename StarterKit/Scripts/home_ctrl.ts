module StarterKit.Home {
    class AppController {
        static $inject = ["$scope", "$location", "$modal", "Authentication"];
        private title: string;
        private user: any;
        private isProfileOpen: boolean;
        private brand: string;
        private service: any;

        constructor(private $scope: ng.IScope, private $modal: angular.ui.bootstrap.IModalService, private $location: ng.ILocationService, private Authentication: any) {
            this.title = 'StarterKit';
            this.user = {};
            this.brand = 'StarterKit';
            this.isProfileOpen = false;
            this.service = this.Authentication;

            this.initEvents();
        }
        logout() {
            var result = this.Authentication.logout();

            result.then(() => {
                this.$location.path('#/login');
            });
        }
        initEvents() {
            this.$scope.$on('user:change', function (e, user) {
                this.user = user;
            });
        }
        resetPassword() {
            var result = this.Authentication.resetPassword(this.user.Email);
        }
    }


    angular.module("starterKit").controller('appController', AppController);
}