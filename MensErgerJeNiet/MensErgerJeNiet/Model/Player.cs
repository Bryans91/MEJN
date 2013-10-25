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
        private bool _isHuman;
        private int _pawnsInGoal , _startRoll;
        private string _color;
        private Player _nextP;
        private Pawn[] _pawns;

        public Player(bool isHuman , string color)
        {
            _isHuman = isHuman;
            _color = color;
            _startRoll = -1;

            _pawns = new Pawn[4];

        }

        public void pawnToSpawn(Pawn p)
        {
           bool placed = false;

          for (int i = 0; i < _spawns.Length; i++)
          {
                if (!placed)
                {
                   if (_spawns[i].pawn == null)
                    {
                        _spawns[i].pawn = p;
                        p.currentField = _spawns[i];
                        placed = true;
                        p.onSpawn = true;
                    }
                }
            }
        }


        //properties
        public Pawn[] pawns
        {
            get { return _pawns; }
        }

        public int startRoll
        {
            get { return _startRoll; }
            set { _startRoll = value; }
               
        }

        public Player nextP
        {
            get {return _nextP;}
            set { _nextP = value; }
        }


        public Field startingField
        {
            get { return _startingField; }
            set { _startingField = value; }
        }

        public bool isHuman
        {
            get { return _isHuman; }
            set { _isHuman = value; }
        }

        //test if writeprotected or not
        public Spawn[] spawns{
            get { return _spawns; }
        }

        public int pawnsInGoal
        {
            get { return _pawnsInGoal; }
            set { _pawnsInGoal = value; }
        }

        public string color
        {
            get { return _color; }
        }
        
    }
}
