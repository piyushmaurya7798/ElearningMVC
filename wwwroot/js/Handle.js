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


$("body").on("change", "#selectedvideo", function () {
    var urlwithid = $("#selectedvideo").val();
    var url = urlwithid.split(',')[0];
    console.log(url);
    var vid = urlwithid.split(',')[1];

    $('#HiddenVid').val(vid);
    url = url.split('v=')[1];
    $("#video")[0].src = "//www.youtube.com/embed/" + url;
    $('#NavBoss').css("display", "block");
    $('body').removeClass('show-sidebar');
    $('body').find('.js-menu-toggle').removeClass('active');
    $('#mcqs2').css("display", "block");
    $('#review').css("display", "block");
    $('#Mcsqdiv').css("display", "none");

    $("#video").show();
});


$('#video-list').on('click', 'li', function () {
    // Retrieve the custom data attributes for URL and video ID
    var url = $(this).data('url');
    var vid = $(this).data('vid');

    // Extract the video ID from the URL
    var videoId = url.split('v=')[1];

    // Set the hidden input's value to the video ID
    $('#HiddenVid').val(vid);

    // Update the video player source to embed the selected video
    $("#video")[0].src = "//www.youtube.com/embed/" + videoId;

    // Show or hide elements as needed
    $('#mcqs2').css("display", "block");
    $('#review').css("display", "block");
    $('#Mcsqdiv').css("display", "none");
    $("#video").show();
});
//$('#mcqs').click(function () {
//    alert('Cliclwed');
//    console.log("sdfgh")
//    //$.ajax({
//    //    url: "/Home/ContactForm",
//    //    type: "Post",
//    //    data: $('#contactform').serialize(),
//    //    contentType: "application/x-www-form-urlencoded; charset=utf-8",
//    //    succeess: function (result) {
//    //        alert("Complete")
//    //    }
//    //})
//})

