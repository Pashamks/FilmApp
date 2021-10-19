using FilmApp.Model;
using System;
using System.Windows;
using System.IO;
using Newtonsoft.Json;
using FilmApp.AppInteraction;

namespace FilmApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FilmList list =null;
        FilmsData filmsData;
        public MainWindow()
        {
            InitializeComponent();
            list = new FilmList();
            filmsData = new FilmsData();
        }

        private void BSort_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                list.SortByCountry();
                filmsData.UpDateFilms(list,ToChange.Yes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BWriteFilmsToFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string films = System.Text.Json.JsonSerializer.Serialize(list);
                File.WriteAllText($"{Directory.GetCurrentDirectory()}/films.json", films);
                MessageBox.Show("Your data saved to file.", "Successe", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void BReadFilmsFromFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(list!=null && list.List.Count > 0)
                {
                    MessageBoxResult result = MessageBox.Show("Do you want to read new data? Your old data will be clear.", "Question",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                FilmData.counter = 0;
                using (StreamReader read = new StreamReader($"{Directory.GetCurrentDirectory()}/films.json"))
                {
                    string json = read.ReadToEnd();
                    list = JsonConvert.DeserializeObject<FilmList>(json);
                }
                filmsData.UpDateFilms(list, ToChange.Yes);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BFindPopularActor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(list.FindPopularActor(), "The most popular actor:",MessageBoxButton.OK ,MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BShowAllList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                filmsData.UpDateFilms(list, ToChange.No);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BFindOldestExpesive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                filmsData.UpDateFilms(list.FindTheMostExpensiveAndOldest(), ToChange.No);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            BOpenMenu.Visibility = Visibility.Collapsed;
            BCloseMenu.Visibility = Visibility.Visible;
            MainGrid.Margin = new Thickness(200, 0, 0, 0);
        }

        private void BCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            BOpenMenu.Visibility = Visibility.Visible;
            BCloseMenu.Visibility = Visibility.Collapsed;
            MainGrid.Margin = new Thickness(70, 0, 0, 0);
        }
    }
}
