using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.ViewModel
{
    public class MenuViewModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public int pId { get; set; }

        public bool @checked { get; set; }

        public bool isParent { get; set; }

        //public List<MenuViewModel> children { get; set; }

        //public State state { get; set; }

        public MenuViewModel()
        {
            //children = new List<MenuViewModel>();
            //state = new State() {opened="true"};
        }


    }
    public class MenuViewModel2
    {
        public string id { get; set; }

        public string text { get; set; }

        public string parent { get; set; }

        //public List<MenuViewModel2> children { get; set; }

        public State state { get; set; }

        public bool children { get; set; }

        public MenuViewModel2()
        {
            //children = new List<MenuViewModel2>();
            state = new State() { opened = "true" };
        }


    }
    public class State
    {
        public string opened { get; set; }
    }
}
