using System;


namespace Chess1
{
    class Pawn : Piece
    {
        public Pawn (string color) :base(color)
        {
            this.name = "P";
            this.weight = Piece.pawn;
        }

        public override bool validateMove(Move move, ChessBoard board)
        {
            if (base.validateMove(move, board) == false)
                return false;

            Piece pieceFrom = (board.getSquare(move.from).piece == null) ? null : board.getSquare(move.from).piece;
            Piece pieceTo = (board.getSquare(move.to).piece == null) ? null : board.getSquare(move.to).piece;


            BoardSquare lastStep_ = null;

            if (pieceFrom.colour == "WHITE")
            {
                if (board.lastBlackStep != null)
                    lastStep_ = board.lastBlackStep;
            }
            else
            {
                if (board.lastWhiteStep != null)
                    lastStep_ = board.lastWhiteStep;

            }

            int critpos = (pieceFrom.colour == "WHITE") ? 4 : 3;

            // check validation on other Pawn
            if ((lastStep_ != null) && (lastStep_.piece != null))
            {
                Piece piecePawn = lastStep_.piece;

                if (piecePawn is Pawn && piecePawn.countStep == 1 &&
                    move.from.y == lastStep_.position.y && Math.Abs(move.from.x - lastStep_.position.x) == 1 &&
                    move.from.y == critpos)
                {

                    board.getSquare(lastStep_.position).piece = null; // killing the other pawn
                    return true;
                }
            }
            int dir =colour == "BLACK" ? -1 : 1;

            if (move.to.y - move.from.y == dir)
            {
                //Moving forward
                if (Math.Abs(move.to.x - move.from.x) == 0)
                {
                    return (board.getSquare(move.to).piece == null);
                }
                //Moving sideways
                if (Math.Abs(move.to.x - move.from.x) == 1)
                {
                    return (board.getSquare(move.to).piece != null && board.getSquare(move.to).piece.colour != colour);
                }

                //Cheating
                return false;
            }
            else if (!moved && move.to.y - move.from.y == 2 * dir && Math.Abs(move.to.x - move.from.x) == 0)
            {
                return (board.getSquare(move.to).piece == null);
            }
            else
            {
                return false;
            }

        }
        public static bool isPromote(Move move, ChessBoard board)
        {
            Piece pieceFrom = (board.getSquare(move.from).piece == null) ? null : board.getSquare(move.from).piece;

            if (pieceFrom != null && pieceFrom.validateMove(move, board) == true && pieceFrom is Pawn)
            {
                if (pieceFrom.colour == "WHITE")
                {
                    if (move.to.y == 7)
                        return true;
                }
                else
                {
                    if (move.to.y == 0)
                        return true;
                }
            }
            return false;
        }
        public static Piece promotePawn(Move move, ChessBoard board)
        {
            string newPiece = "";
            bool flag = false;
            Piece pieceFrom = (board.getSquare(move.from).piece == null) ? null : board.getSquare(move.from).piece;
            Piece pieceTo = (board.getSquare(move.to).piece == null) ? null : board.getSquare(move.to).piece;

            Piece resPiece = pieceFrom;

            if (isPromote(move, board) == true)
            {
                while (flag == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} player, Your pawn is being promoted! Please choose which piece you want to promote", pieceFrom.colour);
                    Console.WriteLine("                 (B)ishop || K(N)ight || (Q)ueen || (R)ook                            ");
                    Console.ForegroundColor = ConsoleColor.White;
                    newPiece = Console.ReadLine();
                    newPiece = ChessBoard.inputValidationPromotion(newPiece);

                    if (String.IsNullOrEmpty(newPiece))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("************ Input was invalid, please try again *************");
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    flag = true;
                }
                switch (newPiece)
                {
                    case "B":
                        {
                            board.getSquare(move.to).piece = new Bishop(pieceFrom.colour);
                            break;
                        }
                    case "N":
                        {
                            board.getSquare(move.to).piece = new Knight(pieceFrom.colour);
                            break;
                        }
                    case "Q":
                        {
                            board.getSquare(move.to).piece = new Queen(pieceFrom.colour);
                            break;
                        }
                    case "R":
                        {
                            board.getSquare(move.to).piece = new Rook(pieceFrom.colour);
                            break;
                        }
                }
                resPiece = board.getSquare(move.to).piece;
                resPiece.countStep++;
                resPiece.moved = true;
            }
            return resPiece;
        }
    }
}
