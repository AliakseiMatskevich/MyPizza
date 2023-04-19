function redirecttologinpage() {
    window.location = "/ProductType/Index";
}

$(document).ready(function () {
    setInterval(redirecttologinpage, 5000);
});