using System;

namespace Chess1
{
    class Knight : Piece
    {
        public Knight (string color) :base(color)
        {
            this.name = "N";
            this.weight = Piece.knight;
        }
        public override bool validateMove(Move move, ChessBoard board)
        {
            if (base.validateMove(move, board) == false)
                return false;
            if ((Math.Abs(move.to.x-move.from.x)==2 && Math.Abs(move.to.y-move.from.y)==1)
                || (Math.Abs(move.to.x - move.from.x) == 1 && Math.Abs(move.to.y - move.from.y) == 2))
            {
                if (board.board[move.to.x, move.to.y].piece != null && board.getSquare(move.to).piece.colour == colour)
                {
                    return false;
                }
                return board.getSquare(move.to).piece == null || board.getSquare(move.to).piece.colour != colour;
            }
            return false;
        }
    }
}
