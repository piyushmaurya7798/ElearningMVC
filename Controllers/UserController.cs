using ElearningMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using SkiaSharp;
using Microsoft.AspNetCore.Mvc.Razor;
using System.util;
using System.Text;
using Razorpay.Api;
using Microsoft.AspNetCore.Http;

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
            HttpContext.Session.SetString("course", data.Subname);
            var data2 = db.Videos.Where(x => x.Subcourse == data.Subname).Count();
            TempData["VideosCount"] = data2;
            HttpContext.Session.SetString("totalprice",data.Price.ToString());
            return View(data);
        }

        public IActionResult paymentsingle(int id)
        {
            var data = db.Courses.Find(id);
            var data2 = db.Videos.Where(x => x.Subcourse == data.Subname).Count();
            TempData["VideosCount"] = data2;
            return View(data);
        }

        public IActionResult InitiateOrder()
        {
            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", Convert.ToDecimal(HttpContext.Session.GetString("totalprice")) * 100);
            input.Add("currency", "INR");
            input.Add("receipt", "12121");

            string KeyId = "rzp_test_4DrJJevYZkd0No";
            string KeySecret = "3oX1Dvxlpy2wB3BGnDPxGtH8";

            RazorpayClient client = new RazorpayClient(KeyId, KeySecret);

            Razorpay.Api.Order order = client.Order.Create(input);
            ViewBag.orderid = order["id"].ToString();
            ViewBag.Amount = input["amount"];
            ViewBag.KeyId = KeyId;
            //            string razorpayScript = $@"
            //var options={{
            //        'key'': '', // Enter the Key ID generated from the Dashboard
            //        'amount':  '{Convert.ToDecimal(HttpContext.Session.GetString("totalprice")) * 100}' , // Amount is in currency subunits. Default currency is INR. Hence, 50000 refers to 50000 paise
            //        'currency': 'INR',
            //        'name': 'Acme Corp',
            //        'description': 'Buy Green Tea',
            //        'order_id': orderId,
            //        'image': 'https://example.com/your_logo',
            //        'theme': {{
            //                'color': '#3399cc'
            //        }}
            //        }};

            //var rzp =new Razorpay(options);
            //rzp.open();";

            //ClientScript.RegisterStartupScript(this.GetType(), "razorpayScript", razorpayScript, true);
            return View();
        }

        public IActionResult Payment(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature)
        {
            RazorpayClient client = new RazorpayClient("[YOUR_KEY_ID]", "[YOUR_KEY_SECRET]");

            Dictionary<string, string> attributes = new Dictionary<string, string>();

            attributes.Add("razorpay_payment_id", razorpay_payment_id);
            attributes.Add("razorpay_order_id", razorpay_order_id);
            attributes.Add("razorpay_signature", razorpay_signature);

            Utils.verifyPaymentSignature(attributes);
            return RedirectToAction("Index");
        }


        public IActionResult DonePayment()
        {
            string coursename = HttpContext.Session.GetString("course");
            var suser = HttpContext.Session.GetString("uemail");
            var data2 = db.Courses.Where(x => x.Subname == coursename).SingleOrDefault();
            var myEmail = "piyushmaurya7798@gmail.com";
            var pass = "qbposjoyllyywcld";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(myEmail, pass)
            };
            StringBuilder invoice = new StringBuilder();
            invoice.AppendLine("<html>");
            invoice.AppendLine("<head>");
            invoice.AppendLine("<style>");
            invoice.AppendLine("table { width: 100%; border-collapse: collapse; }");
            invoice.AppendLine("table, th, td { border: 1px solid black; }");
            invoice.AppendLine("th, td { padding: 10px; text-align: left; }");
            invoice.AppendLine(".header { font-size: 24px; font-weight: bold; }");
            invoice.AppendLine(".footer { font-size: 12px; color: gray; }");
            invoice.AppendLine("</style>");
            invoice.AppendLine("</head>");
            invoice.AppendLine("<body>");
            invoice.AppendLine("<div style='text-align: center;'>");
            //invoice.AppendLine("<img src='' style='width: 200px;' />");
            invoice.AppendLine("</div>");
            invoice.AppendLine("<h1 class='header'>Invoice</h1>");
            invoice.AppendLine("<p>Date: " + DateTime.Now.ToString("dd/MM/yyyy") + "</p>");
            invoice.AppendLine($"<p>Invoice Number: 1111</p>");
            invoice.AppendLine($"<p>Customer Name: {suser}</p>");
            invoice.AppendLine($"<p>Customer Email: {suser}</p>");
            invoice.AppendLine("<table>");
            invoice.AppendLine("<tr><th>ItemID</th><th>CourseName</th><th>Image</th><th>Subcourse</th><th>Price</th></tr>");

            invoice.AppendLine($"<tr><td>{data2.Id}</td><td>{data2.Cname}</td><td><img src='{data2.Banner}'/></td><td>{data2.Subname}</td><td>&#8377;{data2.Price:F2}</td></tr>");
            var obj = new PaymentPlace()
            {
                Course = data2.Cname,
                Subcourse = data2.Subname,
                Price = data2.Price,
                Suser = suser,
                Banner = data2.Banner,
                Dt = DateTime.Now.ToString()
            };
            db.PaymentPlaces.Add(obj);
           db.SaveChanges();
        
            string total = HttpContext.Session.GetString("totalprice");
            invoice.AppendLine($"<tr><td colspan='4' style='text-align: right;'>Total</td><td>&#8377;{total}</td></tr>");
            invoice.AppendLine("</table>");
            invoice.AppendLine("<p>Payment Terms: Net 30 days</p>");
            invoice.AppendLine("<p class='footer'>Thank you for your business!</p>");
            invoice.AppendLine("</body>");
            invoice.AppendLine("</html>");

            var subject = "Purchase Details";
            var mailmessage = new MailMessage
            {
                From = new MailAddress(myEmail),
                Subject = subject,
                Body = invoice.ToString(),
                IsBodyHtml = true,

            };

            mailmessage.To.Add(suser);
            client.Send(mailmessage);

            return RedirectToAction("GetMyCourse","Course");
        }

        public IActionResult Certificate()
        {

            string suser = HttpContext.Session.GetString("uemail");
            if (string.IsNullOrEmpty(suser))
            {
                return RedirectToAction("Login", "Account");
            }



           
            var certificates = db.Certificates
                .Where(c => c.suser == suser)
               .ToList();

            return View(certificates); 
        }
        public IActionResult GenerateCertificate(int id)
        {
            var suser = HttpContext.Session.GetString("uemail");
            var courses = db.Certificates.Find(id).CourseName;
            var user = db.UserAccounts.FirstOrDefault(u => u.UserEmail == suser);
            var course = db.Courses.FirstOrDefault(c => c.Subname == courses);

            if (user == null || course == null)
            {
                return NotFound();
            }

           var certificateData = GenerateCertificatePdf(user.UserFname, course.Subname);

           
            return File(certificateData, "application/pdf", $"Certificate_{course.Subname}_{user.UserFname}.pdf");
        }

        private byte[] GenerateCertificatePdf(string userName, string courseName)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 50, 50, 50, 50);
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                document.Open();

                // Add logo
                string logoPath = "C:\\Users\\Asus\\source\\repos\\ElearningMVC\\wwwroot\\Content\\Images\\Masstechedu.png";
                Image logo = Image.GetInstance(logoPath);
                logo.ScaleToFit(200f, 80f);
                logo.Alignment = Element.ALIGN_CENTER;
                document.Add(logo);

                // Title
                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 36);
                Paragraph title = new Paragraph("Certificate of Completion", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(title);

                document.Add(new Paragraph("\n\n"));

                // Recipient and course details
                Font regularFont = FontFactory.GetFont(FontFactory.HELVETICA, 20);
                Paragraph awardedTo = new Paragraph($"This certifies that\n\n{userName}", regularFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(awardedTo);

                Paragraph courseDetails = new Paragraph($"has successfully completed the course\n\n{courseName}", regularFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(courseDetails);

                document.Add(new Paragraph("\n\n"));

                // Add medal image
                string medalPath = "C:\\Users\\Asus\\source\\repos\\ElearningMVC\\wwwroot\\Content\\Images\\rank1.png"; // Update with your medal image path or byte array
                Image medal = Image.GetInstance(medalPath);
                medal.ScaleToFit(100f, 100f);
                medal.Alignment = Element.ALIGN_CENTER;
                document.Add(medal);

                document.Add(new Paragraph("\n\n"));

                // Signature section
                Paragraph signatureLine = new Paragraph("____________________", regularFont)
                {
                    Alignment = Element.ALIGN_RIGHT
                };
                document.Add(signatureLine);

                Paragraph signatureName = new Paragraph("Authorized Signature", regularFont)
                {
                    Alignment = Element.ALIGN_RIGHT
                };
                document.Add(signatureName);

                document.Close();
                writer.Close();

                return stream.ToArray();
            }
        }

    }
}

