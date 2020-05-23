using cw11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Services
{
    public interface IDoctorDbService
    {
        public Doctor addDoctor(Doctor doctor);
        public Doctor showDoctor(int id);
        public String deleteDoctor(int Id);
        public String modifyDoctor(Doctor doctor);
    }
}
