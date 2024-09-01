using ElearningMVC.MiddleWare;
using ElearningMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return View();
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
    }
}
