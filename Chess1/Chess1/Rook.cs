using System;


namespace Chess1
{
    class Rook :Piece
    {
        public Rook (string color) :base(color)
        {
            this.name = "R";
            this.weight = Piece.rook;
        }
        public override bool validateMove(Move move, ChessBoard board)
        {
            if (base.validateMove(move, board) == false)
                return false;

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
            return false;
        }
    }
}
