function setTimezoneCookie() {

    const timezone_cookie = "timezoneoffset";

    if (!document.cookie.indexOf(timezone_cookie + "=") >= 0) {
        const dt = new Date();
        document.cookie = timezone_cookie + "=" + dt.getTimezoneOffset();
    }    
}