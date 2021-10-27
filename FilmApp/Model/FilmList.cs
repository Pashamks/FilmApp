using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FilmApp.Model
{
    public class FilmList
    {
        List<FilmData> list = null;
        public FilmList() 
        {
            list = new List<FilmData>();
        }
        public FilmList(List<FilmData> list)
        {
            this.list = list;
        }
        public FilmList(FilmList filmList)
        {
            list = filmList.list;
        }
        public List<FilmData> List {  get { return list; } set { list = value; } }

        public void  SortByCountry()
        {
            //Алгоритмом простої вибірки відсортувати записи за Країною виробництва
            if (list == null || list.Count == 0)
                throw new NullReferenceException("Your list is empty");
            FilmData fix, min;
            int min_index;
            for (int j = 0; j < list.Count - 1; j++)
            {
                fix = list[j];
                min = list[j + 1];
                min_index = j + 1;
                for (int i = j + 1; i < list.Count; i++)
                {
                    if (String.Compare(min.Country, list[i].Country, true) > 0)
                    {
                        min = list[i];
                        min_index = i;
                    }
                }
                //міняємо місцями фіксований елемент і мінімальний
                if (String.Compare(fix.Country, min.Country, true) > 0)
                {
                    list[j] = min;
                    list[min_index] = fix;
                }
            }
        }
        public string  FindPopularActor()
        {
            // Встановити найбільш популярного актора
            Dictionary<string, int> actors = new Dictionary<string, int>();
            //формуємо словник акторів і кількістю їхніх зйомок у фільмах
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[j].Actors.Actors.Count; j++)
                {
                    if(!actors.TryAdd(list[i].Actors.Actors[j], 1))
                    {
                        actors[list[i].Actors.Actors[j]]++;
                    }
                }
            }
            int max = 0;
            //знаходимо максимальну кількість зйомок
            foreach (var item in actors)
            {
                if(max < item.Value)
                {
                    max = item.Value;
                }
            }
            //повертаємо актора з максмальною кількістю зйомок
            foreach (var item in actors)
            {
                if (max == item.Value)
                    return item.Key;
            }
            return string.Empty;
        }

        public List<FilmData> FindAllFilmsWithActor(string actor_name)
        {
            // За заданим актором визначити всі фільми, в яких він (вона) знімались
            if (list == null || list.Count == 0)
                throw new NullReferenceException("Your list is empty!");
            List<FilmData> films = new List<FilmData>();
            //знаходимо фільми з потрібним актором
            foreach (var film in list)
            {
                foreach (var actor in film.Actors.Actors)
                {
                    if(actor == actor_name)
                    {
                        films.Add(film);
                    }
                }
            }
            return films;
        }
        public List<FilmData> FindForDirectorsLongestFilm()
        { 
            //Для кожного Режисера визначити фільм з найбільшою тривалістю.
            if (list == null || list.Count == 0)
                throw new NullReferenceException("Your list is empty");
            List<FilmData> films = new List<FilmData>();
            Dictionary<string, FilmData> longest_films = new Dictionary<string, FilmData>();
            double max_time = 0;

            foreach (var film in list)
            {
                // find max time of director films
                foreach (var max_time_film in list)
                {
                    if(max_time_film.Director == film.Director)
                    {
                        if(max_time < max_time_film.Time)
                        {
                            max_time = max_time_film.Time;
                        }
                    }
                }
                // add film with max time to new list
                foreach (var max_time_film in list)
                {
                    if (max_time_film.Director == film.Director)
                    {
                        if (max_time == max_time_film.Time)
                        {
                            longest_films.TryAdd(max_time_film.Director, max_time_film);
                            break;
                        }
                    }
                }
                max_time = 0;
            }
            films = longest_films.Select(val => val.Value).ToList();
            return films;
        }

        public FilmList FindTheMostExpensiveAndOldest()
        {
            ///Знайти найдорожчий та найстарший фільм одночасно
            if (list.Count == 0)
            {
                throw new NullReferenceException("Your list is empty");
            }
            else if (list.Count == 1)
            {
                return this;
            }
            List<FilmData> films = list.ToList() ;
            //шукаємо найдорожчі фільми
            List<FilmData> max_price = list.Where(val => val.Price == list.Max(film => film.Price)).ToList<FilmData>();
            //шукаємо найстарші фільми
            films = max_price.Where(val => val.Year == max_price.Min(film => film.Year)).ToList<FilmData>();

            return new FilmList(films);
        }
       
        public List<FilmData> FindSameDirectorsAndLowesBudget()
        { 
            ///Визначити Назви фільмів, в яких однакові 
            ///режисери та найменші бюджети одночасно.
            
            if (list == null || list.Count == 0)
                throw new NullReferenceException("Your list is empty!");
            //групуємо фільми по режисерах
            var group_films = from filmdata in list group filmdata by filmdata.Director;
            List<double> film_price = new List<double>(group_films.Count());
            int i = 0;
            //рахуємо суми їхніх бюджетів
            foreach (IGrouping<string, FilmData> g in group_films)
            {
                film_price.Add(new double());
                foreach (var film in g)
                    film_price[i] += film.Price;
                i++;
            }
            //знаходимо індекс мінімального
            int index = film_price.IndexOf(film_price.Min());
            List<FilmData> films = group_films.ElementAt(index).Select(g => g).ToList();
            
            return films;
        }
        public List<FilmData> FindByCountry(string country)
        {
            ///За заданою країною виробництва знайти всі фільми, 
            ///в яких найбільші бюджети і найменша тривалість одночасно.
            
            if (list == null || list.Count == 0)
                throw new NullReferenceException("Your list is empty!");
            //вибираємо фільми із заданої країни
            List<FilmData> films = list.Where(val => val.Country == country).ToList<FilmData>();
          
            if (films.Count == 0)
                throw new ArgumentException("There is no films from this country");
            if (films.Count == 1)
                return films;
            // вибираємо фільми з максимальною ціною
            List<FilmData> max_price = films.Where(
                film => film.Price == 
                films.Max(val => val.Price)).ToList<FilmData>();
            // вибираємо фільми з найменшою тривалістю
            List<FilmData> min_time = max_price.Where(
                val => val.Time == 
                max_price.Min(film => film.Time)).ToList<FilmData>();

            return min_time;
        }
    }
}
