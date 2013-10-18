using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensErgerJeNiet.Model
{
    class Player
    {
        private Field _startingField;
        private Spawn[] _spawns; 

        public Player()
        {

        }

        //properties
        public Field startingField
        {
            get { return _startingField; }
            set { _startingField = startingField; }
        }

        //test if writeprotected or not
        public Spawn[] spawns{
            get { return _spawns; }
        }

        
    }
}
