using System.Configuration;
using System.Text.Json;


namespace ClassLibrary
{

    public class StudentRepository
    {

        public List<Student> Students { get; set; } = [];

        #region "Import Methods"

        public void ImportStudents()
        {

            string? url = ConfigurationManager.AppSettings["PathImportStudents"];

            try
            {

                string sStudents = File.ReadAllText(url);
                Students = JsonSerializer.Deserialize<List<Student>>(sStudents);


            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }

        }

        #endregion 

    }

}
