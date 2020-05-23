using cw11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Services
{
    public class DoctorDbService : IDoctorDbService
    {
        public Doctor addDoctor(Doctor doctor)
        {
            using (var db = new CodeFirstContext())
            {
                var st = new Doctor { IdDoctor = doctor.IdDoctor, FirstName = doctor.FirstName, LastName = doctor.LastName, Email = doctor.Email };

                db.Doctor.Add(st);
                db.SaveChanges();
                return st;
            }
        }

        public String deleteDoctor(int Id)
        {
            using (var db = new CodeFirstContext())
            {
                var st = db.Doctor.Where(emp => emp.IdDoctor == Id).FirstOrDefault();

                db.Doctor.Remove(st);
                db.SaveChanges();
                return "Usuniety doktor o ID" + Id;
            }

        }

        public String modifyDoctor(Doctor doctor)
        {
            using (var db = new CodeFirstContext())
            {
                var st = db.Doctor.Where(emp => emp.IdDoctor == doctor.IdDoctor).FirstOrDefault();
                if (st == null)
                {
                    return null;
                }
                Doctor doc = new Doctor
                {
                    IdDoctor = doctor.IdDoctor,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Email = doctor.Email
                };

                db.Attach(doc);
                db.Entry(doc).Property("FirstName").IsModified = true;
                db.Entry(doc).Property("LastName").IsModified = true;
                db.Entry(doc).Property("Email").IsModified = true;



                db.SaveChanges();
                return "Aktualizacja zakonczona";
            }
        }
      
        public Doctor showDoctor(int id)
        {
            using (var db = new CodeFirstContext())
            {
                var st = db.Doctor.Where(emp => emp.IdDoctor == id).FirstOrDefault();

                return st;
            }
        }

    }
}

