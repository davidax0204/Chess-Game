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
                return false;           // base validation

            Piece pieceFrom = (board.getSquare(move.from).piece == null) ? null : board.getSquare(move.from).piece;
            BoardSquare lastStep = null;

            if (pieceFrom.colour == "WHITE")                // checks whome was moving the last one 
            {
                if (board.lastBlackStep != null)
                    lastStep = board.lastBlackStep;
            }
            else
            {
                if (board.lastWhiteStep != null)
                    lastStep = board.lastWhiteStep;
            }

            int criticalPosition = (pieceFrom.colour == "WHITE") ? 4 : 3;            // those are the suspeceted line numbers for "EN PASSANT" 
                                                                                     // for each color
            // check validation on other Pawn
            if ((lastStep != null) && (lastStep.piece != null))
            {
                Piece piecePawn = lastStep.piece;

                if (piecePawn is Pawn && piecePawn.countStep == 1 &&            // "EN PASSANT" happens only if the killed pawn moved only once (by 2 cells) and the 
                    move.from.y == lastStep.position.y &&                       // opposite color pawn was already in his posistion before the 2 cells jump
                    Math.Abs(move.from.x - lastStep.position.x) == 1 &&
                    move.from.y == criticalPosition)
                {
                    // "EN PASSANT" is happening and killing the other pawn
                    // board.getSquare(lastStep.position).piece = null;
                    return true;
                }
            }
            int diraction =colour == "BLACK" ? -1 : 1;

            if (move.to.y - move.from.y == diraction)
            {
                //Moving forward
                if (Math.Abs(move.to.x - move.from.x) == 0)                         // pawn moving only foward or diagonal if he is killing someone
                {
                    return (board.getSquare(move.to).piece == null);
                }
                //Moving diagonal
                if (Math.Abs(move.to.x - move.from.x) == 1)                         // check diagonal move if the pawn is killing
                {
                    return (board.getSquare(move.to).piece != null && board.getSquare(move.to).piece.colour != colour);
                }

                return false;
            }
            else if (!moved && move.to.y - move.from.y == 2 * diraction && Math.Abs(move.to.x - move.from.x) == 0)  // the pawn can move 2 cells foward only in his first step
            {
                return (board.getSquare(move.to).piece == null);                    
            }
            else
            {
                return false;
            }

        }
        public static bool isPromote(Move move, ChessBoard board)           // function to check if a pawn deserve a promotion (if he got to the enemy line without getting killed
        {
            Piece pieceFrom = (board.getSquare(move.from).piece == null) ? null : board.getSquare(move.from).piece;

            if (pieceFrom != null && pieceFrom.validateMove(move, board) == true && pieceFrom is Pawn)      // if the piece is really a pawn and moved correctly there
            {
                if (pieceFrom.colour == "WHITE")
                {
                    if (move.to.y == 7)                 // enemy line
                        return true;
                }
                else
                {
                    if (move.to.y == 0)                 // enemy line
                        return true;
                }
            }
            return false;
        }
        public static Piece promotePawn(Move move, ChessBoard board)        // function that promotion the pawn that successeded to get to the enemy line
        {
            string newPiece = "";
            bool flag = false;
            Piece pieceFrom = (board.getSquare(move.from).piece == null) ? null : board.getSquare(move.from).piece;
            Piece promotedPiece = pieceFrom;

            if (isPromote(move, board) == true)                             // Promotion condition
            {
                while (flag == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} player, Your pawn is being promoted! Please choose which piece you want to promote", pieceFrom.colour);
                    Console.WriteLine("                 (B)ishop || K(N)ight || (Q)ueen || (R)ook                            ");
                    Console.ForegroundColor = ConsoleColor.White;
                    newPiece = Console.ReadLine();                                                      // what to promote the piece
                    newPiece = ChessBoard.inputValidationPromotion(newPiece);                           // and make sure that the input is valid 

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
                            break;                                                                                              // choose what to promote the piece
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
                promotedPiece = board.getSquare(move.to).piece;
                promotedPiece.countStep++;
                promotedPiece.moved = true;                                                                                     // put the promoted piece on the pawn place and remove the pawn
            }
            return promotedPiece;
        }

        public static Position getKilledPawnPassntPosition(Move move, ChessBoard board)
        {
            BoardSquare lastStep = null;
            Position kieldPawnPos = null; 
            string myColor = board.getSquare(move.from).piece.colour;

            if (myColor == "WHITE")                // checks whome was moving the last one 
            {
                if (board.lastBlackStep != null)
                    lastStep = board.lastBlackStep;
            }
            else
            {
                if (board.lastWhiteStep != null)
                    lastStep = board.lastWhiteStep;
            }

            int criticalPosition = (myColor == "WHITE") ? 4 : 3;            // those are the suspeceted line numbers for "EN PASSANT" 
                                                                                     // for each color
                                                                                     // check validation on other Pawn
            if ((lastStep != null) && (lastStep.piece != null))
            {
                Piece piecePawn = lastStep.piece;

                if (piecePawn is Pawn && piecePawn.countStep == 1 &&            // "EN PASSANT" happens only if the killed pawn moved only once (by 2 cells) and the 
                    move.from.y == lastStep.position.y &&                       // opposite color pawn was already in his posistion before the 2 cells jump
                    Math.Abs(move.from.x - lastStep.position.x) == 1 &&
                    move.from.y == criticalPosition)
                {
                    // "EN PASSANT" is happening and killing the other pawn

                    kieldPawnPos = new Position(lastStep.position.x, lastStep.position.y);
                    return kieldPawnPos;
                }
            }
            return null;

        }
    }
}
