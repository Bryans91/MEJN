using MensErgerJeNiet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MensErgerJeNiet.ModelView
{
    public class Game
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

        public void cheatPlayer(int playernumber)
        {
            playersTurn = playerList[playernumber];
            main.changePlayerTurn(playersTurn.color);
        }
        public void cheatThrow(int dicenumber)
        {
            diceRoll = dicenumber;
        }

        public void startFromFile(String[] file)
        {
            _board = null;

            //TEST STRINGARRAY
            string[] strings = new string[8];
            strings = file;

            //Strings == files later on
            int NrofPlayers = 0;
            Int32.TryParse((strings[0].Substring(strings[0].IndexOf('=')+1, 1)), out NrofPlayers);
            int NrofHumans = 0;
            Int32.TryParse((strings[1].Substring(strings[1].IndexOf('=')+1, 1)), out NrofHumans);
            Console.WriteLine(NrofPlayers + " " + NrofHumans);

            createPlayers(NrofPlayers, NrofHumans);

            foreach (Player p in _playerList)
            {
                if(p != null)
                    if (p.color.Equals(strings[3].Substring(strings[3].IndexOf('=')+1, 1)))
                        playersTurn = p;

                //create spawns for players? (nu we toch in een loop zitten)
                //evt ook goals?

            }

            _board = new Board(playerList, this);
            board.newCreateField(strings , _playerList);

        }

        public void startGame(string[] strings , int nrP , int nrH)
        {
            _board = new Board(playerList, this);
            createPlayers(nrP, nrH);
            board.newCreateField(strings, playerList);

           // createPlayers(stri, humans);
           // _board = new Board(_playerList);
            firstRoll(_playerList);
            //addPawn();
        }


        //Starts up the game
        public void createPlayers(int nrOfPlayers, int nrOfHumans)
        {
            _playerList = new Player[4];
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

            _playerList[nrOfPlayers -1].nextP = _playerList[0];
        }

        private void firstRoll(Player[] players)
        {
            Player first = null;
            int highest = 0;

            foreach (Player p in players)
            {
                if (p != null)
                {
                    int temp = rollDice();
                    p.startRoll = temp;

                    if (p.startRoll > highest)
                    {
                        highest = p.startRoll;
                        first = p;
                    }
                }
            }
            
            _playersTurn = first;
            
            

            Console.WriteLine("First: " + playersTurn.color);
            //place first pawn on board
            //playersTurn.pawns[0].currentField = playersTurn.pawns[0].currentField.nextF;
            Field temporary = playersTurn.pawns[0].currentField;

            playersTurn.pawns[0].move(1 , this);

            //Won't draw
            sendFieldCode(temporary);
            sendFieldCode(_playersTurn.pawns[0].currentField);
            
            
            if (!_playersTurn.isHuman)
            {
                
                computerPrep(_playersTurn);
            }
            main.changePlayerTurn(playersTurn.color);
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
                _selected = null;
                main.rollButton.IsEnabled = false;
            }
            main.changePlayerTurn(playersTurn.color);
        }


        //Prepares a turn for the computer
        public void computerPrep(Player p)
        {

            Pawn temp = null;
            main.changeDice(rollDice());
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

                                if (pawn.currentField.GetType() == typeof(Goal))
                                {
                                    temp = pawn;
                                }
                                else
                                {
                                    _selected = pawn;
                                }
                            }

                            if (_selected == null)
                            {
                                if (temp != null)
                                {
                                    _selected = temp;
                                }
                                
                            }

                            
                            select = true;                         
                    

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
                    _selected = null;
                    computerPrep(_playersTurn);
                }
                else
                {
                    main.rollButton.IsEnabled = true;
                   
                }

                _selected = null;
                _diceRoll = 0;
            }
            main.changePlayerTurn(playersTurn.color);
        }


        public void handleTurn(Player p)
        {
            
            Field start = _selected.currentField;
            _selected.move(_diceRoll , this);
            if (diceRoll == 0)
                return;

            if (p.pawnsInGoal == 4)
            {
                main.showEndMessage();
                // end the game here
            }
            else if (_diceRoll == 6)
            {
                _playersTurn = p;
                _diceRoll = -0;
            }
            else
            {
                _playersTurn = p.nextP;
            }

            if (!_playersTurn.isHuman)
            {
                sendFieldCode(start);
                sendFieldCode(_selected.currentField);
                _selected = null;
                _diceRoll = 0;
                computerPrep(_playersTurn);
            }
            else
            {
                //null soms
                

                sendFieldCode(start);
                //sendFieldCode(_selected.currentField);
                
                _selected = null;
                _diceRoll = 0;

                main.rollButton.IsEnabled = true;

            }

            main.changePlayerTurn(playersTurn.color);
        }
        


        //Used by MainWindows EventHandler (click on dice)
        //edit2: implemented in mainwindow eventhandler
        public int rollDice()
        {
            return _random.Next(1, 7);
        }

        public void sendFieldCode(Field f)
        {
            
            Field temp = f;
            if (temp.pawn != null && temp.pawn.player.color == PlayerColor.GREEN)
            {
                main.fillField(temp.fieldCode, Colors.LawnGreen);
            }
            else if (temp.pawn != null && temp.pawn.player.color == PlayerColor.RED)
            {
                main.fillField(temp.fieldCode, Colors.Red);
            }
            else if (temp.pawn != null && temp.pawn.player.color == PlayerColor.BLUE)
            {
                main.fillField(temp.fieldCode, Colors.Blue);
            }
            else if (temp.pawn != null && temp.pawn.player.color == PlayerColor.YELLOW)
            {
                main.fillField(temp.fieldCode, Colors.Yellow);
            }
            else if ((temp.pawn == null && temp.fieldCode.Equals("field0")) || (temp.pawn == null && temp.fieldCode.StartsWith("goalgreen")) || (temp.pawn == null && temp.fieldCode.StartsWith("pgreen")))
            {
                main.fillField(temp.fieldCode, Colors.Green);
            }
            else if ((temp.pawn == null && temp.fieldCode.Equals("field10")) || (temp.pawn == null && temp.fieldCode.StartsWith("goalred")) || (temp.pawn == null && temp.fieldCode.StartsWith("pred")))
            {
                main.fillField(temp.fieldCode, Colors.DarkRed);
            }
            else if ((temp.pawn == null && temp.fieldCode.Equals("field20")) || (temp.pawn == null && temp.fieldCode.StartsWith("goalblue")) || (temp.pawn == null && temp.fieldCode.StartsWith("pblue")))
            {
                main.fillField(temp.fieldCode, Colors.DarkBlue);
            }
            else if ((temp.pawn == null && temp.fieldCode.Equals("field30")) || (temp.pawn == null && temp.fieldCode.StartsWith("goalyellow")) || (temp.pawn == null && temp.fieldCode.StartsWith("pyellow")))
            {
                main.fillField(temp.fieldCode, Colors.Goldenrod);
            }
            else
            {
                main.fillField(temp.fieldCode, Colors.White);
            }
        }

        public void recieveClickedEllipse(string p)
        {
            if (_diceRoll != 0)
            {

                Field current = null;

                current = board.getFieldFromPath(p);

                if (current != null)
                {
                    Console.WriteLine(current.fieldCode);
                    if (current.nextF != null)

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
        }
        public void colorSpawns()
        {
            main.colorEllipses(playerList);
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

        public MainWindow Main
        {
            get { return main; }
        }

    }
}
