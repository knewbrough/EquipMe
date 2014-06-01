using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EquipMe.Models;
using System.Collections;
using System.IO;
using CsvHelper;

namespace EquipMe.Controllers
{
    public class ArmorController : Controller
    {
        private EquipMeDBContext db = new EquipMeDBContext();

        //
        // GET: /Armor/

        public ActionResult Index()
        {
            return View(db.ArmorItems.ToList());
        }

        //
        // GET: /Armor/Details/5

        public ActionResult Details(int id = 0)
        {
            ArmorItem armoritem = db.ArmorItems.Find(id);
            if (armoritem == null)
            {
                return HttpNotFound();
            }
            return View(armoritem);            
        }

        //
        // GET: /Armor/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Armor/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArmorItem armoritem)
        {
            if (ModelState.IsValid)
            {
                db.ArmorItems.Add(armoritem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(armoritem);
        }

        [HttpPost]
        public string CsvCreate(ArmorItem armoritem)
        {
            if (ModelState.IsValid)
            {
                db.ArmorItems.Add(armoritem);
                db.SaveChanges();
                return "Armor item \"" + armoritem.Name + "\" successfully added.<br />";
            }
            
            return "Adding item \"" + armoritem.Name + "\" FAILED.<br />"; 
            
        }

        public string CsvRead()
        {
            var csv = new CsvReader(textReader);
            while (csv.Read())
            {
                var record = csv.GetRecord<Models.ArmorItem>();
                var result = CsvCreate(record);
                return result;
            }
            return "End of file.";

        }

        public ActionResult CsvUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CsvUpload(HttpPostedFileBase file)
        {            
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("../App_Data/uploads"), fileName);
                    file.SaveAs(path);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                ViewBag.Error = TempData["File is null."];
                return View();            
            }
            return RedirectToAction("Index");
        }

        public ICsvParser textReader { get; set; }

        //
        // GET: /Armor/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ArmorItem armoritem = db.ArmorItems.Find(id);
            if (armoritem == null)
            {
                return HttpNotFound();
            }
            return View(armoritem);
        }

        //
        // POST: /Armor/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArmorItem armoritem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(armoritem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(armoritem);
        }

        //
        // GET: /Armor/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ArmorItem armoritem = db.ArmorItems.Find(id);
            if (armoritem == null)
            {
                return HttpNotFound();
            }
            return View(armoritem);
        }

        //
        // POST: /Armor/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArmorItem armoritem = db.ArmorItems.Find(id);
            db.ArmorItems.Remove(armoritem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}