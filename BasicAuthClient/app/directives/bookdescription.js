(function() {
    "use strict";
    
    angular.module("app").directive("bookDescription", function() {
        
        return {
            scope: true,
            templateUrl: "app/directives/bookdescription.html",
            controller: function($scope) {
                $scope.collapsed = false;
                $scope.descriptionCollapse = function() {
                    $scope.collapsed = true;
                }
                
                $scope.descriptionExpand = function() {
                    $scope.collapsed = false;
                };
            }
        };
    });
    
}) ();