window.toggleNavMenu = function (isExpanded) {
    const navScrollable = document.querySelector('.nav-scrollable');
    const navbarToggler = document.querySelector('.navbar-toggler');
    
    if (isExpanded) {
        navScrollable.classList.add('expanded');
        navbarToggler.checked = true;
    } else {
        navScrollable.classList.remove('expanded');
        navbarToggler.checked = false;
    }
}; 