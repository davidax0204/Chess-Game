using System;

namespace Chess1
{
    class Queen : Piece
    {
        public Queen (string color) :base(color)
        {
            this.name = "Q";
            this.weight = Piece.queen;
        }
        public override bool validateMove(Move move, ChessBoard board)
        {
            if ((Math.Abs(move.to.x - move.from.x) > 0) ^ (Math.Abs(move.to.y - move.from.y) > 0))
            {
                // End position possible
                if (Math.Abs(move.to.x - move.from.x) > 0)
                {
                    int numberOfMoves = Math.Abs(move.to.x - move.from.x) - 1;
                    int movePosition = move.from.x;
                    int dir = move.to.x - move.from.x > 0 ? 1 : -1;
                    for (int i = 0; i < numberOfMoves; i++)
                    {
                        movePosition += dir;
                        if (board.board[movePosition, move.from.y].piece != null)
                        {
                            return false;
                        }
                    }
                    return board.getSquare(move.to).piece == null || board.getSquare(move.to).piece.colour != colour;
                }
                if (Math.Abs(move.to.y - move.from.y) > 0)
                {
                    int numberOfMoves = Math.Abs(move.to.y - move.from.y) - 1;
                    int movePosition = move.from.y;
                    int dir = move.to.y - move.from.y > 0 ? 1 : -1;
                    for (int i = 0; i < numberOfMoves; i++)
                    {
                        movePosition += dir;
                        if (board.board[move.from.x, movePosition].piece != null)
                        {
                            return false;
                        }
                    }

                    return board.getSquare(move.to).piece == null || board.getSquare(move.to).piece.colour != colour;
                }
            }
            else if (Math.Abs(move.to.x - move.from.x) == Math.Abs(move.to.y - move.from.y))
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
