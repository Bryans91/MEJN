using MensErgerJeNiet.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MensErgerJeNiet.Model
{
    public class Pawn
    {

        private Player _player;
        private Field _currentField;
        private bool _canHit , _onSpawn;
        

        public Pawn(Player p, Field firstField)
        {
            _player = p;
            _currentField = firstField;
            _onSpawn = true;

            bool inPawnList = false;
            for (int i = 0; i < _player.pawns.Length; i++)
            {
                if (!inPawnList)
                {
                    if (_player.pawns[i] == null)
                    {
                        _player.pawns[i] = this;
                        inPawnList = true;
                    }
                }
            }

        }


        public bool canMove(int steps)
        {
            
            Field goal = null;
            goal = _currentField;
           
            bool direction = true;

            if (_onSpawn && steps != 6)
            {
                return false;
            }
            else
            {
                if (_onSpawn && steps == 6)
                {
                    goal = _currentField.nextF;
                } 


                
            }

            //goal is null
            if (goal == null)
            {
                goal = _currentField;
            }
            
            //check if possible to move
            for (int i = 0; i < steps; i++)
            {
                
                if (goal.nextF == null)
                {
                    direction = false;
                }

                if (direction)
                { //forwards                 
                    if (goal.switchF != null)
                    {

                        if (goal.switchF.player.color.Equals( this._player.color))
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
                } // backwards
                else
                {
                    goal = goal.previousF;
                }
             
            } // end for

            Console.WriteLine("THEGOAL: " + goal.fieldCode);
            //check the goal location
                if (goal.pawn != null)
                {
                   if (goal.pawn == this)
                   {
                        return true;
                   }
                   if (goal.pawn.player == player)
                   {
                       _canHit = false;
                       return false;
                   }
            
                    if (goal.pawn.player != _player)
                    {
                        _canHit = true;
                        return true;
                    }
                    else
                    {
                        _canHit = false;
                        return false;
                    }
                }
                else
                {
                    _canHit = false;
                    return true;
                }                                  
        } //endmethod


        public void move(int steps , Game g)
        {
            //The actual move
            bool direction = true;
           
            Field temp = currentField;
            //TESTCODE
            currentField.pawn = null;


            if (_onSpawn)
            {
                //_currentField = _currentField.nextF;
                _onSpawn = false;
            }
                for (int i = 1; i <= steps; i++)
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
                                _currentField = _currentField.switchF;
                                _player.pawnsInGoal++;
                            }
                            else
                            {
                                //check if last move & is filled *not a switch*
                                if (i == steps)
                                {
                                    if (_currentField.nextF.pawn != null)
                                    {
                                        _currentField.nextF.pawn.player.pawnToSpawn(_currentField.nextF.pawn , g);
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
                                    _currentField.nextF.pawn.player.pawnToSpawn(_currentField.nextF.pawn, g);
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
                                _currentField.previousF.pawn.player.pawnToSpawn(_currentField.previousF.pawn , g);
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

                            if (_currentField.switchF != null)
                            {
                                if (_currentField.switchF.player == _player)
                                {
                                    _player.pawnsInGoal--;
                                }
                            }
                      }   // end if else      


                    //Drawing step by step
                    if (direction)
                    {
                        if (_currentField.pawn == null)
                        {
                            _currentField.pawn = this;

                        }

                        if (_currentField.previousF.pawn == this)
                        {
                            _currentField.previousF.pawn = null;

                            g.sendFieldCode(_currentField.previousF);
                        }
                    }
                    else
                    {
                        if (_currentField.pawn == null)
                        {
                            _currentField.pawn = this;

                        }

                        if (_currentField.nextF.pawn == this)
                        {
                            _currentField.nextF.pawn = null;

                            g.sendFieldCode(_currentField.nextF);
                        }

                        if (_currentField.switchF != null)
                        {
                            if (_currentField.switchF.pawn != null)
                            {
                                if (_currentField.switchF.pawn == this)
                                {
                                    _currentField.switchF.pawn = null;
                                    g.sendFieldCode(_currentField.switchF);
                                }
                            }
                        }


                    }

                        

                        //no clue why, but wont work without V. maybe it delays something
                       g.colorSpawns();
                    
                    //refresh
                    if (temp != currentField)
                    {
                        //Does not draw steps individually
                        g.sendFieldCode(currentField);
                    }
                   
                    
                }//endfor

                Console.WriteLine("NEW LOCATION: " + _currentField.fieldCode);
                g.colorSpawns();
        } //end method


        public bool canHit
        {
            get { return _canHit; }
        }


        public Player player
        {
            get { return _player; }
        }

        public Field currentField
        {
            get { return _currentField; }
            set { _currentField = value; }
        }

        public bool onSpawn
        {
            get { return _onSpawn; }
            set { _onSpawn = value; }
        }


    }
}
