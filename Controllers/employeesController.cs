using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IT_Comp.Models;
using System.IO;




namespace IT_Comp.Controllers
{
    public class employeesController : Controller
    {
        private it_compEntities db = new it_compEntities();

        // GET: employees
        public ActionResult Index()
        {
            var employees = db.employees.Include(e => e.department);
            return View(employees.ToList());
        }

        // GET: employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: employees/Create
        public ActionResult Create()
        {
            ViewBag.dno = new SelectList(db.departments, "dno", "dname");
            return View();
        }

        // POST: employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "eno,ename,gender,city,byear,salary,email,password,picture,dno")] employee employee, HttpPostedFileBase imgFile)
        {
            if (employee.salary < 500)
            {
                ModelState.AddModelError("salary", "Salary must be greater then 500 ");
            }
            if (ModelState.IsValid)
            {
                string path = "";
                //employee.picture = "~/images/emp/n.jpg";
                db.employees.Add(employee);
                db.SaveChanges();

                if (imgFile.FileName != null)
                {
                     path = "~/images/emp/" + employee.eno + ".jpg";
                    imgFile.SaveAs(Server.MapPath(path));
                    employee.picture = path;
                  
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }

            ViewBag.dno = new SelectList(db.departments, "dno", "dname", employee.dno);
            return View(employee);
        }

        // GET: employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.dno = new SelectList(db.departments, "dno", "dname", employee.dno);
            return View(employee);
        }

        // POST: employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "eno,ename,gender,city,byear,salary,email,password,picture,dno")] employee employee, HttpPostedFileBase imgFile)
        {


            string path = "";
            if (imgFile != null)
            {
                path = "~/images/" + Path.GetFileName(imgFile.FileName);
                //path = "~/images/" + employee.eno + "_" + employee.eno +".jpg";


                imgFile.SaveAs(Server.MapPath(path));
                //employee.picture = path;

            }
            employee.picture = path;
            //var originalRec = db.employees.AsNoTracking().Where(x => x.eno == employee.eno).ToList().FirstOrDefault();
            //employee.salary = originalRec.salary;
            //employee.password = originalRec.password;
            //employee.picture = originalRec.picture;


            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dno = new SelectList(db.departments, "dno", "dname", employee.dno);
            return View(employee);
        }

        // GET: employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            employee employee = db.employees.Find(id);
            db.employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public double Sum()
        {
            return double.Parse(db.employees.Sum(x => x.salary).Value.ToString());
        }

        public double Max()
        {
            return double.Parse(db.employees.Max(a => a.salary).Value.ToString());
        }

        public double Min()
        {
            return double.Parse(db.employees.Min(a => a.salary).Value.ToString());
        }
        public double Avg()
        {
            return double.Parse(db.employees.Average(a => a.salary).Value.ToString());
        }

        public int Count()
        {
            return db.employees.ToList().Count;
        }

        public ActionResult getManyRows()
        {
            var records = db.employees.ToList();
            var reco = db.employees.ToList().OrderBy(x => x.dno).ThenBy(x => x.ename);//كل البيانات
            var reco2 = db.employees.ToList().OrderBy(x => x.byear);// ترنيب بالميلاد
            var reco3 = db.employees.ToList().OrderByDescending(x => x.ename); //ترتيب بالعكس
            var reco4 = db.employees.Where(x => x.salary >= 600).ToList();//بشرط معين

            var avg = db.employees.Average(x => x.salary).Value;
            var rec = db.employees.Where(x => x.salary > avg).ToList();
            return View(rec);
        }


        public ActionResult getOneRecord()
        {
            var rec = db.employees.Find(2);
            var rec2 = db.employees.Single(x=>x.eno==2);
            var rec3 = db.employees.Where(x => x.eno == 2).ToList().FirstOrDefault();
            var rec4 = db.employees.ToList().OrderByDescending(x => x.salary).LastOrDefault();
            return View(rec4);
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn([Bind(Include ="Email,Password")]employee employee)
        {
            var rec = db.employees.Where(x => x.email == employee.email && x.password == employee.password).ToList().FirstOrDefault();

            if(rec != null)
            {
                Session["userName"] = rec.ename;
                return RedirectToAction("Index");
            }
            ViewBag.error = "Invalid User";
            return View(employee);

            
        }
        public ActionResult HomePage()
        {
            var hom = db.employees.ToList();
            return View(hom);
        }
    }
}
