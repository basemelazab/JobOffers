using Microsoft.AspNet.Identity;
using Offers.Controllers;
using Offers.Migrations;
using Offers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1;
using WebApplication1.Models;



namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
         var list= db.Categories.ToList();
            return View(list);
        }

        public ActionResult Details(int JobId)
        {
         var job=db.Jobs.Find(JobId);
            if(job==null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = JobId;
            return View(job);
        }

        [HttpGet]
        //[Authorize]
        public ActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(string Message)
        {
          var userId= User.Identity.GetUserId();
            var JobId =(int) Session["JobId"];
           var check= db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == userId).ToList();
            if( check.Count<1) 
            {
                var job = new ApplyForJob();
                job.JobId = JobId;
                job.UserId = userId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;
                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result("job");
            }
            else
            {
                ViewBag.Result("Execuse Me,You Have Already Registered");
            }
       
            return View();
        }
        //[Authorize]
        public ActionResult GetJobByUser()
        {
          var UserId= User.Identity.GetUserId();
          var Jobs=db.ApplyForJobs.Where(a=>a.UserId==UserId);
            return View(Jobs.ToList());
        }
        //[Authorize]
        public ActionResult DetailsForJob(int id)
        {
          var job =  db.ApplyForJobs.Find(id);
            if(job==null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobByUser");
            }
            return View(job);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Rules/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {

            var myjob = db.ApplyForJobs.Find(job.Id);
            db.ApplyForJobs.Remove(myjob);
            db.SaveChanges();

            return RedirectToAction("Index");


        }
        //[Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var UserId = User.Identity.GetUserId();
            var jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == UserId
                       select app;

            var grouped = from J in jobs
                          group J by J.job.JobTitle
                         into gr
                          select new JobsViewModel
                          {

                              JobTitle = gr.Key,
                              items=gr
                         };

            return View(grouped.ToList());

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
          

            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var LoginInfo = new NetworkCredential("Email","Password");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("Email"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "Sender Name: " + contact.Name + "</br>" +
                "Sender Email:" + contact.Email + "</br>" +
                "Sender Subject:" + contact.Subject + "</br>" +
                "Sender Message:<b>" + contact.Message + "</br>";
            mail.Body = contact.Message;
            var smtpClient= new SmtpClient("smtp.gmail.com",587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = LoginInfo;
            smtpClient.Send(mail);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Jobs.Where(a => a.JobTitle.Contains(searchName)
             || a.JobContent.Contains(searchName)
             || a.category.CategoryName.Contains(searchName)
             || a.category.CategoryDescription.Contains(searchName)).ToList();
            return View(result);
        }
    }
}