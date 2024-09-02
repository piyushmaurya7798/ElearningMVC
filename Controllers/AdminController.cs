        using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ElearningMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace ElearningMVC.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class AdminController : Controller
    {
        private readonly ElearningContext db;
        private IWebHostEnvironment env;

        public AdminController(ElearningContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        
            [HttpGet]
            public async Task<IActionResult> GetUserPayments()
            {
                var users = new List<string>();
                var payments = new List<int>();

            var data = db.UserAccounts.ToList();
            var data2=db.PaymentPlaces.Count();
            TempData["TotalUser"]= data.Count();
            var da = data.Count();
            ViewBag.usercount=data.Count();
            ViewBag.Payments = data2;
            TempData["TotalPayment"]= data2;
            foreach (var item in data)
            {
                users.Add(item.UserEmail);
                var paymentcount = db.PaymentPlaces.Where(x => x.Suser == item.UserEmail).Count();
                                payments.Add(paymentcount);
            }
                    

                return Json(new { users, payments });
            }
        
    
    public IActionResult DashBoard()
        {
            return View();
        }

        public IActionResult AddCourse()
        {
            HttpContext.Session.SetString("BreadCrum", "Add Course");


            return View();
        }
        [HttpPost]
        public IActionResult AddCourse(AddCourseView v)
        {

            if (ModelState.IsValid)
            {
                var path = env.WebRootPath;
                var filePath = "Content/Images/" + v.Banner.FileName;
                var fullPath = Path.Combine(path, filePath);
                UploadFile(v.Banner, fullPath);

                var obj = new Course()
                {
                    Cname = v.Cname,
                    Subname = v.Subname,
                    Banner = filePath,
                    Price = v.Price,
                };
                db.Add(obj);
                db.SaveChanges();

                TempData["msg"] = "Course Added Successfully";


            }


            return View();
        }


        public void UploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }

        public IActionResult UploadVideo()
        {
            HttpContext.Session.SetString("BreadCrum", "Upload Video");


            ViewBag.Courses = db.Courses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Cname
            }).ToList();


            ViewBag.SubCourses = db.Courses
                .Where(sc => !string.IsNullOrEmpty(sc.Subname))
                .Select(sc => new SelectListItem
                {
                    Value = sc.Id.ToString(),
                    Text = sc.Subname
                }).ToList();

            TempData["Videomsg"] = "Video Added Successfully";
            
            return View();

        }
        [HttpPost]
        public IActionResult UploadVideo(Video v)
        {
            db.Videos.Add(v);
            db.SaveChanges();
            return View();

        }
        
        public JsonResult CourseDropDown()
        {
            //var data = db.Courses;

                var uniqueCategories = db.Courses
                                 .Select(p => p.Cname)
                                 .Distinct();
            return Json(uniqueCategories);

        }
        
        public JsonResult subCourseDropDown(string data)
        {
            var uniqueCategories = db.Courses.Where(x=>x.Cname==data).ToList();
                                 
            return Json(uniqueCategories);

        }


        public IActionResult UserListModel()
        {
            var users = db.UserAccounts.Select(u => new UserAccount
            {
                Id = u.Id,
                UserFname = u.UserFname + "" + u.UserLname,
                UserEmail = u.UserEmail,
                UserRole = u.UserRole,
                IsActive = u.IsActive,

            }).ToList();

            return View(users);
        }
        
        public IActionResult uploadMcqs()
        {
            var users = db.UserAccounts.Select(u => new UserAccount
            {
                Id = u.Id,
                UserFname = u.UserFname + "" + u.UserLname,
                UserEmail = u.UserEmail,
                UserRole = u.UserRole,
                IsActive = u.IsActive,

            }).ToList();

            return View(users);
        }
        [HttpPost]
        public IActionResult Block(int id)
        {
            var user = db.UserAccounts.Find(id);
            if (user != null)
            {
                if (user.IsActive == true)
                {

                    user.IsActive = false;
                    db.SaveChanges();
                }
                else { 
                    user.IsActive = true;
                    db.SaveChanges();
                
                }
            }
            return RedirectToAction("UserListModel");
        }


        //[HttpPost]
        //public async Task<IActionResult> ApproveReview(int reviewId)
        //{
        //    var review = await db.Reviews.FindAsync(reviewId);
        //    if (review != null)
        //    {
        //        review.IsApproved = true;
        //        await db.SaveChangesAsync();
        //    }
        //    return RedirectToAction("Index");
        //}
    }
}
