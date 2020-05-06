using System;


namespace Chess1
{
    class Move
    {
        public Position from;
        public Position to;
        public Move(int fromX, int fromY, int toX, int toY)
        {
            this.from = new Position(fromX, fromY);
            this.to = new Position(toX, toY);
        }
    }
}
