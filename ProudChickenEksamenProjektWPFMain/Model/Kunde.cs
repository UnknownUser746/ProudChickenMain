using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProudChickenEksamen.Model
{
    /// <summary>
    /// Denne del er lavet af Christian, Jonas, Thomas og William med Pair-Programming på storskærm
    /// </summary>
    class Kunde
    {   
        public int ID { get; set; }
        public int? KontaktID { get; set; }
        public string Navn { get; set; }
        public string VejAdresseringsNavn { get; set; }
        public string OmrådeNr { get; set; }
        public string MyndighedsNavn { get; set; }
        public string Mobilnummer { get; set; }
        public string Email { get; set; }
        public string SmsID { get; set; }
        public string SmsType { get; set; }
        public DateOnly SmsDato { get; set; }
        public List<string> SendtSMS { get; set; }
        public List<string> SendtSMSDato { get; set; }
        public List<string> SendtEmail { get; set; }
        public List<string> SendtEmailDato { get; set; }
        public string DisplayText => $"ID : {ID}, Navn : {Navn}, By : {MyndighedsNavn},  OmrådeNr : {OmrådeNr}";
        public string DisplaySmsText => $"KontaktID : {KontaktID}, SmsType : {SmsType}, SmsDato : {SmsDato}";
        public string DisplayIDText => $"ID : {ID}, SmsType : {SmsType}, SmsDato : {SmsDato}";
        public string DisplaySmsTypeText => $"ID : {KontaktID}, SmsType : {SmsType}, SmsDato : {SmsDato}";
        public string JsonSelectedText => $"{Navn} - {OmrådeNr}";

    }
}
