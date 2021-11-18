using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Chess1
{
    class ChessGame
    {
        public Player whitePlayer = new Player("WHITE", true);
        public Player blackPlayer = new Player("BLACK", false);

        public BoardSquare[] white_board;
        public BoardSquare[] black_board;

        List<BoardSquare[,]> listBoards = new List<BoardSquare[,]>();

        public ChessBoard chessBoard;
        public bool isMate;
        public int stepCounterWithoutKillinWhite;
        public int stepCounterWithoutKillinBlack;
        public bool is3TimesSameBoard;

        public void play ()
        {
            chessBoard = new ChessBoard();
            BoardSquare[,] newBoard = ChessBoard.GetClonedBoard(chessBoard.board);
            listBoards.Add(newBoard);
            isMate = false;
            while (true)
            {
                ChessBoard.Print(chessBoard.board);

                if (isMate == true)
                    break;

                if (this.stepCounterWithoutKillinWhite >= 50 || this.stepCounterWithoutKillinBlack >= 50 || is3TimesSameBoard == true)              // Tie even if the same board is repeated 3 times
                {                                                                                                                               // or there are 50 steps without killing anyone
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("************************* ITS A TIE !!! *************************");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }

                Player activePlayer = whitePlayer.isActive ? whitePlayer : blackPlayer;                 // decide who is the active player

                Move newMove = chessBoard.getMoveInput(activePlayer);                                   // the input of the move of the active player

                Piece piece = chessBoard.getSquare(newMove.from).piece;                                 // the move of the active player

                if (piece == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("************** You tired to move an EMPTY cell, please try again *************");            // tried to move empty cell
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                else
                if (!(piece.colour == activePlayer.colorName))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("************* You are a '{0}' but you tried to move a '{1}' piece ************", activePlayer.colorName, piece.colour);      // tried to move the wrong color
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }

                makeMove(newMove, piece);               // the changes of the move are happening
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*************************GAME OVER*************************");                   // the game is over 
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void makeMove(Move move, Piece piece)
        {
            BoardSquare[,] BordBackup = ChessBoard.GetClonedBoard(chessBoard.board);
            BoardSquare lastStep = chessBoard.getSquare(move.to);

            string playerBackup = piece.colour; // color of active player
            int ifcheck;
            bool flgPieceTypeChanged = false;

            if ( piece.colour == "WHITE")
            {
                chessBoard.lastWhiteStep = lastStep;
            } 
            else
            {
                chessBoard.lastBlackStep = lastStep;
            }
            Position pieceKilledPos = null;
            bool flagMoveValidation = piece.validateMove(move, chessBoard);
            if (piece is Pawn)
            {
                if (Pawn.isPromote(move, chessBoard) == true)
                {
                    Pawn.promotePawn(move, chessBoard);
                    flgPieceTypeChanged = true;
                }
                pieceKilledPos = Pawn.getKilledPawnPassntPosition(move, chessBoard);
            }

            if (piece == null || (flagMoveValidation == false))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("************* Invalid move !!! *************");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

                return;
            }
            // last cell 

            if (flgPieceTypeChanged == false)
            {
                chessBoard.getSquare(move.to).piece = piece;
            }

            if ((piece is Pawn) && (pieceKilledPos != null))
            {
                chessBoard.getSquare(pieceKilledPos).piece = null;
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
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("************* Found Check sutuation . Try other one *************");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;


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

                ifcheck = ChessBoard.CheckMate(chessBoard, playerBackup, false);

                if (ifcheck > 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("************* M A T E TO " + playerBackup + " ! ! ! *************");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    isMate = true;
                }
                else if (ifcheck == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("************* C H E C K TO " + playerBackup + " *************");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (ifcheck == -1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("************* S T A L E   M A T E " + playerBackup + " *************");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    int counter = 0;
                    is3TimesSameBoard = false;
                    BoardSquare[,] newBoard = ChessBoard.GetClonedBoard(chessBoard.board);
                    listBoards.Add(newBoard);
                    for (int i = 0 ; i <listBoards.Count; i++)
                    {
                        BoardSquare[,] item = listBoards[i];
                        if (ChessBoard.isEqualBoards(item, chessBoard.board) == true)
                        {
                            counter++;
                        }
                    }
                    if (counter == 3)
                    {
                        is3TimesSameBoard = true;
                    }

                    if (piece.colour == "WHITE")
                    {
                        
                        if (BordBackup[move.to.x, move.to.y].piece == null)
                        {
                            stepCounterWithoutKillinWhite++;
                        }
                        else
                        {
                            stepCounterWithoutKillinWhite = 0;
                    }
                    }
                    else
                    {
                        if (BordBackup[move.to.x, move.to.y].piece == null)
                        {
                            stepCounterWithoutKillinBlack++;
                        }
                        else
                        {
                            stepCounterWithoutKillinBlack = 0;
                        }
                    }
                }

            }
        }
    }
}
