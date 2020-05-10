using System;
namespace Chess1
{
    class Bishop :Piece
    {
        public Bishop (string color) :base(color)
        {
            this.name = "B";
            weight = Piece.bishop;
        }
        public override bool validateMove(Move move, ChessBoard board)
        {
            if (base.validateMove(move, board) == false)
                return false;

            if (Math.Abs(move.to.x - move.from.x) == Math.Abs(move.to.y - move.from.y))
            {
                // End position possible
                int numberOfMoves = Math.Abs(move.to.x - move.from.x) - 1;
                int moveYPosition = move.from.y;
                int moveXPosition = move.from.x;
                int Xdir = move.to.x - move.from.x > 0 ? 1 : -1;
                int Ydir = move.to.y - move.from.y > 0 ? 1 : -1;
                for (int i = 0; i < numberOfMoves; i++)
                {
                    moveXPosition += Xdir;
                    moveYPosition += Ydir;
                    if (board.board[moveXPosition, moveYPosition].piece != null)
                    {
                        return false;
                    }
                }
                return board.getSquare(move.to).piece == null || board.getSquare(move.to).piece.colour != colour;

            }
            return false;
        }
    }
}
