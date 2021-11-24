angular.module('AccountingApp', []).
    controller('AccountingObjectBankAccountController', function ($scope, $timeout, $http, $location, $window) {

        $scope.model = {
              id: null
            , accountingObjectID: null
            , bankAccount: null
            , bankName: null
            , bankBranchName: null
            , accountHolderName: null
            , orderPriority: null
            , isSelect: null
        };

        $scope.AllClear = function () {
            $scope.model = {
                              id: null
                            , accountingObjectID: null
                            , bankAccount: null
                            , bankName: null
                            , bankBranchName: null
                            , accountHolderName: null
                            , orderPriority: null
                            , isSelect: null
                          };
        }

        $scope.OnInit = function (id) {
            $scope.model.accountingObjectID = id;          
        }
             

        $scope.GetAll = function (id) {

            var url = '/ApiAccountingObjectBankAccount/GetAll/'+id;
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.AllDataList = data.allDataList;
                }

            }, function (response) {
                console.log(response);
            });
        }

        $scope.Save = function (isClose) {
            debugger;
            var model = $scope.model;      
            var url = '/ApiAccountingObjectBankAccount/Save';
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
                            debugger;
                            if (isClose === 1) {            
                                window.location.href = "/AccountingObjectBankAccount/index/" + model.accountingObjectID;
                            }
                            else {
                                window.location.href = "/AccountingObjectBankAccount/create/" + model.accountingObjectID;
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
            var url = '/ApiAccountingObjectBankAccount/GetById/' + id;
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.model.id = data.singleData.id;
                    $scope.model.accountingObjectID = data.singleData.accountingObjectID;
                    $scope.model.bankAccount = data.singleData.bankAccount;
                    $scope.model.bankName = data.singleData.bankName;
                    $scope.model.bankBranchName = data.singleData.bankBranchName;
                    $scope.model.accountHolderName = data.singleData.accountHolderName;
                    $scope.model.isSelect = data.singleData.isSelect
                    $scope.model.orderPriority = data.singleData.orderPriority;                }

            }, function (response) {
                console.log(response);
            });
        }

        $scope.Update = function (isClose) {

            var model = $scope.model;

            var url = '/ApiAccountingObjectBankAccount/Update';
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
                                window.location.href = "/AccountingObjectBankAccount/Index/"+model.accountingObjectID;                           
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


        $scope.DeleteById = function (id,accountObjectID) {
            debugger;

            var url = '/ApiAccountingObjectBankAccount/DeleteById/' + id;
            $http({
                method: 'POST',
                url: url
            }).then(function (response) {
                if (response.status === 200) {
                    if (response.data.ok) {
                        $scope.AllClear();

                        swal({
                            icon: "success",
                            title: "Delete!",
                            text: "Delete Success"
                        }).then((result) => {
                            window.location.href = "/AccountingObjectBankAccount/Index/" + accountObjectID;
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



        $scope.ExportToExcel = function (id) {
            var url = '/ApiAccountingObjectBankAccount/ExportExcel/'+id;
            window.open(url, '_blank');
        }
              
    });