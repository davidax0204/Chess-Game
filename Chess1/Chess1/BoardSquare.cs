using System;

namespace Chess1
{
    class BoardSquare
    {
        public string SquereColor;
        public Piece piece;
        public Position position;
        public BoardSquare (string color,Position position)
        {
            this.SquereColor = color;
            this.position = position;
        }
        public string GetShortName ()
        {
            if (this.SquereColor == "BLACK")
                return "B";
            else if (this.SquereColor == "WHITE")
                return "W";
            else
                return "EE"; // return "Empty" for empty cell
        }
        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
