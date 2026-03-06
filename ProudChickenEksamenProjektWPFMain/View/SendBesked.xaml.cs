using ProudChickenEksamen.Controller;
using ProudChickenEksamen.Data;
using ProudChickenEksamen.Model;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace ProudChickenEksamenProjektWPFMain
{
    /// <summary>
    /// Denne del er lavet af Christian, Jonas, Thomas og William med Pair-Programming på storskærm
    /// </summary>
    public partial class SendBesked : Window
    {
        IRepository repository;
        Controller controller;
        List<Kunde> data;
        public SendBesked()
        {
            InitializeComponent();
            repository = new SQLRepository();
            controller = new Controller(repository);            
        }

        public void VisBeskedMuligheder(object sender, RoutedEventArgs e)
        {
            var ComboBox = sender as ComboBox;
            ComboBox.ItemsSource = controller.SkafSmsListe();
            ComboBox.DisplayMemberPath = "StandardBesked";
            ComboBox.SelectedIndex = 0;
        }

        string smsEmail;
        string områdeNrBy;

        private void OpdaterValg()
        {
            if (By.IsChecked == true)
            {
                områdeNrBy = "MyndighedsNavn";
            }
            else if (OmrådeNr.IsChecked == true)
            {
                områdeNrBy = "PostNummer";
            }
        }

        private void SendBeskedKnappen(object sender, RoutedEventArgs e)
        {
            smsEmail = "";
            if (Sms.IsChecked == true)
            {
                smsEmail = "sms";
            }
            else if (Email.IsChecked == true)
            {
                smsEmail = "email";
            }     

            string stedValg = StedValg.Text;

            var beskedValg = BeskedType.SelectedItem as SMS;
            int beskedId = beskedValg.ID;

            string Bekræft = $"Vil du sende {smsEmail}type: {beskedId} til {stedValg}?";

            MessageBoxResult resultat = MessageBox.Show(Bekræft, "Bekræft Valg", MessageBoxButton.OKCancel);

            if (resultat == MessageBoxResult.OK)
            {
                controller.InsertSmsTypeOgDatoTilDatabase(beskedId, data);
                string SvarSendt = $" Du har sendt {smsEmail}type: {beskedId} til {stedValg}.";
                MessageBox.Show(SvarSendt, "Beskeden er sendt.", MessageBoxButton.OK);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Hide();
            }
        }

        private void TilbageKnappen(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }
       
        private void VisKundeListe(object sender, RoutedEventArgs e)
        {
            OpdaterValg();
            data = controller.SkafListeTilListBox(StedValg.Text, områdeNrBy);
            MyListBox.ItemsSource = data;
            MyListBox.DisplayMemberPath = "DisplayText";
        }
    }
}
