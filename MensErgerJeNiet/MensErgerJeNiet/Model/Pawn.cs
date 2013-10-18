using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensErgerJeNiet.Model
{
    class Pawn
    {

        private Player _player;
        private Field _currentField;

        public Pawn(Player p , Field firstField)
        {
            _player = p;
            _currentField = firstField;
        }



        public void move(int steps)
        {
            Field goal = _currentField; 
            bool direction = true;
            bool canMove = false;

            

            //check if possible to move
            for (int i = 0; i < steps; i++)
            {
                if (goal.nextF == null)
                {
                    direction = false;
                }

                if (direction)
                {
                    goal = goal.nextF;
                }
                else
                {
                    goal = goal.previousF;
                }
                
            }

            //check the goal location 
            if (goal.pawn != null)
            {
                if (goal.pawn.player != _player)
                {
                    canMove = true;
                }
            }

            //The actual move
            direction = true;

            if (canMove)
            {
                for (int i = 0; i <= steps; i++)
                {
                    //check if spot is not filled
                    if (i == steps)
                    {
                        if (goal.pawn != null)
                        {
                            goal.pawn.player.pawnToSpawn(goal.pawn);
                        }
                    }

                    if (_currentField.nextF == null)
                    {
                        direction = false;
                    }

                    if (direction)
                    {
                        if (_currentField.switchF != null)
                        {
                            if (_currentField.switchF.player == this._player)
                            {
                                _currentField = _currentField.switchF;
                            }
                            else
                            {
                                _currentField = _currentField.nextF;
                            }
                        }
                        else
                        {
                            _currentField = _currentField.nextF;
                        }

                    }
                    else
                    {
                        _currentField = _currentField.previousF;
                    }

                    //set pawn on field
                    _currentField.pawn = this;
                }//endfor

            } //endmove
  
        } //end method




        //properties
        public Player player
        {
            get { return _player; }
        }

        public Field currentField
        {
            get { return _currentField; }
            set { _currentField = currentField; }
        }
    }
}
