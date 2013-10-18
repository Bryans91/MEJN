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

        public Goal(Player p)
        {
            _player = p;
        }

        public Player player
        {
            get { return _player; }
        }
        

    }
}
