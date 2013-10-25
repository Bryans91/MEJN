using MensErgerJeNiet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MensErgerJeNiet.ModelView
{
    class Game
    {

        private int _diceRoll;
        private Random _random;
        private Board _board;
        private Player[] _playerList;
        private Player _playersTurn;
        private Pawn _selected;
        private MainWindow main;


        //Constructor: nr of fields evt af te leiden van nr of players? (aantal fields x spelers) of fixed aantal
        public Game(MainWindow mainwindow)
        {
            _diceRoll = -1;
            _random = new Random();
            main = mainwindow;
        }

        //Starts up the game
        public void startGame(int nrOfPlayers, int nrOfHumans)
        {
            _playerList = new Player[nrOfPlayers];
            Player temp = null;

            //add players to player list & sets human or computer player
            for (int i = 0; i < nrOfPlayers; i++)
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
            _board = new Board(_playerList);


            //handle who may start the game
            firstRoll(_playerList);


        }

        private void firstRoll(Player[] players)
        {
            Player first = null;
            int highest = 0;

            foreach (Player p in players)
            {
                int temp = rollDice();
                p.startRoll = temp;

                if (p.startRoll > highest)
                {
                    highest = p.startRoll;
                    first = p;
                }
            }
            
            _playersTurn = first;

            if (!_playersTurn.isHuman)
            {
                computerPrep(_playersTurn);
            }
        }



        //Checks if ANY moves are possible
        public bool canMakeMove(Player p)
        {
            int moveablePawns = 0;
            bool stuck = true;

            foreach (Pawn pawn in p.pawns)
            {
                if (pawn.canMove(_diceRoll))
                {
                    moveablePawns++;
                }
            }

            if (moveablePawns == 0)
            {
                stuck = false;
            }

            return stuck;
        }

        public void playerPrep(Player p)
        {
            if (!canMakeMove(p))
            {
                _playersTurn = p.nextP;

                if (!_playersTurn.isHuman)
                {
                    computerPrep(_playersTurn);
                }

                _diceRoll = 0;
            }
            else
            {
                main.rollButton.IsEnabled = false;
            }

        }


        //Prepares a turn for the computer
        public void computerPrep(Player p)
        {

            _diceRoll = rollDice();
            bool select = false;


            if (canMakeMove(p))
            {

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
                                sendFieldCode(_selected.currentField);
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
                                sendFieldCode(_selected.currentField);
                            }
                        }
                    }
                }

                handleTurn(p);

            }  // end if
            else
            {
                //computer cant move, next players turn
                _playersTurn = p.nextP;

                if (!_playersTurn.isHuman)
                {
                    computerPrep(_playersTurn);
                }

                _diceRoll = 0;
            }
        }


        public void handleTurn(Player p)
        {

            _selected.move(_diceRoll);


            if (p.pawnsInGoal == 4)
            {
                main.showEndMessage();
                // end the game here
            }
            else if (_diceRoll == 6)
            {
                _playersTurn = p;
            }
            else
            {
                _playersTurn = p.nextP;
            }

            if (!_playersTurn.isHuman)
            {
                computerPrep(_playersTurn);
            }

            _selected = null;
            _diceRoll = 0;

            main.rollButton.IsEnabled = true;
            sendFieldCode(_selected.currentField);
        }
        


        //Used by MainWindows EventHandler (click on dice)
        //edit2: implemented in mainwindow eventhandler
        public int rollDice()
        {
            return _random.Next(1, 7);
        }

        private void sendFieldCode(Field f)
        {
            if (f.pawn != null && f.pawn.player.color == PlayerColor.GREEN)
            {
                main.fillField(f.fieldCode, Colors.LawnGreen);
            }
            else if (f.pawn != null && f.pawn.player.color == PlayerColor.RED)
            {
                main.fillField(f.fieldCode, Colors.Red);
            }
            else if (f.pawn != null && f.pawn.player.color == PlayerColor.BLUE)
            {
                main.fillField(f.fieldCode, Colors.Blue);
            }
            else if (f.pawn != null && f.pawn.player.color == PlayerColor.YELLOW)
            {
                main.fillField(f.fieldCode, Colors.Yellow);
            }
            else if (f.pawn == null)
            {
                main.fillField(f.fieldCode, Colors.White);
            }
            else
            {
                main.fillField(f.fieldCode, Colors.White);
            }
        }

        public void recieveClickedEllipse(string p)
        {
            Field current = board.first;
            Field temp = null;
            int i = 0, g = 0;

            while (i < 50)
            {
                Console.WriteLine(current.fieldCode);
                Console.WriteLine(current.nextF.fieldCode);
                if (current.fieldCode != p)
                {
                    if (current.switchF != null)
                    {
                        temp = current.nextF;
                        current = current.switchF;
                    }
                    if (current.nextF == null)
                    {
                        current = temp;
                    }
                    //else
                    //{
                        current = current.nextF;
                    //}
                    i++;
                }
                
            }
            while (g < board.Spawns.Length)
            {
                if (board.Spawns[g].fieldCode == p)
                {
                    current = board.Spawns[g];
                    break;
                }
                g++;
            }
            if (current.pawn != null)
            {
                if (current.pawn.player == playersTurn && current.pawn.canMove(_diceRoll))
                {
                    _selected = current.pawn;
                    handleTurn(playersTurn);
                }
            }
        }

        private void setPlayerTurn()
        {
            main.changePlayerTurn(playersTurn.color);
        }

        //properties

        public Player playersTurn
        {
            get { return _playersTurn; }
            set { _playersTurn = value; }
        }
        public Pawn selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        public Board board
        {
            get { return _board; }
        }

        public Player[] playerList
        {
            get { return _playerList; }
        }
    }
}
