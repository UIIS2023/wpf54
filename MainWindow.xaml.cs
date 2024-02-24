using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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
using WPFFudbalskiKlub.Forme;


namespace WPFFudbalskiKlub
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ucitanaTabela;
        bool azuriraj;
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        


        #region Select upiti
        #region Select sa uslovom
        string selectUslovDrzavaKlub = @"select * from tbl_DrzavaKlub where DrzavaKlubID=";
        string selectUslovFudbKlub = @"select * from tbl_FudbKlub where FudbKlubID=";
        string selectUslovMenadzer = @"select * from tbl_Menadzer where MenadzerID=";
        string selectUslovNoviClan = @"select * from tbl_NoviClan where NoviClanID=";
        string selectUslovOsiguranje = @"select * from tbl_Osiguranje where OsiguranjeID=";
        string selectUslovRadnik = @"select * from tbl_Radnik where RadnikID=";
        string selectUslovUgovor = @"select * from tbl_Ugovor where UgovorID=";
        string selectUslovVlasnikKluba = @"select * from tbl_VlasnikKluba where VlasnikKlubaID=";
        #endregion

        static string DrzavaKlubSelect = @"select DrzavaKlubID as 'ID', Naziv as 'Naziv' from tbl_DrzavaKlub";

        static string FudbKlubSelect = @"select FudbKlubID as 'I'D, imeKluba as 'imeKluba', Pozicija as 'Pozicija' from tbl_FudbKlub
                                           join tbl_DrzavaKlub on tbl_FudbKlub.DrzavaKlubID = tbl_DrzavaKlub.DrzavaKlubID
                                           join tbl_Radnik on tbl_FudbKlub.RadnikID = tbl_Radnik.RadnikID";   
                                                


        static string MenadzerSelect = @"select MenadzerID as 'ID', Ime as 'Ime', Prezime as 'Prezime', BrojLicence as 'BrojLicence , PlataMenadzera as 'PlataMenadzera from tbl_Menadzer";

        static string NoviClanSelect = @"select NoviClanID as 'ID', Ime as 'Ime', Prezime as 'Prezime' , Adresa as 'Adresa', Grad as 'Grad', Jmbg as 'Jmbg', Kontakt as 'Kontakt', Email as 'Email' from tbl_NoviClan
                                                join tbl_Osiguranje on tbl_NoviClan.OsiguranjeID = tbl_Osiguranje.OsiguranjeID";

                
                                    



        static string OsiguranjeSelect = @"select OsiguranjeID as 'ID', TipOsiguranja as 'TipOsiguranja'  from tbl_Osiguranje";

        static string RadnikSelect = @"select RadnikID as 'ID', Ime as 'Ime', Prezime as 'Prezime' , Adresa as 'Adresa' , Pozicija as 'Pozicija' from tbl_Radnik";
                                      
        static string UgovorSelect = @"select UgovorID as 'ID' , Datum as 'Datum', NovcanaVrednost as 'NovcanaVrednost' from tbl_Ugovor
                                                join tbl_NoviClan on tbl_Ugovor.NoviClanID = tbl_NoviCLan.NoviClanID
                                                join tbl_Vlasnik on tbl_Ugovor.VlasnikID = tbl_Vlasnik.VlasnikID
                                                join tbl_Menadzer on tbl_Ugovor.MenadzerID = tbl_Menadzer.MenadzerID";


        static string VlasnikKlubaSelect = @"select VlasnikID as 'ID', Ime as 'Ime', Prezime as 'Prezime'  from tbl_VlasnikKluba";
                                      

        #endregion
        #region Delete upiti
        string drzavaklubDelete = @"Delete from tbl_DrzavaKlub where DrzavaKlubID =";
        string fudbklubDelete = @"Delete from tbl_FudbKlub where FudbalskiKLubID=";
        string menadzerDelete = @"Delete from tbl_Menadzer where MenadzerID=";
        string noviclanDelete = @"Delete from tbl_NoviClan where NoviClanID=";
        string osiguranjeDelete = @"Delete from tbl_Osiguranje where OsiguranjeID=";
        string radnikDelete = @"Delete from tbl_Radnik where RadnikID=";
        string ugovorDelete = @"Delete from tbl_Ugovor where UgovorID=";
        string vlasnikklubaDelete = @"Delete from tbl_VlasnikKluba where VlasnikKlubaID=";
       
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UcitajPodatke(dataGridCentralni, NoviClanSelect);
            

    }
        private void UcitajPodatke(DataGrid grid, string selectUpit)
        {
            try
            {
                konekcija.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUpit, konekcija);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                if (grid != null)
                {
                    grid.ItemsSource = dt.DefaultView;
                }
                ucitanaTabela = selectUpit;
                dt.Dispose();
                dataAdapter.Dispose();
            }
            catch (SqlException) 
            {
                MessageBox.Show("Neuspesno uneti podaci", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                    konekcija.Close();
            }
        }

        void PopuniFormu(DataGrid grid, string selectUslov)
        {
            try
            {
                konekcija.Open();
                azuriraj = true;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                DataRowView red = (DataRowView)grid.SelectedItems[0];

                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                cmd.CommandText = selectUslov + "@id";
                SqlDataReader citac = cmd.ExecuteReader();
                cmd.Dispose();
                if (citac.Read())
                {
                    if (ucitanaTabela.Equals(DrzavaKlubSelect))
                    {
                        FrmDrzavaKlub prozorDrzavaKlub = new FrmDrzavaKlub(azuriraj, red);
                        prozorDrzavaKlub.txtNaziv = citac["Naziv"].ToString();
                        prozorDrzavaKlub.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(FudbKlubSelect))
                    {
                        FrmFudbKlub prozorFudbKlub = new FrmFudbKlub(azuriraj, red);
                        prozorFudbKlub.txtIme.Text = citac["ImeKluba"].ToString();
                        prozorFudbKlub.txtPozicija.Text = citac["Pozicija"].ToString();
                        prozorFudbKlub.cbradnikID.SelectedValue = citac["RadnikID"].ToString();
                        prozorFudbKlub.cbDrzavaKlubID.SelectedValue = citac["DrzavaKlubID"].ToString();
                        prozorFudbKlub.ShowDialog();
                    }
                   
                    else if (ucitanaTabela.Equals(NoviClanSelect))
                    {
                        FrmNoviClan prozorNoviClan = new FrmNoviClan(azuriraj, red);
                        prozorNoviClan.Ime.Text = citac["Ime"].ToString();
                        prozorNoviClan.Prezime.Text = citac["Prezime"].ToString();
                        prozorNoviClan.Adresa.Text = citac["Adresa"].ToString();
                        prozorNoviClan.Grad.Text = citac["Grad"].ToString();
                        prozorNoviClan.Jmbg.Text = citac["Jmbg"].ToString();
                        prozorNoviClan.Kontakt.Text = citac["Kontakt"].ToString();
                        prozorNoviClan.OsiguranjeID.SelectedValue = citac["OsiguranjeID"].ToString();
                        prozorNoviClan.FudbalskiKlubID.SelectedValue = citac["FudbalskiKlubID"].ToString();
                        prozorNoviClan.ShowDialog();
                    }
                   /*else if (ucitanaTabela.Equals(OsiguranjeSelect))
                    {
                        FrmOsiguranje prozorOsiguranje = new FrmOsiguranje(azuriraj, red);
                        prozorOsiguranje. = citac["TipOsiguranja"].ToString();
                        prozorOsiguranje.ShowDialog();
                        
                    }*/
                    else if (ucitanaTabela.Equals(RadnikSelect))
                    {
                        WPFFudbalskiKLub.Forme.FrmRadnik prozorRadnik = new FrmRadnik(azuriraj, red);
                        prozorRadnik.txtIme.Text = citac["Ime"].ToString();
                        prozorRadnik.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorRadnik.txtAdresa.Text = citac["Adresa"].ToString();
                        prozorRadnik.txtPozicija.Text = citac["Pozicija"].ToString();
                        prozorRadnik.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(UgovorSelect))
                    {
                        FrmUgovor prozorUgovor = new FrmUgovor();
                        prozorUgovor.Datum.SelectedDate = (DateTime)red["Datum"];
                        prozorUgovor.NovcanaVrednost.Text = red["NovcanaVrednost"].ToString();

                        // Postavite selektovane vrednosti za ComboBox-e
                        prozorUgovor.cbNoviClanID.SelectedValue = (int)red["NoviClanID"];
                        prozorUgovor.cbVlasnikID.SelectedValue = (int)red["VlasnikID"];
                        prozorUgovor.cbMenadzerID.SelectedValue = (int)red["MenadzerID"];

                        prozorUgovor.ShowDialog();
                    }

                    else if (ucitanaTabela.Equals(VlasnikKlubaSelect))
                    {
                        FrmVlasnikKluba prozorVlasnikKluba = new FrmVlasnikKluba(azuriraj, red);
                        prozorVlasnikKluba.txtIme.Text = citac["Ime"].ToString();
                        prozorVlasnikKluba.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorVlasnikKluba.ShowDialog();
                    }
                }

            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                    konekcija.Close();
                azuriraj = false;
            }
        }
        void ObrisiZapis(DataGrid grid, string deleteUpit)
        {
             
            try
            {
                konekcija.Open();
                DataRowView red = (DataRowView)grid.SelectedItems[0];
                MessageBoxResult rezultat = MessageBox.Show("Da li ste sigurni?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rezultat == MessageBoxResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = deleteUpit + "@id";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red", "Obavestenje", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException)
            {
                MessageBox.Show("Postoje povezani podaci u nekim drugim tabelama", "Obavestenje", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnDrzavaKlub_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, DrzavaKlubSelect);
        }

        private void btnFudbKlub_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, FudbKlubSelect);
        }

        private void btnMenadzer_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, MenadzerSelect);
        }


        private void btnNoviClan_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, NoviClanSelect);
        }

        private void btnOsiguranje_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, OsiguranjeSelect);
        }

        private void btnRadnik_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, RadnikSelect);
        }
        private void btnUgovor_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, UgovorSelect);
        }
        private void btnVlasnikKluba_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, VlasnikKlubaSelect);
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;
            if (ucitanaTabela.Equals(DrzavaKlubSelect))
            {
                prozor = new FrmDrzavaKlub();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, DrzavaKlubSelect);
            }
            else if (ucitanaTabela.Equals(FudbKlubSelect))
            {
                prozor = new FrmFudbKlub(azuriraj);
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, FudbKlubSelect);
            }
            else if (ucitanaTabela.Equals(MenadzerSelect))
            {
                prozor = new FrmMenadzer();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, MenadzerSelect);
            }
            else if (ucitanaTabela.Equals(NoviClanSelect))
            {
                prozor = new FrmNoviClan(azuriraj);
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, NoviClanSelect);
            }
            else if (ucitanaTabela.Equals(OsiguranjeSelect))
            {
                prozor = new FrmOsiguranje();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, OsiguranjeSelect);
            }
            else if (ucitanaTabela.Equals(RadnikSelect))
            {
                prozor = new FrmRadnik();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, RadnikSelect);
            }
            else if (ucitanaTabela.Equals(UgovorSelect))
            {
                prozor = new FrmUgovor();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, UgovorSelect);
            }
            else if (ucitanaTabela.Equals(VlasnikKlubaSelect))
            {
                prozor = new FrmVlasnikKluba();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, VlasnikKlubaSelect);
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(DrzavaKlubSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovDrzavaKlub);
                UcitajPodatke(dataGridCentralni, DrzavaKlubSelect);
            }
            else if (ucitanaTabela.Equals(FudbKlubSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovFudbKlub);
                UcitajPodatke(dataGridCentralni, FudbKlubSelect);
            }
            else if (ucitanaTabela.Equals(MenadzerSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovMenadzer);
                UcitajPodatke(dataGridCentralni, MenadzerSelect);
            }
            else if (ucitanaTabela.Equals(NoviClanSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovNoviClan);
                UcitajPodatke(dataGridCentralni, NoviClanSelect);
            }
            else if (ucitanaTabela.Equals(OsiguranjeSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovOsiguranje);
                UcitajPodatke(dataGridCentralni, OsiguranjeSelect);
            }
            else if (ucitanaTabela.Equals(RadnikSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovRadnik);
                UcitajPodatke(dataGridCentralni, RadnikSelect);
            }
            else if (ucitanaTabela.Equals(UgovorSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovUgovor);
                UcitajPodatke(dataGridCentralni, UgovorSelect);
            }
            else if (ucitanaTabela.Equals(VlasnikKlubaSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovVlasnikKluba);
                UcitajPodatke(dataGridCentralni, VlasnikKlubaSelect);
            }
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(DrzavaKlubSelect))
            {
                ObrisiZapis(dataGridCentralni, drzavaklubDelete);
                UcitajPodatke(dataGridCentralni, DrzavaKlubSelect);
            }
            else if (ucitanaTabela.Equals(FudbKlubSelect))
            {
                ObrisiZapis(dataGridCentralni, fudbklubDelete);
                UcitajPodatke(dataGridCentralni, FudbKlubSelect);
            }
            else if (ucitanaTabela.Equals(MenadzerSelect))
            {
                ObrisiZapis(dataGridCentralni, menadzerDelete);
                UcitajPodatke(dataGridCentralni, MenadzerSelect);
            }
            else if (ucitanaTabela.Equals(NoviClanSelect))
            {
                ObrisiZapis(dataGridCentralni, noviclanDelete);
                UcitajPodatke(dataGridCentralni, NoviClanSelect);
            }
            else if (ucitanaTabela.Equals(OsiguranjeSelect))
            {
                ObrisiZapis(dataGridCentralni, osiguranjeDelete);
                UcitajPodatke(dataGridCentralni, OsiguranjeSelect);
            }
            else if (ucitanaTabela.Equals(RadnikSelect))
            {
                ObrisiZapis(dataGridCentralni, radnikDelete);
                UcitajPodatke(dataGridCentralni, RadnikSelect);
            }
            else if (ucitanaTabela.Equals(UgovorSelect))
            {
                ObrisiZapis(dataGridCentralni, ugovorDelete);
                UcitajPodatke(dataGridCentralni, UgovorSelect);
            }
            else if (ucitanaTabela.Equals(VlasnikKlubaSelect))
            {
                ObrisiZapis(dataGridCentralni, vlasnikklubaDelete);
                UcitajPodatke(dataGridCentralni, VlasnikKlubaSelect);
            }
        }

        private void ObrisiZapis(DataGrid dataGridCentralni, object drzavaKlubDelete)
        {
            throw new NotImplementedException();
        }

        private class FrmMenadzer : Window
        {
        }
    }

    internal class FrmOsiguranje
    {
        internal object OsiguranjeID;
        internal object txtOsiguranjeID;
        private bool azuriraj;
        private DataRowView red;

        public FrmOsiguranje()
        {
        }

        public FrmOsiguranje(bool azuriraj, DataRowView red)
        {
            this.azuriraj = azuriraj;
            this.red = red;
        }

        public static implicit operator Window(FrmOsiguranje v)
        {
            throw new NotImplementedException();
        }
    }

    internal class FrmRadnik : WPFFudbalskiKLub.Forme.FrmRadnik
    {
        private bool azuriraj;
        private DataRowView red;

        public FrmRadnik()
        {
        }

        public FrmRadnik(bool azuriraj, DataRowView red)
        {
            this.azuriraj = azuriraj;
            this.red = red;
        }
    }
}
