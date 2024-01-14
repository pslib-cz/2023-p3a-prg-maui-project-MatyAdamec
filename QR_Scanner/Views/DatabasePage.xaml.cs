using QR_Scanner.Models;
using QR_Scanner.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace QR_Scanner.Views;

public partial class DatabasePage : ContentPage
{
    public ObservableCollection<QRCodeScan> ScannedQRCodes { get; set; }


    public DatabasePage()
    {
        InitializeComponent();

        var viewModel = new ViewModel();
        BindingContext = viewModel;
        viewModel.LoadScannedData();
    }

}