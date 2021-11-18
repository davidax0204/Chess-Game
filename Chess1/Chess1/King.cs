using System;

namespace Chess1
{
    class King : Piece              
    {
        public King (string color) :base(color)
        {
            this.name = "K";
            this.weight = Piece.king;
        }
        public override bool validateMove(Move move, ChessBoard board)
        {
            if (base.validateMove(move, board) == false)                        // the King can move only 1 cell each side indluding diagonaly
                return false;        // base validation                         // it can castle, it cant move close to other king 1 cell away


            Piece pieceKing = (board.getSquare(move.from).piece == null) ? null : board.getSquare(move.from).piece;     // Castling the king check - if the cells of the field, and the king itself
                                                                                                                        // are not under attack and no one move so it is possible to castle the king

            if ((pieceKing !=null) && (pieceKing.moved == false && checkAttack(move.from,board) == false))              // Check if the king is not under attack or moved once
            {                                                                                                           
                if (pieceKing.colour == "WHITE")        // white king
                {
                    if (move.to.x == 1 && move.to.y==0)     // the cords of BIG castling
                    {
                        if (board.getSquare(new Position(1, 0)).piece == null && board.getSquare(new Position(2, 0)).piece == null &&               // check if the field cells are empty 
                            board.getSquare(new Position(3, 0)).piece == null && board.getSquare(new Position(0, 0)).piece != null &&               // and the rook never moved
                            board.getSquare(new Position(0, 0)).piece.moved == false && board.getSquare(new Position(0, 0)).piece is Rook)
                        {
                            if (checkAttack(new Position(1, 0), board) == false && checkAttack(new Position(2, 0), board) == false &&               // check if the cells in the caslting are under attack
                                checkAttack(new Position(3, 0), board) == false)                                                                    
                            {
                                board.getSquare(new Position(2, 0)).piece = board.getSquare(new Position(0, 0)).piece;                              // doing the casltling
                                board.getSquare(new Position(0, 0)).piece = null;
                                board.getSquare(new Position(1, 0)).piece = pieceKing;
                                return true;
                            }
                        }
                    }
                    else if(move.to.x == 6 && move.to.y == 0)   // the cords of SMALL castling
                    {
                        if (board.getSquare(new Position(6, 0)).piece == null && board.getSquare(new Position(5, 0)).piece == null &&               // check if the field cells are empty
                            board.getSquare(new Position(7, 0)).piece != null && board.getSquare(new Position(7, 0)).piece.moved == false &&        // and the rook never moved
                            board.getSquare(new Position(7, 0)).piece is Rook)
                        {
                            if (checkAttack(new Position(5, 0), board) == false && checkAttack(new Position(6, 0), board) == false)                 // check if the cells in the caslting are under attack
                            {
                                board.getSquare(new Position(5, 0)).piece = board.getSquare(new Position(7, 0)).piece;                              // doing the casltling
                                board.getSquare(new Position(7, 0)).piece = null;
                                board.getSquare(new Position(6, 0)).piece = pieceKing;
                                return true;
                            }
                        }
                    }
                    
                }
                else        // black king
                {
                    if (move.to.x == 1 && move.to.y == 7)    // the cords of BIG castling
                    {
                        if (board.getSquare(new Position(1, 7)).piece == null && board.getSquare(new Position(2, 7)).piece == null &&               // check if the field cells are empty
                            board.getSquare(new Position(3, 7)).piece == null && board.getSquare(new Position(0, 7)).piece != null &&               // and the rook never moved
                            board.getSquare(new Position(0, 7)).piece.moved == false && board.getSquare(new Position(0, 7)).piece is Rook)
                        {
                            if (checkAttack(new Position(1, 7), board) == false && checkAttack(new Position(2, 7), board) == false &&               // check if the cells in the caslting are under attack
                                checkAttack(new Position(3, 7), board) == false)
                            {
                                board.getSquare(new Position(2, 7)).piece = board.getSquare(new Position(0, 7)).piece;                              // doing the casltling
                                board.getSquare(new Position(0, 7)).piece = null;
                                board.getSquare(new Position(1, 7)).piece = pieceKing;
                                return true;
                            }
                        }
                    }
                    else if (move.to.x == 6 && move.to.y == 7)       // the cords of SMALL castling
                    {
                        if (board.getSquare(new Position(6, 7)).piece == null && board.getSquare(new Position(5, 7)).piece == null &&               // check if the field cells are empty
                            board.getSquare(new Position(7, 7)).piece != null && board.getSquare(new Position(7, 7)).piece.moved== false &&         // and the rook never moved
                            board.getSquare(new Position(7,7)).piece is Rook)
                        {
                            if (checkAttack(new Position(6, 7), board) == false && checkAttack(new Position(5, 7), board) == false)                 // check if the cells in the caslting are under attack
                            {
                                board.getSquare(new Position(5, 7)).piece = board.getSquare(new Position(7, 7)).piece;                              // doing the casltling
                                board.getSquare(new Position(7, 7)).piece = null;
                                board.getSquare(new Position(6, 7)).piece = pieceKing;
                                return true;
                            }
                        }
                    }
                }
            }
            if (Math.Abs(move.to.x - move.from.x) <= 1 && Math.Abs(move.to.y - move.from.y) <= 1 &&                 // simple move validation
                ((Math.Abs(move.to.x - move.from.x) + Math.Abs(move.to.y - move.from.y) > 0)))
            {
                BoardSquare squareKing = ChessBoard.getPiecesByColor(board.board, (this.colour == "WHITE") ? "BLACK" : "WHITE")[0];         //picking the opposite king 
                Position posKing = squareKing.position;

                if ((Math.Abs(move.to.x - posKing.x) == 1 && Math.Abs(move.to.y - posKing.y) == 1) ||               // checking if the 2 kings are moving too close
                    (Math.Abs(move.to.x - posKing.x) == 0 && Math.Abs(move.to.y - posKing.y) == 1) ||               // to each other if so so the move is invalid
                    (Math.Abs(move.to.x - posKing.x) == 1 && Math.Abs(move.to.y - posKing.y) == 0))
                    return false;

                return board.getSquare(move.to).piece == null || board.getSquare(move.to).piece.colour != colour;   // the king can move only if the desination cell is empty or 
            }                                                                                                       // there is there a enemy piece
            return false;
        }
        private bool checkAttack (Position pos, ChessBoard board)               // funcation that checks if a certain cell is under attack
        {
            bool flag = false;
            BoardSquare[] result = ChessBoard.getPiecesByColor(board.board, this.colour=="BLACK"?"WHITE":"BLACK");      //picking all the enemy pieces to array
            for (int i = 0; i < result.Length; i++)
            {
                if(result[i]!=null && result[i].piece!=null && (!(result[i].piece is King)))
                {
                    Piece piece = result[i].piece;
                    Move moveFrom = new Move(result[i].position.x, result[i].position.y, pos.x, pos.y);                 // checking if is there any piece in the array
                    if (piece.validateMove(moveFrom, board) == true)                                                    // that can move to a certain cell 
                    {
                        flag = true;                        // if the cell is under potencial attack so false
                        break;
                    }

                }
            }
            return flag;
        }
    }

}
