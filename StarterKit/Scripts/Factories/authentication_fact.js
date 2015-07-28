angular.module('starterKit').factory('Authentication', ['$q', '$http', '$rootScope', function ($q, $http, $scope) {
    var fact = {
        user: null,
        register: function (email, firstName, lastName, password, confirmPassword) {
            var deffered = $q.defer();

            $http.post('/Account/Register', {
                Email: email,
                FirstName: firstName,
                LastName: lastName,
                Password: password,
                ConfirmPassword: confirmPassword,
            })
            .success(function (data) {
                $scope.$broadcast('user:change', data.user);

                if (data.success == true) {
                    fact.user = data.user;
                    deffered.resolve(data);
                }
                else {
                    fact.user = null;
                    deffered.resolve(data);
                }
            })
            .error(function () {
                deffered.resolve({ success: false });
            });

            return deffered.promise;
        },
        login: function (email, password, rememberMe) {
            var deffered = $q.defer();

            $http.post('/Account/Login', {
                Email: email,
                Password: password,
                RememberMe: rememberMe
            })
            .success(function (resp) {
                $scope.$broadcast('user:change', resp.data);

                if (resp.success == true) {
                    fact.user = resp.data;
                    deffered.resolve(resp);
                }
                else {
                    fact.user = null;
                    deffered.resolve(resp);
                }
            })
            .error(function () {
                deffered.resolve({ success: false });
            });

            return deffered.promise;
        },
        resendTwoFactor: function () {
            var deffered = $q.defer();

            $http.post('/Account/ResendTwoFactor')
                .success(function (resp) {
                    deffered.resolve(resp)
                })
                .error(function () {
                    deffered.resolve({ success: false });
                });

            return deffered.promise;
        },
        confirmTwoFactor: function (code)  {
            var deffered = $q.defer();

            $http.post('/Account/ConfirmTwoFactor', { code: code })
                .success(function (resp) {
                    deffered.resolve(resp)
                })
                .error(function () {
                    deffered.resolve({ success: false });
                });

            return deffered.promise;
        },
        logout: function () {
            var deffered = $q.defer();

            $http.get('/Account/Logout', {})
            .success(function () {
                $scope.$broadcast('user:change', null);
                fact.user = null;
                deffered.resolve({success: true});
            })
            .error(function (data) {
                fact.user = null;
                deffered.resolve({ success: false });
            });

            return deffered.promise;
        },
        resetPassword: function (email) {
            var deffered = $q.defer();

            $http.post('/Account/ResetPassword', { email: email })
                .success(function (resp) {
                    deffered.resolve(resp)
                })
                .error(function () {
                    deffered.resolve({success: false});
                });

            return deffered.promise;
        },
        confirmEmail: function (userId, token) {
            var deffered = $q.defer();

            $http.post('/Account/ConfirmEmail', { id: userId, code: token })
                .success(function (resp) {
                    deffered.resolve(resp)
                })
                .error(function () {
                    deffered.resolve({ success: false });
                });

            return deffered.promise;
        },
        confirmResetPassword: function (password, confirmPassword, userId, token) {
            var deffered = $q.defer();

            $http.post('/Account/ConfirmResetPassword', { password: password, confirmPassword: confirmPassword, id: userId, code: token })
                .success(function (resp) {
                    deffered.resolve(resp)
                })
                .error(function () {
                    deffered.resolve({ success: false });
                });

            return deffered.promise;
        },
        isAuthenticated: function () {
            return fact.user != null;
        },
        getUser: function () {
            return fact.user;
        },
        getCurrentUser: function () {
            var deffered = $q.defer();

            $http.get('/Account/GetCurrentUser', {})
            .success(function (data) {
                fact.user = data.user;
                deffered.resolve(data.user);
            })
            .error(function (data) {
                fact.user = null;
                deffered.resolve(null);
            });

            return deffered.promise;
        }
    }

    return fact;
}]);