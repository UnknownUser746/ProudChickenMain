using ProudChickenEksamen.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Windows;

namespace ProudChickenEksamen.Data
{
    /// <summary>
    /// Denne del er lavet af Christian, Jonas, Thomas og William med Pair-Programming på storskærm
    /// </summary>
    internal interface IRepository
    {
        public string GlobalConnectionString();
        public List<Kunde> PostNummerEllerByValg(string a);
        public void InsertSmsData(int smsType, List<Kunde> KundeListe);        
        public List<Kunde> StartDatoSlutDatoSøgning(DateTime startDato, DateTime slutDato);
        public List<Kunde> VisSmsTypeVedIDListe(int ComboBoxValg);
        public List<Kunde> VisKundeIDsSmsListe(string radioButtonValg, string StedValg);
        public List<Kunde> SøgEfterPostnummerEllerBy(string radioButtonValg, string StedValg);
    }
}
