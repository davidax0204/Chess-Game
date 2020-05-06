using System;
using System.Net.NetworkInformation;

namespace Chess1
{
    class Game
    {
        public Player whitePlayer = new Player("WHITE", true);
        public Player blackPlayer = new Player("BLACK", false);

        public BoardSquare[] white_board;
        public BoardSquare[] black_board;

        public ChessBoard chessBoard;


        public Game()
        {
            chessBoard = new ChessBoard();
            while (true)
            {
                ChessBoard.Print(chessBoard.board);

                Player activePlayer = whitePlayer.isActive ? whitePlayer : blackPlayer;
                // get moving
                Move newMove = chessBoard.getMoveInput(activePlayer);

                Piece piece = chessBoard.getSquare(newMove.from).piece;

                if ( piece == null)
                {
                    Console.WriteLine("************** You tired to move an EMPTY cell, please try again *************");
                    Console.WriteLine();
                    continue;
                }
                else
                if (!(piece.colour == activePlayer.colorName))
                {
                    Console.WriteLine("************* You are a '{0}' but you tried to move a '{1}' piece ************", activePlayer.colorName, piece.colour);
                    Console.WriteLine();
                    continue;
                }

                makeMove(newMove, piece);
            }
        }
        public void makeMove(Move move, Piece piece)
        {
            int ifcheck;
            BoardSquare[,] BordBackup = ChessBoard.GetClonedBoard(chessBoard.board);
            string playerBackup = piece.colour; // color of active player

            bool flgPieceTypeChanged = false;
            Piece promotedPiece;
            BoardSquare lastStep = chessBoard.getSquare(move.to);
            if ( piece.colour == "WHITE")
            {
                chessBoard.lastWhiteStep = lastStep;
            } 
            else
            {
                chessBoard.lastBlackStep = lastStep;
            }

            bool flagMoveValidation = piece.validateMove(move, chessBoard);

            if (Pawn.isPromote(move, chessBoard) == true)
            {
                promotedPiece = Pawn.promotePawn(move, chessBoard);
                flgPieceTypeChanged = true;
            }


            if (piece == null || (flagMoveValidation == false))
            {
                Console.WriteLine("************* Invalid move *************");
                Console.WriteLine();
                return;
            }
            // last cell 

            if (flgPieceTypeChanged == false)
            {
                chessBoard.getSquare(move.to).piece = piece;
            }
            piece.moved = true;
            piece.countStep++;
            if (piece.moved == true)
            {
                if (whitePlayer.isActive == true)
                {
                    whitePlayer.isActive = false;
                    blackPlayer.isActive = true;
                    this.chessBoard.lastWhiteStep = lastStep;
                }
                else
                {
                    whitePlayer.isActive = true;
                    blackPlayer.isActive = false;
                    this.chessBoard.lastBlackStep = lastStep;
                }
            }

            chessBoard.getSquare(move.from).piece = null;

            white_board = ChessBoard.getPiecesByColor(chessBoard.board, "WHITE");
            black_board = ChessBoard.getPiecesByColor(chessBoard.board, "BLACK");
            ifcheck = ChessBoard.CheckMate(chessBoard, piece.colour, true);
            if (ifcheck > 0)
            {
                Console.WriteLine("************* Found Check sutuation . Try other one *************");
                Console.WriteLine();
                chessBoard.board = ChessBoard.GetClonedBoard(BordBackup);
                if (playerBackup == "WHITE")
                {
                    whitePlayer.isActive = true;
                    blackPlayer.isActive = false;
                } else
                {
                    whitePlayer.isActive = false;
                    blackPlayer.isActive = true;
                }

            }

            //this.white_board = ChessBoard.getPiecesByColor(chessBoard.board, "WHITE");
            //this.black_board = ChessBoard.getPiecesByColor(chessBoard.board, "BLACK");
        }
    }
}
