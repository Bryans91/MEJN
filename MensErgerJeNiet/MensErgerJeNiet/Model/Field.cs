using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensErgerJeNiet.Model
{
    class Field
    {

        protected Field _nextF, _previousF;
        protected Goal _switchF;
        protected Pawn _pawn;

        public Field(){


        }


        //Properties
        public Pawn pawn
        {
            get { return _pawn; }
            set { _pawn = pawn; }
        }
        
        public Field nextF
        {
            get { return _nextF; }
            set { _nextF = nextF; }
        }

        public Field previousF
        {
            get { return _previousF; }
            set{ _previousF = previousF;}
        }

        public Goal switchF
        {
            get { return _switchF; }
            set { _switchF = switchF; }
        }

        



    }
}
