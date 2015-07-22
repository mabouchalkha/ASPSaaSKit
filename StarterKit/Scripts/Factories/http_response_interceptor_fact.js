angular.module('starterKit').factory('HttpResponseInterceptor', ['$q', '$location', 'notif', '$injector', function ($q, $location, notif, $injector) {
    return {
        response: function (response) {
            if (response.status === 401) {
                console.log("HTTP : Status 401");
            }

            if (response.data && response.data.message) {
                var title;

                switch (response.data.type) {
                    case 'info':
                        title = 'Notification';
                        break;
                    default:
                        title = 'Operation successful';
                        break;
                };

                notif[response.data.type](title, response.data.message);
            }

            return response || $q.when(response);
        },
        responseError: function (rejection) {
            if (rejection.status == 500 || rejection.status == 401 || rejection.status == 403 || rejection.status == 503) {
                if (rejection.status === 401) {
                    var auth = $injector.get('Authentication'); //need to be injected that way to prevent ciruclar depedency

                    if (auth.isAuthenticated() == true) {
                        notif.error("You do not have access to this ressource", "Access denied");
                        window.history.back();
                    }
                    else {
                        $location.search('returnUrl', $location.path());
                        $location.path('/login');
                    }
                }

                else {
                    notif.error(rejection.data.message, rejection.data.meta);
                }
            }
     
            return $q.reject(rejection);
        }
    }
}]);