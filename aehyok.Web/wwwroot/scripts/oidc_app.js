// <reference path="oidc-client.js" />

//从当前url中获取参数值
function GetUrlRegExp(name) {
    var reg = new RegExp("(^|\\?|&)" + name + "=([^&]*)(\\s|&|$)", "i");
    if (reg.test(window.location.href)) return decodeURIComponent(RegExp.$2.replace(/\+/g, " ")); return "";
}

//oidc-client 全局配置
var config = {
    authority: "http://localhost:5000",
    client_id: "js",
    redirect_uri: "http://localhost:5003/callback.html",
    response_type: "id_token token",
    scope: "openid profile api1",
    //post_logout_redirect_uri: "http://localhost:5003/Home/Index", //登出后跳转的页面（注释后可以跳转回原页面）
};
var mgr = new Oidc.UserManager(config);
var oidcUser = null;
$(document).ready(function () {
    var menuId = GetUrlRegExp("menuId");
    if (menuId == null || menuId == "") {
    } else {
        Layout.setSidebarMenuActiveLink('click', $('#' + menuId));
    }

    //获取当前用户是否有效，无效需登录
    mgr.getUser().then(function (user) {
        if (user) {
            $("#UserName").html(user.profile.name);
            console.log("User logged in");
            oidcUser = user;
            Vue.prototype.$http = axios;

            //设置全局请求添加Authorization Header
            axios.interceptors.request.use(
                config => {
                    config.headers.Authorization = "Bearer " + user.access_token;
                    return config;
                },
                err => {
                    return Promise.reject(err);
                });
        }
        else {
            console.log("User not logged in");
            //用户无效进行跳转IdentityServer4登录
            mgr.signinRedirect({ state: window.location.href });
        }
    });
});


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

//登出
function logout() {
    mgr.signoutRedirect();
}