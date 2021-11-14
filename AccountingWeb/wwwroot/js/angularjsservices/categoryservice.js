angular.module('AccountingApp', []).
    controller('CategoryController', function ($scope, $timeout, $http, $location, $window) {
        //#regin Globalvariabls
        $scope.model = {
            matGoodsCode: "",
            matGoodsName: "",
            customCode: "",
            matGoodsCatId: "",
            unit: "",
            warrantyTime: "0",
            minimumStock: "",
            purchasePrice: "",
            salesPrice: "",
            repositoryId: "",
            taxRate: "",
            expanceAccountId: "",
            repositoryAccountId: "",
            purchaseDiscountRate: "",
            revenueAccountId: "",
            salesDiscountRate:"" ,
            NhomisSalesDiscountPolicy: false,
            itemSource:""
        };

        $scope.customCodeList = [];
        $scope.matGoodsCatList = [];
        $scope.warrantyTimeList = warrantyTimeList;
        $scope.repositoryList = [];
        $scope.taxRateList = [];
        $scope.expanceAccountList = [];
        $scope.repositoryAccountList = [];
        $scope.revenueAccountList = [];
        $scope.NhomList = [];

        //Index Table
        $scope.IndexTable = [];

        //#endregin


        //#regin FrontEnd
       

        $scope.AllClear = function () {
            $scope.model = {
                matGoodsCode: "",
                matGoodsName: "",
                customCode: "",
                matGoodsCatId: "",
                unit: "",
                warrantyTime: "0",
                minimumStock: "",
                purchasePrice: "",
                salesPrice: "",
                repositoryId: "",
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
        //#endregin



        //#regin Operations

        $scope.OnInitIndexTable = function () {
            var url = '../ApiCategory/GetIndexTable';
            $http({
                method: 'GET',
                url: url,
                /*headers: headers,*/
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.IndexTable = data.categoryEntityModelList;
                }

            }, function (response) {
                console.log(response);
            });
        }

        $scope.OnInit = function () {
            var url = '../ApiCategory/GetInititalData';
            $http({
                method: 'GET',
                url: url,
                /*headers: headers,*/
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.matGoodsCatList = data.materialGoodsCategoryList;
                    $scope.repositoryList = data.repositoryList;
                }

            }, function (response) {
                console.log(response);
            });
        }

        $scope.Save = function (isClose) {
            
            var model = $scope.model;
            console.log(isClose);
            console.log(model);

            var url = '../ApiCategory/SaveCategory';
            $http({
                method: 'POST',
                url: url,
                data: model
                /*headers: headers,*/
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
                                    window.location.href = "../Home/CategoryIndex";
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
        //#endregin
    });

function ReRenderSelect2() {
    $('select').select2({
        theme: 'classic'
    });
}