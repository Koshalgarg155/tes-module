angular.module('GiiftCatalogModule')
    .controller('GiiftCatalogModule.helloWorldControllers', ['$scope', 'platformWebApp.bladeNavigationService', 'virtoCommerce.catalogModule.listEntries',
        function ($scope, bladeNavigationService, listEntries) {
            var blade = $scope.blade;
            var type = blade.productType ? 'CatalogProduct' : 'Category';

            function refresh() {
                $scope.linksCount = '...';

                var searchCriteria = {
                    objectIds: [blade.currentEntityId],
                    objectType: type,
                    take: 0
                };

                listEntries.searchlinks(searchCriteria, function (data) {
                    $scope.linksCount = data.totalCount;
                });
            }

            $scope.openLinksBlade = function () {
                var newBlade = {
                    id: 'linksListBlade',
                    currentEntity: blade.currentEntity,
                    type: type,
                    controller: 'virtoCommerce.catalogModule.linksListController',
                    template: 'Modules/$(Giift.GiiftCatalogModule)/Scripts/blades/links-list-status.tpl.html'
                };
                bladeNavigationService.showBlade(newBlade, blade);
            };

            refresh();
        }]);
