using QR_Scanner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QR_Scanner.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<QRCodeScan> ScannedQRCodes { get; private set; }
        public ICommand DeleteScanCommand { get; }
        public ICommand ShowOnWebCommand { get; }

        public ViewModel()
        {
            ScannedQRCodes = new ObservableCollection<QRCodeScan>();
            DeleteScanCommand = new Command<QRCodeScan>(DeleteScan);
            ShowOnWebCommand = new Command<QRCodeScan>(ShowOnWeb);
        }

        private void ShowOnWeb(QRCodeScan scanToShow)
        {
            if (scanToShow != null)
            {
                Launcher.OpenAsync(new Uri(scanToShow.Link));
            }
        }

        private void DeleteScan(QRCodeScan scanToDelete)
        {
            if (scanToDelete != null)
            {
                var db = new QRCodeDatabaseContext();
                db.QRCodeScans.Remove(scanToDelete);
                db.SaveChanges();

                Console.WriteLine(db.QRCodeScans);
                ScannedQRCodes.Remove(scanToDelete);
            }
        }

        public void LoadScannedData()
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
