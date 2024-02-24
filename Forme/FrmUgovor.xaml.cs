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



namespace WPFFudbalskiKlub
{
    public partial class FrmUgovor 
    {
        public FrmUgovor()
        {
            InitializeComponent();

            // Postavljanje događaja za dugmad
            btnSacuvaj.Click += btnSacuvaj_Click;
            btnOtkazi.Click += btnOtkazi_Click;

            // Primer popunjavanja ComboBox-a za NoviClanID
            // Dodajte logiku za punjenje ComboBox-a prema vašim potrebama
            cbNoviClanID.ItemsSource = GetNoviClanData(); // Postavite izvor podataka za ComboBox;
            cbNoviClanID.DisplayMemberPath = "ImePrezime"; // Postavite naziv svoje svojstvo u cbNoviClanID

            // Primer popunjavanja ComboBox-a za VlasnikID
            // Dodajte logiku za punjenje ComboBox-a prema vašim potrebama
            cbVlasnikID.ItemsSource = GetVlasnikKlubaData(); // Postavite izvor podataka za ComboBox;
            cbVlasnikID.DisplayMemberPath = "ImePrezime"; // Postavite naziv svoje svojstvo u cbVlasnikID

            // Primer popunjavanja ComboBox-a za MenadzerID
            // Dodajte logiku za punjenje ComboBox-a prema vašim potrebama
            cbMenadzerID.ItemsSource = GetMenadzerData(); // Postavite izvor podataka za ComboBox;
            cbMenadzerID.DisplayMemberPath = "ImePrezime"; // Postavite naziv svoje svojstvo u cbMenadzerID
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            // Primer kako dohvatiti vrednosti unesenih podataka
            string ugovorID = UgovorID.Text;
            DateTime datum = Datum.SelectedDate ?? DateTime.Now; // Koristi trenutni datum ako nije izabran drugi
            string novcanaVrednost = NovcanaVrednost.Text;
            int noviClanID = (cbNoviClanID.SelectedItem as NoviClan)?.NoviClanID ?? 0; // Prilagodite tip podataka
            int vlasnikID = (cbVlasnikID.SelectedItem as VlasnikKluba)?.VlasnikID ?? 0; // Prilagodite tip podataka
            int menadzerID = (cbMenadzerID.SelectedItem as Menadzer)?.MenadzerID ?? 0; // Prilagodite tip podataka

            // Dodajte logiku za čuvanje podataka
            MessageBox.Show($"Podaci uspešno sačuvani:\nUgovor ID: {ugovorID}\nDatum: {datum}\nNovčana vrednost: {novcanaVrednost}\nNovi član ID: {noviClanID}\nVlasnik ID: {vlasnikID}\nMenadžer ID: {menadzerID}", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            // Dodajte logiku za otkazivanje i zatvaranje prozora
            MessageBox.Show("Operacija otkazana.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        // Primer metoda za dobijanje podataka za NoviClan
        private List<NoviClan> GetNoviClanData()
        {
            // Implementirajte logiku za dohvat podataka iz izvora podataka
            // Primer implementacije:
            return new List<NoviClan>
            {
                new NoviClan { NoviClanID = 1, ImePrezime = "Novi Član 1" },
                new NoviClan { NoviClanID = 2, ImePrezime = "Novi Član 2" },
                // Dodajte ostale podatke
            };
        }

        // Primer metoda za dobijanje podataka za VlasnikKluba
        private List<VlasnikKluba> GetVlasnikKlubaData()
        {
            // Implementirajte logiku za dohvat podataka iz izvora podataka
            // Primer implementacije:
            return new List<VlasnikKluba>
            {
                new VlasnikKluba { VlasnikID = 1, ImePrezime = "Vlasnik 1" },
                new VlasnikKluba { VlasnikID = 2, ImePrezime = "Vlasnik 2" },
                // Dodajte ostale podatke
            };
        }

        // Primer metoda za dobijanje podataka za Menadzer
        private List<Menadzer> GetMenadzerData()
        {
            // Implementirajte logiku za dohvat podataka iz izvora podataka
            // Primer implementacije:
            return new List<Menadzer>
            {
                new Menadzer { MenadzerID = 1, ImePrezime = "Menadžer 1" },
                new Menadzer { MenadzerID = 2, ImePrezime = "Menadžer 2" },
                // Dodajte ostale podatke
            };
        }
    }


    internal class Menadzer
    {
        public int MenadzerID { get; set; }
        public string? ImePrezime { get; set; }
    }

    internal class VlasnikKluba
    {
        public int VlasnikID { get; set; }
        public string? ImePrezime { get; set; }
    }

    internal class NoviClan
    {
        public int NoviClanID { get; set; }
        public string? ImePrezime { get; set; }
    }
}



