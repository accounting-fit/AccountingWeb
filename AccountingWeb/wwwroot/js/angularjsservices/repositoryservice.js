angular.module('AccountingApp', []).
    controller('RepositoryController', function ($scope, $timeout, $http, $location, $window) {
        $scope.model = {
                         branchID: null
                        ,defaultAccount:null
                        ,description: null
                        ,id: null
                        ,isActive: null
                        ,repositoryCode:null
                        ,repositoryName: null
                      };

        $scope.AllClear = function () {
            $scope.model = {
                             branchID: null
                            ,defaultAccount: null
                            ,description: null
                            ,id: null
                            ,isActive: null
                            ,repositoryCode: null
                          };
        }

        $scope.allDataList = [];
        
        $scope.GetAll = function () {
            var url = '/ApiRepository/GetAll';
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

            var model = $scope.model;       

            var url = '/ApiRepository/Save';
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
                                window.location.href = "/Repository/Index";
                            }
                            else {
                                window.location.href = "/Repository/Create";
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
            var url = '/ApiRepository/GetById/' + id;
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.model.branchID = data.singleData.branchID;
                    $scope.model.defaultAccount = data.singleData.defaultAccount;
                    $scope.model.description = data.singleData.description;
                    $scope.model.id = data.singleData.id;
                    $scope.model.isActive = data.singleData.isActive;
                    $scope.model.repositoryCode = data.singleData.repositoryCode;
                    $scope.model.repositoryName = data.singleData.repositoryName;
                    
                }

            }, function (response) {
                console.log(response);
            });
        }

        $scope.Update = function (isClose) {

            var model = $scope.model;

            var url = '/ApiRepository/Update';
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
                               
                                window.location.href = "/Repository/Index";
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

            var url = '/ApiRepository/DeleteById/' + id;
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
                                window.location.href = "/Repository/Index";
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
            var url = '/ApiRepository/ExportExcel';
            window.open(url, '_blank');
        }
    });