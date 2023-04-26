function redirecttologinpage() {
    window.location = "/Identity/Account/Login";
}

$(document).ready(function () {
    setInterval(redirecttologinpage, 5000);
});