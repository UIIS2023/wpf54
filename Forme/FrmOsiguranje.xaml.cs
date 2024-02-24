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
    public partial class FrmOsiguranje : Window
    {
        private DataRowView red;

        public FrmOsiguranje()
        {
            InitializeComponent();

            // Postavljanje događaja za dugmad
            btnSacuvaj.Click += btnSacuvaj_Click;
            btnOtkazi.Click += btnOtkazi_Click;

    
        }

        public FrmOsiguranje(bool azuriraj, DataRowView red) : this()
        {
            this.red = red;

            // Postavite vrednosti polja ako je u modu ažuriranja
            if (azuriraj)
            {
                txtOsiguranjeID.Text = red["OsiguranjeID"].ToString();
                txtTipOsiguranja.Text = red["TipOsiguranja"].ToString();
            }
        }

        private void ClearFields()
        {
            txtOsiguranjeID.Text = "";
            txtTipOsiguranja.Text = "";
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection konekcija = new SqlConnection(@"DESKTOP-26BVQVG\SQLEXPRESS01"))
                {
                    konekcija.Open();
                    SqlCommand cmd;
                    if (red != null && red.Row.RowState != DataRowState.Detached)
                    {
                        // Ažuriranje reda
                        cmd = new SqlCommand("UPDATE tbl_Osiguranje SET TipOsiguranja = @TipOsiguranja WHERE OsiguranjeID = @OsiguranjeID", konekcija);
                        cmd.Parameters.AddWithValue("@OsiguranjeID", txtOsiguranjeID.Text);
                        
                    }
                    else
                    {
                        // Dodavanje novog reda
                        cmd = new SqlCommand("INSERT INTO tbl_Osiguranje (TipOsiguranja) VALUES (@TipOsiguranja)", konekcija);
                    }

                    cmd.Parameters.AddWithValue("@TipOsiguranja", txtTipOsiguranja.Text);

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
                this.Close();
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            // Dodajte logiku za otkazivanje i zatvaranje prozora
            MessageBox.Show("Operacija otkazana.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (red != null && MessageBox.Show("Da li ste sigurni da želite da obrišete osiguranje?", "Potvrda brisanja", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (SqlConnection konekcija = new SqlConnection(@"DESKTOP-26BVQVG\SQLEXPRESS01"))
                    {
                        konekcija.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM tbl_Osiguranje WHERE OsiguranjeID = @OsiguranjeID", konekcija);
                        cmd.Parameters.AddWithValue("@OsiguranjeID", red["OsiguranjeID"]);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Osiguranje uspešno obrisano.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Greška prilikom brisanja: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    this.Close();
                }
            }
        }

        private void txtOsiguranjeID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}