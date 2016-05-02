(function() {
    "use strict";
    angular.module("app").controller("BooksController", function($scope, booksFactory) {

        $scope.ready = false;
        booksFactory.getBooks().then(function(response) {
            $scope.books = response;
            $scope.ready = true;
        }, function() {
            console.log("Failed to retrieve book list!");
        }

        );

        $scope.addNewBook = function() {
            $scope.books.push(
                {
                    title: $scope.newBook.title,
                    description: $scope.newBook.description,
                    price: 11.11,
                    inStock: true,
                    imageUrl: $scope.newBook.imageUrl
                }
            );
        };
        

    });
})();