(function () {
    "use strict";
    angular.module("app").controller("OrdersController", function ($scope, ordersFactory) {

        $scope.ready = false;
        ordersFactory.getOrders().then(function (response) {
            $scope.orders = response;
            $scope.ready = true;
        }, function () {
            console.log("Failed to retrieve book list!");
        });
    });
})();