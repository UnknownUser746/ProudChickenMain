using ProudChickenEksamen.Model;
using ProudChickenEksamen.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ProudChickenEksamen.View
{
    class GUI
    {
        //----------------------VælgSmsEmailMetode---------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Denne del er lavet af Christian, Jonas, Thomas og William med Pair-Programming på storskærm
        /// </summary>

        /// <summary>
        /// GUI klassen bruges kun til vores Konsol Program, dvs. at den ikke bruges i WPF programmet.
        /// </summary>
        /// <returns></returns>


        public int MainMenuMetode()
        {
            try
            {
                Console.WriteLine("Vælg Metode: Send SMS (1), Send Email (2), Søg i kundeliste (3)");
                return int.Parse(Console.ReadLine());
            }
            catch 
            { 
                return 404;
            }
        }

        public int VælgSMSEllerEmail()
        {
            Console.WriteLine("Vælg SMS (1) eller Email (2).");
            return int.Parse(Console.ReadLine());
        }

        public int VælgSMS()
        {
            Console.WriteLine("Vælg Sms: VelkomstSMS (1), Er i områdetSMS (2), FarvelSMS (3) eller CustomSMS (4).");
            return int.Parse(Console.ReadLine());
        }

        public int VælgEMail()
        {
            Console.WriteLine("Vælg e-mail: VelkomstEmail (1), TilbudsEmail (2), FarvelEmail (3) eller CustomEmail (4).");
            return int.Parse(Console.ReadLine());
        }

        //----------------------VælgOmrådeNrByMetode---------------------------------------------------------------------------------------------------------------------

        public int VælgListeFraOmrådeNrEllerBy()
        {
            Console.WriteLine("Vælg mellem: OmrådeNr (1), myndighedsnavn (2).");
            return int.Parse(Console.ReadLine());
        }

        public string VælgOmrådeNummer()
        {
            Console.WriteLine("Indtast OmrådeNummer:");
            return Console.ReadLine();             
        }

        public string VælgBy()
        {
            Console.WriteLine("Indtast myndighedsnavn:");
            return Console.ReadLine();
        }

        //----------------------AndreValgOgBekræftMetode---------------------------------------------------------------------------------------------------------------------

        public int VælgID()
        {
            Console.WriteLine("Vælg brugerID:");
            return int.Parse(Console.ReadLine());
        }

        public int SøgIKundeListeUdFraKriterier()
        {
            Console.WriteLine("Søg i kundeliste ud fra områdenr (1), by (2), efter dato (3), ID (4), smstype (5), Emailtype (6)");
            return int.Parse(Console.ReadLine());
        }

        public string BekræftValgAfSMSEllerEmailOgKundeKriterie()
        {
            Console.WriteLine("Bekræftelse: Ja (1), Nej (2)");
            return Console.ReadLine();
        }

        //----------------------DatoValgMetode---------------------------------------------------------------------------------------------------------------------

        public string VælgStartDatoOgSlutDato()
        {
            Console.WriteLine("Vælg startDato (dd-MM yy) og slutDato (dd-MM yy):");
            return Console.ReadLine();
        }

        public string VælgStartDato()
        {
            Console.WriteLine("Vælg startDato (dd-MM yy):");
            return Console.ReadLine();
        }

        public string VælgSlutDato()
        {
            Console.WriteLine("Vælg slutDato (dd-MM yy):");
            return Console.ReadLine();
        }


        //----------------------DatoFilterMetode---------------------------------------------------------------------------------------------------------------------

        public void FilterSMS(List<(DateTime, string, int)> matchendeBesked)
        {
            List<int> visteKundeIds = new List<int>();

            foreach (var besked in matchendeBesked)
            {
                int kundeId = besked.Item3;

                if (!visteKundeIds.Contains(kundeId))
                {
                    Console.WriteLine("Kunde id: " + kundeId);

                    foreach (var b in matchendeBesked)
                    {
                        if (b.Item3 == kundeId)
                        {
                            Console.WriteLine($"{b.Item1:dd-MM yy} - SMS type: {b.Item2}");
                        }
                    }

                    Console.WriteLine();
                    visteKundeIds.Add(kundeId);
                }
            }
        }

        public void FilterEmail(List<(DateTime, string, int)> matchendeBesked)
        {
            List<int> visteKundeIds = new List<int>();

            foreach (var besked in matchendeBesked)
            {
                int kundeId = besked.Item3;

                if (!visteKundeIds.Contains(kundeId))
                {
                    Console.WriteLine("Kunde id: " + kundeId);

                    foreach (var b in matchendeBesked)
                    {
                        if (b.Item3 == kundeId)
                        {
                            Console.WriteLine($"{b.Item1:dd-MM yy} - Email type: {b.Item2}");
                        }
                    }

                    Console.WriteLine();
                    visteKundeIds.Add(kundeId);
                }
            }
        }

        //----------------------PrintListeMetode---------------------------------------------------------------------------------------------------------------------

        public void PrintListe(string a)
        {           
                Console.WriteLine(a);                
        }

        public List<SMS> PrintSmsListe(List<SMS> nyListe)
        {
            Console.WriteLine(nyListe[0].SMSStandardBesked);
            nyListe.RemoveAt(0);
            return nyListe;
        }

        public List<Email> PrintEmailListe(List<Email> nyListe)
        {
            Console.WriteLine(nyListe[0].EmailStandardBesked);
            nyListe.RemoveAt(0);
            return nyListe;
        }

        public List<Kunde> VisKunderFraOmrådeNr(List<Kunde> kunder)
        {
            if (kunder == null || kunder.Count == 0)
            {
                Console.WriteLine("Ingen kunder fundet med de angivne kriterier.");
                return kunder;
            }

            Console.WriteLine("\nDisse kunder bor i områdenummer: " + kunder[0].OmrådeNr);
            int i = 0;
            while (i < kunder.Count)
            {
                Console.WriteLine("Kunde ID: " + kunder[i].ID);
                i++;
            }
            Console.WriteLine("");
            return kunder;
        }

        public List<Kunde> VisKunderFraBy(List<Kunde> kunder)
        {
            if (kunder == null || kunder.Count == 0)
            {
                Console.WriteLine("Ingen kunder fundet med de angivne kriterier.");
                return kunder;
            }

            Console.WriteLine("\nDisse kunder bor i byen: " + kunder[0].MyndighedsNavn);
            int i = 0;
            while (i < kunder.Count)
            {
                Console.WriteLine("Kunde ID: " + kunder[i].ID);
                i++;
            }
            Console.WriteLine("");
            return kunder;
        }

        public List<Kunde> VisKunderFraID(List<Kunde> kunder)
        {
            if (kunder == null || kunder.Count == 0)
            {
                Console.WriteLine("Ingen kunder fundet med de angivne kriterier.");
                return kunder;
            }

            Console.WriteLine("\nKundeId nummer: " + kunder[0].ID + " tilhører ");
            int i = 0;
            while (i < kunder.Count)
            {
                Console.WriteLine($"\nNavn: {kunder[i].Navn}\nAdresse: {kunder[i].VejAdresseringsNavn}\nOmrådenr: {kunder[i].OmrådeNr}" +
                    $"\nBy: {kunder[i].MyndighedsNavn}\nMobilnr: {kunder[i].Mobilnummer}\nEmail: {kunder[i].Email}");
                i++;
            }
            Console.WriteLine("");
            return kunder;
        }

        public List<string> VisAntalSendtSMS(List<string> antalSms)
        {
            int i = 0;
            while (i < antalSms.Count)
            {
                Console.WriteLine(antalSms[i]);
                i++;
            }
            Console.WriteLine();
            return antalSms;
        }

        public List<string> VisAntalSendtEmail(List<string> antalEmail)
        {
            int i = 0;
            while (i < antalEmail.Count)
            {
                Console.WriteLine(antalEmail[i]);
                i++;
            }
            Console.WriteLine();
            return antalEmail;

        }

        public List<Kunde> VisHeleKundeListenMedAlleInfo(List<Kunde> kunder)
        {
            if (kunder == null || kunder.Count == 0)
            {
                Console.WriteLine("Ingen kunder fundet med de angivne kriterier.");
                return kunder;
            }

            Console.WriteLine("1 = VelkomstSMS, 2 = TilbudsSMS 3 = FarvelSMS 4 = CustomSMS");
            int i = 0;
            while (i < kunder.Count)
            {
                Console.WriteLine($"\nID: {kunder[i].ID}, navn: {kunder[i].Navn}, adresse: {kunder[i].VejAdresseringsNavn}, områdenr: {kunder[i].OmrådeNr}, " +
                    $"by: {kunder[i].MyndighedsNavn}, mobilnr: {kunder[i].Mobilnummer}, email: {kunder[i].Email}");

                if (kunder[i].SendtSMS != null)
                {
                    Console.Write("Sms Type: ");
                    int j = 0;
                    while (j < kunder[i].SendtSMS.Count)
                    {

                        Console.Write($"{kunder[i].SendtSMS[j]}, ");

                        j++;
                    }
                    Console.Write("\nSms dato: ");
                    j = 0;
                    while (j < kunder[i].SendtSMS.Count)
                    {

                        Console.Write($"{kunder[i].SendtSMSDato[j]}, ");

                        j++;
                    }
                    Console.Write("\nEmail type: ");
                    j = 0;
                    while (j < kunder[i].SendtEmail.Count)
                    {

                        Console.Write($"{kunder[i].SendtEmail[j]}, ");

                        j++;
                    }
                    Console.Write("\nEmail dato: ");
                    j = 0;
                    while (j < kunder[i].SendtEmail.Count)
                    {

                        Console.Write($"{kunder[i].SendtEmailDato[j]}, ");

                        j++;
                    }
                }
                i++;
                Console.WriteLine(" ");
            }
            return kunder;
        }

        //----------------------CustomBeskedMetode---------------------------------------------------------------------------------------------------------------------

        public string CustomSMS()
        {
            Console.WriteLine("Input customSMS: ");
            return Console.ReadLine();
        }

        public string CustomEmail()
        {
            Console.WriteLine("Input custom Email: ");
            return Console.ReadLine();
        }        

        //----------------------FejlMeddelelseMetode---------------------------------------------------------------------------------------------------------------------

        public void VisFejl()
        {
            Console.WriteLine("Ugyldigt valg");
        }

        public void AfvisBekræft()
        {
            Console.Clear();
            Console.WriteLine("Afsendelse afbrudt. Du sendes til Main Menu.\n");
        }
    }
}
