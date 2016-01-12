
var TelRegExp = /^(13[0-9]|15[012356789]|17[0678]|18[0-9]|14[57])[0-9]{8}$/;

var prepare_register_page = function () {
    console.log(123);
    $("button[type='submit']").click(function () {
        if (!TelRegExp.test($("input[name='Tel']").val())) {
            alert("请输入格式正确的手机号码");
        }
        else {
            $("#register_form").submit();
        }
        return false;
    });
};

var prepare_edit_page = function () {
    $("button[type='submit']").click(function () {
        console.log($("input[name='Tel']").val());
        if (!TelRegExp.test($("input[name='Tel']").val())) {
            alert("请输入格式正确的手机号码");
        }
        else {
            return $("#edit_form").submit();
        }
        return false;
    });
};

$(document).ready(function () {
    var current_url = window.location.href;
    console.log(current_url);
    if (new RegExp("/user/register").test(current_url)) {
        prepare_register_page();
    }
    if (new RegExp("/user/edit").test(current_url)) {
        prepare_edit_page();
    }
});