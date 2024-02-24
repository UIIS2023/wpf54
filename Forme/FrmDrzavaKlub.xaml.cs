using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public partial class FrmDrzavaKlub : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;
        internal object txtNaziv;

        public FrmDrzavaKlub()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();

            btnOtkazi.Click += BtnOtkazi_Click;
            btnSacuvaj.Click += btnSacuvaj_Click_1;
        }

        private void BtnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public FrmDrzavaKlub(bool azuriraj, DataRowView pomocniRed)
        {
            //InitializeComponent();
            konekcija = kon.KreirajKonekciju();
           
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
        }

        private void btnSacuvaj_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand()
                {

                    Connection = konekcija
                };
                cmd.Parameters.Add("@Naziv", System.Data.SqlDbType.NVarChar);
                cmd.Parameters.Add("@DrzavaKlubID", System.Data.SqlDbType.NVarChar);

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update tbl_DrzavaKlub
                                        Set Naziv = @Naziv where DrzavaKlubID = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tbl_DrzavaKlub(naziv)
                                    values(@Naziv)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Greska prilikom konverzije podataka", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
