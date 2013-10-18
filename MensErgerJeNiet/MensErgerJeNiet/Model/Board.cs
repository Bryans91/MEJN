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
            int i = 0, s = 0;
            while (i < normalFields)
            {
                Field newField = new Field();
                newField.nextF = first;
                first = newField;

                switch (i)
                {
                    case 0:
                        last = newField;
                        break;
                    case 8:
                        Goal newGoal = new Goal(/*Game.getPlayer?*/);
                        newField.switchF = newGoal;
                        break;
                    case 9:
                        spawns[1].nextF = newField;
                        spawns[2].nextF = newField;
                        spawns[3].nextF = newField;
                        spawns[4].nextF = newField;
                        break;
                    case 18:
                        Goal newGoal1 = new Goal(/*Game.getPlayer?*/);
                        newField.switchF = newGoal1;
                        break;
                    case 19:
                        spawns[5].nextF = newField;
                        spawns[6].nextF = newField;
                        spawns[7].nextF = newField;
                        spawns[8].nextF = newField;
                    case 28:
                        Goal newGoal2 = new Goal(/*Game.getPlayer?*/);
                        newField.switchF = newGoal2;
                        break;
                    case 29:
                        spawns[9].nextF = newField;
                        spawns[10].nextF = newField;
                        spawns[11].nextF = newField;
                        spawns[12].nextF = newField;
                    case 38:
                        Goal newGoal3 = new Goal(/*Game.getPlayer?*/);
                        newField.switchF = newGoal3;
                        break;
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

    }
}
