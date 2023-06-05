// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(".mainSwitcher").click(function () {
    $(".Switcher").prop('checked', this.checked);
});

$(".Switcher").click(function () {
    if (!this.checked && $(".mainSwitcher").is(":checked")) {
        $(".mainSwitcher").prop('checked', false)
    }

})