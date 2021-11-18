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
            if (base.validateMove(move, board) == false)                                    // Knight can move only 2 cells certain diraction and 1 cell to certain diraction
                return false;       // base validation

            if ((Math.Abs(move.to.x-move.from.x)==2 && Math.Abs(move.to.y-move.from.y)==1) ||                   // simple check if the move is 2 steps (X) and 1 (Y) 
                (Math.Abs(move.to.x - move.from.x) == 1 && Math.Abs(move.to.y - move.from.y) == 2))             // or 1 steps (X) and 2 steps (Y)
            {
                if (board.board[move.to.x, move.to.y].piece != null && board.getSquare(move.to).piece.colour == colour)         // if in the cell is a friendly piece so it false
                {
                    return false;
                }
                return board.getSquare(move.to).piece == null || board.getSquare(move.to).piece.colour != colour;       // can move only if the cell is contain enemy piece 
            }                                                                                                           // or its empty
            return false;
        }
    }
}
