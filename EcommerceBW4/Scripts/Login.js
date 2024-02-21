function show() {
    var p = document.getElementById('txtPassword');
    p.setAttribute('type', 'text');
}

function hide() {
    var p = document.getElementById('txtPassword');
    p.setAttribute('type', 'password');
}

var pwShown = 0;

document.getElementById("eye").addEventListener("click", function () {
    if (pwShown == 0) {
        pwShown = 1;
        show();
    } else {
        pwShown = 0;
        hide();
    }
}, false);

/* funzione mouse hover */
/*
document.addEventListener('DOMContentLoaded', (event) => {
    const card = document.querySelector(".card");
    const overlay = document.querySelector(".overlay");

    // La funzione per applicare l'effetto overlay.
    const applyOverlayMask = (e) => {
        const x = e.pageX - card.offsetLeft;
        const y = e.pageY - card.offsetTop;

        overlay.style = `--opacity: 1; --x: ${x}px; --y:${y}px;`;
    };

    // Ascolta l'evento di movimento del mouse.
    document.body.addEventListener("pointermove", applyOverlayMask);
});
*/