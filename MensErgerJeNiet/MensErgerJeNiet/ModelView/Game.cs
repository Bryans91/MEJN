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
        private Pawn _selected;
        

        //Constructor: nr of fields evt af te leiden van nr of players? (aantal fields x spelers) of fixed aantal
        public Game()
        {
            _diceRoll = -1;
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
                        color = PlayerColor.GREEN;
                        break;
                    case 1:
                        color = PlayerColor.RED;
                        break;
                    case 2:
                        color = PlayerColor.BLUE;
                        break;
                    case 3:
                        color = PlayerColor.YELLOW;
                        break;
                }


                if (i > nrOfHumans)
                {
                    _playerList[i] = new Player(false, color);
                    if (temp != null)
                    {
                        temp.nextP = _playerList[i];
                    }
                    
                }
                else
                {
                    _playerList[i] = new Player(true, color);
                    if (temp != null)
                    {
                        temp.nextP = _playerList[i];
                    }
                }

                temp = _playerList[i];

                if (i == nrOfPlayers)
                {
                    _playerList[0].nextP = _playerList[i];
                }
            }
            
            //create board
            _board = new Board(this);
           

            //handle who may start the game
<<<<<<< HEAD
           // handleTurn(firstRoll(_playerList));
=======
            //handleTurn(firstRoll(_playerList));
>>>>>>> 11538455217a5470337763a27fb8f657cc146f2f



        }

        private Player firstRoll(Player[] players)
        {

            Player first = null;
            int outOfComp = 0 , highest = 0;

            //Players first roll        
                foreach (Player p in players)
                {
                    if (p.isHuman)
                    {
                        while (p.startRoll == -1)
                        {
                            if (_diceRoll != -1)
                            {
                                p.startRoll = _diceRoll;
                                _diceRoll = -1;
                            }
                        }
                    }
                    else
                    {
                        p.startRoll = rollDice();
                    }

                    if (p.startRoll > highest)
                    {
                        highest = p.startRoll;
                    }
                 }//end first roll

                while (outOfComp != players.Length - 1)
                {
                    int newHigh = 0;

                    foreach (Player p in players)
                    {
                        if (p.startRoll != 0)
                        {
                            if (p.startRoll < highest)
                            {
                                p.startRoll = 0;
                                outOfComp++;
                            }
                            else
                            {
                                if (!p.isHuman)
                                {
                                    p.startRoll = rollDice();
                                }
                                else
                                {
                                    while (p.startRoll == -1)
                                    {
                                        if (_diceRoll != -1)
                                        {
                                            p.startRoll = _diceRoll;
                                            _diceRoll = -1;
                                        }
                                    }
                                }
                            }

                            if (p.startRoll > newHigh)
                            {
                                newHigh = p.startRoll;
                                first = p;
                            }
                        }
                    }
                    highest = newHigh;
                } // end while

                first.pawns[0].currentField = first.startingField;
                return first;
             }
                 


        //handles individual turns
        private void handleTurn(Player p)
        {
            bool turnFinished = false , stuck = false;
            _diceRoll = 0;
            _selected = null;

            //computerprep + check if not stuck
            if (!p.isHuman)
            {

                _diceRoll = rollDice();
                bool select = false;
                while (!select)
                {
                    foreach (Pawn pawn in p.pawns)
                    {

                        if (pawn.canMove(_diceRoll))
                        {
                            if (pawn.canHit)
                            {
                                _selected = pawn;
                                select = true;
                            }
                        }
                    }

                    if (_selected == null)
                    {
                        foreach (Pawn pawn in p.pawns)
                        {
                            if (pawn.canMove(_diceRoll))
                            {
                                _selected = pawn;
                                select = true;
                            }
                        }

                        if (_selected == null)
                        {
                            stuck = true;
                        }
                    }
                }
            }
            else
            {
                int moveablePawns = 0;
               
                while(_diceRoll == 0) {
                    //Wait for diceroll
                }

                foreach (Pawn pawn in p.pawns)
                {
                    if (pawn.canMove(_diceRoll))
                    {
                        moveablePawns++;
                    }                                   
                }
                if (moveablePawns == 0)
                {
                    stuck = true;
                }
            }
            
            //Actual move and stuck check
            if(!stuck){
                while (!turnFinished)
                {
                    if (_selected != null)
                    {
                        if (_selected.player == p)
                        {
                            if (_selected.canMove(_diceRoll))
                            {
                                _selected.move(_diceRoll);
                                turnFinished = true;
                            }
                        }
                    }
                }

                if (p.pawnsInGoal == 4)
                {
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
        public Pawn selected
        {
            get { return _selected; }
            set { _selected = selected; }
        }

        public Player[] playerList
        {
            get { return _playerList; }
        }

    }
}
