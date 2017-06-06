// Write your Javascript code.
window.addEventListener("load", function () {
    var a = document.querySelector("a.PostLogoutRedirectUri");
    if (a) {
        window.location = a.href;
    }
});
