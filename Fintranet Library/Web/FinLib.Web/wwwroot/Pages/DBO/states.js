altairApp
    .config([
    '$stateProvider',
    '$urlRouterProvider',
    '$locationProvider',
    function ($stateProvider, $urlRouterProvider, $locationProvider) {
        $stateProvider
            .state("restricted.management.categories", {
            url: "/categories",
            templateUrl: "/pages/DBO/categories/categoryView.html?ver=".concat(ver),
            controller: 'CategoryController',
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                            'ADM',
                            "/pages/DBO/categories/CategoryService.js?ver=".concat(ver),
                            "/pages/DBO/categories/CategoryController.js?ver=".concat(ver)
                        ]);
                    }]
            },
            data: {
                pageTitle: 'Categories'
            },
            ncyBreadcrumb: {
                label: 'Home'
            }
        })
            .state("restricted.management.books", {
            url: "/books",
            templateUrl: "/pages/DBO/books/bookView.html?ver=".concat(ver),
            controller: 'BookController',
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                            'ADM',
                            "/pages/DBO/categories/categoryService.js?ver=".concat(ver),
                            "/pages/DBO/books/BookService.js?ver=".concat(ver),
                            "/pages/DBO/books/BookController.js?ver=".concat(ver)
                        ]);
                    }]
            },
            data: {
                pageTitle: 'Books'
            },
            ncyBreadcrumb: {
                label: 'Home'
            }
        })
            .state("restricted.options", {
            abstract: true,
            url: '/options',
            template: '<ui-view/>',
        })
            .state("restricted.options.books-list", {
            url: "/books-list",
            templateUrl: "/pages/DBO/booksList/booksListView.html?ver=".concat(ver),
            controller: 'BooksListController',
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                            'ADM',
                            "/pages/DBO/categories/categoryService.js?ver=".concat(ver),
                            "/pages/DBO/books/BookService.js?ver=".concat(ver),
                            "/pages/DBO/booksList/BooksListController.js?ver=".concat(ver)
                        ]);
                    }]
            },
            data: {
                pageTitle: 'Books List'
            },
            ncyBreadcrumb: {
                label: 'Home'
            }
        })
            .state("restricted.options.book-borrowing", {
            url: "/book-borrowing",
            templateUrl: "/pages/DBO/bookBorrowing/bookBorrowingView.html?ver=".concat(ver),
            controller: 'BookBorrowingController',
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                            'ADM',
                            "/pages/DBO/categories/categoryService.js?ver=".concat(ver),
                            "/pages/DBO/books/BookService.js?ver=".concat(ver),
                            "/pages/SEC/users/userService.js?ver=".concat(ver),
                            "/pages/DBO/bookBorrowing/BookBorrowingService.js?ver=".concat(ver),
                            "/pages/DBO/bookBorrowing/BookBorrowingController.js?ver=".concat(ver)
                        ]);
                    }]
            },
            data: {
                pageTitle: 'Book Borrowing'
            },
            ncyBreadcrumb: {
                label: 'Home'
            }
        })
            .state("restricted.options.my-borrowing-history", {
            url: "/my-borrowing-history",
            templateUrl: "/pages/DBO/myBorrowingHistory/myBorrowingHistoryView.html?ver=".concat(ver),
            controller: 'MyBorrowingHistoryController',
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([
                            'ADM',
                            "/pages/DBO/categories/categoryService.js?ver=".concat(ver),
                            "/pages/DBO/books/BookService.js?ver=".concat(ver),
                            "/pages/SEC/users/userService.js?ver=".concat(ver),
                            "/pages/DBO/myBorrowingHistory/MyBorrowingHistoryService.js?ver=".concat(ver),
                            "/pages/DBO/myBorrowingHistory/MyBorrowingHistoryController.js?ver=".concat(ver)
                        ]);
                    }]
            },
            data: {
                pageTitle: 'My Borrowings History'
            },
            ncyBreadcrumb: {
                label: 'Home'
            }
        });
    }
]);
//# sourceMappingURL=states.js.map