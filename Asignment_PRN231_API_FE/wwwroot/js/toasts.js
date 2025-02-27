// For more details see: https://getbootstrap.com/docs/5.0/components/toasts/#usage

window.addEventListener('DOMContentLoaded', event => {

    const toastNoAutohideEl = document.getElementById('toastNoAutohide');

    const toastNoAutohide = new bootstrap.Toast(toastNoAutohideEl);

    const toastNoAutohideTrigger = document.getElementById('toastNoAutohideTrigger');

    toastNoAutohide.show();

})
