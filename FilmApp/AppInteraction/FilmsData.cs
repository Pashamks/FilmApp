using FilmApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FilmApp.AppInteraction
{
    public class FilmsData
    {
        public delegate void FilmListEvent(FilmList list);
        public static event FilmListEvent FilmsFill;
        public void FillFilms(FilmList filmList)
        {
            FilmsFill?.Invoke(filmList);
        }
    }
}
