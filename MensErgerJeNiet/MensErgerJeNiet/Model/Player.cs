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

        public Player(bool isHuman , string color , int start)
        {
            _isHuman = isHuman;
            _color = color;
            _startRoll = start;

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
                        placed = true;
                    }
                }
            }
        }


        //properties
        public int startRoll
        {
            get { return _startRoll; }
            set{_startRoll = startRoll;}
               
        }

        public Player nextP
        {
            get {return _nextP;}
            set { _nextP = nextP; }
        }


        public Field startingField
        {
            get { return _startingField; }
            set { _startingField = startingField; }
        }

        public bool isHuman
        {
            get { return _isHuman; }
            set { _isHuman = isHuman; }
        }

        //test if writeprotected or not
        public Spawn[] spawns{
            get { return _spawns; }
        }

        public int pawnsInGoal
        {
            get { return _pawnsInGoal; }
            set { _pawnsInGoal = pawnsInGoal; }
        }

        public string color
        {
            get { return _color; }
        }
        
    }
}
