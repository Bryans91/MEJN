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
        private int normalFields = 40;
        private Game theGame;


        public Board(Game g)
        {
            theGame = g;
            first = null;
            last = null;
            createField();


        }

        private void createField()
        {
            createSpawns();
            createWalkingPath();
        }

        private void createField(String pathname)
        {
            throw new NotImplementedException();
        }

        private void createWalkingPath()
        {
            Goal temp;
            int i = 0, s = 0;
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
                        temp = createGoals(theGame.playerList[0]);
                        newField.switchF = temp;
                        temp.previousF = newField;
                        temp = null;
                        break;
                    case 9:
                        spawns[1].nextF = newField;
                        spawns[2].nextF = newField;
                        spawns[3].nextF = newField;
                        spawns[4].nextF = newField;
                        break;
                    case 18:
                        temp = createGoals(theGame.playerList[1]);
                        newField.switchF = temp;
                        temp.previousF = newField;
                        temp = null;
                        break;
                    case 19:
                        spawns[5].nextF = newField;
                        spawns[6].nextF = newField;
                        spawns[7].nextF = newField;
                        spawns[8].nextF = newField;
                    case 28:
                        if (theGame.playerList.length > 3)
                        {
                            temp = createGoals(theGame.playerList[2]);
                            newField.switchF = temp;
                            temp.previousF = newField;
                            temp = null;
                        }
                        break;
                    case 29:
                        spawns[9].nextF = newField;
                        spawns[10].nextF = newField;
                        spawns[11].nextF = newField;
                        spawns[12].nextF = newField;
                    case 38:
                        if (theGame.playerList.length > 4)
                        {
                            temp = createGoals(theGame.playerList[3]);
                            newField.switchF = temp;
                            temp.previousF = newField;
                            temp = null;
                        }
                    case 39:
                        newField.nextF = last;
                        spawns[13].nextF = newField;
                        spawns[14].nextF = newField;
                        spawns[15].nextF = newField;
                        spawns[16].nextF = newField;
                        break;
                }
                i++;
            }
        }


        private void createSpawns()
        {
            int i = 0;
            while (i < 16)
            {
                Spawn newSpawn = new Spawn();
                spawns[i] = newSpawn;
            }
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
