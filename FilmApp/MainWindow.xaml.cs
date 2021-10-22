using FilmApp.Model;
using System;
using System.Windows;
using System.IO;
using Newtonsoft.Json;
using FilmApp.AppInteraction;
using System.Diagnostics;
using Microsoft.Win32;

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
                filmsData.UpDateFilms(list,ToChange.Yes, "Your films sorted by country:");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = false;
                fileDialog.Filter = "Text files|*.json*.*";
                fileDialog.DefaultExt = ".txt";
                Nullable<bool> dialogOk = fileDialog.ShowDialog();
                string filePath=string.Empty;
                if (dialogOk == true)
                {
                     filePath = fileDialog.FileNames[0];
                }
                if (!filePath.Contains(".json"))
                {
                    throw new Exception("We don't work with this type of files!");
                }
                FilmData.counter = 0;
                //$"{Directory.GetCurrentDirectory()}/films.json")
                using (StreamReader read = new StreamReader(filePath))
                {
                    string json = read.ReadToEnd();
                    if (json == string.Empty)
                        throw new Exception("Your file is empty! Try to open another file.");
                    list = JsonConvert.DeserializeObject<FilmList>(json);
                }
                if (list.List.Count == 0)
                    throw new Exception("We can't convert your file data to film. Fix your data to json format and try again.");
                filmsData.UpDateFilms(list, ToChange.Yes,"Your films:");
                MessageBox.Show("You successfully readed films from file", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BShowAllList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (list.List.Count == 0)
                    throw new Exception("Your list is empty!");
                filmsData.UpDateFilms(list, ToChange.No, "Your films:");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BFindOldestExpesive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                filmsData.UpDateFilms(list.FindTheMostExpensiveAndOldest(), ToChange.No,"Your the most expensive and the oldest films:");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void BInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This app created by Somko Pavlo.\nStudent of NULP, PZ-25.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
