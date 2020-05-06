using System.Reflection.Metadata;

namespace Chess1
{
    class Piece
    {
        public static int pawn = 0, bishop = 8, rook = 9, queen = 10, knight = 13, king = 100;
        public static int black = 1, white = 0;

        public string colour;
        public string name;
        public int weight;
        public int countStep;
        public bool moved = false;

        public Piece (string colour)
        {
            this.colour = colour;
        }
        public string GetColorPiece()
        {
            if (this.colour == "BLACK")
                return "B";
            else
                return "W";
        }
        public  string print(string text)
        {
            return string.Format("{0} {1} ",text,this.name);
        }
        public virtual bool validateMove(Move move, ChessBoard board) { return true; }
        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
