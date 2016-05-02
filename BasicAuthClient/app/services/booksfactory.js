(function() {
    "use strict";
    angular.module("app")
        .factory("booksFactory", function($http) {
            return {
                getBooks: function() {
                    var booksUrl = "http://elibrarydemo.azurewebsites.net/api/library/books";
                    return $http.get(booksUrl)
                        .then(function(r) {
                            return r.data.results;
                        });

                }
            };
        });

})();