function redirecttoorderspage() {
    window.location = "/Order/Index";
}

$(document).ready(function () {
    setInterval(redirecttoorderspage, 5000);
});