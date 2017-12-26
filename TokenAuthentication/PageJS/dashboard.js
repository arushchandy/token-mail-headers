"use strict";


var getDashboardData = function () {
    var token = getAccessToken();
    var testid = "testid";
    var param = JSON.stringify({ "testid": testid });
    $.ajax({
        type: "POST",
        data: param,
        url: "/api/user/getdashboard",
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        success: function (response) {
            var result = (response);
            $('#main-data').html(response);
        },
        error: function (event, jqxhr, settings, thrownError) {
            if (event.status !== undefined && (event.status === "403" || event.status === 403)) {
                alert('Please log in again.');
                LogUserOutAndRedirect();
            } 
        }
    });
};


var LogUserOutAndRedirect = function () {
    window.location.href = '/index.html';
};

var getAccessToken = function () {
    var accessToken = localStorage.getItem('accesstoken');
    if (accessToken !== undefined && accessToken !== "") {
        return accessToken;
    } else {
        window.location.href = '/index.html';
    }
}

$(document).ready(function () {
    getDashboardData();
});
