using FilmApp.Model;
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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace FilmApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FilmList list =null;
        public MainWindow()
        {
            InitializeComponent();
            list = new FilmList();
           
            FilmTable.ItemsSource = list.List;
        }

        private void BFindTheLongesFilms_Click(object sender, RoutedEventArgs e)
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

        private void BSort_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                list.SortByCountry();
                FilmTable.ItemsSource = list.List;
                FilmTable.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void BWriteFilmsToFile_Click(object sender, RoutedEventArgs e)
        {
            string films = System.Text.Json.JsonSerializer.Serialize(list);
            File.WriteAllText($"{Directory.GetCurrentDirectory()}/films.json",films);
        }

        private void BReadFilmsFromFile_Click(object sender, RoutedEventArgs e)
        {
            using (StreamReader read = new StreamReader($"{Directory.GetCurrentDirectory()}/films.json"))
            {
                string json = read.ReadToEnd();
                list = JsonConvert.DeserializeObject<FilmList>(json);
            }
            FilmTable.ItemsSource = list.List;
            FilmTable.Items.Refresh();
        }

        private void BFindPopularActor_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(list.FindPopularActor());
        }

        private void BShowAllList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FilmTable.ItemsSource = list.List;
                FilmTable.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BFindFilmsWithActor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FilmTable.ItemsSource = list.FindAllFilmsWithActor(ActorName.Text);
                FilmTable.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void ActorName_GotFocus(object sender, RoutedEventArgs e)
        {
            ActorName.Text = "";
        }

        private void ActorName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ActorName.Text == "")
                ActorName.Text = "Enter actor name";
        }

        private void Country_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Country.Text == "")
                Country.Text = "Enter country";
        }

        private void Country_GotFocus(object sender, RoutedEventArgs e)
        {
            Country.Text = "";
        }

        private void BFindFilmsByCountry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FilmTable.ItemsSource = list.FindByCountry(Country.Text);
                FilmTable.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BFindOldestExpesive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FilmTable.ItemsSource = list.FindTheMostExpensiveAndOldest();
                FilmTable.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BFindDirectorsLowestBudget_Click(object sender, RoutedEventArgs e)
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
    }
}
