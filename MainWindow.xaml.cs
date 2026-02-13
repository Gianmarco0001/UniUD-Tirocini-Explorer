using CsvHelper;
using CsvHelper.Configuration;
using ExcelDataReader;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace UniUdTirocini
{
    public partial class MainWindow : Window
    {
        private List<Azienda> _database = new List<Azienda>();
        private const string DB_FILENAME = "db_aziende.csv";
        private const string FAV_FILENAME = "preferiti.txt";

        public MainWindow()
        {
            InitializeComponent();
            CaricaDatabase();
        }

        // --- 1. CARICAMENTO DATI ---
        private void CaricaDatabase()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DB_FILENAME);

            if (!File.Exists(path))
            {
                var res = MessageBox.Show("Nessun database trovato.\nVuoi caricare il file Excel scaricato da UniUD?", "Benvenuto", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes) BtnUpdate_Click(null, null);
                return;
            }

            try
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    MissingFieldFound = null,
                    HeaderValidated = null,
                    BadDataFound = null
                }))
                {
                    _database = csv.GetRecords<Azienda>().ToList();
                }

                // Carica i cuoricini salvati
                CaricaPreferitiDaFile();

                // Aggiorna vista
                FiltraDati(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore caricamento: {ex.Message}");
            }
        }

        // --- 2. LOGICA PREFERITI ---
        private void CaricaPreferitiDaFile()
        {
            string favPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FAV_FILENAME);
            if (File.Exists(favPath))
            {
                var preferiti = File.ReadAllLines(favPath).ToHashSet();
                foreach (var az in _database)
                {
                    if (az.Nome != null && preferiti.Contains(az.Nome))
                    {
                        az.IsFavorite = true;
                    }
                }
            }
        }

        private void SalvaPreferitiSuFile()
        {
            string favPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FAV_FILENAME);
            var nomi = _database.Where(x => x.IsFavorite).Select(x => x.Nome).ToList();
            File.WriteAllLines(favPath, nomi!);
        }

        private void BtnFavorite_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Azienda az)
            {
                az.IsFavorite = !az.IsFavorite;
                SalvaPreferitiSuFile();

                // Se siamo in modalità "Solo Preferiti", aggiorniamo la lista per rimuovere chi non lo è più
                if (TogFav.IsChecked == true && !az.IsFavorite)
                {
                    FiltraDati(null, null);
                }
            }
        }

        // --- 3. FILTRI ---
        private void FiltraDati(object sender, RoutedEventArgs e)
        {
            if (_database == null) return;

            string query = TxtSearch.Text?.ToLower() ?? "";

            bool bIng = ChkIng.IsChecked == true;
            bool bEco = ChkEco.IsChecked == true;
            bool bMat = ChkMat.IsChecked == true;
            bool bLin = ChkLin.IsChecked == true;
            bool bGiu = ChkGiu.IsChecked == true;
            bool onlyFav = TogFav.IsChecked == true;

            var risultati = _database.Where(x =>
            {
                if (onlyFav && !x.IsFavorite) return false;

                bool matchText = string.IsNullOrEmpty(query) ||
                                 (x.Nome?.ToLower().Contains(query) ?? false) ||
                                 (x.Citta?.ToLower().Contains(query) ?? false) ||
                                 (x.Settore?.ToLower().Contains(query) ?? false);

                if (!matchText) return false;

                if (bIng && !x.IsIng) return false;
                if (bEco && !x.IsEco) return false;
                if (bMat && !x.IsMatInfo) return false;
                if (bLin && !x.IsLin) return false;
                if (bGiu && !x.IsGiu) return false;

                return true;
            }).ToList();

            ListAziende.ItemsSource = risultati;
        }

        // --- 4. IMPORTAZIONE ---
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "File Excel/CSV|*.xlsx;*.xls;*.csv",
                Title = "Seleziona file UniUD"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string source = openFileDialog.FileName;
                string dest = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DB_FILENAME);
                string ext = Path.GetExtension(source).ToLower();

                try
                {
                    if (ext.Contains("xls")) ConvertiXlsxInCsv(source, dest);
                    else File.Copy(source, dest, true);

                    CaricaDatabase();
                    MessageBox.Show("Database aggiornato!", "Successo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Errore: {ex.Message}\n\nChiudi il file Excel se è aperto!", "Errore");
                }
            }
        }

        private void ConvertiXlsxInCsv(string source, string dest)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(source, FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var result = reader.AsDataSet();
                var table = result.Tables[0];

                using (var sw = new StreamWriter(dest, false, Encoding.UTF8))
                {
                    // Cerca l'intestazione vera (dove c'è scritto "AZIENDA") saltando le prime righe vuote
                    int headerIndex = 0;
                    for (int i = 0; i < Math.Min(10, table.Rows.Count); i++)
                    {
                        var cellVal = table.Rows[i][2]?.ToString() ?? ""; 
                        if (cellVal.ToUpper().Contains("AZIENDA"))
                        {
                            headerIndex = i;
                            break;
                        }
                    }

                    // Scrive righe
                    for (int i = headerIndex; i < table.Rows.Count; i++)
                    {
                        var items = table.Rows[i].ItemArray.Select(obj =>
                        {
                            string txt = obj?.ToString() ?? "";
                            // Escape per CSV (virgolette e virgole)
                            if (txt.Contains(",") || txt.Contains("\"") || txt.Contains("\n"))
                                txt = "\"" + txt.Replace("\"", "\"\"") + "\"";
                            return txt;
                        });
                        sw.WriteLine(string.Join(",", items));
                    }
                }
            }
        }

        // --- 5. LINK ESTERNI ---
        private void BtnWeb_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is string url && !string.IsNullOrWhiteSpace(url))
            {
                if (!url.StartsWith("http")) url = "http://" + url;
                try { Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); } catch { }
            }
        }

        private void BtnEmail_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is string email && !string.IsNullOrWhiteSpace(email))
            {
                try { Process.Start(new ProcessStartInfo($"mailto:{email}") { UseShellExecute = true }); } catch { }
            }
        }

        // --- 6. ABOUT ---
        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            // Apre la finestra About creata in precedenza
            var about = new AboutWindow();
            about.ShowDialog();
        }
    }
}