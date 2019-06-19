
function toggleAll(name) {
    $("input[name=".name).prop('checked', $(this).prop("checked"));
    //$(".checkbox").prop('checked', $(this).prop("checked"));

    }
$(document).ready(function () {
    $("#SelectAll").click(function () {
        var status = $(this).prop('checked');
        var name = $(this).data().check;

        console.log(name);

        var checkboxes = $("input[name *= '" + name + "']");

        $(checkboxes).each(function () {
            $(this).prop('checked', status);
        })
    })
})