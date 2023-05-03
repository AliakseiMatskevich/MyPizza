function redirecttomainpage() {
    window.location = "/Order/Index";
}

$(document).ready(function () {
    setInterval(redirecttomainpage, 5000);
});