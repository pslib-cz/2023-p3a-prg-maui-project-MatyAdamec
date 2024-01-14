using QR_Scanner.Models;
using QR_Scanner.Views;
using ZXing.Net.Maui;

namespace QR_Scanner;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }


    private void OnBarcodeDetected(object sender, BarcodeDetectionEventArgs e)
    {
        var first = e.Results?.FirstOrDefault();

        if (first == null)
        {
            resultLabel.Text = "No barcode detected...";
            return;
        }

        Dispatcher.DispatchAsync(async () =>
        {
            await DisplayAlert("Barcode Detected", first.Value, "OK");
        });
        using (var db = new QRCodeDatabaseContext())
        {
            db.Database.EnsureCreated();
        }
        SaveToDatabase(first.Value);

        //resultLabel.Text = first.Value;

    }

    private void SaveToDatabase(string link)
    {
        var db = new QRCodeDatabaseContext();
        var scan = new QRCodeScan
        {
            Link = link,
            ScanDate = DateTime.Now
        };

        db.QRCodeScans.Add(scan);
        db.SaveChanges();
    }

    private async void OnViewScans(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DatabasePage());
    }    
    

}
