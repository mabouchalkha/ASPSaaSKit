angular.module('starterKit').directive('highlightActive', [
  function() {
    return {
      restrict: "A",
      controller: [
        '$scope', '$element', '$attrs', '$location', function($scope, $element, $attrs, $location) {
          var highlightActive, links, path;
          links = $element.find('a');
          path = function() {
            return $location.path();
          };
          highlightActive = function(links, path) {
            path = '#' + path;
            return angular.forEach(links, function(link) {
              var $li, $link, href;
              $link = angular.element(link);
              $li = $link.parent('li');
              href = $link.attr('href');
              
              if ($li.hasClass('active')) {
                    $li.removeClass('active');
              }
             
              if (path.indexOf(href) === 0) {
                  if (!$li.hasClass('home_link') && path !== '#/') {
                    return $li.addClass('active');   
                  }
              }
              
              if ($li.hasClass('home_link') && path === '#/') {
                  return $li.addClass('active'); 
              }
              
            });
          };
          highlightActive(links, $location.path());
          return $scope.$watch(path, function(newVal, oldVal) {
            if (newVal === oldVal) {
              return;
            }
            return highlightActive(links, $location.path());
          });
        }
      ]
    };
  }
]);
