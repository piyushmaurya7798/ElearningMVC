using ElearningMVC.MiddleWare;
using ElearningMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElearningMVC.Controllers
{
    public class CourseController : Controller
    {
        //public readonly ElearningContext db;
        CourseInterface r;

        public CourseController(CourseInterface r)
        {
            this.r = r;
        }
        public IActionResult Index()
        {

            return View();
        }
        
        
        public IActionResult GetMyCourse()
        {
            //     string suser=HttpContext.Session.GetString("Suser");
            string suser = "piyush@gmail.com";
            var data=r.MyCourse(suser);
            return View(data);
        }
        
        public IActionResult VideoList(int id)
        {
            var data = r.getVideos(id);
            return View(data);
        }
        
        public JsonResult Mcqs(int data)
        {
            var data2 = r.Getmcqs(data);
            return Json(data2);
        }
    }
}
