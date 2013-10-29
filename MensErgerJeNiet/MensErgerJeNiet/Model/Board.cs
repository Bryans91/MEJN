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
        private Player[] _playerList;
        private Goal[] goals = new Goal[16];
        private Game theGame;

        public Board(Player[] plist, Game game)
        {
            playerList = plist;
            first = null;
            last = null;
            theGame = game;
        }


        
        public void newCreateField(string[] field , Player[] players)
        {
            Console.WriteLine("creating field");
            playerList = players;
            //Field creation prep
            char[] normalF = new char[field[3].Length];
            char[] greenG = new char[4];
            char[] redG = new char[4];
            char[] blueG = new char[4];
            char[] yellowG = new char[4];

            normalF = field[3].ToCharArray();
            greenG = field[4].ToCharArray();
            redG = field[5].ToCharArray();
            blueG = field[6].ToCharArray();
            yellowG = field[7].ToCharArray();

            int yellowPawns = 0, greenPawns = 0, bluePawns = 0, redPawns = 0;

            Field previous = null;
            Field current = null;
            first = null;
            Pawn tempPawn = null;
           


            //create spawns
            foreach (Player p in players)
            {
                if (p != null)
                {
                    for (int s = 0; s < p.spawns.Length; s++)
                    {
                        string spwnCode = "p" + p.color + "spawn" + s;
                        p.spawns[s] = new Spawn(spwnCode);
                        Console.WriteLine(p.spawns[s].fieldCode);
                    }
                }
            }


            //create field & goal
            for (int i = 0; i < normalF.Length; i++)
            {
               

                tempPawn = null;
                //the normal fieldcreation
                switch (normalF[i]) 
                {
                    // empty field
                    case 'O':
                        current = new Field();
                        current.fieldCode = "field" + i;
                        break;
                    
                     // green on board
                    case 'G':
                        current = new Field();
                        current.fieldCode = "field" + i;
                        tempPawn = new Pawn(players[0], current);
                        current.pawn = tempPawn;
                        theGame.sendFieldCode(current);
                        //add pawn
                        if (players[0].pawns[greenPawns] == null)
                        {
                            players[0].pawns[greenPawns] = tempPawn;
                            greenPawns++;
                        }
                        break;

                    //Red on board
                    case 'R':
                        current = new Field();
                        current.fieldCode = "field" + i;
                        tempPawn = new Pawn(players[1], current);
                        current.pawn = tempPawn;
                        theGame.sendFieldCode(current);
                       
                        //add pawn
                        if (players[1].pawns[redPawns] == null)
                        {
                            players[1].pawns[redPawns] = tempPawn;
                            redPawns++;
                        }
                        break;

                     // blue on board
                    case 'B':
                        current = new Field();
                        current.fieldCode = "field" + i;
                        tempPawn = new Pawn(players[2], current);
                        current.pawn = tempPawn;
                        theGame.sendFieldCode(current);
                       
                        //add pawn
                        if (players[2].pawns[bluePawns] == null)
                        {
                            players[2].pawns[bluePawns] = tempPawn;
                            bluePawns++;
                        }
                        break; 
                        
                    //yellow on board
                    case 'Y':
                        current = new Field();
                        current.fieldCode = "field" + i;
                        tempPawn = new Pawn(players[3], current);
                        current.pawn = tempPawn;
                        theGame.sendFieldCode(current);
                       
                        //add pawn
                        if (players[3].pawns[yellowPawns] == null)
                        {
                            players[3].pawns[yellowPawns] = tempPawn;
                            yellowPawns++;
                        }
                        break;


                }


                //special steps
                switch (i)
                {

                        //startingfields
                    case 0:
                        first = current;

                        players[0].startingField = current;
                        foreach (Spawn sp in players[0].spawns)
                        {
                            sp.nextF = current;                           
                        }
                        break;
                    case 10:
                        players[1].startingField = current;
                        foreach (Spawn sp in players[1].spawns)
                        {
                            sp.nextF = current;
                        }
                        break;
                    case 20:
                        if (players[2] != null)
                        {
                            players[2].startingField = current;
                            foreach (Spawn sp in players[2].spawns)
                            {
                                sp.nextF = current;
                            }
                        }
                        break;
                    
                    case 30:

                        if (players[3] != null)
                        {
                            players[3].startingField = current;
                            foreach (Spawn sp in players[3].spawns)
                            {
                                sp.nextF = current;
                            }
                        }
                        break;
                       

                        //switchpoints + goal creation
                    case 9:
                        Goal rTemp = null;
                        Goal rPrev = null;

                        for (int g = 0; g < redG.Length; g++)
                        {
                            string fc = "goal" + players[1].color + g;
                            if (redG[g].Equals('R'))
                            {
                                rTemp = new Goal(players[1], fc);
                                rTemp.pawn = new Pawn(players[1], rTemp);
                                players[1].pawnsInGoal += 1;
                                theGame.sendFieldCode(rTemp);
                                if (players[1].pawns[redPawns] == null)
                                {
                                    players[1].pawns[redPawns] = tempPawn;
                                    redPawns++;
                                }
                            }
                            else
                            {
                                rTemp = new Goal(players[1], fc);
                            }

                            if (g == 0)
                            {
                                current.switchF = rTemp;
                                rTemp.previousF = current;
                            }
                            else
                            {
                                rTemp.previousF = rPrev;
                                rPrev.nextF = rTemp;

                            }
                            Console.WriteLine(rTemp.fieldCode);
                            rPrev = rTemp;
                        }
                        break;
                        
                    case 19:
                        if (players[2] != null)
                        {

                            Goal bTemp = null;
                            Goal bPrev = null;

                            for (int g = 0; g < blueG.Length; g++)
                            {
                                string fc = "goal" + players[2].color + g;
                                if (blueG[g].Equals('B'))
                                {
                                    bTemp = new Goal(players[2], fc);
                                    bTemp.pawn = new Pawn(players[2], bTemp);
                                    players[2].pawnsInGoal += 1;
                                    theGame.sendFieldCode(bTemp);
                                    if (players[2].pawns[bluePawns] == null)
                                    {
                                        players[2].pawns[bluePawns] = tempPawn;
                                        bluePawns++;
                                    }
                                }
                                else
                                {
                                    bTemp = new Goal(players[2], fc);
                                }

                                if (g == 0)
                                {
                                    current.switchF = bTemp;
                                    bTemp.previousF = current;
                                }
                                else
                                {
                                    bTemp.previousF = bPrev;
                                    bPrev.nextF = bTemp;

                                }
                                Console.WriteLine(bTemp.fieldCode);
                                bPrev = bTemp;
                            }
                        }
                        break;
                    
                    case 29:
                        Goal yTemp = null;
                        Goal yPrev = null;

                        if (players[3] != null)
                        {


                            for (int g = 0; g < yellowG.Length; g++)
                            {
                                string fc = "goal" + players[3].color + g;
                                if (yellowG[g].Equals('Y'))
                                {
                                    yTemp = new Goal(players[3], fc);
                                    yTemp.pawn = new Pawn(players[3], yTemp);
                                    players[3].pawnsInGoal += 1;
                                    theGame.sendFieldCode(yTemp);
                                    if (players[3].pawns[yellowPawns] == null)
                                    {
                                        players[3].pawns[yellowPawns] = tempPawn;
                                        yellowPawns++;
                                    }
                                }
                                else
                                {
                                    yTemp = new Goal(players[3], fc);
                                }

                                if (g == 0)
                                {
                                    current.switchF = yTemp;
                                    yTemp.previousF = current;
                                }
                                else
                                {
                                    yTemp.previousF = yPrev;
                                    yPrev.nextF = yTemp;

                                }
                                Console.WriteLine(yTemp.fieldCode);

                                yPrev = yTemp;
                            }

                        }
                    break;
                    case 39:

                        if (players[0] != null)
                        {
                            Goal gTemp = null;
                            Goal gPrev = null;

                            for (int g = 0; g < greenG.Length; g++)
                            {
                                string fc = "goal" + players[0].color + g;
                                if (greenG[g].Equals('G'))
                                {
                                    gTemp = new Goal(players[0], fc);
                                    gTemp.pawn = new Pawn(players[0], gTemp);
                                    players[0].pawnsInGoal += 1;
                                    theGame.sendFieldCode(gTemp);
                                    if (players[1].pawns[greenPawns] == null)
                                    {
                                        players[1].pawns[greenPawns] = tempPawn;
                                        greenPawns++;
                                    }
                                }
                                else
                                {
                                    gTemp = new Goal(players[0], fc);
                                }

                                if (g == 0)
                                {
                                    current.switchF = gTemp;
                                    gTemp.previousF = current;
                                }
                                else
                                {
                                    gTemp.previousF = gPrev;
                                    gPrev.nextF = gTemp;

                                }
                                Console.WriteLine(gTemp.fieldCode);
                                gPrev = gTemp;
                            }
                            //link ends


                            previous.nextF = current;
                            current.previousF = previous;
                            
                            current.nextF = first;
                            first.previousF = current;
                            Console.WriteLine("prev: " + previous.fieldCode + " next: " + current.fieldCode);
                            
                        }
                        break;

                }

                //link the list
                if (i != 0 && i != 39)
                {
                    previous.nextF = current;
                    current.previousF = previous;
                    Console.WriteLine("prev: " + previous.fieldCode + " next: " + current.fieldCode);
                }

                previous = current;
               

            } // end for

            //create pawns if not created yet:
            foreach (Player p in players)
            {
                if (p != null)
                {
                    for (int counter1 = 0; counter1 < 4; counter1++)
                    {
                        if (p.pawns[counter1] == null)
                        {
                            bool placed = false;
                            while (!placed)
                            {
                                for (int counter2 = 0; counter2 < p.spawns.Length; counter2++)
                                {

                                    if (p.pawns[counter2] == null)
                                    {
                                        p.pawns[counter1] = new Pawn(p, p.spawns[counter2]);
                                        p.spawns[counter2].pawn = p.pawns[counter1];
                                        placed = true;
                                    }
                                    Console.WriteLine(p.spawns[counter2].pawn + "<-- Pawns in spawn , fieldcode--> " + p.spawns[counter2].fieldCode);
                                } // last for
                            }
                        }
                    } // first for
                }
            }


            Console.WriteLine("FIELD CREATED");
        } 


        private Boolean isEmpty()
        {
            return first == null;
        }


        public Field getFieldFromPath(String fieldcode)
        {
            Field current = first, temp = null;
            int i = 0;
            while (current.fieldCode != fieldcode)
            {
                

                current = current.nextF;
                if (temp != null)
                {
                    if (temp.fieldCode == fieldcode)
                    {
                        current = temp;
                        break;
                    }
                    if (temp.nextF != null)
                    {
                        temp = temp.nextF;
                    }
                }

                switch (i)
                {
                    case 0:
                        foreach (Spawn sp in playerList[0].spawns)
                        {
                            if (current.fieldCode != fieldcode)
                                current = sp;
                            else 
                                break;
                        }
                        break;
                    case 10:
                        temp = current.switchF;
                        foreach (Spawn sp in playerList[1].spawns)
                        {
                            if (current.fieldCode != fieldcode)
                                current = sp;
                            else
                                break;
                        }
                        break;
                    case 20:
                        if (playerList[2] != null)
                        {
                            temp = current.switchF;
                            foreach (Spawn sp in playerList[2].spawns)
                            {
                                if (current.fieldCode != fieldcode)
                                    current = sp;
                                else
                                    break;
                            }
                        }
                        break;
                    case 30:
                        if (playerList[3] != null)
                        {
                            temp = current.switchF;
                            foreach (Spawn sp in playerList[3].spawns)
                            {
                                if (current.fieldCode != fieldcode)
                                    current = sp;
                                else
                                    break;
                            }
                        }
                        break;
                    case 40:
                        temp = current.switchF;
                        break;
                }
                i++;
                
            }
            return current;
        }

        public Player[] playerList
        {
            get { return _playerList; }
            private set { _playerList = value; }
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
