angular.module("starterKit").service('overlay', [function () {
    return {
        show: function () {
            $('#overlay').show();
        },
        hide: function () {
            $('#overlay').hide();
        }
    }
}]);