using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ClassLibrary;
using WebAllUni_Manager.DataModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAllUni_Manager.Controllers
{

    [Route("[controller]")]
    [ApiController]

    public class StudentsController : ControllerBase
    {

        List<Student> students = [];

        DBManagerService dBManagerService;

        StudentsService studentsService;


        public StudentsController()
        {

            dBManagerService = new(DBConnection.ConnectionString);

            studentsService = new StudentsService(DBConnection.ConnectionString);

        }


        // GET: api/<StudentsController>

        [HttpGet("byMatricola/{Matricola}")]

        public List<Student> GetMatricola(string Matricola)
        {

            Console.WriteLine("Matricola: " + Matricola);
            return studentsService.GetStudentsByMatricola(Matricola);

        }

        [HttpGet("byName/{Name}")]

        public List<Student> GetName(string Name)
        {

            Console.WriteLine("Nome: " + Name);
            return studentsService.GetStudentsByName(Name);

        }

        // PUT: api/<StudentsController>

        [HttpPut]

        public string Put()
        {

            studentsService.AddStudent();
            return "STUDENTE INSERITO CORRETTAMENTE!";

        }

        // DELETE: api/<StudentsController>

        [HttpDelete]

        public string Delete()
        {

            studentsService.DeleteStudent();
            return "STUDENTE ELIMINATO CORRETTAMENTE!";

        }

    }

}
