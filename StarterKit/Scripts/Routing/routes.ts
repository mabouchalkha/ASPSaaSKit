module StarterKit {
    export class Routes {
        static $inject = ['$routeProvider', '$httpProvider', '$locationProvider', '$injector'];      
        static configureRoutes($routeProvider: ng.route.IRouteProvider, $httpProvider: ng.IHttpProvider, $locationProvider: ng.ILocationProvider, $injector: any) {
            $routeProvider
                .when('/', { templateUrl: '/Scripts/Pages/Dashboard/dashboard.html', controller: 'dashboardController as vm' })

                .when('/login', { templateUrl: '/Scripts/Pages/Login/login.html', controller: 'loginController as vm' })
                .when('/resetPassword', { templateUrl: '/Scripts/Pages/ResetPassword/reset_password.html', controller: 'resetPasswordController as vm' })
                .when('/confirmemail', { templateUrl: '/Scripts/Pages/ConfirmEmail/confirm_email.html', controller: 'confirmEmailController as vm' })
                .when('/twofactor', { templateUrl: '/Scripts/Pages/TwoFactor/twofactor.html', controller: 'twofactorController as vm' })

                .when('/404', { templateUrl: '/Scripts/Pages/404.html', })

                .when('/account', { templateUrl: '/Scripts/Pages/Account/account.html', controller: 'accountController as vm', resolve: $injector.get('accountResolver').resolve })

                .when('/user', { templateUrl: '/Scripts/Pages/index/index.html', controller: 'indexController as vm', resolve: $injector.get('userResolver').resolveIndex })
                .when('/user/create', { templateUrl: '/Scripts/Pages/User/user.html', controller: 'userController as vm', resolve: $injector.get('userResolver').resolve })
                .when('/user/:id', { templateUrl: '/Scripts/Pages/User/user.html', controller: 'userController as vm', resolve: $injector.get('userResolver').resolve })
                .when('/invite', { templateUrl: '/Scripts/Pages/Invite/invite.html', controller: 'inviteController as vm' })

                .otherwise({ redirectTo: '/404' });

            $httpProvider.interceptors.push('HttpResponseInterceptor');
        }
    }
}