using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week5Test.Entities;

namespace Week5Test.Repositories
{
    class RepositoryEsame : IRepositoryEsame
    {
        const string connectionString = @"Server = .\SQLEXPRESS; Persist Security Info = False;
        Integrated Security = true; Initial Catalog = Studio;";

        public IList<Esame> GetAll()
        {
            IList<Esame> esami = new List<Esame>();

            //RECUPERO DA DB
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    //apertura connessione
                    connection.Open();

                    //creare il comando
                    SqlCommand command = new SqlCommand()
                    {
                        Connection = connection,
                        CommandType = System.Data.CommandType.Text,
                        CommandText = "SELECT * FROM Esame"
                    };
                    //esecuzione comando
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Esame esameDaInserire = new Esame()
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            Nome = reader[1].ToString(),
                            CFU = Int32.Parse(reader[2].ToString()),
                            DataE = DateTime.Parse(reader[3].ToString()),
                            Votazione = Int32.Parse(reader[4].ToString()),
                            //Passato = bool.Parse(reader[5].ToString()),
                           
                            StudenteID = Int32.Parse(reader[6].ToString()),
                        };
                        esami.Add(esameDaInserire);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            return esami;
        }

        public void Add()
        {
            Console.WriteLine("Inserisci nome esame");
            string nome = Console.ReadLine();
            Console.WriteLine("Inserisci cfu esame");
            int cfu = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Inserisci data esame");
            DateTime data = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Inserisci voto esame");
            int votazione = Convert.ToInt32(Console.ReadLine());
            bool pass = false;
            if (votazione > 17)
            {
                pass = true;
            }
            Console.WriteLine("Inserisci studente ID");
            int studID = Convert.ToInt32(Console.ReadLine());

            RepositoryStudente rs = new RepositoryStudente();
            if (rs.GetById(studID) == null)
            {
                Console.WriteLine("studente inesistente");
            }
            else
            {
                //Creare la connessione
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        //Aprire la connessione
                        connection.Open();

                        //Creare il comando
                        SqlCommand insertCommand = new SqlCommand();
                        insertCommand.Connection = connection;
                        insertCommand.CommandType = System.Data.CommandType.Text;
                        insertCommand.CommandText = "INSERT INTO Esame VALUES(@Nome, @CFU, @DataE, @Votazione, " +
                            "@Passato, @StudenteID)";

                        
                        insertCommand.Parameters.AddWithValue("@Nome", nome);
                        insertCommand.Parameters.AddWithValue("@CFU", cfu);
                        insertCommand.Parameters.AddWithValue("@DataE", data);
                        insertCommand.Parameters.AddWithValue("@Votazione", votazione);
                        insertCommand.Parameters.AddWithValue("@Passato", pass);
                        insertCommand.Parameters.AddWithValue("@StudenteID", studID);

                        //Esecuzione del comando
                        int row = insertCommand.ExecuteNonQuery();
                        Console.WriteLine("Numero di righe aggiornate");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public Esame GetById(int value)
        {
            Esame  esameTrovato = GetAll().FirstOrDefault(esame => esame.ID == value);
            return esameTrovato;
        }

        public IList<Esame> EsamiPerStudente(int value)
        {


            //LAMBDA EXPRESSION
            IEnumerable<Esame> esamiOrdinati = GetAll()
                .OrderBy(esame => esame.Votazione)
                .ThenBy(esame => esame.DataE)
                .Where(esame => esame.StudenteID.Equals(value));

            return esamiOrdinati.ToList();
        }



    }
}
