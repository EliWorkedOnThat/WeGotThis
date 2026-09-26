function setTimezoneCookie() {
    const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;
    document.cookie = `timezone=${timezone}; path=/; max-age=${60 * 60 * 24 * 365}`;
}

setTimezoneCookie();

function animateCounter(element, duration) {
    const target = parseInt(element.dataset.target, 10);
    const startTime = performance.now();

    function update(currentTime) {
        const elapsed = currentTime - startTime;
        const progress = Math.min(elapsed / duration, 1);
        element.textContent = Math.floor(progress * target);

        if (progress < 1) {
            requestAnimationFrame(update);
        }
    }

    requestAnimationFrame(update);
}

const counter = document.getElementById("goalCounter");
if (counter) {
    animateCounter(counter, 2000);
}