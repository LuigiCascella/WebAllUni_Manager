using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary
{
    public class Student : Person // Eredita la classe Person
    {

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public ObjectId Id { get; set; }

        [Key]
        public string Matricola { get; set; } // Matricola

        public DateTime AnnoDiIscrizione { get; set; } // Anno di iscrizione

        public string Department { get; set; }

        public List<Exam> Exams { get; set; }

        public long TimeStamp { get; set; }

        public DateTime CreationTime { get; set; }

        public Student() : base() { } // Costruttore vuoto

        // Costruttore
        public Student(string name, string surname, int age, string gender, string matricola, DateTime annoDiIscrizione, string department)
            : base(name, surname, age, gender)
        {

            Department = department;
            Matricola = matricola;
            AnnoDiIscrizione = annoDiIscrizione;

        }

    }

}
