var app = angular.module('starterKit', ['ngRoute', 'ui.bootstrap', 'toastr', 'ngResource', 'ui.grid', 'ui.grid.selection', 'ui.grid.pinning']);

angular.module('starterKit').config(['$routeProvider', '$httpProvider', '$locationProvider', '$injector', function ($routeProvider, $httpProvider, $locationProvider, $injector) {
    $routeProvider
        .when('/', { templateUrl: '/Scripts/Pages/Dashboard/dashboard.html', controller: 'dashboardController as vm' })
        .when('/one', { templateUrl: '/Scripts/Pages/One/one.html', controller: 'oneController as vm' })
        .when('/two', { templateUrl: '/Scripts/Pages/Two/two.html', controller: 'twoController as vm' })
        .when('/login', { templateUrl: '/Scripts/Pages/Login/login.html', controller: 'loginController as vm' })
        .when('/resetPassword', { templateUrl: '/Scripts/Pages/ResetPassword/reset_password.html', controller: 'resetPasswordController as vm' })
        .when('/confirmpassword', { templateUrl: '/Scripts/Pages/ConfirmPassword/confirm_password.html', controller: 'confirmPasswordController as vm' })
        .when('/twofactor', { templateUrl: '/Scripts/Pages/TwoFactor/twofactor.html', controller: 'twofactorController as vm' })
        .when('/404', { templateUrl: '/Scripts/Pages/404.html', })

        .when('/user', { templateUrl: '/Scripts/Pages/index/index.html', controller: 'indexController as vm', resolve: $injector.get('userResolver').resolveIndex })
        .when('/user/create', { templateUrl: '/Scripts/Pages/User/edit.html', controller: 'userController as vm', resolve: $injector.get('userResolver').resolve })

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
        var authRoutes = ['/two', '/one', '/user'];

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
