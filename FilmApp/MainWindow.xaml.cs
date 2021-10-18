﻿using FilmApp.Model;
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
using FilmApp.Model;
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
            //FilmTable.ItemsSource = list.List;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void BReadFilmsFromFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
                MessageBox.Show(ex.Message);
            }
        }

        private void BFindPopularActor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(list.FindPopularActor());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
        }


        //private void ActorName_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    ActorName.Text = "";
        //}

        //private void ActorName_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (ActorName.Text == "")
        //        ActorName.Text = "Enter actor name";
        //}

        //private void Country_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    if (Country.Text == "")
        //        Country.Text = "Enter country";
        //}

        //private void Country_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    Country.Text = "";
        //}


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
