using MensErgerJeNiet.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensErgerJeNiet.Model
{
    class Board
    {
        private Field _first, _last;
        private Spawn[] spawns = new Spawn[16];
        private int normalFields = 40, numberOfSpawns = 4;
        private int player1 = 0, player2 = 1, player3 = 2, player4 = 3;
        private Player[] _playerList;
        private Player currentPlayer;
        private int createSpawnCounter = 1, createGoalCounter = 1;


        public Board(Player[] plist)
        {
            playerList = plist;
            first = null;
            last = null;
            createField();
        }

        public void createField()
        {
            //in case we want to change it to getting the path from .txt files we got a 2nd method
            createWalkingPath();
        }

        public void createField(String[] field)
        {
            char[] charField = new char[field[3].Length];
            char[] charGGoal = new char[field[4].Length];
            char[] charRGoal = new char[field[5].Length];
            char[] charBGoal = new char[field[6].Length];
            char[] charYGoal = new char[field[7].Length];

            //charfield is normal field
            charField = field[3].ToCharArray();
            charGGoal = field[4].ToCharArray();
            charRGoal = field[5].ToCharArray();
            charBGoal = field[6].ToCharArray();
            charYGoal = field[7].ToCharArray();
            
            
            for (int i = 0; i < charField.Length; i++)
            {
               

                switch (charField[i])
                {
                        //count pawns of players
                    case 'G':
                        //field with Green pawn
                        break;
                    case 'R':
                        //field with red pawn
                        break;
                    case 'B':
                        //field with blue pawn
                        break;
                    case 'Y':
                        //field with yellow pawn
                        break;
                    default:
                        //normal field
                        break;
          
                }

                //Switches zetten en goals maken
                switch (i)
                {
                        //set switches at each case:
                    case 9:
                        //charRGoals
                        //connect to normal field
                        //create goalfields
                        //create pawns if any
                        //set pawncounter for player
                        break;
                    case 19:
                        //charBGoals
                        //connect to normal field
                        //create goalfields
                        //create pawns if any
                        //set pawncounter for player
                        break;
                    case 29:
                        //charYGoals
                        //connect to normal field
                        //create goalfields
                        //create pawns if any
                        //set pawncounter for player                      
                        break;
                    case 39:
                        //charGGoals
                        //connect to normal field
                        //create goalfields
                        //create pawns if any
                        //set pawncounter for player
                        break;
                   
                   //set starfields
                    case 0:
                        //set this field to startfield of player
                        break;
                    case 10:
                        //set this field to startfield of player
                        break;
                    case 20:
                        //set this field to startfield of player
                        break;
                    case 30:
                        //set this field to startfield of player
                        break;

                    default:
                        //donothing
                        break;
                }

                //Fields koppelen aan elkaar


            }

            //counted pawns of players: if pawncount < 4 set difference to spawn (spawn is already created)


          

        }

        private Boolean isEmpty()
        {
            return first == null;
        }

        private void createWalkingPath()
        {
            Goal temp;
            int i = 0;
            while (i < normalFields)
            {
                Field newField = new Field();
                newField.fieldCode = "field" + (i + 1);
                if (isEmpty())
                {
                    last = newField;
                }
                else
                {
                    first.previousF = newField;
                }
                newField.nextF = first;
                first = newField;
                
                switch (i)
                {
                    case 8:
                        currentPlayer = playerList[player1];
                        createSpawns(currentPlayer);
                        temp = createGoals(currentPlayer);
                        newField.switchF = temp;
                        temp.previousF = newField;
                        temp = null;
                        break;
                    case 9:
                        //set spawns to direct to the according startingfield
                        spawns[0].nextF = newField;
                        spawns[1].nextF = newField;
                        spawns[2].nextF = newField;
                        spawns[3].nextF = newField;
                        break;
                    case 18:
                        currentPlayer = playerList[player2];
                        createSpawns(currentPlayer);
                        temp = createGoals(currentPlayer);
                        newField.switchF = temp;
                        temp.previousF = newField;
                        temp = null;
                        break;
                    case 19:
                        spawns[4].nextF = newField;
                        spawns[5].nextF = newField;
                        spawns[6].nextF = newField;
                        spawns[7].nextF = newField;
                        break;
                    case 28:
                        if (playerList.Length >= 3)
                        {
                            currentPlayer = playerList[player3];
                            createSpawns(currentPlayer);
                            temp = createGoals(currentPlayer);
                            newField.switchF = temp;
                            temp.previousF = newField;
                            temp = null;
                        }
                        break;
                    case 29:
                        if (playerList.Length >= 3)
                        {
                            spawns[8].nextF = newField;
                            spawns[9].nextF = newField;
                            spawns[10].nextF = newField;
                            spawns[11].nextF = newField;
                        }
                        break;
                    case 38:
                        if (playerList.Length >= 4)
                        {
                            currentPlayer = playerList[player4];
                            createSpawns(currentPlayer);
                            temp = createGoals(currentPlayer);
                            newField.switchF = temp;
                            temp.previousF = newField;
                            temp = null;
                        }
                        break;
                    case 39:
                        if (playerList.Length >= 4)
                        {
                            spawns[12].nextF = newField;
                            spawns[13].nextF = newField;
                            spawns[14].nextF = newField;
                            spawns[15].nextF = newField;
                        }
                        break;
                }
                i++;
            }
        }

        public Field getFieldFromPath(String fieldcode)
        {
            Field current = first;
            while (current.fieldCode != fieldcode)
            {
                current = current.nextF;
                if (current == null)
                {
                    return null;
                }
            }
            if (current == first)
                first = current.nextF;
            else
            {
                current.previousF.nextF = current.nextF;
            }
            if (current == last)
                last = current.previousF;
            else
                current.nextF.previousF = current.previousF;
            return current;

        }


        private void createSpawns(Player p)
        {
            int i;
            int g = 1;
            switch (createSpawnCounter)
            {
                case 1:
                    i = 0;
                    numberOfSpawns = 4;
                    break;
                case 2:
                    i = 4;
                    numberOfSpawns = 8;
                    g = 1;
                    break;
                case 3:
                    i = 8;
                    numberOfSpawns = 12;
                    g = 1;
                    break;
                case 4:
                    i = 12;
                    numberOfSpawns = 16;
                    g = 1;
                    break;
                default:
                    i = 0;
                    break;
                
            }
            while (i < numberOfSpawns)
            {
                Spawn newSpawn = new Spawn("p" + createSpawnCounter + "spawn" + g);
                newSpawn.pawn = new Pawn(p, newSpawn);
                spawns[i] = newSpawn;
                g++;
                i++;
            }
            createSpawnCounter++;
        }

        private Goal createGoals(Player p)
        {
            Goal newGoal1 = new Goal(p, "p" + createGoalCounter + "end1");
            Goal newGoal2 = new Goal(p, "p" + createGoalCounter + "end2");
            Goal newGoal3 = new Goal(p, "p" + createGoalCounter + "end3");
            Goal newGoal4 = new Goal(p, "p" + createGoalCounter + "end4");
            newGoal1.nextF = newGoal2;
            newGoal2.nextF = newGoal3;
            newGoal3.nextF = newGoal2;
            newGoal4.nextF = null;//making sure it's null
            newGoal2.previousF = newGoal1;
            newGoal3.previousF = newGoal2;
            newGoal4.previousF = newGoal3;
            createGoalCounter++;
            return newGoal1;
        }

        private Player[] playerList
        {
            get { return _playerList; }
            set { _playerList = value; }
        }

        public Spawn[] Spawns
        {
            get { return spawns; }
        }

        public Field first
        {
            get { return _first; }
            private set { _first = value; }
        }

        public Field last
        {
            get { return _last; }
            private set { _last = value; }
        }

    }
}
