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

                htmlContent += "<option value='" + item + "'>" + item + "</li>";
               
                i++;
            });
            console.log(htmlContent);
            $('#CourseDrop').html(htmlContent);
            
        }
    })
});

$('#CourseDrop').change(function () {
$.ajax({
    url: "/Admin/subCourseDropDown",
    type: "Get",
    data: { data: $('#CourseDrop').val() },
    contentType: "application/x-www-form-urlencoded; charset=utf-8",
    success: function (result) {
        var htmlContent = "";
        var i = 1;
        $.each(result, function (index, item) {
            console.log("Processing item:", item);

            htmlContent += "<option value='" + item.subname + "'>" + item.subname + "</li>";

            i++;
        });
        console.log(htmlContent);
        $('#SubCourseDrop').html(htmlContent);

    }
})
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
                htmlContent += "<div id='question" + i + "'" + " style='display:none'>";
                htmlContent += "<div>Question: " + item.question + "</div>";
                htmlContent += "<ol id='olist'>";
                htmlContent += "<input type='hidden' id='mcqid" + i + "' value=" + item.correct + ">";
                htmlContent += "<li><input type='radio' name='option" + i + "' value='" + item.option1 + "'>" + item.option1 + "</li>";
                htmlContent += "<li><input type='radio' name='option" + i + "' value='" + item.option2 + "'>" + item.option2 + "</li>";
                htmlContent += "<li><input type='radio' name='option" + i + "' value='" + item.option3 + "'>" + item.option3 + "</li>";
                htmlContent += "<li><input type='radio' name='option" + i + "' value='" + item.option4 + "'>" + item.option4 + "</li>";
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
