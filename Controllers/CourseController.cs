using ElearningMVC.MiddleWare;
using ElearningMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElearningMVC.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class CourseController : Controller
    {
        public readonly ElearningContext db;
        CourseInterface r;
        private IWebHostEnvironment env;
        public CourseController(CourseInterface r, ElearningContext db, IWebHostEnvironment env)
        {
            this.r = r;
            this.db = db;
            this.env = env;
        }
        public IActionResult Index()
        {

            return View();
        }
        
        
        public IActionResult GetMyCourse()
        {
                string suser=HttpContext.Session.GetString("uemail");
            
            var data=r.MyCourse(suser);
            return View(data);
        }
        
        public IActionResult VideoList(int id)
        {
            var data = r.getVideos(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult UploadAssignmentForm(IFormFile videoFile, string hiddenFieldInForm)
        {

            var fileName = Path.GetFileName(videoFile.FileName);
            var suser = HttpContext.Session.GetString("uemail"); ;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images", fileName);
            var fileuploadpath = "/Content/Images/" + fileName;
            // Ensure the uploads directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (var stream = new FileStream(path, FileMode.Create))
            {
                videoFile.CopyTo(stream);
            }
            var obj = new TaskAssignment() { 
            Finishdate = DateTime.Now,
            taskUpload = fileuploadpath,
            VideoId= int.Parse( hiddenFieldInForm),
            Score=""
            };
            db.TaskAssignments.Add(obj);
            db.SaveChanges();
            return RedirectToAction("GetMyCourse");
        }

        public void UploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }

        public JsonResult Mcqs(int data)
        {
            var data2 = r.Getmcqs(data);
            return Json(data2);
        }


        [HttpPost]
        public IActionResult MarkAsWatched(int videoId)
        {
            string userId = HttpContext.Session.GetString("uemail");
            var course = db.Videos.Find(videoId).Subcourse;
            var existingCertificate = db.Certificates
       .FirstOrDefault(c => c.suser == userId && c.CourseName == course);
            if (existingCertificate != null)
            {
                // If the certificate already exists, return a specific response
                return Json(new { success = false, message = "You already have a certificate for this course." });
            }
            // Check if the record already exists
            var progress = db.UserVideoProgresses
                .FirstOrDefault(uvp => uvp.Suser == userId && uvp.videoId == videoId);

            if (progress == null)
            {
                // If it doesn't exist, create a new record
                progress = new UserVideoProgress
                {
                    Suser = userId,
                    videoId = videoId,
                    IsWatched = true,
                    WatcheDate = DateTime.Now
                };
                db.UserVideoProgresses.Add(progress);
            }
            else
            {
                // Update the existing record
                progress.IsWatched = true;
                progress.WatcheDate = DateTime.Now;
            }

            db.SaveChanges();

            // Check if all videos in the course are watched
            bool allWatched = db.Videos
            .Where(v => v.Subcourse == course)
                .All(v => db.UserVideoProgresses.Any(uvp => uvp.videoId == v.Vid && uvp.Suser == userId && uvp.IsWatched));

            if (allWatched)
            {
                // If all videos are watched, issue a certificate
                var certificate = new Certificate
                {
                    suser = userId,
                    CourseName = course,
                    IssuseDate = DateTime.Now
                };
                db.Certificates.Add(certificate);
                db.SaveChanges();

                return Json(new { success = true, certificateIssued = true });
            }

            return Json(new { success = true, certificateIssued = false });
        }

    }
}
