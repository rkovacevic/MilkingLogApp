angular.module('milkApp', ['ngInputDate'])
.controller('mainController', function ($scope, $http, $timeout) {

    var resetMilkingLog = function () {
        $scope.milkingLog = {
            date: Date.now(),
            cycle6: false,
            cycle12: false,
            cycle16: false,
            cycle21: false,
            amount: 0
        };
    }

    var fetchMilkingLogs = function () {
        $http.get('/api/MilkingLogs').success(function (milkingLogs) {
            $scope.milkingLogs = milkingLogs;
        })
    }

    resetMilkingLog();
    fetchMilkingLogs();

    var clearMessages = function () {
        $timeout(function () {
            $scope.successMessage = null;
            $scope.errorMessage = null;
        }, 3000);
    }
    var success = function (message) {
        $scope.successMessage = message;
        clearMessages();
    }

    var error = function (message) {
        $scope.errorMessage = message;
        clearMessages();
    }

    $scope.submit = function () {
        if (!$scope.milkingLog.cycle6 && !$scope.milkingLog.cycle12 &&
            !$scope.milkingLog.cycle16 && !$scope.milkingLog.cycle21) {
            error("Select at least one cycle");
            return;
        }
        var milkingLogForSending = angular.copy($scope.milkingLog);
        milkingLogForSending.date = moment($scope.milkingLog.date).startOf('day').format();
        $scope.milkingLogs = null;
        $http.post(
               '/api/MilkingLogs',
               milkingLogForSending,
               {
                   headers: {
                       'Content-Type': 'application/json'
                   }
               }
           ).success(function (data) {
               resetMilkingLog();
               fetchMilkingLogs();
               success("Added milking log");
           }).error(function (data) {
               error(data.Message);
           });
    }
});;

