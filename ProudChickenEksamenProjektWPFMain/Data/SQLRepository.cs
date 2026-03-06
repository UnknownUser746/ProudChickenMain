using ProudChickenEksamen.Controller;
using ProudChickenEksamen.Model;
using System.Data.SqlClient;
using System.Runtime.InteropServices.JavaScript;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ProudChickenEksamen.Data
{
    /// <summary>
    /// Denne del er lavet af Christian, Jonas, Thomas og William med Pair-Programming på storskærm
    /// Quieries til databasen er lavet med hjælp fra ChatGPT, dog har vi selv manuelt rettet i dem så de passer til projektet
    /// </summary>
    class SQLRepository : IRepository
    {
        public string GlobalConnectionString()
        {
            return "Data Source=LAPTOP-UV1J9I1L;Initial Catalog=Kunder;Integrated Security=True;";
        }

        public List<Kunde> PostNummerEllerByValg(string Valg)
        {
            string connectionString = GlobalConnectionString();
            string svar = "";
            List<Kunde> KundeKontaktOplysninger = new List<Kunde>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Valg, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0) && !reader.IsDBNull(1))
                            {
                                int ID = reader.GetInt32(0);
                                string Name = reader.GetString(1);
                                Kunde kunder = new Kunde() { ID = ID, Navn = Name};
                                KundeKontaktOplysninger.Add(kunder);
                              
                            }
                        }
                    }
                }
            }
            return KundeKontaktOplysninger;
        }

        public List<Kunde> ListeTilListBox(string StedValg, string områdeNrBy)
        {
            List<Kunde> data = new List<Kunde>();
            string connectionString = GlobalConnectionString();
            string column = områdeNrBy == "MyndighedsNavn" ? "MyndighedsNavn" : "PostNummer";

            string query = $"SELECT DISTINCT KontaktID, Navn, MyndighedsNavn, PostNummer FROM KontaktView WHERE {column} = @value";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                if (områdeNrBy == "MyndighedsNavn")
                {
                    cmd.Parameters.AddWithValue("@value", StedValg);
                }
                else
                {
                    if (int.TryParse(StedValg, out int postnummer))
                    {
                        cmd.Parameters.AddWithValue("@value", postnummer);
                    }
                    else
                    {
                        MessageBox.Show("Indtast venligst et gyldigt postnummer.");
                        return data;
                    }
                }

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(new Kunde
                    {
                        ID = (int)reader["KontaktID"],
                        Navn = reader["Navn"].ToString(),
                        MyndighedsNavn = reader["MyndighedsNavn"].ToString(),
                        OmrådeNr = reader["PostNummer"].ToString(),
                    });
                }
            }
            return data;
        }

        public void InsertSmsData(int smsType, List<Kunde> KundeListe)
        {
            string connectionString = GlobalConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    int newSmsID = 0;

                    using (SqlCommand insertCommand = new SqlCommand(
                        "INSERT INTO SmsBeskeder (SmsType, SmsDato) VALUES (@SmsType, CAST(GETDATE() AS DATE)); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                        connection, transaction))
                    {
                        insertCommand.Parameters.AddWithValue("@SmsType", smsType);
                        newSmsID = (int)insertCommand.ExecuteScalar();
                    }
                    for (int i = 0; i < KundeListe.Count; i++)
                    {
                        int kontaktID = KundeListe[i].ID;

                        using (SqlCommand cmd = new SqlCommand(
                            "INSERT INTO KontaktSmsSammenlægning (SmsID, KontaktID) VALUES (@SmsID, @KontaktID)",
                            connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@SmsID", newSmsID);
                            cmd.Parameters.AddWithValue("@KontaktID", kontaktID);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<Kunde> StartDatoSlutDatoSøgning(DateTime startDato, DateTime slutDato)
        {
            string connectionString = GlobalConnectionString();
            List<Kunde> data = new List<Kunde>();

            string query = @" SELECT s.SmsID,s.SmsType,s.SmsDato,k.KontaktID FROM SmsBeskeder s 
                            LEFT JOIN KontaktSmsSammenlægning k ON s.SmsID = k.SmsID 
                            WHERE s.SmsDato BETWEEN @StartDato AND @SlutDato";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@StartDato", startDato);
                cmd.Parameters.AddWithValue("@SlutDato", slutDato);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(new Kunde
                    {
                        SmsID = reader["SmsID"].ToString(),
                        SmsType = reader["SmsType"].ToString(),
                        SmsDato = DateOnly.FromDateTime((DateTime)reader["SmsDato"]), //DateOnly har vi haft ChatGPT til at hjælpe med.
                        KontaktID = reader["KontaktID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["KontaktID"]) : null 
                        //Den ovenstående linje har vi også haft ChatGPT til at hjælpe med,da vi har nogle felter i database kolonnen der er null.
                    });
                }
            }

            return data;
        }

        public List<Kunde> VisSmsTypeVedIDListe(int ComboBoxValg)
        {
            List<Kunde> data = new List<Kunde>();
            string connectionString = GlobalConnectionString();

            string query = $"SELECT s.SmsType, s.SmsDato, k.KontaktID FROM SmsBeskeder s LEFT JOIN KontaktSmsSammenlægning k " +
                           $"ON s.SmsID = k.SmsID WHERE s.SmsType = {ComboBoxValg}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);


                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(new Kunde
                    {
                        KontaktID = reader["KontaktID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["KontaktID"]) : null,
                        SmsType = reader["SmsType"].ToString(),
                        SmsDato = DateOnly.FromDateTime((DateTime)reader["SmsDato"]), //DateOnly har vi haft ChatGPT til at hjælpe med.
                    });
                }
            }
            return data;
        }

        public List<Kunde> VisKundeIDsSmsListe(string radioButtonValg, string StedValg)
        {
            List<Kunde> data = new List<Kunde>();

            try
            {
                string connectionString = GlobalConnectionString();

                string query = $"SELECT s.SmsID, s.SmsType, s.SmsDato, k.KontaktID FROM SmsBeskeder s " +
                               $"LEFT JOIN KontaktSmsSammenlægning k ON s.SmsID = k.SmsID WHERE k.KontaktID = {StedValg}";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        data.Add(new Kunde
                        {
                            ID = (int)reader["KontaktID"],
                            SmsID = reader["SmsID"].ToString(),
                            SmsType = reader["SmsType"].ToString(),
                            SmsDato = DateOnly.FromDateTime((DateTime)reader["SmsDato"]),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der opstod en fejl: {ex.Message}");
            }

            return data;
        }


        public List<Kunde> SøgEfterPostnummerEllerBy(string radioButtonValg, string StedValg)
        {
            List<Kunde> data = new List<Kunde>();

            try
            {
                string connectionString = GlobalConnectionString();
                string column;

                if (radioButtonValg == "MyndighedsNavn")
                {
                    column = "MyndighedsNavn";
                }
                else if (radioButtonValg == "PostNummer")
                {
                    column = "PostNummer";
                }
                else
                {
                    column = "SmsDato";
                }

                string query = $"SELECT DISTINCT KontaktID, Navn, MyndighedsNavn, PostNummer FROM KontaktView WHERE {column} = @value";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);

                    if (radioButtonValg == "MyndighedsNavn")
                    {
                        if (string.IsNullOrEmpty(StedValg))
                        {
                            MessageBox.Show($"Indtast venligst et gyldigt {column}.");
                            return data;
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@value", StedValg);
                        }
                    }
                    else
                    {
                        if (int.TryParse(StedValg, out int postnummer))
                        {
                            cmd.Parameters.AddWithValue("@value", postnummer);
                        }
                        else
                        {
                            MessageBox.Show($"Indtast venligst et gyldigt {column}.");
                            return data;
                        }
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        data.Add(new Kunde
                        {
                            ID = (int)reader["KontaktID"],
                            Navn = reader["Navn"].ToString(),
                            MyndighedsNavn = reader["MyndighedsNavn"].ToString(),
                            OmrådeNr = reader["PostNummer"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der opstod en fejl: {ex.Message}");
            }

            return data;
        }

    }
}