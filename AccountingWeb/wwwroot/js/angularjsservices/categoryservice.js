angular.module('AccountingApp', []).
    controller('CategoryController', function ($scope, $timeout, $http, $location, $window) {
    
        $scope.model = {
            matGoodsCode: "",
            matGoodsName: "",
            customCode: "1",
            matGoodsCatId: "",
            unit: "",
            warrantyTime: "0",
            minimumStock: "",
            purchasePrice: "",
            salesPrice: "",
            repositoryId: null,
            taxRate: "",
            expanceAccountId: "",
            repositoryAccountId: "",
            purchaseDiscountRate: "",
            revenueAccountId: "",
            salesDiscountRate: "",
            NhomisSalesDiscountPolicy: false,
            itemSource: ""
        };

        var customList = [
                                 { id: "1", text: "Vat tu hang hoa" }
                                ,{ id: "2", text: "VTHH lap rap/thao do" }
                                ,{ id: "3", text: "Dich vu" }
                                ,{ id: "4", text: "Thanh pham" }
                                ,{ id: "5", text: "Chi la dien giai" }
                                ,{ id: "6", text: "Khac" }
                         ]

        $scope.customCodeList = customList;

        $scope.matGoodsCatList = [];

        $scope.warrantyTimeList = warrantyTimeList;

        $scope.repositoryList = [];
        var taxList = [
                         { id: "10", text: "10%" }
                        ,{ id: "20", text: "20%" }
                        ,{ id: "30", text: "30%" }
                        ,{ id: "40", text: "40%" }
                      ]
        $scope.taxRateList = taxList;
        var expanceAccountList = [
                                 { id: "551", text:"551" }
                                ,{ id: "552", text: "552" }
                                ,{ id: "553", text:"553" }
                                ,{ id: "554", text: "554" }                                
                           ]
        $scope.expanceAccountList = expanceAccountList;
        var repositoryAccountList = [
                                        { id: "551", text:"551" }
                                       ,{ id: "552", text:"552" }
                                       ,{ id: "553", text:"553" }
                                       ,{ id: "554", text:"554" }
                                 ]
        $scope.repositoryAccountList = repositoryAccountList;

        var revenueAccountList = [
                                      { id: "5511", text:"5511" }
                                    , { id: "5522", text:"5522" }
                                    , { id: "5533", text:"5533" }
                                    , { id: "5544", text:"5544" }
                          ]
        $scope.revenueAccountList = revenueAccountList;

        var NhomList = [
                             { id: "551", text:"551" }
                            ,{ id: "552", text:"552" }
                            ,{ id: "553", text:"553" }
                            ,{ id: "554", text:"554" }
        ]
        $scope.NhomList = NhomList;

       

        $scope.AllClear = function () {
            $scope.model = {
                matGoodsCode: "",
                matGoodsName: "",
                customCode: "1",
                matGoodsCatId: "",
                unit: "",
                warrantyTime: "0",
                minimumStock: "",
                purchasePrice: "",
                salesPrice: "",
                repositoryId: null,
                taxRate: "",
                expanceAccountId: "",
                repositoryAccountId: "",
                purchaseDiscountRate: "",
                revenueAccountId: "",
                salesDiscountRate: "",
                NhomisSalesDiscountPolicy: false,
                itemSource: ""
            };
        }

        GetAllMaterialGoodsCategory();
        GetAllRepository();
       

        $scope.GetAll = function () {
            var url = '/ApiCategory/GetAll';
            $http({
                method: 'GET',
                url: url,               
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.GetAll = data.categoryEntityModelList;
                }

            }, function (response) {
                console.log(response);
            });
        }

       

        $scope.Save = function (isClose) {
            
            var model = $scope.model;
            console.log(isClose);
            console.log(model);

            var url = '/ApiCategory/SaveCategory';
            $http({
                method: 'POST',
                url: url,
                data: model
               
            }).then(function (response) {
                if (response.status === 200) {
                    if (response.data.ok) {
                        $scope.AllClear();

                        swal({
                            icon: "success",
                            title: "Success!",
                            text: "Save Success"
                        }).then((result) => {
                            
                            if (isClose === 1) {
                                window.location.href = "/Home/CategoryIndex";
                            }
                            else {
                                window.location.href = "/Home/CategoryCreate";
                            }
                           });
                        
                    } else {
                        console.log(response);
                        swal({
                            icon: "error",
                            title: "Error!",
                            text: "Save fail",
                            confirmButtonText: "OK"
                        });
                    }
                } else {
                    console.log(response);
                    swal({
                        icon: "error",
                        title: "Error!",
                        text: "Save fail",
                        confirmButtonText: "OK"
                    });
                }
                
            }, function (response) {
                console.log(response);
            });
        }

        $scope.GetById = function (id) {
            var url = '/ApiCategory/GetById/' + id;
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                debugger;
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.model.id = data.singleData.id;
                    $scope.model.matGoodsCode = data.singleData.matGoodsCode;
                    $scope.model.matGoodsName = data.singleData.matGoodsName;
                    $scope.model.customCode = "1";
                    $scope.model.matGoodsCatId = data.singleData.matGoodsCatId;
                    $scope.model.unit = data.singleData.unit;
                    $scope.model.warrantyTime = data.singleData.warrantyTime;
                    $scope.model.minimumStock = data.singleData.minimumStock;
                    $scope.model.purchasePrice = data.singleData.purchasePrice;
                    $scope.model.salesPrice = data.singleData.salesPrice;
                    $scope.model.repositoryId = data.singleData.repositoryId;
                    $scope.model.taxRate = Math.ceil(data.singleData.taxRate).toString();
                    $scope.model.expanceAccountId = data.singleData.expanceAccountId;
                    $scope.model.repositoryAccountId = data.singleData.repositoryAccountId;
                    $scope.model.purchaseDiscountRate = data.singleData.purchaseDiscountRate;
                    $scope.model.revenueAccountId = data.singleData.revenueAccountId;
                    $scope.model.salesDiscountRate = data.singleData.salesDiscountRate;
                    $scope.model.isSalesDiscountPolicy = data.singleData.isSalesDiscountPolicy;
                    $scope.model.itemSource = data.singleData.itemSource;
                    $scope.model.isActive = data.singleData.isActive;
                }

            }, function (response) {
                console.log(response);
            });
        }

        $scope.Update = function (isClose) {

            var model = $scope.model;

            var url = '/ApiCategory/Update';
            $http({
                method: 'POST',
                url: url,
                data: model
            }).then(function (response) {
                if (response.status === 200) {
                    if (response.data.ok) {
                        swal({
                            icon: "success",
                            title: "Update!",
                            text: "Update Success"
                        }).then((result) => {
                            $scope.AllClear();
                            if (isClose === 1) {

                                window.location.href = "/Home/CategoryIndex";
                            }
                        });

                    } else {
                        console.log(response);
                        swal({
                            icon: "error",
                            title: "Error!",
                            text: "Update fail",
                            confirmButtonText: "OK"
                        });
                    }
                } else {
                    console.log(response);
                    swal({
                        icon: "error",
                        title: "Error!",
                        text: "Update fail",
                        confirmButtonText: "OK"
                    });
                }

            }, function (response) {
                console.log(response);
            });
        }

        $scope.DeleteById = function (id) {
            debugger;

            var url = '/ApiCategory/DeleteById/' + id;
            $http({
                method: 'POST',
                url: url
            }).then(function (response) {
                if (response.status === 200) {
                    if (response.data.ok) {


                        swal({
                            icon: "success",
                            title: "Delete!",
                            text: "Delete Success"
                        }).then((result) => {
                            $scope.AllClear();
                            window.location.href = "/Home/CategoryIndex";
                        });

                    } else {
                        console.log(response);
                        swal({
                            icon: "error",
                            title: "Error!",
                            text: "Delete fail",
                            confirmButtonText: "OK"
                        });
                    }
                } else {
                    console.log(response);
                    swal({
                        icon: "error",
                        title: "Error!",
                        text: "Delete fail",
                        confirmButtonText: "OK"
                    });
                }

            }, function (response) {
                console.log(response);
            });
        }

        $scope.ExportToExcel = function () {
            var url = '../ApiCategory/ExportExcel';
            window.open(url,'_blank');
        }
       

        function GetAllMaterialGoodsCategory() {
            var url = '/ApiCategory/GetAllMaterialGoodsCategory';
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.GetAllMaterialGoodsCategory = data.getAllMaterialGoodsCategory;
                }

            }, function (response) {
                console.log(response);
            });
        }


        function GetAllRepository() {
            var url = '/ApiCategory/GetAllRepository';
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.GetAllRepository = data.getAllRepository;
                }

            }, function (response) {
                console.log(response);
            });
        }

    });

function ReRenderSelect2() {
    $('select').select2({
        theme: 'classic'
    });
}