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
        private Field first, last;
        private Spawn[] spawns = new Spawn[16];
        private int normalFields = 40, numberOfSpawns = 4;
        private int player1 = 0, player2 = 1, player3 = 2, player4 = 3;
        private Game theGame;
        private Player currentPlayer;
        private int createSpawnCounter = 0;


        public Board(Game g)
        {
            theGame = g;
            first = null;
            last = null;
            createField();
        }

        private void createField()
        {
            //in case we want to change it to getting the path from .txt files we got a 2nd method
            createWalkingPath();
        }

        private void createField(String filename)
        {
            throw new NotImplementedException();
        }

        private void createWalkingPath()
        {
            Goal temp;
            int i = 0;
            while (i < normalFields)
            {
                Field newField = new Field();
                newField.nextF = first;
                if (first != null)
                {
                    first.previousF = newField;
                }
                first = newField;

                switch (i)
                {
                    case 0:
                        last = newField;
                        break;
                    case 8:
                        currentPlayer = theGame.playerList[player1];
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
                        currentPlayer = theGame.playerList[player2];
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
                        if (theGame.playerList.Length > 2)
                        {
                            currentPlayer = theGame.playerList[player3];
                            createSpawns(currentPlayer);
                            temp = createGoals(currentPlayer);
                            newField.switchF = temp;
                            temp.previousF = newField;
                            temp = null;
                        }
                        break;
                    case 29:
                        spawns[8].nextF = newField;
                        spawns[9].nextF = newField;
                        spawns[10].nextF = newField;
                        spawns[11].nextF = newField;
                        break;
                    case 38:
                        if (theGame.playerList.Length > 3)
                        {
                            currentPlayer = theGame.playerList[player4];
                            createSpawns(currentPlayer);
                            temp = createGoals(currentPlayer);
                            newField.switchF = temp;
                            temp.previousF = newField;
                            temp = null;
                        }
                        break;
                    case 39:
                        newField.nextF = last;
                        spawns[12].nextF = newField;
                        spawns[13].nextF = newField;
                        spawns[14].nextF = newField;
                        spawns[15].nextF = newField;
                        break;
                }
                i++;
            }
        }


        private void createSpawns(Player p)
        {
            int i;
            switch (createSpawnCounter)
            {
                case 0:
                    i = 0;
                    break;
                case 1:
                    i = 4;
                    numberOfSpawns = 8;
                    break;
                case 2:
                    i = 8;
                    numberOfSpawns = 12;
                    break;
                case 3:
                    i = 12;
                    numberOfSpawns = 16;
                    break;
                default:
                    i = 0;
                    break;
                
            }
            while (i < numberOfSpawns)
            {
                Spawn newSpawn = new Spawn();
                newSpawn.pawn = new Pawn(p, newSpawn);
                spawns[i] = newSpawn;
                i++;
            }
            createSpawnCounter++;
        }

        private Goal createGoals(Player p)
        {
            Goal newGoal1 = new Goal(p);
            Goal newGoal2 = new Goal(p);
            Goal newGoal3 = new Goal(p);
            Goal newGoal4 = new Goal(p);
            newGoal1.nextF = newGoal2;
            newGoal2.nextF = newGoal3;
            newGoal3.nextF = newGoal2;
            newGoal4.nextF = null;//making sure it's null
            newGoal2.previousF = newGoal1;
            newGoal3.previousF = newGoal2;
            newGoal4.previousF = newGoal3;
            return newGoal1;
        }

    }
}
