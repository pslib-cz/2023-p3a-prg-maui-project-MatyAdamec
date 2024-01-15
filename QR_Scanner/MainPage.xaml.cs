using QR_Scanner.Models;
using QR_Scanner.Views;
using System.Windows.Input;
using ZXing.Net.Maui;

namespace QR_Scanner;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    bool isScanning = true;

    private void OnBarcodeDetected(object sender, BarcodeDetectionEventArgs e)
    {
        var first = e.Results?.FirstOrDefault();

        if (first == null)
        {
            return;
        }

        if (isScanning)
        {
            isScanning= false;
            Dispatcher.DispatchAsync(async () =>
            {
                bool openInBrowser = await DisplayAlert("Barcode Detected", first.Value, "Open In Browser", "OK");
                if (openInBrowser)
                {
                    await Launcher.OpenAsync(new Uri(first.Value));
                }
                else
                {
                isScanning= true;

                }
            });

            MainThread.BeginInvokeOnMainThread(() => {
                resultLabel.Text = first.Value;
            });

            using (var db = new QRCodeDatabaseContext())
            {
                db.Database.EnsureCreated();
            }
            SaveToDatabase(first.Value);


        }


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
