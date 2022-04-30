using GalaSoft.MvvmLight.CommandWpf;
using PaleontologiaMVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaleontologiaMVVM.ViewModels
{
    public class PaleontologiaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Fosil> ListaFosiles { get; set; } = new ObservableCollection<Fosil>();

        private Fosil? fosil;

        public Fosil? Fosil
        {
            get { return fosil; }
            set { fosil = value; }
        }


        public string Vista { get; set; } = "HomeVista";
        public string Error { get; set; } = "";

        public ICommand CambiarVistaCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public PaleontologiaViewModel()
        {
            CambiarVistaCommand = new RelayCommand<string>(CambiarVista);
            AgregarCommand = new RelayCommand(Agregar);
        }

        private void Agregar()
        {
            CambiarVista("AgregarVista");
        }

        public void CambiarVista(string vista)
        {
            Vista = vista;
            Evento();
        }
        public void Evento(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }


    }
}
