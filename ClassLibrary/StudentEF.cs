using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{

    public class StudentEF : Person // Eredita la classe Person
    {

        [Key]
        public string Matricola { get; set; } // Matricola

        public DateTime AnnoDiIscrizione { get; set; } // Anno di iscrizione

        public string Department { get; set; }

        public StudentEF() : base() { } // Costruttore vuoto

        // Costruttore
        public StudentEF(string name, string surname, int age, string gender, string matricola, DateTime annoDiIscrizione, string department)
            : base(name, surname, age, gender)
        {

            Department = department;
            Matricola = matricola;
            AnnoDiIscrizione = annoDiIscrizione;

        }


    }

}