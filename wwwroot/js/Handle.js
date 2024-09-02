$(document).ready(function () {
    $.ajax({
        url: "/Admin/CourseDropDown",
        type: "Get",
        /* data: $('#contactform').serialize(),*/
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        success: function (result) {
            var htmlContent = "";
            var i = 1;
            $.each(result, function (index, item) {
                console.log("Processing item:", item);

                htmlContent += "<option value='" + item.Cname + "'>" + item.Cname + "</li>";

                i++;
            });
            console.log(htmlContent);
            $('#CourseDrop').html(htmlContent);

        }
    })
});

$('#ContactForm').click(function () {
    $.ajax({
        url: "/Home/ContactForm",
        type: "Post",
        data: $('#contactform').serialize(),
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        succeess: function (result) {
            alert("Complete")
        }
    })
});


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
    var url = $(this).data('url');
    var vid = $(this).data('vid');
    var videoId = url.split('v=')[1];
    $('#HiddenVid').val(vid);
    $('#mcqsdisplay').css("display", "flex");
    $("#video")[0].src = "//www.youtube.com/embed/" + videoId;
    $('#mcqs').css("display", "block");
    $('#review').css("display", "block");
    $('#Mcsqdiv').css("display", "none");
    $("#video").show();
    $('#UploadSolution').css("display", "none");
    markAsWatched(vid);
});
$('#mcqs').click(function () {

    $('#Mcsqdiv').css("display", "block");
    $('#UploadSolution').css("display", "none");

    var s = $('#HiddenVid').val();
    $.ajax({
        url: "/course/Mcqs?data=" + s,
        type: "Get",
        data: $('#contactform').serialize(),
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        success: function (result) {
            var htmlContent = "";
            var i = 1;
            $.each(result, function (index, item) {
                console.log("Processing item:", item);
                htmlContent += "<div id='question" + i +"'" +" style='display:none'>";
                htmlContent += "<div>Question: " + item.question + "</div>";
                htmlContent += "<ol id='olist'>";
                htmlContent += "<input type='hidden' id='mcqid"+i+"' value=" + item.correct + ">";
                htmlContent += "<li><input type='radio' name='option" + i + "' value='"+item.option1+"'>" + item.option1 + "</li>";
                htmlContent += "<li><input type='radio' name='option" + i + "' value='"+item.option2+"'>" + item.option2 + "</li>";
                htmlContent += "<li><input type='radio' name='option" + i + "' value='"+item.option3+"'>" + item.option3 + "</li>";
                htmlContent += "<li><input type='radio' name='option" + i + "' value='" + item.option4+"'>" + item.option4 + "</li>";
                htmlContent += "</ol>";
                htmlContent += "</div>";
                i++;
            });
            console.log(htmlContent);
            $('#questionn').html(htmlContent);
            $('#question1').css("display", "block");
            $('#Backward').css("display", "none");
        }
    })
});


$('#mcqssubmit').click(function () {
    let score = 0;  
    const totalQuestions = 5; 
    for (let i = 1; i <= totalQuestions; i++) {
        let correctAnswer = $('#mcqid' + i).val();
        let selectedAnswer = $("input[name='option" + i + "']:checked").val();
        if (selectedAnswer === correctAnswer) {
            score++;
        }
    }
    alert("Your score is: " + score + " out of " + totalQuestions);
});

$('#Forward').click(function () {
    var questions = ['#question1', '#question2', '#question3', '#question4', '#question5'];
console.log("HitTImes")
    for (var i = 0; i < questions.length; i++) {
        if ($(questions[i]).css("display") === "block") {
            $(questions[i]).css("display", "none");
            if (i + 1 < questions.length) {
                $(questions[i + 1]).css("display", "block");
                console.log(i + 1);
                $('#Backward').css("display", "block"); 
                $('#Forward').css("display", "block"); 
             if (i + 1 === questions.length - 1) {
                    $('#Forward').css("display", "none");
                    $('#mcqssubmit').css("display", "block");
                }
            } else {
                $('#Forward').css("display", "none");
            }

            break; 
        }
    }
});

$('#UploadAssignmentbutton').click(function () {
    $('#Mcsqdiv').css("display", "none");
    $('#UploadSolution').css("display", "block");
});

$('#Backward').click(function () {
    var questions = ['#question1', '#question2', '#question3', '#question4', '#question5'];
    console.log("tinews")
    $('#mcqssubmit').css("display", "none");
    for (var i = 0; i < questions.length; i++) {
        if ($(questions[i]).css("display") === "block") {

            $(questions[i]).css("display", "none");
            if (i - 1 >= 0) {
                $(questions[i - 1]).css("display", "block");
                $('#Forward').css("display", "block");
                if (i - 1 === 0) {
                    $('#Backward').css("display", "none");
                }
            } else {

                $('#Backward').css("display", "none");
            }
            break;
        }
    }
}); 




function markAsWatched(videoId, courseId) {
    $.ajax({
        url: '/Course/MarkAsWatched', // Controller action to handle marking the video
        method: 'POST',
        data: { videoId: videoId, courseId: courseId },
        success: function (response) {
            if (response.success) {
                alert('Video marked as watched.');
            } else {
                if (response.message === "You already have a certificate for this course.") {
                alert('You already have certificate for this course');
                    // Redirect to another page if the user already has a certificate
                    //window.location.href = '/Course/CertificateExists';
                } else {
                    alert('Could not mark video as watched. Please try again.');
                }
            }
        },
        error: function (xhr, status, error) {
            alert('An error occurred while marking the video as watched.');
            console.error(error);
        }
    });
}
