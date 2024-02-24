using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;



namespace WPFFudbalskiKlub
{
    public partial class FrmNoviClan : Window
    {
        private DataRowView red;

        public FrmNoviClan(bool azuriraj)
        {
            InitializeComponent();

            // Postavljanje događaja za dugmad
            btnSacuvaj.Click += btnSacuvaj_Click;
            btnOtkazi.Click += btnOtkazi_Click;

            // Primer popunjavanja ComboBox-a za OsiguranjeID
            OsiguranjeID.ItemsSource = (System.Collections.IEnumerable)GetOsiguranjeList();  // Pogledajte funkciju GetOsiguranjeList za dobavljanje podataka
            OsiguranjeID.DisplayMemberPath = "NazivOsiguranja";

            // Primer popunjavanja ComboBox-a za FudbalskiKlubID
            FudbalskiKlubID.ItemsSource = (System.Collections.IEnumerable)GetFudbalskiKlubList();  // Pogledajte funkciju GetFudbalskiKlubList za dobavljanje podataka
            FudbalskiKlubID.DisplayMemberPath = "NazivKluba";
        }

        public FrmNoviClan(bool azuriraj, DataRowView red) : this(azuriraj)
        {
            this.red = red;

            // Postavite vrednosti polja prema prosleđenom redu za ažuriranje
            Ime.Text = red["Ime"].ToString();
            Prezime.Text = red["Prezime"].ToString();
            Adresa.Text = red["Adresa"].ToString();
            Grad.Text = red["Grad"].ToString();
            Jmbg.Text = red["Jmbg"].ToString();
            Kontakt.Text = red["Kontakt"].ToString();
            Email.Text = red["Email"].ToString();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            // Primer kako dohvatiti vrednosti unesenih podataka
            string ime = Ime.Text;
            string prezime = Prezime.Text;
            string adresa = Adresa.Text;
            string grad = Grad.Text;
            string jmbg = Jmbg.Text;
            string kontakt = Kontakt.Text;
            string email = Email.Text;

            // Dodajte logiku za čuvanje podataka
            MessageBox.Show($"Podaci su uspešno sačuvani:\nIme: {ime}\nPrezime: {prezime}\nAdresa: {adresa}\nGrad: {grad}\nJMBG: {jmbg}\nKontakt: {kontakt}\nEmail: {email}", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            // Dodajte logiku za otkazivanje i zatvaranje prozora
            MessageBox.Show("Operacija otkazana.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        // Funkcija za dobavljanje podataka za ComboBox OsiguranjeID
        private DataTable GetOsiguranjeList()
        {
            // Implementirajte logiku za dobavljanje podataka iz baze ili nekog drugog izvora
            DataTable dt = new DataTable();
            // Popunite DataTable sa podacima
            return dt;
        }

        // Funkcija za dobavljanje podataka za ComboBox FudbalskiKlubID
        private DataTable GetFudbalskiKlubList()
        {
            // Implementirajte logiku za dobavljanje podataka iz baze ili nekog drugog izvora
            DataTable dt = new DataTable();
            // Popunite DataTable sa podacima
            return dt;
        }
    }
}

