using CsvHelper.Configuration.Attributes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UniUdTirocini
{
    public class Azienda : INotifyPropertyChanged
    {
        // --- DATI DAL CSV ---
        [Name("AZIENDA")] public string? Nome { get; set; }
        [Name("LOCALITÀ")] public string? Citta { get; set; }
        [Name("PV")] public string? Provincia { get; set; }
        [Name("EMAIL")] public string? Email { get; set; }
        [Name("WEB")] public string? Web { get; set; }
        [Name("SETTORE")] public string? Settore { get; set; }

        // --- FILTRI ---
        [Name("ING")] public string? _ing { get; set; }
        public bool IsIng => _ing?.ToLower().Trim() == "x";

        [Name("ECO")] public string? _eco { get; set; }
        public bool IsEco => _eco?.ToLower().Trim() == "x";

        [Name("MAT_INFO")] public string? _matInfo { get; set; }
        public bool IsMatInfo => _matInfo?.ToLower().Trim() == "x";

        [Name("LIN")] public string? _lin { get; set; }
        public bool IsLin => _lin?.ToLower().Trim() == "x";

        [Name("GIU")] public string? _giu { get; set; }
        public bool IsGiu => _giu?.ToLower().Trim() == "x";

        // --- PROPRIETÀ EXTRA (NON NEL CSV) ---

        // Preferiti
        private bool _isFavorite;
        [Ignore] // Non cercarlo nel CSV
        public bool IsFavorite
        {
            get => _isFavorite;
            set { _isFavorite = value; OnPropertyChanged(); OnPropertyChanged(nameof(FavoriteIcon)); OnPropertyChanged(nameof(FavoriteColor)); }
        }

        public string FavoriteIcon => IsFavorite ? "Heart" : "HeartOutline";
        public string FavoriteColor => IsFavorite ? "#E91E63" : "Gray"; // Rosso se attivo, Grigio se no

        // Mappe
        public string GoogleMapsUrl => $"https://www.google.com/maps/search/?api=1&query={System.Uri.EscapeDataString($"{Nome} {Citta} {Provincia}")}";

        // UI Helpers
        public string Iniziale => string.IsNullOrEmpty(Nome) ? "?" : Nome.Substring(0, 1).ToUpper();
        public string LuogoCompleto => string.IsNullOrEmpty(Provincia) ? (Citta ?? "") : $"{Citta} ({Provincia})";

        // --- EVENTO PER AGGIORNARE LA GRAFICA ---
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}