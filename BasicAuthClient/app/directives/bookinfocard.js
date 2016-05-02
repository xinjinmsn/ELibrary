(function () {
    "use strict";

    angular.module("app").directive("bookInfoCard", function () {

        return {
            scope: {
                book: "=",
                initialCollapsed: "@collapsed"
            },
            templateUrl: 'app/directives/bookinfocard.html',
            controller: function ($scope) {
                $scope.collapsed = ($scope.initialCollapsed === "true");

                $scope.collapse = function () {
                    $scope.collapsed = !$scope.collapsed;
                };

            }
        };


    });


})();