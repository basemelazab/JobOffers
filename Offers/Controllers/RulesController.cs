using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Offers.Controllers
{
    public class RulesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Rules
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Rules/Details/5
        public ActionResult Details(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Rules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rules/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {

            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);

        }

        // GET: Rules/Edit/5
        public ActionResult Edit(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Rules/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name")]IdentityRole role)
        {
            if (ModelState.IsValid)
            { 
            db.Entry(role).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
            return View(role);
    }

        // GET: Rules/Delete/5
        public ActionResult Delete(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Rules/Delete/5
        [HttpPost]
        public ActionResult Delete(IdentityRole role)
        {

                var myrole= db.Roles.Find(role.Id);
                db.Roles.Remove(myrole);
                db.SaveChanges();
                     
                return RedirectToAction("Index");
           
           
        }
    }
}
