// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(".myLink").click(function (e) {
    e.preventDefault();
    $.ajax({
        url: $(this).attr("href"),
        success: function () {
            alert("Value Added");
        }
    });
    location.reload();
});
