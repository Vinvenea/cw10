using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw11.Models;
using cw11.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw11.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private IDoctorDbService _service;

        public DoctorController(IDoctorDbService service)
        {
            _service = service;
        }

        [HttpPut]
        public IActionResult PutDoctor(Doctor doctor)
        {
            _service.addDoctor(doctor);

            return Ok("Doktor dodany");
        }
        [HttpPost]
        public IActionResult modify(Doctor doctor)
        {

            if (_service.modifyDoctor(doctor) == null)
            {
                return BadRequest("Cos poszlo nie tak");
            }

            return Ok("Aktualizacja zakonczona");
        }
        [HttpDelete("{id}")]
        public IActionResult deleteDoctor(int id)
        {
           if( _service.deleteDoctor(id) == null)
            {
                return BadRequest("Cos poszlo nie tak");
            }
            return Ok("Usuwanie zakonczone");
        }

        [HttpGet("{id}")]
        public IActionResult getDoctor(int id)
        {
            var st = _service.showDoctor(id);

           if(st == null)
            {
                return BadRequest("Cos poszlo nie tak");
            }
            return Ok(st);
        }

    }
}