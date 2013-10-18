using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensErgerJeNiet.Model
{
    class Goal : Field
    {
        private string _color;

        public Goal(string c)
        {
            _color = c;
        }

        public string color
        {
            get { return _color; }
        }
        

    }
}
