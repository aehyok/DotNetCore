var Menu = {
    //点击顶部菜单事件
    TopMenuItemClick: function (e) {
        var menuId = e.id;
        window.location.href = "../Home/Index?menuId="+menuId+"&type=header";

    }
}