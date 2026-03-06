using ProudChickenEksamen.Data;
using ProudChickenEksamen.Model;
using ProudChickenEksamen.Services;
using ProudChickenEksamen.View;

namespace ProudChickenEksamen.Controller
{
    /// <summary>
    /// Denne del er lavet af Christian, Jonas, Thomas og William med Pair-Programming på storskærm
    /// </summary>
    class Controller
    {        
        GUI Gui;
        Chicken Chicken;
        public Controller(IRepository repository)
        {
           Gui = new GUI();
           Chicken = new Chicken(repository);
           
        }
        SQLRepository sqlrepository = new SQLRepository();
        JsonRepository jsonrepository = new JsonRepository();

        //----------------------MainMenuCasesMetode---------------------------------------------------------------------------------------------------------------------
        public void InsertSmsTypeOgDatoTilDatabase(int a, List<Kunde> kundeListe)
        {
            sqlrepository.InsertSmsData(a, kundeListe);
        }

        public List<Kunde> JSONREAD(string checkBoxValg, string StedValg)
        {
            return jsonrepository.SøgEfterPostnummerEllerBy(checkBoxValg, StedValg);
        }

        public string ConnectionStringConnection()
        {
            return sqlrepository.GlobalConnectionString();
        }

        public List<Kunde> HentKundeListe(string radioButtonValg, string valg)
        {
            return sqlrepository.SøgEfterPostnummerEllerBy(radioButtonValg, valg);
        }
        public List<Kunde> HentSmsListe(DateTime startDato, DateTime slutDato)
        {
            return sqlrepository.StartDatoSlutDatoSøgning(startDato, slutDato);
        }

        public List<Kunde> HentIDsSmsListe(string radioButtonValg, string valg)
        {
            return sqlrepository.VisKundeIDsSmsListe(radioButtonValg, valg);
        }

        public List<Kunde> HentSmsTypeListe(int ComboBoxValg)
        {
            return sqlrepository.VisSmsTypeVedIDListe(ComboBoxValg);
        }

        public List<SMS> SkafSmsListe()
        {
            return Chicken.LavSmsListe();
        }

        public List<Email> SkafEmailListe()
        {
            return Chicken.LavEmailListe();
        }

        public List<Kunde> SkafListeTilListBox(string a, string b)
        {
            return sqlrepository.ListeTilListBox(a, b);
        }
        
        /// <summary>
        /// Det nedenstående kode hører til konsoldelen men vi har valgt at beholde det i programmet som en udkommenteret del,
        /// så det kan give et indblik i hvordan vores konsoldel var sat op.
        /// </summary>
        
        /*
        public void Run()
        {
            while (true)
            {
                int choice = Gui.MainMenuMetode();

                switch (choice)
                {
                    case 1:
                        //SMSValgOgKriterieValg();
                        repository1.databaseConnection(Gui.VælgOmrådeNummer());
                        break;

                    case 2:
                        EmailValgOgKriterieValg();
                        break;

                    case 3:
                        SøgefunktionValgKriterier();
                        break;

                    case 404:
                        Gui.VisFejl();
                        break;

                    default:
                        Gui.VisFejl();
                        break;
                }
            }
        }

        //----------------------BeskedListeObjekterMetode---------------------------------------------------------------------------------------------------------------------
        */

        /*
        //----------------------SøgeFunktionCasesMetode---------------------------------------------------------------------------------------------------------------------

        public void SøgefunktionValgKriterier()
        {
            int søgeKriterieValg = Gui.SøgIKundeListeUdFraKriterier();

            switch (søgeKriterieValg)
            {
                case 1:
                    List<Kunde> matchendOmrådernr = Chicken.FindKunderOmrådeNr(Gui.VælgOmrådeNummer());
                    Gui.VisKunderFraOmrådeNr(matchendOmrådernr);
                    break;

                case 2:
                    List<Kunde> matchendeBy = Chicken.FindKunderBy(Gui.VælgBy());
                    Gui.VisKunderFraBy(matchendeBy);
                    break;

                case 3:
                    int a = Gui.VælgSMSEllerEmail();
                    if (a == 1)
                    {
                        Gui.FilterSMS(Chicken.FiltrerEfterSMSDato(Gui.VælgStartDato(), Gui.VælgSlutDato()));
                    }
                    else if  (a == 2)
                    {
                        Gui.FilterEmail(Chicken.FiltrerEfterEmailDato(Gui.VælgStartDato(), Gui.VælgSlutDato()));
                    }
                    else
                    {
                        Gui.VisFejl();                        
                    }
                    break;

                case 4:
                    List<Kunde> matchendeId = Chicken.FindKunderID(Gui.VælgID());
                    Gui.VisKunderFraID(matchendeId);
                    break;

                case 5:
                    List<string> matchendeSMSTyper = Chicken.FindAntalKunderSendtSMS(Gui.VælgSMS());
                    Gui.VisAntalSendtSMS(matchendeSMSTyper);
                    break;

                case 6:
                    List<string> matchendeEmailTyper = Chicken.FindAntalKunderSendtEmail(Gui.VælgEMail());
                    Gui.VisAntalSendtEmail(matchendeEmailTyper);
                    break;

                default:
                    Gui.VisFejl();
                    break;
            }
        }

        //----------------------BeskedListeMetode---------------------------------------------------------------------------------------------------------------------

        public void SMSValgOgKriterieValg()
        {
            int kriterieValg = Gui.VælgListeFraOmrådeNrEllerBy();
            int smsValg = Gui.VælgSMS();            
            Gui.PrintSmsListe(Chicken.SMSValgt(smsValg));
            //Chicken.ForbindelseTilSQLRepository(smsValg, kundeListe);
            switch (kriterieValg)
            {
                case 1:
                    //Gui.PrintListe(Chicken.OmrådeNummerValg());
                    break;

                case 2:
                    //Gui.PrintListe(Chicken.ByNavnValg());

                    break;

                default:
                    Gui.VisFejl();
                    break;
            }
        }

        public void EmailValgOgKriterieValg()
        {
            int kriterieValg = Gui.VælgListeFraOmrådeNrEllerBy();
            int EmailValg = Gui.VælgEMail();
            Gui.PrintEmailListe(Chicken.EmailValgt(EmailValg));

            //Chicken.ForbindelseTilSQLRepository(EmailValg);
            switch (kriterieValg)
            {
                case 1:
                    //Gui.PrintListe(Chicken.OmrådeNummerValg());
                    break;

                case 2:
                    //Gui.PrintListe(Chicken.ByNavnValg());
                    break;

                default:
                    Gui.VisFejl();
                    break;
            }
        }*/
    }
}