using System;
using System.Collections.Generic;
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

namespace PaleontologiaMVVM.Views
{
    /// <summary>
    /// Lógica de interacción para AgregarView.xaml
    /// </summary>
    public partial class AgregarView : UserControl
    {
        public AgregarView()
        {
            InitializeComponent();
        }

        private void textNombre_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtNombre.Focus();
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtNombre.Text) && txtNombre.Text.Length > 0)
            {
                textNombre.Visibility = Visibility.Collapsed;
            }
            else
            {
                textNombre.Visibility = Visibility.Visible;
            }
        }

        private void textimagen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtimagen.Focus();
        }

        private void txtimagen_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtimagen.Text) && txtimagen.Text.Length > 0)
            {
                textimagen.Visibility = Visibility.Collapsed;
            }
            else
            {
                textimagen.Visibility = Visibility.Visible;
            }


        }

        private void textAño_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtAño.Focus();
        }

        private void txtAño_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAño.Text) && txtAño.Text.Length > 0)
            {
                textAño.Visibility = Visibility.Collapsed;
            }
            else
            {
                textAño.Visibility = Visibility.Visible;
            }
        }

        private void txtAño_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtAño.Text = "";
            txtimagen.Text = "";
            txtNombre.Text = "";
            txtdescripcion.Text = "";

        }
    }
}
