using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using PaleontologiaMVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaleontologiaMVVM.ViewModels
{
    public class PaleontologiaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Fosil> Lista { get; set; } = new ObservableCollection<Fosil>();

        private Fosil? fosil;

        public Fosil? Fosil
        {
            get { return fosil; }
            set { fosil = value;
                Evento("Fosil");

            }
        }


        public string Vista { get; set; } = "HomeVista";
        public string Error { get; set; } = "";

        public ICommand CambiarVistaCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public PaleontologiaViewModel()
        {
            CargarArchivo();
            CancelarCommand = new RelayCommand(Cancelar);
            EditarCommand = new RelayCommand(Editar);
            CambiarVistaCommand = new RelayCommand<string>(CambiarVista);
            AgregarCommand = new RelayCommand(Agregar);
            EliminarCommand = new RelayCommand(Eliminar);
        }

        private void Editar()
        {
            if (Fosil != null)
            {
                Lista[posicionelementomodificado] = Fosil;
                GuardarArchivo();
                Cancelar();
            }
        }

        private void Cancelar()
        {
            Fosil = null;
            CambiarVista("ColeccionVista");
        }

        private void Eliminar()
        {
            if(Fosil != null)
            {
                Lista.Remove(Fosil);
                Evento();
                GuardarArchivo();
            }
        }

        private void Agregar()
        {

            if(Fosil != null)
            {

                if (string.IsNullOrEmpty(Fosil.Nombre))
                {
                    Error = "Por favor rellene el espacio vacio Nombre";
                    Evento("Error");
                    return;
                }
                if (string.IsNullOrEmpty(Fosil.Descripcion))
                {
                    Error = "Por favor rellene el espacio vacio descripcion";
                    Evento("Error");
                    return;
                }

                if ((from f in Lista
                     where f.Nombre == Fosil.Nombre
                     select f.Nombre).Any())
                {
                    Error = "El nombre del fosil ya existe en el inventario actual";
                    Evento("Error");
                    return;
                }
                if (string.IsNullOrEmpty(Fosil.Imagen))
                {
                    Error = "Por favor rellene el espacio vacio imagen";
                    Evento("Error");
                    return;
                }
                if (Fosil.Año == 0)
                {
                    Error = "El año tiene que ser mayor a 0";
                    Evento("Error");
                    return;
                }
                if(!Uri.TryCreate(Fosil.Imagen, UriKind.Absolute, out var uri))
                {
                    Error = "Escriba una URL de la imagen valida";
                    Evento("Error");
                    return;
                }

                    Lista.Add(Fosil);
                    CambiarVista("ColeccionVista");
                    GuardarArchivo();
            }
        }
        private int posicionelementomodificado;
        public int Posicion
        {
            get { return posicionelementomodificado; }
            set { posicionelementomodificado = value;
                Evento("Posicion");
            }
        }
        public void CambiarVista(string vista)
        {
            Vista = vista;
            Evento();
            if (Vista == "AgregarVista")
            {
                Fosil = new Fosil();
            }
            if(Vista == "EditarVista")
            {
                if(Fosil != null)
                {
                    var clon = new Fosil()
                    {
                        Nombre = Fosil.Nombre,
                        Descripcion = Fosil.Descripcion,
                        Imagen = Fosil.Imagen,
                        Año = Fosil.Año
                    };
                    posicionelementomodificado = Lista.IndexOf(Fosil);
                    Fosil = clon;
                }

            }
        }
        public void GuardarArchivo()
        {
            var json = JsonConvert.SerializeObject(Lista);
            File.WriteAllText("Fosiles.json", json);  
        }

        public void CargarArchivo()
        {
            if (File.Exists("Fosiles.json"))
            {
                var json = File.ReadAllText("Fosiles.json");
                var datos = JsonConvert.DeserializeObject<ObservableCollection<Fosil>?>(json);
                if (datos != null)
                    Lista = datos;
                else
                    Lista = new ObservableCollection<Fosil>();

            }
        }
        public void Evento(string? propiedad = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propiedad));
        }


    }
}
