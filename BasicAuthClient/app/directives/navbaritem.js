(function () {
    "use strict";

    angular.module("app").directive('navbarItem', function ($location) {
        return {
            template: '<li><a ng-href="{{route}}" ng-transclude></a></li>',
            restrict: 'E',
            transclude: true,
            replace: true,
            scope: { route: '@route' },
            link: function (scope, element, attributes, controller) {
                scope.$on('$routeChangeSuccess', function () {
                    var path = $location.path();
                    var isSamePath = path == scope.route;
                    var isSubpath = path.indexOf(scope.route + '/') == 0;
                    if (isSamePath || isSubpath) {
                        element.addClass('active');
                    } else {
                        element.removeClass('active');
                    }
                });
            }
        };
    });
})();