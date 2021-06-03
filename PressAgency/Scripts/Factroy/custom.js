let menu = document.getElementById('right-side');
let side = document.getElementById('nav-bar');
let exit = document.getElementById('exit');
menu.addEventListener('click', showMenu);
exit.addEventListener('click', hideMenu);

function showMenu() {
    side.classList.add("showMenu")
}

function hideMenu() {
    side.classList.remove("showMenu")
}


