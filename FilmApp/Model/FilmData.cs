using FilmApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmApp
{
    public class FilmData
    {
        protected static int counter;
        protected string _name;
        protected string _director;
        protected int _year;
        protected ActorsList _actors;
        protected double _price;
        protected string _country;
        protected double _time;

        public string Name { get { return _name; } set { _name = value; } }
        public string Director { get { return _director; } set { _director = value; } }
        public int Year { get { return _year; } set { _year = value; } }

        public ActorsList Actors { get { return _actors; } set { _actors = value; } }

        public double Price { get { return _price; } set { _price = value; } }
        public string Country { get { return _country; } set { _country = value; } }
        public double Time { get { return _time; } set { _time = value; } }

        static FilmData() { counter = 0; }
        public FilmData() { counter++;  }
        public FilmData(string name,string director, int year, ActorsList  actors,
        double price, string country, double time)
        {
            _name = name;
            _director = director;
            _year = year;
            _actors = actors;
            _price = price;
            _country = country;
            _time = time;
            counter++;
        }
        public FilmData(FilmData val)
        {
            _name = val._name;
            _director = val._director;
            _year = val._year;
            _actors = val._actors;
            _price = val._price;
            _country = val._country;
            _time = val._time;
            counter++;
        }
    }
}
