angular.module('GiiftCatalogModule')
    .controller('GiiftCatalogModule.helloWorldController', ['$scope', 'GiiftCatalogModule.webApi', function ($scope, api) {
        var blade = $scope.blade;
        blade.title = 'GiiftCatalogModule';


        blade.refresh();
    }]);
