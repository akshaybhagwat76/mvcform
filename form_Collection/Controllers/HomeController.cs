using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using form_Collection.Models;

namespace form_Collection.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        Student_DBEntities sd = new Student_DBEntities();

        public ActionResult Index()
        {
            ViewData["list"] = sd.students.ToList();
            return View();
        }

        public ActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent(FormCollection fc)
        {
            student s = new student();
            s.f_name = fc["txtfn"];
            s.age = Convert.ToInt16(fc["txtag"]);

            sd.students.Add(s);
            sd.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var data = (from n in sd.students where n.student_id == id select n).FirstOrDefault();
            ViewData["edit"] = data;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(FormCollection fc)
        {
            int id = Convert.ToInt16(fc["txtid"]);
            var data = from n in sd.students where n.student_id == id select n;

            foreach (var item in data)
            {
                item.f_name = fc["txtfn"];
                item.age = Convert.ToInt16(fc["txtag"]);
            }

            sd.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            student s = sd.students.Find(id);
            sd.students.Remove(s);
            sd.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
