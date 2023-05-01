const hoursBlock = document.querySelector(".time-hours");
const minutesBlock = document.querySelector(".time-minutes");
const secondsBlock = document.querySelector(".time-seconds");

const updateTime = () => {
    const date = new Date();

    const hours = date.getHours();
    const minutes = date.getMinutes();
    const seconds = date.getSeconds();

    hoursBlock.textContent = formatNumber(hours);
    minutesBlock.textContent = formatNumber(minutes);
    secondsBlock.textContent = formatNumber(seconds);
}

const formatNumber = (number) => {
    return number > 9 ? number : "0" + number
}

setInterval(updateTime, 500)