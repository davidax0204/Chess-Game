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
        public bool isMate;
        public int stepCountWithoutKill;


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
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("************** You tired to move an EMPTY cell, please try again *************");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                else
                if (!(piece.colour == activePlayer.colorName))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("************* You are a '{0}' but you tried to move a '{1}' piece ************", activePlayer.colorName, piece.colour);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
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
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("************* Invalid move !!! *************");
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.Black;

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
                if (ifcheck > 1)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("************* M A T E ! ! ! *************");
                    Console.WriteLine();
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("************* Found Check sutuation . Try other one *************");
                    Console.WriteLine();
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;

                }

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
            else
            {
                if (playerBackup == "WHITE")
                    playerBackup = "BLACK";
                else
                    playerBackup = "WHITE";

                ifcheck = ChessBoard.CheckMate(chessBoard, playerBackup,  true);

                if (ifcheck > 1)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("************* M A T E TO " + playerBackup + " ! ! ! *************");
                    Console.WriteLine();
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (ifcheck == 1)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("************* C H E C K TO " + playerBackup + " *************");
                    Console.WriteLine();
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;

                }
                else {
                    if (move.to == null)
                        stepCountWithoutKill++;
                    else
                        stepCountWithoutKill = 0;
                }

            }

            //this.white_board = ChessBoard.getPiecesByColor(chessBoard.board, "WHITE");
            //this.black_board = ChessBoard.getPiecesByColor(chessBoard.board, "BLACK");
        }
    }
}
