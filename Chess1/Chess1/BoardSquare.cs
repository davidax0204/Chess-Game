using System;

namespace Chess1
{
    // this class is presenting cell and the piece inside of it and the cords in the board
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
    }
}
