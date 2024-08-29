using ElearningMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElearningMVC.MiddleWare
{
    public interface CourseInterface
    {
        public List<PaymentPlace> MyCourse(string user);
        public List<Video> getVideos(int id);
    }
}
