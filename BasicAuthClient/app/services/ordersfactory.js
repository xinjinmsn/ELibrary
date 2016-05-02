(function() {
    "use strict";
    angular.module("app")
        .factory("ordersFactory", function($http) {
            return {
                getOrders: function() {
                    var ordersUrl = "http://elibrarydemo.azurewebsites.net/api/user/orders";
                    return $http.get(ordersUrl)
                        .then(function(r) {
                            return r.data;
                        });

                }
            };
        });

})();