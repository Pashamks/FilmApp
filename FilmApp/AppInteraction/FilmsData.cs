using FilmApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FilmApp.AppInteraction
{
    public enum ToChange { Yes, No}
    public class FilmsData
    {
        public delegate void FilmListEvent(FilmList list, ToChange value);
        public static event FilmListEvent FilmsFill;
        public void UpDateFilms(FilmList filmList, ToChange value)
        {
            FilmsFill?.Invoke(filmList, value);
        }
    }
}
