using ElearningMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElearningMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly ElearningContext db;
        private IWebHostEnvironment env;

        public AdminController(ElearningContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
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
                var filePath = "/Content/Images/" + v.Banner.FileName;
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


        public IActionResult UserListModel()
        {
            var users = db.UserAccounts.Select(u => new UserAccount
            {
                Id = u.Id,
                UserFname = u.UserFname + "" + u.UserLname,
                UserEmail = u.UserEmail,
                UserRole = u.UserRole,

            }).ToList();

            return View(users);
        }
        [HttpPost]
        public IActionResult Block(int id)
        {
            var user = db.UserAccounts.Find(id);
            if (user != null)
            {

                user.IsActive = false;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
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
