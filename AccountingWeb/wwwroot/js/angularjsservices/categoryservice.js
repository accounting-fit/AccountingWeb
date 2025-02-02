﻿angular.module('AccountingApp', []).
    controller('CategoryController', function ($scope, $timeout, $http, $location, $window) {
    
        $scope.model = {
            matGoodsCode: "",
            matGoodsName: "",
            materialGoodsType:null,
            matGoodsCatId: "",
            unit: "",
            warrantyTime: "",
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
            isSalesDiscountPolicy: false,
            itemSource: ""
        };

       

        $scope.matGoodsCatList = [];

        $scope.warrantyTimeList = warrantyTimeList;

        $scope.repositoryList = [];
       
        var expanceAccountList = [
            { id: "154", text:"154" }
            , { id: "2411", text: "2411" }
            , { id: "2412", text:"2412" }
            , { id: "2413", text: "2413" }
            , { id: "631", text: "631" }
            , { id: "632", text: "632" }
            , { id: "6421", text: "6421" }
            , { id: "6422", text: "6422" }
                           ]
        $scope.expanceAccountList = expanceAccountList;
        var repositoryAccountList = [
            { id: "551", text:"551" }
            ,{ id: "552", text:"552" }
            ,{ id: "553", text:"553" }
            , { id: "554", text: "554" }
            , { id: "557", text: "557" }
            , { id: "558", text: "558" }
            , { id: "557", text: "557" }
                                 ]
        $scope.repositoryAccountList = repositoryAccountList;

        var revenueAccountList = [
             { id: "5111", text:"5111" }
            , { id: "5112", text:"5112" }
            , { id: "5113", text:"5113" }
            , { id: "5118", text: "5118" }
            , { id: "515", text: "515" }
                          ]
        $scope.revenueAccountList = revenueAccountList;

        var NhomList = [
              { id: "1", text:"1" }
            , { id: "101", text:"101" }
            , { id: "102", text:"102" }
            , { id: "1021", text: "1021" }
            , { id: "1022", text: "1022" }
            , { id: "103", text: "103" }
            , { id: "104", text: "104" }
            , { id: "1042", text: "1042" }
        ]
        $scope.NhomList = NhomList;

       

        $scope.AllClear = function () {
            $scope.model = {
                matGoodsCode: "",
                matGoodsName: "",
                materialGoodsType: null,
                matGoodsCatId: "",
                unit: "",
                warrantyTime: "",
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
                isSalesDiscountPolicy: false,
                itemSource: ""
            };
        }

        GetAllMaterialGoodsCategory();
        GetAllRepository();
        GetAllMaterialGoodsTypeList();
        GetAllTaxRateList();
       

        $scope.GetAll = function () {
            var url = '/ApiCategory/GetAll';
            $http({
                method: 'GET',
                url: url,                    }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    debugger;
                    var allData = data.categoryEntityModelList;                                      
                    $scope.GetAll = allData;
                }

            }, function (response) {
                console.log(response);
            });
        }

        $scope.OnInit = function () {
            $scope.model.materialGoodsType="1"
             $scope.model.warrantyTime="0"
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
                    $scope.model.materialGoodsType = data.singleData.materialGoodsType.toString();
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

        function GetAllMaterialGoodsTypeList() {
            var url = '/ApiCategory/GetAllMaterialGoodsTypeList';
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.materialGoodsTypeList = data.getAllMaterialGoodsTypeList;
                }

            }, function (response) {
                console.log(response);
            });
        }

        function GetAllTaxRateList() {
            var url = '/ApiCategory/GetAllTaxRateList';
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.taxRateList = data.getAllTaxRateList;
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