using MensErgerJeNiet.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensErgerJeNiet.Model
{
    public class Board
    {
        private Field _first, _last;
        private Spawn[] spawns = new Spawn[16];
        private int normalFields = 40, numberOfSpawns = 4, numberOfCurrentGoals = 0;
        private int player1 = 0, player2 = 1, player3 = 2, player4 = 3;
        private Player[] _playerList;
        private Player currentPlayer;
        private int createSpawnCounter = 1, createGoalCounter = 1;
        private Goal[] goals = new Goal[16];


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
            createWalkingPath(true);
        }



        public void createField(String[] field)
        {
            char[] charField = new char[field[3].Length];
            char[] charGGoal = new char[field[4].Length];
            char[] charRGoal = new char[field[5].Length];
            char[] charBGoal = new char[field[6].Length];
            char[] charYGoal = new char[field[7].Length];

            int[] greenPawns = null, redPawns = null, bluePawns = null, yellowPawns = null;
            int g = 0, r = 0, b = 0, y = 0;
            int[] pawnsAtGreenEnd = null, pawnsAtRedEnd = null, pawnsAtBlueEnd = null, pawnsAtYellowEnd = null;
            int gEnd = 0, rEnd = 0, bEnd = 0, yEnd = 0;

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
                        greenPawns[g] = i;
                        g++;
                        break;
                    case 'R':
                        redPawns[r] = i;
                        r++;
                        break;
                    case 'B':
                        bluePawns[b] = i;
                        b++;
                        break;
                    case 'Y':
                        yellowPawns[y] = i;
                        y++;
                        break;
                    default:
                        break;
                }
            }

            for (int i = 0; i < charGGoal.Length; i++)
            {
                if (charGGoal[i] == 'G')
                {
                    pawnsAtGreenEnd[gEnd] = i;
                    gEnd++;
                }
                if (charRGoal[i] == 'R')
                {
                    pawnsAtRedEnd[rEnd] = i;
                    rEnd++;
                }
                if (charBGoal[i] == 'B')
                {
                    pawnsAtBlueEnd[bEnd] = i;
                    bEnd++;
                } 
                if(charYGoal[i] == 'Y')
                {
                    pawnsAtYellowEnd[yEnd] = i;
                    yEnd++;
                }

            }
            //counted pawns of players: if pawncount < 4 set difference to spawn
            // create all the pawns on the spawns for players
            createSpawns(playerList[player1], (4 - g - gEnd));
            createSpawns(playerList[player2], (4 - r - rEnd));
            createSpawns(playerList[player3], (4 - b - bEnd));
            createSpawns(playerList[player4], (4 - y - yEnd));

            if (field[3].Length == 40 && field[4].Length == 4 && field[5].Length == 4 && field[5].Length == 4 && field[7].Length == 4)
                createWalkingPath(false);
            if (g > 0)
                getFieldFromPath("field" + greenPawns[g + 1]).pawn = new Pawn(playerList[player1], getFieldFromPath("field" + greenPawns[g + 1]));
            if (r > 0)
                getFieldFromPath("field" + greenPawns[r + 1]).pawn = new Pawn(playerList[player1], getFieldFromPath("field" + greenPawns[r + 1]));
            if (b > 0)
                getFieldFromPath("field" + greenPawns[b + 1]).pawn = new Pawn(playerList[player1], getFieldFromPath("field" + greenPawns[b + 1]));
            if (y > 0)
                getFieldFromPath("field" + greenPawns[y + 1]).pawn = new Pawn(playerList[player4], getFieldFromPath("field" + greenPawns[y + 1]));
            if (gEnd > 0)
                getFieldFromPath("p1end" + greenPawns[gEnd + 1]).pawn = new Pawn(playerList[player1], getFieldFromPath("p1end" + greenPawns[gEnd + 1]));
            if (rEnd > 0)
                getFieldFromPath("p2end" + greenPawns[rEnd + 1]).pawn = new Pawn(playerList[player1], getFieldFromPath("p2end" + greenPawns[rEnd + 1]));
            if (bEnd > 0)
                getFieldFromPath("p3end" + greenPawns[bEnd + 1]).pawn = new Pawn(playerList[player1], getFieldFromPath("p3end" + greenPawns[bEnd + 1]));
            if (yEnd > 0)
                getFieldFromPath("p4end" + greenPawns[yEnd + 1]).pawn = new Pawn(playerList[player4], getFieldFromPath("p4end" + greenPawns[yEnd + 1]));





        }

        private Boolean isEmpty()
        {
            return first == null;
        }

        private void createWalkingPath(Boolean newGame)
        {
            Goal temp;
            int i = 0;
            while (i < normalFields)
            {
                Field newField = new Field();
                newField.fieldCode = "field" + (i + 1);
                if (isEmpty())
                {
                    first = newField;
                }
                else
                {
                    last.nextF = newField;
                }
                newField.previousF = last;
                last = newField;
                
                switch (i)
                {
                    case 8:
                        currentPlayer = playerList[player2];
                        if(newGame)
                            createSpawns(currentPlayer, 4);
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
                        if (playerList.Length >= 3)
                        {
                            currentPlayer = playerList[player3];
                            if (newGame)
                                createSpawns(currentPlayer, 4);
                            temp = createGoals(currentPlayer);
                            newField.switchF = temp;
                            temp.previousF = newField;
                            temp = null;
                        }
                        break;
                    case 19:
                        if(spawns[4] == null)
                        {
                            spawns[4] = new Spawn("No Current Player");
                            spawns[5] = new Spawn("No Current Player");
                            spawns[6] = new Spawn("No Current Player");
                            spawns[7] = new Spawn("No Current Player");
                            createSpawnCounter++;
                        }
                        if (playerList.Length >= 4)
                        {
                            spawns[4].nextF = newField;
                            spawns[5].nextF = newField;
                            spawns[6].nextF = newField;
                            spawns[7].nextF = newField;
                        }
                        break;
                    case 28:
                        if (playerList.Length >= 4)
                        {
                            currentPlayer = playerList[player4];
                            if (newGame)
                                createSpawns(currentPlayer, 4);
                            temp = createGoals(currentPlayer);
                            newField.switchF = temp;
                            temp.previousF = newField;
                            temp = null;
                        }
                        break;
                    case 29:
                        if (spawns[8] == null)
                        {
                            spawns[8] = new Spawn("No Current Player");
                            spawns[9] = new Spawn("No Current Player");
                            spawns[10] = new Spawn("No Current Player");
                            spawns[11] = new Spawn("No Current Player");
                            createSpawnCounter++;
                        }
                        if (playerList.Length >= 4)
                        {
                            spawns[8].nextF = newField;
                            spawns[9].nextF = newField;
                            spawns[10].nextF = newField;
                            spawns[11].nextF = newField;
                        }
                        break;
                    case 38:
                        currentPlayer = playerList[player1];
                        if (newGame)
                            createSpawns(currentPlayer, 4);
                        temp = createGoals(currentPlayer);
                        newField.switchF = temp;
                        temp.previousF = newField;
                        temp = null;
                        break;
                    case 39:
                        spawns[12].nextF = newField;
                        spawns[13].nextF = newField;
                        spawns[14].nextF = newField;
                        spawns[15].nextF = newField;
                        newField.nextF = first;
                        first.previousF = newField;
                        break;
                }
                i++;
            }
        }

        public Field getFieldFromPath(String fieldcode)
        {
            Field current = first;
            int i = 0;
            while (current.fieldCode != fieldcode && i < 50)
            {
                current = current.nextF;
                if (current == null || i >= 49)
                {
                    return null;
                }
                i++;
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


        private void createSpawns(Player p, int numberOfPawns)
        {
            int i, pNR = 0;
            int g = 1;
            switch (createSpawnCounter)
            {
                case 1:
                    i = 0;
                    numberOfSpawns = 4;
                    g = 1;
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

            switch (p.color)
            {
                case PlayerColor.GREEN:
                    pNR = 1;
                    break;
                case PlayerColor.RED:
                    pNR = 2;
                    break;
                case PlayerColor.BLUE:
                    pNR = 3;
                    break;
                case PlayerColor.YELLOW:
                    pNR = 4;
                    break;
            }
            while (i < numberOfSpawns)
            {
                Spawn newSpawn = new Spawn("p" + pNR + "spawn" + g);
                if(g <= numberOfPawns)
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
            goals[numberOfCurrentGoals] = newGoal1;
            numberOfCurrentGoals++;
            goals[numberOfCurrentGoals] = newGoal2;
            numberOfCurrentGoals++;
            goals[numberOfCurrentGoals] = newGoal3;
            numberOfCurrentGoals++;
            goals[numberOfCurrentGoals] = newGoal4;
            numberOfCurrentGoals++;
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

        public Goal[] Goals
        {
            get { return goals; }
        }

    }
}
