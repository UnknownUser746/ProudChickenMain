using ProudChickenEksamen.Controller;
using ProudChickenEksamen.Data;
using ProudChickenEksamen.Model;
using ProudChickenEksamen.View;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProudChickenEksamenProjektWPFMain
{
    /// <summary>
    /// Denne del er lavet af Christian, Jonas, Thomas og William med Pair-Programming på storskærm
    /// </summary>
    public partial class Søgninger : Window 
    {        
        Controller controller;

        public Søgninger()  
        {
            InitializeComponent();
            IRepository repository = new SQLRepository();
            controller = new Controller(repository);
        }

        //private void RepoSelect_Checked(object sender, RoutedEventArgs e)
        //{
        ////    IRepository repository2 = null;
        ////    if (SQLSELECT.IsChecked == true)            
        ////    {                
        ////        repository2 = new SQLRepository();
        ////    }
        ////    else if (JSONSELECT.IsChecked == true)
        ////    {
        ////        repository2 = new JsonRepository();                
        ////    }
        ////    controller = new Controller(repository2);         
        //}

        public void VisBeskedMuligheder(object sender, RoutedEventArgs e)
        {
            var ComboBox = sender as ComboBox;
            ComboBox.ItemsSource = controller.SkafSmsListe();
            ComboBox.DisplayMemberPath = "StandardBesked";
            ComboBox.SelectedIndex = 0;
        }
        string checkBoxValg;

        private void OpdaterCheckBoxValg(object sender, RoutedEventArgs e)
        {
            OpdaterValg();
        }

        private void VisKundeListe(object sender, RoutedEventArgs e)
        {
            OpdaterValg(); 

            if (JSONSELECT.IsChecked == true && OmrådeNr.IsChecked == true)
            {                
                var kunder = controller.JSONREAD(checkBoxValg, StedValg.Text);      
                MyListBox.ItemsSource = kunder;
                MyListBox.DisplayMemberPath = "JsonSelectedText"; 
                return;
            }            
            if (Dato.IsChecked == true)
            {
                DateTime startDato = FraDato.SelectedDate.Value;
                DateTime slutDato = TilDato.SelectedDate.Value;
                var data = controller.HentSmsListe(startDato, slutDato);
                MyListBox.ItemsSource = data;
                MyListBox.DisplayMemberPath = "DisplaySmsText";
            }
            else if (ID.IsChecked == true)
            {
                var data = controller.HentIDsSmsListe(checkBoxValg, StedValg.Text);
                MyListBox.ItemsSource = data;
                MyListBox.DisplayMemberPath = "DisplayIDText";
            }
            else if (SmsType.IsChecked == true)
            {
                SMS SmsType = (SMS)BeskedType.SelectedItem;
                var data = controller.HentSmsTypeListe(SmsType.ID);
                MyListBox.ItemsSource = data;
                MyListBox.DisplayMemberPath = "DisplaySmsTypeText";
            }
            else
            {
                var data = controller.HentKundeListe(checkBoxValg, StedValg.Text);
                MyListBox.ItemsSource = data;
                MyListBox.DisplayMemberPath = "DisplayText";
            }
        }
        bool harVistJsonBesked = false;
        private void OpdaterValg()
        {
            
            if (JSONSELECT.IsChecked == true && !harVistJsonBesked)
            {
                MessageBox.Show("Der kan kun søges efter OmmrådeNr, når Json er valgt. " +
                                "\nDa det er dummydata, så er der begrænset data søgning, så vælg venligst område 8000.");
                harVistJsonBesked = true;
            }
            if (By.IsChecked == true)
            {
                checkBoxValg = "MyndighedsNavn";
                FraDato.IsEnabled = false;
                TilDato.IsEnabled = false;
                visKundeKnappen.IsEnabled = true;
            }
            if (OmrådeNr.IsChecked == true)
            {
                checkBoxValg = "PostNummer";
                FraDato.IsEnabled = false;
                TilDato.IsEnabled = false;
                visKundeKnappen.IsEnabled = true;
            }
            if (Dato.IsChecked == true)
            {
                checkBoxValg = "SmsDato";
                FraDato.IsEnabled = true;
                TilDato.IsEnabled = true;
                visKundeKnappen.IsEnabled = true;
            }
            if (ID.IsChecked == true)
            {
                checkBoxValg = "ID";
                FraDato.IsEnabled = false;
                TilDato.IsEnabled = false;
                visKundeKnappen.IsEnabled = true;
            }
            if (SmsType.IsChecked == true)
            {
                checkBoxValg = "SmsType";
                FraDato.IsEnabled = false;
                TilDato.IsEnabled = false;
                visKundeKnappen.IsEnabled = true;
            }

        }

        private void TilbageKnappen(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }        
    }
}
