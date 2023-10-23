// Call this to register your module to main application
var moduleName = 'GiiftCatalogModule';

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleName);
}

angular.module(moduleName, [])
    .config(['$stateProvider',
        function ($stateProvider) {
            $stateProvider
                .state('workspace.GiiftCatalogModuleState', {
                    url: '/GiiftCatalogModule',
                    templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
                    controller: [
                        'platformWebApp.bladeNavigationService',
                        function (bladeNavigationService) {
                            var newBlade = {
                                id: 'blade1',
                                controller: 'GiiftCatalogModule.helloWorldController',
                                template: 'Modules/$(Giift.GiiftCatalogModule)/Scripts/blades/hello-world.html',
                                isClosingDisabled: true,
                            };
                            bladeNavigationService.showBlade(newBlade);
                        }
                    ]
                });
        }
    ])
    .run(['platformWebApp.widgetService', '$state', 'platformWebApp.toolbarService', 'platformWebApp.metaFormsService', 'platformWebApp.ui-grid.extension', 'giiftCatalogService',
        function (widgetService, $state, toolbarService, metaFormsService, gridOptionExtension, giiftCatalogService) {
            //Register module in main menu
            var bladesCatalogCommand = {
                name: 'catalog.commands.update-link-status',
                icon: 'fa fa-upload',
                index: 10,
                executeMethod: function (blade) {
                    var selectedItems = blade.$scope.gridApi.selection.getSelectedRows();
                    var resultList = [];
                    angular.forEach(selectedItems, function (product) {
                        var filteredLink = product.links.filter(function (link) {
                            if (link.catalogId === blade.catalogId && (!blade.categoryId || link.categoryId === blade.categoryId)) {
                                    return link;
                                } 
                            });

                            if (filteredLink) {
                                resultList.push({
                                    catalogId: filteredLink[0].catalogId,
                                    categoryId: filteredLink[0].categoryId,
                                    listEntryId: product.id,
                                    isItemRelationActive: filteredLink[0].isItemRelationActive ? false : true,
                                });
                            }
                    });
                    giiftCatalogService.updateLinks(resultList).then(x =>
                        blade.refresh()
                    );
                },
                canExecuteMethod: function (blade) {
                    return blade.$scope.gridApi && blade.$scope.gridApi.selection.getSelectedCount() > 0;
                },
            };
            console.log($state)
            toolbarService.register(bladesCatalogCommand, 'virtoCommerce.catalogModule.categoriesItemsListController');
            console.log(gridOptionExtension);
            metaFormsService.registerMetaFields("productDetail2",[
                    {
                        name: "links",
                        title: "catalog.blades.item-detail.labels.linkStatus",
                        colSpan:6,
                        templateUrl:"Modules/$(Giift.GiiftCatalogModule)/Scripts/widgets/current-link-status.html"
                    }
            ]);
            var itemsLinksWidget = {
                controller: 'GiiftCatalogModule.helloWorldControllers',
                template: 'Modules/$(Giift.GiiftCatalogModule)/Scripts/widgets/hello-world.html'
            };
            widgetService.registerWidget(itemsLinksWidget, 'itemDetail');
            gridOptionExtension.tryExtendGridOptions("link-list-icon.cell.html", function (gridOptions) {
                var customColumnDefs = [
                    { name: 'newField', displayName: 'orders.blades.customerOrder-list.labels.newField', width: '***' }
                ];

                gridOptions.columnDefs = _.union(gridOptions.columnDefs, customColumnDefs);
            });

            gridOptionExtension.registerExtension("link-list-icon.cell.html", function (gridOptions) {
                var customColumnDefs = [
                    { name: 'newField', displayName: 'orders.blades.customerOrder-list.labels.newField', width: '***' }
                ];

                gridOptions.columnDefs = _.union(gridOptions.columnDefs, customColumnDefs);
            });
        }
    ]);

