using Newtonsoft.Json;
using ProudChickenEksamen.Model;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace ProudChickenEksamen.Data
{
    /// <summary>
    /// Denne del er lavet af Christian, Jonas, Thomas og William med Pair-Programming på storskærm
    /// </summary>
    class JsonRepository : IRepository
    {
        public List<Kunde> SøgEfterPostnummerEllerBy(string radioButtonValg, string StedValg)
        {
            string json = File.ReadAllText("Kunder.json");

            List<Kunde> kundeliste = JsonConvert.DeserializeObject<List<Kunde>>(json);
            List<Kunde> data = new List<Kunde>();

            int i = 0;
            while (i < kundeliste.Count)
            {
                Kunde kunde = kundeliste[i];
                if (kunde.OmrådeNr == StedValg)
                {
                    data.Add(kunde);
                }
                i++;
            }
            return data;
        }

        public string GlobalConnectionString()
        {
            throw new NotImplementedException();
        }

        public List<Kunde> PostNummerEllerByValg(string a)
        {
            throw new NotImplementedException();
        }

        public void InsertSmsData(int smsType, List<Kunde> KundeListe)
        {
            throw new NotImplementedException ();
        }

        public void InsertSmsKontakter(int smsID, List<Kunde> kontaktIDListe)
        {
            throw new NotImplementedException () ;  
        }

        public List<Kunde> StartDatoSlutDatoSøgning(DateTime startDato, DateTime slutDato)
        {
            throw new NotImplementedException();
        }

        public List<Kunde> VisSmsTypeVedIDListe(int ComboBoxValg)
        {
            throw new NotImplementedException ();
        }

        public List<Kunde> VisKundeIDsSmsListe(string radioButtonValg, string StedValg)
        {
            throw new NotImplementedException ();
        }
    }
}
