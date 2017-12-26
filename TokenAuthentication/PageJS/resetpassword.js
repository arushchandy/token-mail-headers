"use strict";


var resetPasswordRequest = function (event) {
    clearErrors();
    var resetPassword = $('#reset-password').val();
    var confirmResetPassword = $('#reset-reenter-password').val();
    if (resetPassword === '' || confirmResetPassword === '') {
        setErrorMessageToLogin('All Fields Mandatory.');
    } else if (confirmResetPassword === resetPassword) {
        var processId = getUrlParameter("resetprocessid");
        resetPasswordRequestService(processId, confirmResetPassword);
    }
    return false;
};

var clearErrors = function () {
    $('.error-message').html('');
};

var setErrorMessageToLogin = function (msg) {
    $('#reset-error').html(msg);
};

var resetPasswordRequestService = function (username, password) {
    showOverlay();
    var saveDataObject = { "username": username, "password": password };
    $.ajax({
        type: "POST",
        url: "/api/user/resetpassword",
        data: JSON.stringify(saveDataObject),
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        success: function (response) {
            var result = (response);
            console.log(response);
            if (response !== undefined) {
                $('#reset-password').val('');
                $('#reset-reenter-password').val('');
                setErrorMessageToLogin("Password Changed.");
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


function getUrlParameter(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
};

$(function () {

    $('#resetpasswordBtn').on('click', function (event) {
        resetPasswordRequest();
        event.preventDefault();
    });

});
