using ElearningMVC.MiddleWare;
using ElearningMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElearningMVC.Services
{
    public class CourseImplement : CourseInterface
    {
        public readonly ElearningContext db;
        public CourseImplement(ElearningContext db)
        {
            this.db = db;
        }

        public List<Mcqs> Getmcqs(int id)
        {
            var mcqslist=db.Mcqss.Where(x=>x.vid==id).ToList();
            return mcqslist;
        }

        public List<Video> getVideos(int id)
        {
            var course=db.PaymentPlaces.Find(id).Course;
            var SubCourse=db.PaymentPlaces.Find(id).Subcourse;
            var data = db.Videos.Where(x => x.Course == course && x.Subcourse == SubCourse).ToList();
            return data;
        }

        public List<PaymentPlace> MyCourse(string user)
        {
            var data=db.PaymentPlaces.Where(x => x.Suser == user).ToList();
            
            return data;
        }

       
    }
}
