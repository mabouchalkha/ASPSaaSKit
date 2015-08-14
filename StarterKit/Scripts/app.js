angular.module('starterKit', ['ngRoute', 'ui.bootstrap', 'toastr', 'ngResource', 'ui.grid', 'ui.grid.selection', 'ui.grid.pinning']);
angular.module('starterKit').config(['$routeProvider', '$httpProvider', '$locationProvider', '$injector', function ($routeProvider, $httpProvider, $locationProvider, $injector) {
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
    }]);
angular.element(document).ready(function () {
    var req = $.ajax({ url: 'Account/GetCurrentUser' });
    var handler = function (resp) {
        var app = angular.bootstrap(document, ["starterKit"]);
        var root = app.get('$rootScope');
        var auth = app.get('Authentication');
        var $location = app.get('$location');
        var notif = app.get('notif');
        auth.user = resp.data.user;
        root.$broadcast('user:change', auth.user);
        var anonRoutes = ['/login', '/register', '/404', '/resetPassword', '/twofactor', '/confirmpassword'];
        var authRoutes = ['/user', '/account', '/invite'];
        root.$on('$routeChangeStart', function (event, next, current) {
            notif.wait();
            if (anonRoutes.indexOf($location.url()) == -1 && !auth.isAuthenticated()) {
                $location.path('/login');
            }
        });
        root.$on('$routeChangeSuccess', function (event, next, current) {
            notif.clear();
        });
        root.$on('$routeChangeError', function (event, next, current) {
            notif.clear();
        });
        if (auth.user == null && $location.path() == '/') {
            $location.path('/login');
        }
        root.$apply();
    };
    req.done(handler);
    req.fail(handler);
});
//# sourceMappingURL=app.js.map