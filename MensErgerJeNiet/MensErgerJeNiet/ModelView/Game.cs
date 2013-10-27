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

        private void addPawn()
        {
            Field temp = board.first.nextF.nextF.nextF;
            temp.pawn = new Pawn(playerList[0], temp);
            sendFieldCode(temp);
        }

        public void startFromFile(String[] file)
        {
            //TEST STRINGARRAY
            string[] strings = new string[4];
            strings[0] = "NrPlayers=4";
            strings[1] = "NrHumans=2";
            strings[2] = "Turn=RED";
            strings[3] = "oooooooRooooo1ooooGooooooRooo2ooooooBoooooo3ooooooYoooooo4"; //incomplete
            //TEST STRINGARRAY


            //Strings == files later on
            int NrofPlayers = Convert.ToInt32(strings[0].Substring(strings[0].IndexOf('='), 1));
            int NrofHumans = Convert.ToInt32(strings[1].Substring(strings[0].IndexOf('='), 1));

            createPlayers(NrofPlayers, NrofHumans);

            foreach (Player p in _playerList)
            {
                if (p.color.Equals(strings[3].Substring(strings[3].IndexOf('='), 1)))
                {
                    playersTurn = p;
                }

                //create spawns for players? (nu we toch in een loop zitten)
                //evt ook goals?

            }

            board.createField(strings);


        }

        public void startGame(int players, int humans)
        {
            createPlayers(players, humans);
            _board = new Board(_playerList);
            firstRoll(_playerList);
            //addPawn();
        }


        //Starts up the game
        public void createPlayers(int nrOfPlayers, int nrOfHumans)
        {
            _playerList = new Player[nrOfPlayers];
            Player temp = null;

            Console.WriteLine(nrOfPlayers + " " + nrOfHumans);
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


                if (i >= nrOfHumans)
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
            }

            _playerList[_playerList.Length -1].nextP = _playerList[0];
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
         

            Console.WriteLine("First: " + playersTurn.color);
            //place first pawn on board
            //playersTurn.pawns[0].currentField = playersTurn.pawns[0].currentField.nextF;
            playersTurn.pawns[0].move(1);

            //Won't draw

            sendFieldCode(_playersTurn.pawns[0].currentField);
            
            
            if (!_playersTurn.isHuman)
            {
                
                computerPrep(_playersTurn);
            }
        }



        //Checks if ANY moves are possible
        public bool canMakeMove(Player p)
        {
            Console.WriteLine("Checking for move player: " + p.color + " roll: " + _diceRoll);

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
              
                _playersTurn = _playersTurn.nextP;

                Console.WriteLine(_playersTurn.color + " " + playersTurn.isHuman);
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

                    select = true;
                    Console.WriteLine("Selected: " + _selected);
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
            
            //null soms
            Console.WriteLine("SELECTED: " + _selected);
            sendFieldCode(_selected.currentField);

            _selected = null;
            _diceRoll = 0;

            main.rollButton.IsEnabled = true;

            //NULLPOINTER
           
        }
        


        //Used by MainWindows EventHandler (click on dice)
        //edit2: implemented in mainwindow eventhandler
        public int rollDice()
        {
            return _random.Next(1, 7);
        }

        private void sendFieldCode(Field f)
        {
            Field temp = f;
            if (temp.pawn != null && temp.pawn.player.color == PlayerColor.GREEN)
            {
                Console.WriteLine("hij komt hier");
                main.fillField(temp.fieldCode, Colors.LawnGreen);
            }
            else if (temp.pawn != null && temp.pawn.player.color == PlayerColor.RED)
            {
                main.fillField(temp.fieldCode, Colors.Red);
                Console.WriteLine("hij komt hier2");
            }
            else if (temp.pawn != null && temp.pawn.player.color == PlayerColor.BLUE)
            {
                main.fillField(temp.fieldCode, Colors.Blue);
                Console.WriteLine("hij komt hier3");
            }
            else if (temp.pawn != null && temp.pawn.player.color == PlayerColor.YELLOW)
            {
                main.fillField(temp.fieldCode, Colors.Yellow);
                Console.WriteLine("hij komt hier4");
            }
            else if (temp.pawn == null)
            {
                main.fillField(temp.fieldCode, Colors.White);
                Console.WriteLine("witte ding");
            }
            else
            {
                main.fillField(temp.fieldCode, Colors.White);
                Console.WriteLine("witte v2");
            }
        }

        public void recieveClickedEllipse(string p)
        {
            Field current = null;
            
            current = board.getFieldFromPath(p);
            if (current == null)
            {
                int g = 0;
                while (g < 16)
                {
                    if (board.Spawns[g].fieldCode == p)
                    {
                        current = board.Spawns[g];
                        break;
                    }
                    if (board.Goals[g].fieldCode == p)
                    {
                        current = board.Goals[g];
                        break;
                    }
                    g++;
                }
            }
            if (current != null)
            {
                Console.WriteLine(current.fieldCode);
                Console.WriteLine("Next: " + current.nextF.fieldCode);
                if (current.pawn != null)
                {
                    if (current.pawn.player == playersTurn && current.pawn.canMove(_diceRoll))
                    {
                        _selected = current.pawn;
                        handleTurn(playersTurn);
                    }
                }
            }
        }

        private void setPlayerTurn()
        {
            main.changePlayerTurn(playersTurn.color);
        }

        //properties
        public int diceRoll
        {
            get { return _diceRoll; }
            set{ _diceRoll = value;}
        }

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
