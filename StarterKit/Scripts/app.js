var app = angular.module('starterKit', [
    //angular modules
    'ngResource',
    //3rd party angular modules
    'ui.router',
    'ui.bootstrap',
    'ui.grid',
    'ui.grid.selection',
    'ui.grid.pinning',
    'toastr'
    //our own starterkit modules
])
.constant('APP', {
    name: 'starterKit',
    logo: 'Content/Images/logo.png',
    version: '0.0.1',
    languages: [{
        name: 'LANGUAGES.ENGLISH',
        key: 'en'
    }, {
        name: 'LANGUAGES.FRENCH',
        key: 'fr'
    }],
});

angular.module('starterKit').config(['$stateProvider', '$urlRouterProvider', '$httpProvider', '$injector', function ($stateProvider, $urlRouterProvider, $httpProvider, $injector) {
    $stateProvider
        .state('dashboard', {
            url: '/',
            templateUrl: '/Scripts/Pages/Dashboard/dashboard.html',
            controller: 'dashboardController as vm'
        })
        .state('login', {
            url: '/login?returnUrl',
            templateUrl: '/Scripts/Pages/Login/login.html',
            controller: 'loginController as vm'
        })
        .state('resetPassword', {
            url: '/resetPassword?userid&code',
            templateUrl: '/Scripts/Pages/ResetPassword/reset_password.html',
            controller: 'resetPasswordController as vm'
        })
        .state('confirmemail', {
            url: '/confirmemail?userid&code',
            templateUrl: '/Scripts/Pages/ConfirmEmail/confirm_email.html',
            controller: 'confirmEmailController as vm'
        })
        .state('twofactor', {
            url: '/twofactor',
            templateUrl: '/Scripts/Pages/TwoFactor/twofactor.html',
            controller: 'twofactorController as vm'
        })
        .state('account', {
            url: '/account',
            templateUrl: '/Scripts/Pages/Account/account.html',
            controller: 'accountController as vm',
            resolve: $injector.get('accountResolver').resolve
        })
        .state('user', {
            abstract: true,
            url: '/user',
            template: '<div data-ui-view></div>',
        })
            .state('user.list', {
                url: '',
                templateUrl: '/Scripts/Pages/index/index.html',
                controller: 'indexController as vm',
                resolve: $injector.get('userResolver').resolveIndex
            })
            .state('user.create', {
                url: '/create',
                templateUrl: '/Scripts/Pages/User/user.html',
                controller: 'userController as vm',
                resolve: $injector.get('userResolver').resolve
            })
            .state('user.detail', {
                url: '/:id',
                templateUrl: '/Scripts/Pages/User/user.html',
                controller: 'userController as vm',
                resolve: $injector.get('userResolver').resolve
            })
        .state('invite', {
            url: '/invite',
            templateUrl: '/Scripts/Pages/Invite/invite.html',
            controller: 'inviteController as vm'
        })
        .state('404', {
            url: '/404',
            templateUrl: '/Scripts/Pages/404.html'
        });

    $urlRouterProvider.otherwise('/404');

    // set default routes when no path specified
    $urlRouterProvider.when('', '/');
    $urlRouterProvider.when('/', '/');

    $httpProvider.interceptors.push('HttpResponseInterceptor');
}]);

angular.module('starterKit').run(['$state', 'stateWatcherService', function ($state, stateWatcherService) {
    
}]);


angular.element(document).ready(function () {
    var req = $.ajax({ url: 'Account/GetCurrentUser' });

    var handler = function (resp) {
        var app = angular.bootstrap(document, ["starterKit"]);
        var root = app.get('$rootScope');
        var auth = app.get('Authentication');
        var $state = app.get('$state');
        var notif = app.get('notif');

        auth.user = resp.data.user;
        root.$broadcast('user:change', auth.user);
        var anonRoutes = ['login', 'register', '404', 'resetPassword', 'twofactor', 'confirmpassword'];
        var authRoutes = ['user', 'user', 'account', 'invite'];

        root.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
            notif.wait();

            if (anonRoutes.indexOf($state.current.name) == -1 && !auth.isAuthenticated()) {
                $state.go('login');
            }
        });

        root.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
            notif.clear();
        });

        root.$on('$stateChangeError', function (event, toState, toParams, fromState, fromParams) {
            notif.clear();
        });

        if (auth.user == null && $state.current.name == '') {
            $state.go('login');
        }

        root.$apply();
    };
    
    req.done(handler);
    req.fail(handler);
});
