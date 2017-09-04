var Menu = {
    //点击顶部菜单事件
    TopMenuItemClick: function (e) {
        var menuId = e.id;
        window.location.href = "../Home/Index?menuId="+menuId+"&type=header";

    }
}

//通用函数
var Util = {
    GetUrlRegExp: function(name) {
        var reg = new RegExp("(^|\\?|&)" + name + "=([^&]*)(\\s|&|$)", "i");
        if (reg.test(window.location.href))
            return decodeURIComponent(RegExp.$2.replace(/\+/g, " ")); return "";
        }
}