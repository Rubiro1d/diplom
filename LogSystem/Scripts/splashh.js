const splash = document.querySelector('.splash');

document.addEventListener('DOMContentLoaded', (e) => {
    setTimeout(()=> {
            splash.classList.add('display-none');
        }, 2000);
})

function randomIntFromInterval(min, max) { // min and max included 
    return Math.floor(Math.random() * (max - min + 1) + min)
}

randhue = randomIntFromInterval(0, 360)

document.querySelector('.ice-cont').style.filter = "hue-rotate("+ randhue +"deg)";