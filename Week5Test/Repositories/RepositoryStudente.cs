using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week5Test.Entities;

namespace Week5Test.Repositories
{
    public class RepositoryStudente : IRepositoryStudente
    {
        const string connectionString = @"Server = .\SQLEXPRESS; Persist Security Info = False;
        Integrated Security = true; Initial Catalog = Studio;";


        public IList<Studente> GetAll()
        {
            IList<Studente> studenti = new List<Studente>();

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
                        CommandText = "SELECT * FROM Studente"
                    };
                    //esecuzione comando
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Studente studenteDaInserire = new Studente()
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            Nome = reader[1].ToString(),
                            Cognome = reader[2].ToString(),
                            AnnoNascita = Int32.Parse(reader[3].ToString()),
                        };
                        studenti.Add(studenteDaInserire);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            return studenti;
        }

        public void Add()
        {
            throw new NotImplementedException();
        }

        public Studente GetById(int value)
        {
            Studente studenteTrovato = GetAll().FirstOrDefault(studente => studente.ID==value);
            return studenteTrovato;
        }

        public void DisconnectedInsert(string nome, string cognome, int anno)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                //Creazione comando da associare all'adapter
                SqlCommand selectCommand = new SqlCommand()
                {
                    Connection = connection,
                    CommandType = System.Data.CommandType.Text,
                    CommandText = "SELECT * FROM Studente"
                };

                //Creazione comando insert
                SqlCommand insertCommand = new SqlCommand()
                {
                    Connection = connection,
                    CommandType = System.Data.CommandType.Text,
                    CommandText = "INSERT INTO Studente VALUES(@Nome, @Cognome, @AnnoNascita)"
                };

                insertCommand.Parameters.Add("@Nome", System.Data.SqlDbType.VarChar, 255, "Nome");
                insertCommand.Parameters.Add("@Cognome", System.Data.SqlDbType.VarChar, 255, "Cognome");
                insertCommand.Parameters.Add("@AnnoNascita", System.Data.SqlDbType.Int, 500, "AnnoNascita");

                //associazione comandi
                dataAdapter.SelectCommand = selectCommand;
                dataAdapter.InsertCommand = insertCommand;

                //Creo il dataset
                DataSet dataSet = new DataSet();
                try
                {
                    //Connessione verso il database
                    connection.Open();
                    dataAdapter.Fill(dataSet, "Studente");

                    //Creare una riga DataRow
                    DataRow studente = dataSet.Tables["Studente"].NewRow();
                    studente["Nome"] = nome;
                    studente["Cognome"] = cognome;
                    studente["Annonascita"] = anno;

                    dataSet.Tables["Studente"].Rows.Add(studente);

                    //Riconciliazione con l'origine dei dati
                    dataAdapter.Update(dataSet, "Studente");

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
