using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace ClassLibrary
{

    public class StudentsService
    {

        const string lineSeparator = "-------------------------------------------";

        private readonly DBManagerService dbManagerService;

        public StudentRepository studentRepository { get; set; } = new();

        public StudentsService(string connectionString) => dbManagerService = new(connectionString);

        #region "Read Key Confirm Methods"

        public void ReadKeyConfirm()
        {

            int selectedIndex = 0;
            ConsoleKeyInfo key;

            List<string> options = new List<string>
            {

                "Conferma -->", "Esci X"

            };

            do
            {

                Console.Clear(); // Pulisce la console prima di ristampare le opzioni
                Console.WriteLine("Confermare le modifiche?");
                Console.WriteLine(lineSeparator);
                Console.WriteLine();

                for (int i = 0; i < options.Count; i++)
                {

                    if (i == selectedIndex)
                    {

                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.White;

                    }

                    Console.WriteLine(options[i]);
                    Console.ResetColor();

                }

                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow)
                {

                    selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : options.Count - 1;

                }
                else if (key.Key == ConsoleKey.DownArrow)
                {

                    selectedIndex = (selectedIndex < options.Count - 1) ? selectedIndex + 1 : 0;

                }

            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();

            string choice = options[selectedIndex];

            if (choice == "Esci X")
            {

                Environment.Exit(0);

            }

        }

        #endregion

        #region "Get Methods - DB"

        // Get Student By Matricola

        public List<Student> GetStudentsByMatricola(string Matricola)
        {

            //ReadKeyConfirm();

            List<Student> StudentsDB = new List<Student>(); // List of Students

            // Try Catch

            try
            {

                Console.WriteLine(lineSeparator);

                Console.WriteLine($"Ricerca studenti con matricola: {Matricola}");

                dbManagerService.CheckOpenedDB(); // Check if DB is Open

                // Using: SQL Query, create @Matricola = Parameter, dbManagerService._connection = Connection
                using (SqlCommand command = new SqlCommand("SELECT * FROM Student WHERE Matricola = @Matricola", dbManagerService._connection)) // SQL Query
                {

                    command.Parameters.AddWithValue("@Matricola", Matricola); // Add Input Value On @Parameter

                    using (SqlDataReader dataReader = command.ExecuteReader()) // Esecuzione Query
                    {

                        if (!dataReader.HasRows) // Controllo dei risultati (HasRows = se ha righe)
                        {

                            Console.WriteLine("Studente non presente nel database!");

                        }
                        else
                        {

                            Console.WriteLine();
                            Console.WriteLine("Studenti trovati nel Database");
                            Console.WriteLine(lineSeparator);

                            while (dataReader.Read()) // Lettura dati fino alla prossima riga
                            {

                                // Creazione Studente
                                Student student = new Student
                                {

                                    Matricola = dataReader["Matricola"].ToString(),
                                    Name = dataReader["Name"].ToString(),
                                    Surname = dataReader["Surname"].ToString(),
                                    Age = Convert.ToInt32(dataReader["Age"]),
                                    Gender = dataReader["Gender"].ToString(),
                                    AnnoDiIscrizione = Convert.ToDateTime(dataReader["AnnoDiIscrizione"]),
                                    Department = dataReader["Department"].ToString(),

                                };

                                StudentsDB.Add(student); // Add Student to List

                            }

                        }

                    }

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Errore durante il recupero degli studenti: {ex.Message}"); // Print Error
                throw; // Throw Exception

            }

            return StudentsDB; // Return List

        }

        // Get Student By Name

        public List<Student> GetStudentsByName(string name)
        {

            Console.WriteLine(lineSeparator);

            List<Student> StudentsDB = new List<Student>(); // List of Students

            // Try Catch

            try
            {

                Console.WriteLine();

                Console.WriteLine(lineSeparator);
                Console.WriteLine($"Ricerca studenti con nome: {name}");
                Console.WriteLine(lineSeparator);

                dbManagerService.CheckOpenedDB(); // Check if DB is Open

                // Using: SQL Query, create @Name = Parameter, dbManagerService._connection = Connection
                using (SqlCommand command = new SqlCommand("SELECT * FROM Student WHERE Name = @Name", dbManagerService._connection)) // SQL Query
                {

                    command.Parameters.AddWithValue("@Name", name); // Parametri

                    using (SqlDataReader dataReader = command.ExecuteReader()) // Esecuzione Query
                    {

                        if (!dataReader.HasRows) // Controllo dei risultati (HasRows = se ha righe)
                        {

                            Console.WriteLine();
                            Console.WriteLine("Studente non presente nel database!");

                            Console.WriteLine(lineSeparator);

                        }
                        else
                        {

                            Console.WriteLine();
                            Console.WriteLine("Studenti trovati nel Database");
                            Console.WriteLine(lineSeparator);

                            while (dataReader.Read()) // Lettura dati
                            {

                                // Creazione Studente

                                Student student = new Student
                                {

                                    Matricola = dataReader["Matricola"].ToString(),
                                    Name = dataReader["Name"].ToString(),
                                    Surname = dataReader["Surname"].ToString(),
                                    Age = Convert.ToInt32(dataReader["Age"]),
                                    Gender = dataReader["Gender"].ToString(),
                                    AnnoDiIscrizione = Convert.ToDateTime(dataReader["AnnoDiIscrizione"]),
                                    Department = dataReader["Department"].ToString(),

                                };

                                // Aggiunta dello studente alla lista
                                StudentsDB.Add(student);

                            }

                        }

                    }

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Errore durante il recupero degli studenti: {ex.Message}");
                throw; // Throw Exception

            }

            return StudentsDB; // Return List

        }

        #endregion

        #region "Delete Methods - DB"

        public void DeleteStudent()
        {

            ReadKeyConfirm();
            Console.WriteLine(lineSeparator);

            Console.WriteLine("Vuoi eliminare lo studente in base alla matricola? (Y/N).");
            string choiceString = Console.ReadLine();
            Console.WriteLine();

            if (choiceString.Equals("y", StringComparison.OrdinalIgnoreCase))
            {

                try
                {

                    Console.WriteLine("Inserisci la matricola dello Studente da rimuovere.");
                    string Matricola = Console.ReadLine();

                    //GetStudentsByMatricola(Matricola);
                    ReadKeyConfirm();
                    Console.WriteLine(lineSeparator);

                    dbManagerService.CheckOpenedDB(); // Controlla se il DB è aperto

                    // Query di eliminazione
                    using (SqlCommand command = new SqlCommand("DELETE FROM Student WHERE Matricola = @Matricola", dbManagerService._connection))
                    {

                        command.Parameters.AddWithValue("@Matricola", Matricola); // Aggiunge il parametro

                        int rowsAffected = command.ExecuteNonQuery(); // Esegue la query e restituisce il numero di righe interessate

                        if (rowsAffected > 0)
                        {

                            Console.WriteLine("Studente rimosso con successo.");

                        }
                        else
                        {

                            Console.WriteLine("Nessuno studente trovato con la matricola specificata.");

                        }

                    }

                    dbManagerService.CheckClosedDB(null); // Chiude la connessione al DB

                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Errore durante la rimozione dello studente: {ex.Message}");
                    throw;

                }
            }
            else
            {

                try
                {

                    Console.WriteLine("Inserisci il nome dello Studente da rimuovere.");
                    string name = Console.ReadLine();

                    //GetStudentsByName(name);

                    Console.WriteLine("Inserisci il cognome dello Studente da rimuovere.");
                    string Surname = Console.ReadLine();

                    ReadKeyConfirm();
                    Console.WriteLine(lineSeparator);

                    dbManagerService.CheckOpenedDB(); // Controlla se il DB è aperto

                    // Query di eliminazione
                    using (SqlCommand command = new SqlCommand("DELETE FROM Students WHERE Name = @Name AND Surname = @Surname", dbManagerService._connection))
                    {

                        command.Parameters.AddWithValue("@Name", name); // Aggiunge il parametro

                        command.Parameters.AddWithValue("@Surname", Surname); // Aggiunge il parametro

                        int rowsAffected = command.ExecuteNonQuery(); // Esegue la query e restituisce il numero di righe interessate

                        if (rowsAffected > 0)
                        {

                            Console.WriteLine("Studente rimosso con successo.");

                        }
                        else
                        {

                            Console.WriteLine("Nessuno studente trovato con il nome specificato.");

                        }

                    }

                    dbManagerService.CheckClosedDB(null); // Chiude la connessione al DB

                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Errore durante la rimozione dello studente: {ex.Message}");
                    throw;

                }

            }

        }

        #endregion

        #region "Print Methods - DB"

        // Get Student By Matricola

        public void PrintStudents()
        {

            ReadKeyConfirm();
            Console.WriteLine(lineSeparator);

            Console.WriteLine("Vuoi visualizzare lo studente in base alla matricola? (Y/N).");
            string choiceString = Console.ReadLine();
            Console.WriteLine();

            if (choiceString.Equals("y", StringComparison.OrdinalIgnoreCase))
            {

                try
                {

                    Console.WriteLine("Inserisci la matricola dello Studente da visualizzare.");
                    string Matricola = Console.ReadLine();

                    List<Student> StudentsDB = GetStudentsByMatricola(Matricola);

                    Console.WriteLine();
                    Console.WriteLine(lineSeparator);
                    Console.WriteLine("Anagrafica Studente:");
                    Console.WriteLine(lineSeparator);

                    if (StudentsDB.Count == 0)
                    {

                        Console.WriteLine("Nessuno studente trovato con la matricola specificata.");

                    }

                    foreach (Student student in StudentsDB)
                    {

                        Console.WriteLine($"Matricola: {student.Matricola}");
                        Console.WriteLine($"Nome: {student.Name}");
                        Console.WriteLine($"Cognome: {student.Surname}");
                        Console.WriteLine($"Età: {student.Age}");
                        Console.WriteLine($"Genere: {student.Gender}");
                        Console.WriteLine($"Anno di iscrizione: {student.AnnoDiIscrizione}");
                        Console.WriteLine($"Dipartimento: {student.Department}");
                        Console.WriteLine(lineSeparator);

                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Errore durante il recupero degli studenti: {ex.Message}"); // Print Error
                    throw; // Throw Exception

                }

            }
            else
            {

                try
                {

                    Console.WriteLine("Inserisci il nome dello Studente da visualizzare.");
                    string name = Console.ReadLine();

                    List<Student> StudentsDB = GetStudentsByName(name);

                    Console.WriteLine();
                    Console.WriteLine(lineSeparator);
                    Console.WriteLine("Anagrafica Studente:");
                    Console.WriteLine(lineSeparator);

                    if (StudentsDB.Count == 0)
                    {

                        Console.WriteLine("Nessuno studente trovato con il nome specificato.");

                    }

                    foreach (Student student in StudentsDB)
                    {

                        Console.WriteLine($"Matricola: {student.Matricola}");
                        Console.WriteLine($"Nome: {student.Name}");
                        Console.WriteLine($"Cognome: {student.Surname}");
                        Console.WriteLine($"Età: {student.Age}");
                        Console.WriteLine($"Genere: {student.Gender}");
                        Console.WriteLine($"Anno di iscrizione: {student.AnnoDiIscrizione}");
                        Console.WriteLine($"Dipartimento: {student.Department}");
                        Console.WriteLine(lineSeparator);

                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Errore durante il recupero degli studenti: {ex.Message}"); // Print Error
                    throw; // Throw Exception

                }

            }

        }

        #endregion

        #region "Update Methods - DB"

        public bool UpdateStudent()
        {

            ReadKeyConfirm();
            Console.WriteLine(lineSeparator);

            try
            {

                Console.WriteLine("Inserisci la matricola dello Studente da aggiornare.");
                string Matricola = Console.ReadLine();

                dbManagerService.CheckOpenedDB(); // Controlla se il DB è aperto

                List<Student> matchingStudents = GetStudentsByMatricola(Matricola);

                if (matchingStudents.Count > 1)
                {

                    Console.WriteLine("Errore: Matricola duplicata, aggiornarla.");
                    return false;

                }

                Student studentToUpdate = matchingStudents.First();

                Console.WriteLine($"Nome attuale: {studentToUpdate.Name}. Inserire nuovo nome (oppure lascia vuoto):");
                string newName = Console.ReadLine();

                Console.WriteLine($"Cognome attuale: {studentToUpdate.Surname}. Inserire nuovo cognome (oppure lascia vuoto):");
                string newSurname = Console.ReadLine();

                Console.WriteLine($"Età attuale: {studentToUpdate.Age}. Inserire nuova età (oppure lascia vuoto):");
                string newAgeString = Console.ReadLine();

                int newAge = studentToUpdate.Age;

                if (!string.IsNullOrWhiteSpace(newAgeString))
                {

                    newAge = int.Parse(newAgeString);

                }

                Console.WriteLine($"Genere attuale: {studentToUpdate.Gender}. Inserire nuovo genere (oppure lascia vuoto):");
                string newGender = Console.ReadLine();

                // Aggiorna i campi solo se l'utente ha inserito un valore nuovo
                if (!string.IsNullOrWhiteSpace(newName))
                {

                    studentToUpdate.Name = newName;

                }
                if (!string.IsNullOrWhiteSpace(newSurname))
                {

                    studentToUpdate.Surname = newSurname;

                }
                if (!string.IsNullOrWhiteSpace(newGender))
                {

                    studentToUpdate.Gender = newGender;

                }

                studentToUpdate.Age = newAge;

                // Esegui l'aggiornamento nel database
                using (SqlCommand command = new SqlCommand("UPDATE Student SET Name = @Name, Surname = @Surname, Age = @Age, Gender = @Gender WHERE Matricola = @Matricola", dbManagerService._connection))
                {

                    command.Parameters.AddWithValue("@Name", studentToUpdate.Name);
                    command.Parameters.AddWithValue("@Surname", studentToUpdate.Surname);
                    command.Parameters.AddWithValue("@Age", studentToUpdate.Age);
                    command.Parameters.AddWithValue("@Gender", studentToUpdate.Gender);
                    command.Parameters.AddWithValue("@Matricola", Matricola);

                    int rowsAffected = command.ExecuteNonQuery(); // Esegue la query e restituisce il numero di righe interessate

                    if (rowsAffected > 0)
                    {

                        Console.WriteLine("Studente aggiornato con successo.");
                        return true;

                    }
                    else
                    {

                        Console.WriteLine("Nessuno studente trovato con la matricola specificata.");
                        return false;

                    }

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Errore durante l'aggiornamento dello studente: {ex.Message}");
                throw;

            }

        }

        #endregion

        #region "Add Methods - DB"

        public void AddStudent()
        {

            ReadKeyConfirm();
            Console.WriteLine(lineSeparator);

            bool isValidField = false;

            string matricola = string.Empty;
            string name = string.Empty;
            string surname = string.Empty;
            string gender = string.Empty;
            string department = string.Empty;

            int age = 0;

            DateTime annoDiIscrizione = DateTime.MinValue;

            try
            {

                Console.Clear();
                Console.WriteLine("Inserimento Studente");

                // Validazione della matricola
                while (!isValidField)
                {

                    try
                    {

                        Console.Write("Inserire Numero Matricola (4 caratteri): ");
                        matricola = Console.ReadLine();

                        if (matricola.Length == 4)
                        {

                            // Verifica se la matricola esiste già nel database
                            dbManagerService.CheckOpenedDB();

                            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Student WHERE Matricola = @Matricola", dbManagerService._connection))
                            {

                                command.Parameters.AddWithValue("@Matricola", matricola);
                                int existingCount = (int)command.ExecuteScalar();

                                if (existingCount > 0)
                                {

                                    throw new ArgumentException("La matricola inserita è già assegnata a uno studente.");

                                }

                            }

                            isValidField = true;
                        }
                        else
                        {

                            throw new ArgumentException("La matricola deve essere di 4 caratteri! ¯\\_(ツ)_/¯ ");

                        }

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Errore: " + ex.Message + ". Reinserire il dato. ¯\\_(ツ)_/¯ ");

                    }

                }

                isValidField = false; // Reset per il campo successivo

                // Validazione del nome
                while (!isValidField)
                {

                    try
                    {

                        Console.Write("Inserire Nome (almeno 2 caratteri): ");
                        name = Console.ReadLine();

                        if (name.Length >= 2)
                        {

                            isValidField = true;

                        }
                        else
                        {

                            throw new ArgumentException("Il nome deve essere di almeno 2 caratteri! ¯\\_(ツ)_/¯ ");

                        }

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Errore: " + ex.Message + ". Reinserire il dato. ¯\\_(ツ)_/¯ ");

                    }

                }

                isValidField = false; // Reset per il campo successivo

                // Validazione del cognome
                while (!isValidField)
                {

                    try
                    {

                        Console.Write("Inserire Cognome (almeno 3 caratteri): ");
                        surname = Console.ReadLine();

                        if (surname.Length >= 3)
                        {

                            isValidField = true;

                        }
                        else
                        {

                            throw new ArgumentException("Il cognome deve essere di almeno 3 caratteri! ¯\\_(ツ)_/¯ ");

                        }

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Errore: " + ex.Message + ". Reinserire il dato.");
                    }

                }

                isValidField = false; // Reset per il campo successivo

                // Validazione dell'età
                while (!isValidField)
                {

                    Console.Write("Età: ");
                    string ageInput = Console.ReadLine();

                    if (int.TryParse(ageInput, out age) && age >= 18)
                    {

                        isValidField = true;

                    }
                    else
                    {

                        Console.WriteLine("Età non valida.");

                    }

                }

                isValidField = false; // Reset per il campo successivo

                // Scelta del genere
                List<string> optionsGender = new List<string> { "Maschio", "Femmina" };

                int selectedIndexGender = 0;
                ConsoleKeyInfo keyGender;

                do
                {

                    Console.Clear();
                    Console.WriteLine("Scegli il genere:");

                    for (int i = 0; i < optionsGender.Count; i++)
                    {

                        if (i == selectedIndexGender)
                        {

                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.ForegroundColor = ConsoleColor.White;

                        }

                        Console.WriteLine(optionsGender[i]);
                        Console.ResetColor();

                    }

                    keyGender = Console.ReadKey(true);

                    if (keyGender.Key == ConsoleKey.UpArrow)
                    {

                        selectedIndexGender = (selectedIndexGender > 0) ? selectedIndexGender - 1 : optionsGender.Count - 1;

                    }
                    else if (keyGender.Key == ConsoleKey.DownArrow)
                    {

                        selectedIndexGender = (selectedIndexGender < optionsGender.Count - 1) ? selectedIndexGender + 1 : 0;

                    }

                } while (keyGender.Key != ConsoleKey.Enter);

                gender = optionsGender[selectedIndexGender];

                // Scelta della facoltà
                List<string> options = new List<string>
                {

                    "Facoltà di Giurisprudenza", "Facoltà di Economia", "Facoltà di Ingegneria",
                    "Facoltà di Medicina", "Facoltà di Lettere e Filosofia", "Facoltà di Scienze",
                    "Facoltà di Architettura", "Facoltà di Scienze Politiche", "Facoltà di Psicologia"

                };

                int selectedIndex = 0;
                ConsoleKeyInfo key;

                do
                {

                    Console.Clear();
                    Console.WriteLine("Scegli la facoltà:");

                    for (int i = 0; i < options.Count; i++)
                    {

                        if (i == selectedIndex)
                        {

                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.ForegroundColor = ConsoleColor.White;

                        }

                        Console.WriteLine(options[i]);
                        Console.ResetColor();

                    }

                    key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.UpArrow)
                    {

                        selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : options.Count - 1;

                    }
                    else if (key.Key == ConsoleKey.DownArrow)
                    {

                        selectedIndex = (selectedIndex < options.Count - 1) ? selectedIndex + 1 : 0;

                    }

                } while (key.Key != ConsoleKey.Enter);

                department = options[selectedIndex];

                // Validazione della data di iscrizione
                isValidField = false;

                Console.WriteLine();
                Console.WriteLine("Anno di iscrizione:");

                while (!isValidField)
                {

                    string dateInput = Console.ReadLine();

                    if (DateTime.TryParse(dateInput, out annoDiIscrizione))
                    {

                        isValidField = true;

                    }
                    else
                    {

                        Console.WriteLine("Data non valida.");

                    }

                }

                // Inserimento nel database
                dbManagerService.CheckOpenedDB();

                using (SqlCommand command = new SqlCommand("INSERT INTO Student (Matricola, Name, Surname, Age, Gender, Department, AnnoDiIscrizione) VALUES (@Matricola, @Name, @Surname, @Age, @Gender, @Department, @AnnoDiIscrizione)", dbManagerService._connection))
                {

                    command.Parameters.AddWithValue("@Matricola", matricola);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Surname", surname);
                    command.Parameters.AddWithValue("@Age", age);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@Department", department);
                    command.Parameters.AddWithValue("@AnnoDiIscrizione", annoDiIscrizione);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                        Console.WriteLine("Studente inserito correttamente. (｡◕‿◕｡)");

                    }
                    else
                    {

                        Console.WriteLine("Errore durante l'inserimento dello studente.");

                    }

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Errore durante l'inserimento dello studente: " + ex.Message);

            }

        }

        #endregion

    }

}
