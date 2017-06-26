using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFYaJia
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ContosoUniversityEntities())
            {

                QueryData(db);

                //addNewRecord(db);
                //update(db);

                //db.Database.Log = (log) => { Console.WriteLine(log); };

                foreach (var item in db.Course.Where(p=>p.CourseID>=12 && p.CourseID <=16).ToList())
                {
                    db.Course.Remove(item);
                }

                db.SaveChanges();


                QueryData(db);


            }
        }

        private static void update(ContosoUniversityEntities db)
        {
            foreach (var item in db.Course.ToList())
            {
                item.Credit += 3;
            }
        }

        private static void addNewRecord(ContosoUniversityEntities db)
        {
            db.Course.Add(new Course()
            {
                Title = "Hello 1",
                Department = db.Department.Find(5)
            });
        }

        private static void QueryData(ContosoUniversityEntities db)
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
