"use strict";


var resetPasswordRequest = function (event) {
    clearErrors();
    var userId = $('#forgot-userid').val();
    if (userId === '') {
        setErrorMessageToLogin('User email is mandatory.');
    } else {
        resetPasswordRequestService(userId, "");
    }
    return false;
};

var clearErrors = function () {
    $('.error-message').html('');
};

var setErrorMessageToLogin = function (msg) {
    $('#forgot-error').html(msg);
};

var resetPasswordRequestService = function (username, password) {
    showOverlay();
    var saveDataObject = { "username": username, "password": password };
    $.ajax({
        type: "POST",
        url: "/api/user/forgotpassword",
        data: JSON.stringify(saveDataObject),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            var result = (response);
            console.log(response);
            if (response !== undefined) {
                setErrorMessageToLogin("Please check email.");
            }
            hideOverlay();
        },
        error: function (response) {
            console.log(response);
            setErrorMessageToLogin("Error Occured.");
            hideOverlay();

        }
    });
};


$(function () {

    $('#forgotUserBtn').on('click', function (event) {
        resetPasswordRequest();
        event.preventDefault();
    });

});