(function () {
    "use strict";

    angular.module("app", ["http-auth-interceptor", "ngRoute", "ngCookies"]);



    angular.module("app").config(function ($routeProvider, $httpProvider, $locationProvider) {


        $httpProvider.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';

        // use the HTML5 History API
        $locationProvider.html5Mode(true);

        $routeProvider.when("/books", {
            templateUrl: "app/partials/books.html",
            controller: "BooksController"
        })
            .when("/orders", {
                templateUrl: "app/partials/orders.html",
                controller: "OrdersController"
            })
            .otherwise({
                redirectTo: "/"
            });

    });

})();