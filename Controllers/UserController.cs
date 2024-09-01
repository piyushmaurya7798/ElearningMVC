using ElearningMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace ElearningMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ElearningContext db;

        public UserController(ElearningContext db)
        {
            this.db = db;
        }
        public IActionResult Index(Course c)
        {
            var viewModel = new CombinedViewModel
            {
                Courses = db.Courses.GroupBy(c => c.Cname).Select(g => g.First()).ToList(),
                ContactForm = new ContactFormModel()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(CombinedViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Create a new MailMessage object
                    string recipient = viewModel.ContactForm.Email;
                    string myemail = "piyushmaurya7798@gmail.com";
                    string subject = viewModel.ContactForm.Subject;
                    string body = viewModel.ContactForm.Message;

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(recipient);
                    mail.To.Add(myemail);
                    mail.Subject = subject;
                    mail.Body = body;

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential("piyushmaurya7798@gmail.com", "qbposjoyllyywcld");
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mail);

                    ViewBag.Message = "Message sent successfully!";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"Error: {ex.Message}";
                }
            }

            // Reload the courses to keep the list populated
            viewModel.Courses = db.Courses.ToList();
            return View(viewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Course(Course c)
        {
            var data = db.Courses.ToList();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Course(int dropdownCourse, string keyword)
        {
            var filteredCourses = await db.Courses.Where(x => x.Id == dropdownCourse).ToListAsync();

            if (!string.IsNullOrEmpty(keyword))
            {
                filteredCourses = filteredCourses.Where(x => x.Cname.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return View(filteredCourses);
        }

        public IActionResult MyCourse(Course c)
        {
            var data = db.Courses.ToList();
            return View(data);
        }
        
        public IActionResult details(int id)
        {
            var data = db.Courses.Find(id);
            var data2 = db.Videos.Where(x => x.Subcourse == data.Subname).Count();
            TempData["VideosCount"]=data2;
                return View(data);
        }
        
        public IActionResult paymentsingle(int id)
        {
            var data = db.Courses.Find(id);
            var data2 = db.Videos.Where(x => x.Subcourse == data.Subname).Count();
            TempData["VideosCount"]=data2;
                return View(data);
        }
    }
}
