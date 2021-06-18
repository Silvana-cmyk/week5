using System;
using System.Collections.Generic;
using Week5Test.Entities;
using Week5Test.Repositories;

namespace Week5Test
{
    class Program
    {
        static void Main(string[] args)
        {

            bool continua = true;
            while (continua)
            {
                int scelta = SchermoMenu();

                switch (scelta)
                {
                    case 1:
                        VisualizzaStudenti();
                        break;
                    case 2:
                        RegistraEsame();
                        break;
                    case 3:
                        MostraEsamiPerStudenti();
                        break;
                    case 4:
                        AggiungiStudente();
                        break;
                    //case 0: //casistica dell'eccezione
                    //    break;
                    default: //un numero diverso da 0
                        continua = false;
                        Console.WriteLine("Sistema chiuso");
                        break;
                }
            }


             static int SchermoMenu()
             {
                Console.WriteLine("1. visualizza studenti");
                Console.WriteLine("2. registra esame");
                Console.WriteLine("3. mostar esami per studente");
                Console.WriteLine("4. aggiungi studente");
                Console.WriteLine("5. Premi un altro numero uscire");
                int scelta = 0;
                try
                {
                    scelta = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Inserisci un numero corretto");
                    scelta = 0;
                }
                return scelta;
             }
        }

        private static void AggiungiStudente()
        {
            try
            {
                RepositoryStudente rs = new RepositoryStudente();
                Console.WriteLine("Inserisci nome studente");
                string nome = Console.ReadLine();
                Console.WriteLine("Inserisci cognome studente");
                string cognome = Console.ReadLine();
                Console.WriteLine("Inserisci anno nascita studente");
                int anno = Convert.ToInt32(Console.ReadLine());
                rs.DisconnectedInsert(nome, cognome, anno);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void MostraEsamiPerStudenti()
        {
            Console.WriteLine("Inserisci ID studente");
            try
            {
                int i = Convert.ToInt32(Console.ReadLine());
                RepositoryEsame re = new RepositoryEsame();
                foreach (var esame in re.EsamiPerStudente(i))
                {
                    Console.WriteLine("ID: {0} Nome: {1} Votazione: {2} DataE: {3} Passato: {4}",
                        esame.ID, esame.Nome, esame.Votazione, esame.DataE, esame.Passato);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void RegistraEsame()
        {
            RepositoryEsame re = new RepositoryEsame();
            re.Add();
        }

        private static void VisualizzaStudenti()
        {
            RepositoryStudente rs = new RepositoryStudente();
            foreach (var studente in rs.GetAll())
            {
                Console.WriteLine("ID: {0} Nome: {1} Cognome: {2} anno: {3}",
                    studente.ID, studente.Nome, studente.Cognome, studente.AnnoNascita);
            }
        }
    }
}
