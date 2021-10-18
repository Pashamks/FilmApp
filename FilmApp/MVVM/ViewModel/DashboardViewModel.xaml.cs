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
        private FilmList list;
        public DashboardViewModel()
        {
            InitializeComponent();
            FilmTable.ItemsSource = new List<FilmData> { };
            _filmdata = new FilmsData();
            FilmsData.FilmsFill += OnInteract;
        }

        private void OnInteract(FilmList obj, ToChange value)
        {
            if (value == ToChange.Yes)
            {
                amount_fo_films.Text = "Amount of films in app: " + FilmData.counter.ToString();
                list = obj;
            }
            FilmTable.ItemsSource = obj.List;
            FilmTable.Items.Refresh();
        }

        private void BFindByCountry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FilmTable.ItemsSource = list.FindByCountry(parametr_to_find.Text);
                FilmTable.Items.Refresh();
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
                FilmTable.ItemsSource = list.FindAllFilmsWithActor(parametr_to_find.Text);
                FilmTable.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BFindWithIdentDirAndLowesPRice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FilmTable.ItemsSource = list.FindSameDirectorsAndLowesBudget();
                FilmTable.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FindLongesstForEachDirector_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FilmTable.ItemsSource = list.FindForDirectorsLongestFilm();
                FilmTable.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BAddNewFilm_Click(object sender, RoutedEventArgs e)
        {
            if (film_name.Text.Any(c => !char.IsLetter(c)) ||
               film_director.Text.Any(c => !char.IsLetter(c)) ||
               film_country.Text.Any(c => !char.IsLetter(c)) ||
               film_actors.Text.Any(c => !char.IsLetter(c)) ||
               film_budget.Text.Any(c => !char.IsDigit(c)) ||
               film_time.Text.Any(c => !char.IsDigit(c)) ||
               film_year.Text.Any(c => !char.IsDigit(c)))
            {
                MessageBox.Show("There is an error in your data, so we can't add this to films list. Fix your data and try again.");
                return;
            }
            else
            {
                var film = new FilmData
                {
                    Name = film_name.Text,
                    Director = film_director.Text,
                    Actors = new ActorsList(film_actors.Text.Split(',').ToList<string>()),
                    Country = film_country.Text,
                    Price = Convert.ToDouble(film_budget.Text),
                    Time = Convert.ToDouble(film_time.Text),
                    Year = Convert.ToInt32(film_year.Text),
                };
                list.List.Add(film);
                FilmTable.ItemsSource = list.List;
                FilmTable.Items.Refresh();
            }
        }
    }
}
