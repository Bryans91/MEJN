using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensErgerJeNiet.Model
{
    class Goal : Field
    {
        private Player _player;

        public Goal(Player p, string fieldC)
        {
            _player = p;
            _fieldCode = fieldC;
        }

        public Player player
        {
            get { return _player; }
            set { _player = value; }
        }
        

    }
}
