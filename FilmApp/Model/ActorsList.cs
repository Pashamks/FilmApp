using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmApp.Model
{
    public class ActorsList
    {
        List<string> actors = null;
        public List<string> Actors { get { return actors; }  set { actors = value; } }
        
        public ActorsList() { actors = new List<string>(); }
        public ActorsList(string name)
        {
            if(actors==null)
                actors = new List<string>();
            actors.Add(name);
        }
        public ActorsList(List<string> actors_list)
        {
            actors = actors_list;
        }
        public ActorsList(ActorsList _list)
        {
            actors = _list.actors;
        }
        public override string ToString()
        {
            string str = string.Empty;
            foreach (var actor in actors)
            {
                str += actor + " ; ";
            }
            return str;
        }
    }
}
