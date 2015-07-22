angular.module("starterKit").service('notif', ['toastr', '$rootScope', 'overlay', function (toastr, $rootScope, overlay) {
    var _toasts = [];
    var _options = {
        regular: {
            "allowHtml": true,
            "positionClass": "toast-top-right",
            "stimeOut": "3600",
            "extendedTimeOut": "3600",
            "closeButton": true
        }
    }
    var logIt = function (title, message, type) {
        if (type == 'wait') {
            overlay.show();
            return null;
        }
        else {
            toastr.options = _options.regular;
        }

        return toastr[type](message, title, toastr.options);
    };
    return {
        info: function (message, title) {
            return logIt(message, title, 'info');
        },
        warning: function (message, title) {
            return logIt(message, title, 'warning');
        },
        success: function (message, title) {
            return logIt(message, title, 'success');
        },
        error: function (message, title) {
            return logIt(title || 'Oups! An error occured', message, 'error');
        },
        wait: function () {
            return logIt(null, null, 'wait');
        },
        clear: function (toast) {
            overlay.hide();
        },
    };
}
]);