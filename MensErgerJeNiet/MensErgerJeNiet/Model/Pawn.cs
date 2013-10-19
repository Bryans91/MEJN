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

        public Pawn(Player p, Field firstField)
        {
            _player = p;
            _currentField = firstField;

        }


        public bool canMove(int steps)
        {
            Field goal = _currentField;
            bool direction = true;
         
            //check if possible to move
            for (int i = 0; i < steps; i++)
            {
                if (goal.nextF == null)
                {
                    direction = false;
                }

                if (direction)
                {
                    if (goal.nextF.switchF != null)
                    {
                        if (goal.switchF.player == _player)
                        {
                            goal = goal.switchF;
                        }
                        else
                        {
                            goal = goal.nextF;
                        }
                    }
                    else
                    {
                        goal = goal.nextF;
                    }
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
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }                                  
        } //endmethod


        public void move(int steps)
        {


            //The actual move
            bool direction = true;
            
                for (int i = 0; i <= steps; i++)
                {

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
                                //check if last move & is filled *switch*
                                if (i == steps)
                                {
                                    if (_currentField.switchF.pawn != null)
                                    {
                                        _currentField.switchF.pawn.player.pawnToSpawn(_currentField.switchF.pawn);
                                        _currentField = _currentField.switchF;
                                        _currentField.pawn = this;
                                    }
                                    else 
                                    {
                                        _currentField = _currentField.switchF;
                                        _currentField.pawn = this;
                                    }
                                }
                                else
                                {
                                //normal move
                                    _currentField = _currentField.switchF;
                                }
                            }
                            else
                            {
                                //check if last move & is filled *not a switch*
                                if (i == steps)
                                {
                                    if (_currentField.nextF.pawn != null)
                                    {
                                        _currentField.nextF.pawn.player.pawnToSpawn(_currentField.nextF.pawn);
                                        _currentField = _currentField.nextF;
                                        _currentField.pawn = this;
                                    }
                                    else
                                    {
                                        _currentField = _currentField.nextF;
                                        _currentField.pawn = this;
                                    }
                                }
                                else
                                {
                                    //normal move
                                    _currentField = _currentField.nextF;
                                }
                            }
                        }
                        else
                        {
                            //check if last move & is filled *no switch*
                            if (i == steps)
                            {
                                if (_currentField.nextF.pawn != null)
                                {
                                    _currentField.nextF.pawn.player.pawnToSpawn(_currentField.nextF.pawn);
                                    _currentField = _currentField.nextF;
                                    _currentField.pawn = this;
                                }
                                else
                                {
                                    _currentField = _currentField.nextF;
                                    _currentField.pawn = this;
                                }
                            }
                            else
                            {
                                //normal move
                                _currentField = _currentField.nextF;
                            }                           
                        }

                    }
                    else
                    {
                        //check if last move & is filled *going backwards*
                        if (i == steps)
                        {
                            if (_currentField.previousF.pawn != null)
                            {
                                _currentField.previousF.pawn.player.pawnToSpawn(_currentField.previousF.pawn);
                                _currentField = _currentField.previousF;
                                _currentField.pawn = this;
                            }
                            else
                            {
                                _currentField = _currentField.previousF;
                                _currentField.pawn = this;
                            }
                        }
                        else
                        {
                            //normal move
                            _currentField = _currentField.previousF;
                        }                      
                    }        
                }//endfor
        } //end method



        public Player player
        {
            get { return _player; }
        }

    }
}
