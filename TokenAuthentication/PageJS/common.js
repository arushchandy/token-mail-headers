"use strict";



var hideOverlay = function () {
    var isHidden = $('#overlay').hasClass('hideEle');
    if (!isHidden) {
        $('#overlay').addClass('hideEle');
    }
};



var showOverlay = function () {
    var isHidden = $('#overlay').hasClass('hideEle');
    if (isHidden) {
        $('#overlay').removeClass('hideEle');
    }
};


var hideErrorMessage = function () {
    var isHidden = $('#main-error-message').hasClass('hideEle');
    if (!isHidden) {
        $('#main-error-message').addClass('hideEle');
    }
};


var clearErrors = function () {
    $('.error-message').html('');
};
