using System;
using System.Collections.Generic;
using System.Data;
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



namespace WPFFudbalskiKlub.Forme
{
    public partial class FrmVlasnikKluba : Window
    {
        public FrmVlasnikKluba()
        {
            InitializeComponent();

            // Postavljanje događaja za dugmad
            btnSacuvaj.Click += btnSacuvaj_Click;
            btnOtkazi.Click += btnOtkazi_Click;
        }

        public FrmVlasnikKluba(bool azuriraj, DataRowView red)
        {
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            // Primer kako dohvatiti vrednosti unesenih podataka
            string ime = txtIme.Text;
            string prezime = txtPrezime.Text;
            string vlasnikID = txtVlasnikID.Text;

            // Dodajte logiku za čuvanje podataka
            MessageBox.Show($"Podaci uspešno sačuvani:\nIme: {ime}\nPrezime: {prezime}\nVlasnik ID: {vlasnikID}", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            // Dodajte logiku za otkazivanje i zatvaranje prozora
            MessageBox.Show("Operacija otkazana.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}

