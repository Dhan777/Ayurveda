
function isNumber(evt) {
evt = (evt) ? evt : window.event;
var charCode = (evt.which) ? evt.which : evt.keyCode;
if (charCode > 31 && (charCode < 48 || charCode > 57)) {
return false;
}
return true;
}

function onlyAlphabets(e, t) {
try {
if (window.event) {
var charCode = window.event.keyCode;
}
else if (e) {
var charCode = e.which;
}
else { return true; }
if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 32))
return true;
else
return false;
}
catch (err) {
alert(err.Description);
}
}

function onlyAlphaNum(e, t) {
debugger;
try {
if (window.event) {
var charCode = window.event.keyCode;
}
else if (e) {
var charCode = e.which;
}
else { return true; }
if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 32) || ( (charCode >= 48 && charCode < 57)))
return true;
else
return false;
}
catch (err) {
alert(err.Description);
}
}

function ValidateEmail(email) {
var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
return regex.test(email);
};

function ValidatePanCard(panNo) {
var regpan = /^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$/;
return regpan.test(panNo);
}

function ValidateMobileNo(MobileNo) {
var regMobileNo = /^(\+\d{1,3}[- ]?)?\d{10}$/;
return regMobileNo.test(MobileNo);
}
