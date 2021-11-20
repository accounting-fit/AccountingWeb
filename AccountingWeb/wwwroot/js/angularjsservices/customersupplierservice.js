angular.module('AccountingApp', []).
    controller('CustomerSupplierController', function ($scope, $timeout, $http, $location, $window) {
        $scope.model = {
                              accountObjectGroupID: null
                            , accountingObjectCategory: null
                            , accountingObjectCode: null
                            , accountingObjectName: null
                            , address: null
                            , agreementSalary: null
                            , bankAccount: null
                            , bankName: null
                            , branchID: null
                            , contactAddress: null
                            , contactEmail: null
                            , contactHomeTel: null
                            , contactMobile: null
                            , contactName: null
                            , contactOfficeTel: null
                            , contactSex: null
                            , contactTitle: null
                            , departmentID: null
                            , description: null
                            , dueTime: null
                            , email: null
                            , employeeBirthday: null
                            , familyDeductionAmount: null
                            , fax: null
                            , id: null
                            , identificationNo: null
                            , insuranceSalary: null
                            , isActive: null
                            , isEmployee: false
                            , isInsured: null
                            , isLabourUnionFree: null
                            , isUnofficialStaff: null
                            , issueBy: null
                            , issueDate: null
                            , maximizaDebtAmount: null
                            , numberOfDependent: null
                            , objectType: null
                            , paymentClauseID: null
                            , salaryCoefficient: null
                            , scaleType: null
                            , taxCode: null
                            , tel: null
                            , website: null
        };

        var scaleTypeList = [
                                  { id: "1", text: "individual" }
                                , { id: "0", text: "organization" }
                             ]
        $scope.scaleTypeList = scaleTypeList;

        var objectTypeList = [
                                 { id: "0", text: "customer" }
                                ,{ id: "1", text: "supplier" }
                                ,{ id: "2", text: "customer/supplier" }
                                ,{ id: "3", text: "other" }
                             ]

        $scope.objectTypeList = objectTypeList;

        var genderList = [
            { id: "0", text: "Male" }
          , { id: "1", text: "Female" }
        ]
        $scope.genderList = genderList;


        $scope.AllClear = function () {
            $scope.model = {
                accountObjectGroupID: null
                , accountingObjectCategory: null
                , accountingObjectCode: null
                , accountingObjectName: null
                , address: null
                , agreementSalary: null
                , bankAccount: null
                , bankName: null
                , branchID: null
                , contactAddress: null
                , contactEmail: null
                , contactHomeTel: null
                , contactMobile: null
                , contactName: null
                , contactOfficeTel: null
                , contactSex: null
                , contactTitle: null
                , departmentID: null
                , description: null
                , dueTime: null
                , email: null
                , employeeBirthday: null
                , familyDeductionAmount: null
                , fax: null
                , id: null
                , identificationNo: null
                , insuranceSalary: null
                , isActive: null
                , isEmployee: false
                , isInsured: null
                , isLabourUnionFree: null
                , isUnofficialStaff: null
                , issueBy: null
                , issueDate: null
                , maximizaDebtAmount: null
                , numberOfDependent: null
                , objectType: null
                , paymentClauseID: null
                , salaryCoefficient: null
                , scaleType: null
                , taxCode: null
                , tel: null
                , website: null
            };
        }
        $('#thirdDiv').hide();
        $scope.divShowHide = function () {
            if ($scope.model.scaleType == '0') {
                $('#thirdDiv').show();
            }         
            else {
                $('#thirdDiv').hide();
            }
        };

        GeneratedCode();
        //GeneratedTaxCode();
        GetAllAccountingObjectGroup();
        GetAllPaymentClause();

        $scope.allDataList = [];
        
        $scope.GetAll = function () {
            debugger;
            var url = '/ApiCustomerSupplier/GetAll';
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


        $scope.OnInit = function () {
            $scope.model.scaleType = "1"
        }
        $scope.Save = function (isClose) {
            debugger;
            var model = $scope.model;    
            var url = '/ApiCustomerSupplier/Save';
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
                                window.location.href = "/CustomerSupplier/Index";
                            }
                            else {
                                window.location.href = "/CustomerSupplier/Create";
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
            debugger;           
            var url = '/ApiCustomerSupplier/GetById/' + id;
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;    
                        $scope.model.accountObjectGroupID = data.singleData.accountObjectGroupID;
                        $scope.model.accountingObjectCategory = data.singleData.accountingObjectCategory;
                        $scope.model.accountingObjectCode = data.singleData.accountingObjectCode;
                        $scope.model.accountingObjectName = data.singleData.accountingObjectName;
                        $scope.model.address = data.singleData.address;
                        $scope.model.agreementSalary = data.singleData.agreementSalary;
                        $scope.model.bankAccount = data.singleData.bankAccount;
                        $scope.model.bankName = data.singleData.bankName;
                        $scope.model.branchID = data.singleData.branchID;
                        $scope.model.contactAddress = data.singleData.contactAddress;
                        $scope.model.contactEmail = data.singleData.contactEmail;
                        $scope.model.contactHomeTel = data.singleData.contactHomeTel;
                        $scope.model.contactMobile = data.singleData.contactMobile;
                        $scope.model.contactName = data.singleData.contactName;
                        $scope.model.contactOfficeTel = data.singleData.contactOfficeTel;
                        $scope.model.contactSex = data.singleData.contactSex;
                        $scope.model.contactTitle = data.singleData.contactTitle;
                        $scope.model.departmentID = data.singleData.departmentID;
                        $scope.model.description = data.singleData.description;
                        $scope.model.dueTime = data.singleData.dueTime;
                        $scope.model.email = data.singleData.email;
                        $scope.model.employeeBirthday = new Date(data.singleData.employeeBirthday);
                        $scope.model.familyDeductionAmount = data.singleData.familyDeductionAmount;
                        $scope.model.fax = data.singleData.fax;
                        $scope.model.id = data.singleData.id;
                        $scope.model.identificationNo = data.singleData.identificationNo;
                        $scope.model.insuranceSalary = data.singleData.insuranceSalary;
                        $scope.model.isActive = data.singleData.isActive;
                        $scope.model.isEmployee = data.singleData.isEmployee;
                        $scope.model.isInsured = data.singleData.isInsured;
                        $scope.model.isLabourUnionFree = data.singleData.isLabourUnionFree;
                        $scope.model.isUnofficialStaff = data.singleData.isUnofficialStaff;
                        $scope.model.issueBy = data.singleData.issueBy;
                        $scope.model.issueDate = new Date(data.singleData.issueDate);
                        $scope.model.maximizaDebtAmount = data.singleData.maximizaDebtAmount;
                        $scope.model.numberOfDependent = data.singleData.numberOfDependent;
                        $scope.model.objectType = data.singleData.objectType;
                        $scope.model.paymentClauseID = data.singleData.paymentClauseID;
                        $scope.model.salaryCoefficient = data.singleData.salaryCoefficient;
                        $scope.model.scaleType = data.singleData.scaleType;
                        $scope.model.taxCode = data.singleData.taxCode;
                        $scope.model.tel = data.singleData.tel;
                        $scope.model.website = data.singleData.website;
                        $scope.divShowHide();
                   
                   
                }

            }, function (response) {
                console.log(response);
            });
        }

        $scope.Update = function (isClose) {

            var model = $scope.model;

            var url = '/ApiCustomerSupplier/Update';
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
                            title: "Update!",
                            text: "Update Success"
                        }).then((result) => {

                            if (isClose === 1) {
                                window.location.href = "/CustomerSupplier/Index";
                            }
                            else {
                                window.location.href = "/CustomerSupplier/Create";
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

            var url = '/ApiCustomerSupplier/DeleteById/' + id;
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
                                window.location.href = "/CustomerSupplier/Index";
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
            var url = '/ApiCustomerSupplier/ExportExcel';
            window.open(url, '_blank');
        }


        function GeneratedCode () {         
            var url = '/ApiCustomerSupplier/GeneratedCode';
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.model.accountingObjectCode = data.generatedCode;
                }

            }, function (response) {
                console.log(response);
            });
        }

        function GeneratedTaxCode  () {
            debugger;
            var url = '/ApiCustomerSupplier/GeneratedTaxCode';
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.model.taxCode = data.generatedTaxCode;
                }

            }, function (response) {
                console.log(response);
            });
        }


        function GetAllAccountingObjectGroup () {
            var url = '/ApiCustomerSupplier/GetAllAccountingObjectGroup';
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.GetAllAccountingObjectGroup = data.getAllAccountingObjectGroup;
                }

            }, function (response) {
                console.log(response);
            });
        }

        function GetAllPaymentClause() {
            var url = '/ApiCustomerSupplier/GetAllPaymentClause';
            $http({
                method: 'GET',
                url: url,
            }).then(function (response) {
                console.log(response);
                if (response.status === 200) {
                    var data = response.data;
                    $scope.GetAllPaymentClause = data.getAllPaymentClause;
                }

            }, function (response) {
                console.log(response);
            });
        }


    });