(function () {
    "use strict";

    angular.module("app").directive('logoutLink', function () {
        return {
            templateUrl: 'app/directives/logoutLink.html',
            restrict: 'E',
            replace: true,
            controller: function ($scope, $location, loginFactory) {
                $scope.isLoggedIn = function () {
                    return loginFactory.isLoggedIn();
                }

                $scope.logOut = function () {
                    loginFactory.logOut();
                    $location.path('/');
                }
            }
        }
    })


})();