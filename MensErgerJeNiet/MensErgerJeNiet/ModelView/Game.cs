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

        private int _diceRoll;
        private Random _random;
        private Board _board;
        private Player[] _playerList;
        

        //Constructor: nr of fields evt af te leiden van nr of players? (aantal fields x spelers) of fixed aantal
        public Game()
        {
           
            _random = new Random();

           
        }

        //Starts up the game
        public void startGame(int nrOfPlayers , int nrOfHumans)
        {
            _playerList = new Player[nrOfPlayers];
            Player temp = null;

            //add players to player list & sets human or computer player
            for (int i = 0; i < nrOfPlayers; i++ )
            {
                string color = "";
                switch (i)
                {
                    case 0:
                        color = "green";
                        break;
                    case 1:
                        color = "red";
                        break;
                    case 2:
                        color = "blue";
                        break;
                    case 3:
                        color = "yellow";
                        break;
                }


                if (i > nrOfHumans)
                {
                    _playerList[i] = new Player(false, color , rollDice());
                    temp.nextP = _playerList[i];
                }
                else
                {
                    _playerList[i] = new Player(true, color , rollDice());
                    temp.nextP = _playerList[i];
                }

                temp = _playerList[i];

                if (i == nrOfPlayers)
                {
                    _playerList[0].nextP = _playerList[i];
                }
            }
            
            //create board
            //_board = new Board();
           

            //handle who may start the game
            Player first = _playerList[0];

            foreach (Player p in _playerList)
            {
                if (p.startRoll > first.startRoll)
                {
                    first = p;
                }
            }

            handleTurn(first);

        }

        //handles individual turns
        private void handleTurn(Player p)
        {
            bool turnFinished = false;
            _diceRoll = 0;
            //pawn selected

            if (!p.isHuman)
            {
               _diceRoll = rollDice();
                //select a pawn

            }

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

            if (p.pawnsInGoal == 4) { 
                // end the game here
            }
            else if (_diceRoll == 6)
            {
                handleTurn(p);
            }
            else
            {
                handleTurn(p.nextP);
            }
        }

        //Used by MainWindows EventHandler (click on dice)
        //edit2: implemented in mainwindow eventhandler
        public int rollDice()
        {
            return _random.Next(1, 7);
        }



        //properties
        public Player[] playerList
        {
            get { return _playerList; }
        }

    }
}
