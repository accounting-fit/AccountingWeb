angular.module('AccountingApp', []).
    controller('AccountingObjectBankAccountController', function ($scope, $timeout, $http, $location, $window) {
        $scope.model = {
              id: null
            , accountingObjectId: null
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
                            , accountingObjectId: null
                            , bankAccount: null
                            , bankName: null
                            , bankBranchName: null
                            , accountHolderName: null
                            , orderPriority: null
                            , isSelect: null
                          };
        }

        $scope.OnInit = function (id) {
            $scope.model.accountingObjectId = id;          
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
                                window.location.href = "/CustomerSupplier/Index";
                            }
                            else {
                                window.location.href = "/AccountingObjectBankAccount/create/" + model.accountingObjectId;
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
              
    });