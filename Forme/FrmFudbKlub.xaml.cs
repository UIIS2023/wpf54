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



namespace WPFFudbalskiKlub.Forme
{
    public partial class FrmFudbKlub : Window
    {
        private DataRowView red;

        public FrmFudbKlub(bool azuriraj)
        {
            InitializeComponent();

            // Postavljanje događaja za dugmad
            btnSacuvaj.Click += btnSacuvaj_Click;
            btnOtkazi.Click += btnOtkazi_Click;
         
            

            // Popunjavanje ComboBox-a za DrzavaKlubID
            PopuniDrzaveComboBox();

            // Primer popunjavanja Imena kluba i ostalih podataka
            txtIme.Text = "ImeKluba"; // Postavite vrednost prema stvarnim podacima
            txtPozicija.Text = "PozicijaKluba"; // Postavite vrednost prema stvarnim podacima
            txtFudbalskiKLubID.Text = "123"; // Postavite vrednost prema stvarnim podacima

            // Primer popunjavanja ComboBox-a za RadnikID
            PopuniRadnikeComboBox();
        }

        public FrmFudbKlub(bool azuriraj, DataRowView red) : this(azuriraj)
        {
            this.red = red;

            // Postavite vrednosti polja ako je u modu ažuriranja
            if (azuriraj)
            {
                txtIme.Text = red["ImeKluba"].ToString();
                txtPozicija.Text = red["PozicijaKluba"].ToString();
                txtFudbalskiKLubID.Text = red["FudbalskiKlubID"].ToString();

                // Postavite selektovane vrednosti za ComboBox-e
                cbDrzavaKlubID.SelectedValue = red["DrzavaKlubID"];
                cbradnikID.SelectedValue = red["RadnikID"];
            }
        }

        private void PopuniDrzaveComboBox()
        {
            try
            {
                using (SqlConnection konekcija = new SqlConnection(@"DESKTOP-26BVQVG\SQLEXPRESS01"))
                {
                    konekcija.Open();
                    string vratiDrzave = @"SELECT DrzavaID, NazivDrzave FROM tbl_NazivDrzave";
                    DataTable dtDrzave = new DataTable();
                    SqlDataAdapter daDrzave = new SqlDataAdapter(vratiDrzave, konekcija);
                    daDrzave.Fill(dtDrzave);
                    cbDrzavaKlubID.ItemsSource = dtDrzave.DefaultView;
                    cbDrzavaKlubID.DisplayMemberPath = "NazivDrzave";

                    // Ako imate vrednost DrzavaKlubID u podacima, postavite selektovanu vrednost u ComboBox-u
                    if (red != null && red.Row.RowState != DataRowState.Detached && red["DrzavaKlubID"] != DBNull.Value)
                    {
                        cbDrzavaKlubID.SelectedValue = red["DrzavaKlubID"];
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuće liste nisu popunjene", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Zatvorite konekciju ovde ako je potrebno
            }
        }

        private void PopuniRadnikeComboBox()
        {
            try
            {
                using (SqlConnection konekcija = new SqlConnection(@"DESKTOP-26BVQVG\SQLEXPRESS01"))
                {
                    konekcija.Open();
                    string vratiRadnike = @"SELECT RadnikID, ImePrezime FROM tbl_Radnik";
                    DataTable dtRadnici = new DataTable();
                    SqlDataAdapter daRadnici = new SqlDataAdapter(vratiRadnike, konekcija);
                    daRadnici.Fill(dtRadnici);
                    cbradnikID.ItemsSource = dtRadnici.DefaultView;
                    cbradnikID.DisplayMemberPath = "ImePrezime";

                    // Ako imate vrednost RadnikID u podacima, postavite selektovanu vrednost u ComboBox-u
                    if (red != null && red.Row.RowState != DataRowState.Detached && red["RadnikID"] != DBNull.Value)
                    {
                        cbradnikID.SelectedValue = red["RadnikID"];
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuće liste nisu popunjene", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Zatvorite konekciju ovde ako je potrebno
            }
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Otvorite konekciju i izvršite SQL upit za dodavanje ili izmenu reda
                using (SqlConnection konekcija = new SqlConnection(@"DESKTOP-26BVQVG\SQLEXPRESS01"))
                {
                    konekcija.Open();
                    SqlCommand cmd;
                    if (red != null && red.Row.RowState != DataRowState.Detached)
                    {
                        // Ažuriranje reda
                        cmd = new SqlCommand("UPDATE tbl_FudbalskiKlub SET ImeKluba = @ImeKluba, PozicijaKluba = @PozicijaKluba, DrzavaKlubID = @DrzavaKlubID, RadnikID = @RadnikID WHERE FudbalskiKlubID = @FudbalskiKlubID", konekcija);
                        cmd.Parameters.AddWithValue("@FudbalskiKlubID", red["FudbalskiKlubID"]);
                    }
                    else
                    {
                        // Dodavanje novog reda
                        cmd = new SqlCommand("INSERT INTO tbl_FudbalskiKlub (ImeKluba, PozicijaKluba, DrzavaKlubID, RadnikID) VALUES (@ImeKluba, @PozicijaKluba, @DrzavaKlubID, @RadnikID)", konekcija);
                    }

                    cmd.Parameters.AddWithValue("@ImeKluba", txtIme.Text);
                    cmd.Parameters.AddWithValue("@PozicijaKluba", txtPozicija.Text);
                    cmd.Parameters.AddWithValue("@DrzavaKlubID", cbDrzavaKlubID.SelectedValue);
                    cmd.Parameters.AddWithValue("@RadnikID", cbradnikID.SelectedValue);

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
            if (red != null && MessageBox.Show("Da li ste sigurni da želite da obrišete klub?", "Potvrda brisanja", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    // Otvorite konekciju i izvršite SQL upit za brisanje reda
                    using (SqlConnection konekcija = new SqlConnection(@"DESKTOP-26BVQVG\SQLEXPRESS01"))
                    {
                        konekcija.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM tbl_FudbalskiKlub WHERE FudbalskiKlubID = @FudbalskiKlubID", konekcija);
                        cmd.Parameters.AddWithValue("@FudbalskiKlubID", red["FudbalskiKlubID"]);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Klub uspešno obrisan.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    // Zatvorite prozor nakon brisanja
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Greška prilikom brisanja: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txtFudbalskiKLubID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
