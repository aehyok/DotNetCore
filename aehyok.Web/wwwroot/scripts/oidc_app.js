// <reference path="oidc-client.js" />

function GetUrlRegExp(name) {
    var reg = new RegExp("(^|\\?|&)" + name + "=([^&]*)(\\s|&|$)", "i");
    if (reg.test(window.location.href)) return decodeURIComponent(RegExp.$2.replace(/\+/g, " ")); return "";
}

//function log() {
//    document.getElementById('results').innerText = '';

//    Array.prototype.forEach.call(arguments, function (msg) {
//        if (msg instanceof Error) {
//            msg = "Error: " + msg.message;
//        }
//        else if (typeof msg !== 'string') {
//            msg = JSON.stringify(msg, null, 2);
//        }
//        document.getElementById('results').innerHTML += msg + '\r\n';
//    });
//}



var config = {
    authority: "http://localhost:5000",
    client_id: "js",
    redirect_uri: "http://localhost:5003/callback.html",
    response_type: "id_token token",
    scope: "openid profile api1",
    //post_logout_redirect_uri: "http://localhost:5003/Home/Index", //登出后跳转的页面（注释后可以跳转回原页面）
};
var mgr = new Oidc.UserManager(config);

$(document).ready(function () {
    var menuId = GetUrlRegExp("menuId");
    if (menuId == null || menuId == "") {
    } else {
        Layout.setSidebarMenuActiveLink('click', $('#' + menuId));
    }

    //获取当前用户是否有效，无效需登录
    mgr.getUser().then(function (user) {
        if (user) {
            //log("User logged in", user.profile);
            $("#UserName").html(user.profile.name);
            console.log("User logged in");
        }
        else {
            //log("User not logged in");
            console.log("User not logged in");
            //用户无效进行跳转IdentityServer4登录
            mgr.signinRedirect({ state: window.location.href });
        }
    });
});



function login() {
    console.info(window.location.href);
    mgr.signinRedirect({ state: window.location.href }); //登录后跳转到原来页面
}

function api() {
    mgr.getUser().then(function (user) {
        var url = "http://localhost:5001/api/Blog/Article/1/8/";
        //var url = "http://localhost:5001/api/GuideLine";
        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            //log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

function logout() {
    mgr.signoutRedirect();
}