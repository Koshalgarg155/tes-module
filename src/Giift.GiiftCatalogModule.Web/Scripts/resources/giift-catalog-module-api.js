var giiftCatalogService = angular.module('GiiftCatalogModule')
giiftCatalogService.service('giiftCatalogService', ['$http', function ($http, $localStorage) {
        return {
            updateLinks: function (result) {
                return $http.put('api/listentrylink/update', result);
            }
        }
    }]);
