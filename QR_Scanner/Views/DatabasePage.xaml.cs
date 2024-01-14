using QR_Scanner.Models;
using System.Collections.ObjectModel;

namespace QR_Scanner.Views;

public partial class DatabasePage : ContentPage
{
    public ObservableCollection<QRCodeScan> ScannedQRCodes { get; set; }


    public DatabasePage()
    {
        InitializeComponent();
        ScannedQRCodes = new ObservableCollection<QRCodeScan>();
        LoadScannedData();
        BindingContext = this;
    }

    private void LoadScannedData()
    {
        try
        {
            var db = new QRCodeDatabaseContext();
            var scans = db.QRCodeScans.ToList();
            foreach (var scan in scans)
            {
                ScannedQRCodes.Add(scan);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Došlo k chybě při načítání dat z databáze: {ex.Message}");
        }
    }
}