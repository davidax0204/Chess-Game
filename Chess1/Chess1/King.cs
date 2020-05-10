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
            if (base.validateMove(move, board) == false)
                return false;
            // castling the KING
            Piece pieceKing = (board.getSquare(move.from).piece == null) ? null : board.getSquare(move.from).piece;
            Piece pieceTo =  (board.getSquare(move.to).piece == null) ? null : board.getSquare(move.to).piece;

            if (pieceKing.moved == false && checkAttack(move.from,board) == false)
            {
                if (pieceKing.colour == "WHITE")
                {
                    if (board.getSquare(new Position(0, 0)).piece != null && board.getSquare(new Position(0, 0)).piece.moved == false && board.getSquare(new Position(0, 0)).piece is Rook)
                    {
                        if (board.getSquare(new Position(1, 0)).piece == null && board.getSquare(new Position(2, 0)).piece == null && board.getSquare(new Position(3, 0)).piece == null && move.to.x == 1)
                        {
                            if (checkAttack(new Position(1, 0),board)==false && checkAttack(new Position(2, 0), board) == false && checkAttack(new Position(3, 0), board) == false)
                            {
                                board.getSquare(new Position(2, 0)).piece = board.getSquare(new Position(0, 0)).piece;
                                board.getSquare(new Position(0, 0)).piece = null;
                                board.getSquare(new Position(1, 0)).piece = pieceKing;
                                return true;
                            }
                        }
                        if (board.getSquare(new Position(6, 0)).piece == null && board.getSquare(new Position(5, 0)).piece == null && move.to.x == 6)
                        {
                            if (checkAttack(new Position(6, 0), board) == false && checkAttack(new Position(5, 0), board) == false)
                            {
                                board.getSquare(new Position(5, 0)).piece = board.getSquare(new Position(7, 0)).piece;
                                board.getSquare(new Position(7, 0)).piece = null;
                                board.getSquare(new Position(6, 0)).piece = pieceKing;
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    if (board.getSquare(new Position(0, 7)).piece != null && board.getSquare(new Position(0, 7)).piece.moved == false && board.getSquare(new Position(0, 7)).piece is Rook)
                    {
                        if (board.getSquare(new Position(1,7)).piece==null && board.getSquare(new Position(2,7)).piece == null && board.getSquare(new Position(3,7)).piece == null && move.to.x == 1)
                        {
                            if (checkAttack(new Position(1, 7), board) == false && checkAttack(new Position(2, 7), board) == false && checkAttack(new Position(3, 7), board) == false)
                            {
                                board.getSquare(new Position(2, 7)).piece = board.getSquare(new Position(0, 7)).piece;
                                board.getSquare(new Position(0, 7)).piece = null;
                                board.getSquare(new Position(1, 7)).piece = pieceKing;
                                return true;
                            }
                        }
                        if (board.getSquare(new Position(6, 7)).piece == null && board.getSquare(new Position(5, 7)).piece == null && move.to.x == 6)
                        {
                            if (checkAttack(new Position(6, 7), board) == false && checkAttack(new Position(5, 7), board) == false)
                            {
                                board.getSquare(new Position(5, 7)).piece = board.getSquare(new Position(7, 7)).piece;
                                board.getSquare(new Position(7, 7)).piece = null;
                                board.getSquare(new Position(6, 7)).piece = pieceKing;
                                return true;
                            }
                        }
                    }
                }
            }

            if (Math.Abs(move.to.x - move.from.x) <= 1 && Math.Abs(move.to.y - move.from.y) <= 1 &&
                ((Math.Abs(move.to.x - move.from.x) + Math.Abs(move.to.y - move.from.y) > 0)))
            {
                BoardSquare squareKing = ChessBoard.getPiecesByColor(board.board, (this.colour == "WHITE") ? "BLACK" : "WHITE")[0];
                Position posKing = squareKing.position;
                if ((Math.Abs(move.to.x - posKing.x) == 1 && Math.Abs(move.to.y - posKing.y) == 1) || (Math.Abs(move.to.x - posKing.x) == 0 && Math.Abs(move.to.y - posKing.y) == 1) || (Math.Abs(move.to.x - posKing.x) == 1 && Math.Abs(move.to.y - posKing.y) == 0))
                    return false;
                return board.getSquare(move.to).piece == null || board.getSquare(move.to).piece.colour != colour;
            }
            return false;
        }
        private bool checkAttack (Position pos, ChessBoard board)
        {
            bool res = false;
            BoardSquare[] result = ChessBoard.getPiecesByColor(board.board, this.colour=="BLACK"?"WHITE":"BLACK");
            for (int i = 0; i < result.Length; i++)
            {
                if(result[i]!=null && result[i].piece!=null && (!(result[i].piece is King)))
                {
                    Piece piece = result[i].piece;
                    Move moveFrom = new Move(result[i].position.x, result[i].position.y, pos.x, pos.y);
                    if (piece.validateMove(moveFrom, board) == true)
                    {
                        res = true;
                        break;
                    }

                }
            }
            return res;
        }
    }

}
