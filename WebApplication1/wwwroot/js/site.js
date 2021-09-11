// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    var modalHere = $('#modalHere');
    $('button[data-toggle="ajax-modal"]').click(function (event) {

        var url = $(this).data('url');
        $.get(url).done(function (data) {
            modalHere.html(data);
            modalHere.find('.modal').modal('show');
        })
    })

    modalHere.on('click', '[data-save="modal"]', function (event) {

        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var senddata = form.serialize();
        $.post(actionUrl, senddata).done(function (data) {
            modalHere.find('.modal').modal('hide');
        })
    })

})
