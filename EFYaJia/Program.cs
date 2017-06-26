using EFYaJia.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFYaJia
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new Models.ContosoUniversityEntities())
            {
                //QueryData(db);

                //addNewRecord(db);
                //update(db);

                //db.Database.Log = (log) => { Console.WriteLine(log); };

                //foreach (var item in db.Course.Where(p=>p.CourseID>=12 && p.CourseID <=16).ToList())
                //{
                //    db.Course.Remove(item);
                //}

                //多對多關聯CRUD(db);
                var c = db.Course.Find(1);

                Console.WriteLine(db.Entry(c).State);
                
                c.Credit++;

                Console.WriteLine(db.Entry(c).State);
                var ce = db.Entry(c);
                Console.WriteLine("修改前: " + ce.OriginalValues.GetValue<int>("Credit"));
                Console.WriteLine("修改後: " + ce.CurrentValues.GetValue<int>("Credit"));
                
                var cc = new Course()
                {
                    Title = "Hello 2",
                    Department = db.Department.Find(5)
                 };
                db.Course.Add(cc);
                
                var ce2 = db.Entry(cc);
                
                if (ce2.State == System.Data.Entity.EntityState.Added)

                {
                    ce.Entity.Credit++;
                }

                db.SaveChanges();
                
                Console.WriteLine(db.Entry(c).State);
                Console.WriteLine(ce2.State);
                //try
                //{
                //    db.SaveChanges();

                //}
                //catch (DbEntityValidationException ex)
                //{

                //    throw ex;
                //}


                //QueryData(db);


            }
        }

        private static void 多對多關聯CRUD(Models.ContosoUniversityEntities db)
        {
            var c = db.Course.Find(1);
            ///由課程去新增一個新的人員
            c.Person.Add(new Person { FirstName = "Sam", LastName = "Walker", Discriminator = "123" });
            ///由課程去新增一個現有的人員
            c.Person.Add(db.Person.Find(3));
            ///由課程去剔除一個現有的人員
            c.Person.Remove(db.Person.Find(3));

            db.Person.Remove(db.Person.Find(3));

            ///由人員去新增一個現有課程
            var p = db.Person.Find(31);

            p.Course.Add(db.Course.Find(3));
        }

        private static void update(Models.ContosoUniversityEntities db)
        {
            foreach (var item in db.Course.ToList())
            {
                item.Credit  = 3;
            }
        }

        private static void addNewRecord(Models.ContosoUniversityEntities db)
        {
            db.Course.Add(new Course()
            {
                Title = "Hello 1",
                Department = db.Department.Find(5)
            });
        }

        private static void QueryData(Models.ContosoUniversityEntities db)
        {
            var depts = db.Department.ToList();

            foreach (var dept in depts)
            {
                Console.WriteLine("部門: " + dept.Name);
                foreach (var course in dept.Course)
                {
                    Console.WriteLine("\t" + course.Title);
                }
            }
        }
    }
}
