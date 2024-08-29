using ElearningMVC.Models;
using EmailSend.MiddleWare;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace ElearningMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly ElearningContext db;

        public HomeController(ILogger<HomeController> logger, ElearningContext db)
        {
            _logger = logger;
            this.db = db;
          
        }

        public IActionResult Index()
        {
            var data=db.Courses.ToList();
            TempData["CourseCount"] = db.Courses.Select(x => x.Cname).Distinct().Count();
            TempData["StudentCount"] = db.UserAccounts.Count();
            TempData["SubCourseCount"] = db.Courses.Count();
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> ContactForm(string email,string subject,string message)
        {
            var myEmail = "piyushmaurya7798@gmail.com";
            var pass = "qbposjoyllyywcld";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(myEmail, pass)
            };

             client.Send(
                new MailMessage(from: myEmail,
                to: email,
                subject,
                message
                )
                );
            return View();
        }

        public IActionResult About() 
        {

            return View();
        }
    }
}
