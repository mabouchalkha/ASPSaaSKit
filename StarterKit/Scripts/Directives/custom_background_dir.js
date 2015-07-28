angular.module("starterKit").directive('customBackground', ['$location', function ($location) {
  return {
    restrict: "A",
    link: function(scope, element, attrs) {
        var path = function () {
            return $location.path();
        }
        
        var addBg = function (path) {
            element.removeClass('body-home body-special body-tasks body-lock');
            switch (path) {
                case '/404':
                case '/500':
                case '/login':
                case '/confirmemail':
                case '/resetPassword':
                case '/twofactor':
                case '/lock-screen':
                    $('#navigation_container').hide();
                    $('#nav-container').hide();
                    $('#content').removeClass('col-md-10');
                    return element.addClass('body-special body-lock');
                default:
                    $('#navigation_container').show();
                    $('#nav-container').show();
                    $('#content').addClass('col-md-10');
                    return element.addClass('body-home');
              }
            };
            
            addBg($location.path());
            
            scope.$watch(path, function (newValue, oldValue) {
                if (newValue === oldValue) return;
                
                return addBg($location.path());
            })
        }
    }
}]);
