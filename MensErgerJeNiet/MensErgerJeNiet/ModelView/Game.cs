using MensErgerJeNiet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensErgerJeNiet.ModelView
{
    class Game
    {

        private int _nrOfPlayers, _nrOfFields, _diceRoll;
        private Random _random;
        private Board _board;
        //private list players

        //Constructor: nr of fields evt af te leiden van nr of players? (aantal fields x spelers) of fixed aantal
        public Game(int nrOfPlayers , int nrOfFields)
        {
            _nrOfPlayers = nrOfPlayers;
            _nrOfFields = nrOfFields;
            _random = new Random();

            startGame(_nrOfPlayers, _nrOfFields);
        }

        //Starts up the game
        private void startGame(int nrPlayers , int nrFields)
        {
            //add players to player list

            //create board
            //_board = new Board();

        }

        //handles individual turns
        private void handleTurn(Player p)
        {
            bool turnFinished = false;
            _diceRoll = 0;
            //pawn selected

            while (!turnFinished)
            {
                if (_diceRoll != 0)
                {
                    //if( pawn selected = true)
                        //if(pawn == p.pawn?)
                            //movePawn(roll, pawn);
                              turnFinished = true;
                }
            }
            
            //if(player.pawnsingoal ==4)
                //END GAME
            //if(_diceRoll == 6)
                //handleTurn(p);
            //else
                 //handleTurn(p.next);

        }

        //Used by MainWindows EventHandler (click on dice)
        public void rollDice()
        {
            _diceRoll = _random.Next(1, 7);
        }


    }
}
