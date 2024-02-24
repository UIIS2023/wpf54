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

namespace WPFFudbalskiKLub.Forme
{
    public partial class FrmMenadzer 
    {
        private bool azuriraj;
        private DataRowView red;

        public FrmMenadzer(bool azuriraj, DataRowView red)
        {
            InitializeComponent();

            // Postavljanje događaja za dugmad
            btnSacuvaj.Click += btnSacuvaj_Click;
            btnOtkazi.Click += btnOtkazi_Click;
           
         
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            azuriraj = false;
            ClearFields();
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            azuriraj = true;
            if (red != null)
            {
                txtMenadzerID.Text = red["MenadzerID"].ToString();
                txtIme.Text = red["Ime"].ToString();
                txtPrezime.Text = red["Prezime"].ToString();
                txtBrojLicence.Text = red["BrojLicence"].ToString();
                txtPlataMenadzera.Text = red["PlataMenadzera"].ToString();
            }
            else
            {
                MessageBox.Show("Izaberite menadžera za izmenu.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (red != null && MessageBox.Show("Da li ste sigurni da želite da obrišete menadžera?", "Potvrda brisanja", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (SqlConnection konekcija = new SqlConnection(@"DESKTOP-26BVQVG\SQLEXPRESS01"))
                    {
                        konekcija.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM tbl_Menadzer WHERE MenadzerID = @MenadzerID", konekcija);
                        cmd.Parameters.AddWithValue("@MenadzerID", red["MenadzerID"]);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Menadžer uspešno obrisan.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Greška prilikom brisanja: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    ClearFields();
                    this.Close();
                }
            }
        }

        private void ClearFields()
        {
            txtMenadzerID.Text = "";
            txtIme.Text = "";
            txtPrezime.Text = "";
            txtBrojLicence.Text = "";
            txtPlataMenadzera.Text = "";
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection konekcija = new SqlConnection(/* vaša konekcija */))
                {
                    konekcija.Open();
                    SqlCommand cmd;
                    if (azuriraj)
                    {
                        // Ažuriranje reda
                        cmd = new SqlCommand("UPDATE tbl_Menadzer SET Ime = @Ime, Prezime = @Prezime, BrojLicence = @BrojLicence, PlataMenadzera = @PlataMenadzera WHERE MenadzerID = @MenadzerID", konekcija);
                        cmd.Parameters.AddWithValue("@MenadzerID", txtMenadzerID.Text);
                    }
                    else
                    {
                        // Dodavanje novog reda
                        cmd = new SqlCommand("INSERT INTO tbl_Menadzer (Ime, Prezime, BrojLicence, PlataMenadzera) VALUES (@Ime, @Prezime, @BrojLicence, @PlataMenadzera)", konekcija);
                    }

                    cmd.Parameters.AddWithValue("@Ime", txtIme.Text);
                    cmd.Parameters.AddWithValue("@Prezime", txtPrezime.Text);
                    cmd.Parameters.AddWithValue("@BrojLicence", txtBrojLicence.Text);
                    cmd.Parameters.AddWithValue("@PlataMenadzera", txtPlataMenadzera.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Podaci uspešno sačuvani.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška prilikom čuvanja podataka: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                ClearFields();
                this.Close();
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            // Dodajte logiku za otkazivanje i zatvaranje prozora
            MessageBox.Show("Operacija otkazana.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}

