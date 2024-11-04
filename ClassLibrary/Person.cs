using MongoDB.Bson.Serialization.Attributes;

namespace ClassLibrary
{

    // Abstract Class

    public abstract class Person
    {

        public string Name { get; set; }
       
        public string Surname { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public Person() { }

        public Person(string name, string surname, int age, string gender)
        {

            Name = name;
            Surname = surname;
            Age = age;
            Gender = gender;

        }

    }

}
