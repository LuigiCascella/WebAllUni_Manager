using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassLibrary;
using WebAllUni_Manager.DataModel;


namespace WebAllUni_Manager.Controllers
{

    [Route("[controller]")]
    [ApiController]

    public class StudentEFController : ControllerBase
    {

        private readonly StudentDBContext _context;

        public StudentEFController(StudentDBContext context)
        {

            _context = context;

        }

        // GET: api/StudentEFs
        [HttpGet]

        public async Task<ActionResult<IEnumerable<StudentEF>>> GetStudentEFs()
        {

            return await _context.StudentEF.ToListAsync();

        }

        // GET: api/StudentEFs/5
        [HttpGet("{matricola}")]

        public async Task<ActionResult<StudentEF>> GetStudentEF(string matricola)
        {

            var StudentEF = await _context.StudentEF.FindAsync(matricola);

            if (StudentEF == null)
            {

                return NotFound();

            }

            return StudentEF;

        }

        // PUT: api/StudentEFs/5

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkmatricola=2123754
        
        [HttpPut("{matricola}")]

        public async Task<IActionResult> PutStudentEF(string matricola, StudentEF StudentEF)
        {

            if (matricola != StudentEF.Matricola)
            {

                return BadRequest();

            }

            _context.Entry(StudentEF).State = EntityState.Modified;

            try
            {

                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {

                if (!StudentExists(matricola))
                {

                    return NotFound();

                }
                else
                {

                    throw;

                }
            }

            return NoContent();

        }

        // POST: api/StudentEFs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkmatricola=2123754
        [HttpPost]
        public async Task<ActionResult<StudentEF>> PostStudentEF(StudentEF StudentEF)
        {

            _context.StudentEF.Add(StudentEF);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentEF", new { matricola = StudentEF.Matricola }, StudentEF);
        
        }

        // DELETE: api/StudentEFs/5
        [HttpDelete("{matricola}")]

        public async Task<IActionResult> DeleteStudentEF(int matricola)
        {
            var StudentEF = await _context.StudentEF.FindAsync(matricola);
            if (StudentEF == null)
            {

                return NotFound();

            }

            _context.StudentEF.Remove(StudentEF);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        private bool StudentExists(string matricola)
        {

            return _context.StudentEF.Any(s => s.Matricola == matricola);

        }

    }

}
