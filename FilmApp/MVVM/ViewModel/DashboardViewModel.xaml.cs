using FilmApp.AppInteraction;
using FilmApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FilmApp.MVVM.ViewModel
{
    /// <summary>
    /// Interaction logic for DashboardViewModel.xaml
    /// </summary>
    public partial class DashboardViewModel : UserControl
    {
        private readonly FilmsData _filmdata;
        public DashboardViewModel()
        {
            InitializeComponent();
            FilmTable.ItemsSource = new List<FilmData> { };
            _filmdata = new FilmsData();
            FilmsData.FilmsFill += OnInteract;
        }

        private void OnInteract(FilmList obj)
        {
            FilmTable.ItemsSource = obj.List;
            FilmTable.Items.Refresh();
        }

        private void BFindByCountry_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FindFilmsWithActor_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BFindWithIdentDirAndLowesPRice_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FindLongesstForEachDirector_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
