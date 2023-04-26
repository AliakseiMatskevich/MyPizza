function redirecttomainpage() {
    window.location = "/ProductType/Index";
}

$(document).ready(function () {
    setInterval(redirecttomainpage, 5000);
});