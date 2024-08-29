$(document).ready(function () {
});

$('#ContactForm').click(function(){
    $.ajax({
        url: "/Home/ContactForm",
        type: "Post",
        data: $('#contactform').serialize(),
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        succeess: function (result) {
            alert("Complete")
        }
    })
})