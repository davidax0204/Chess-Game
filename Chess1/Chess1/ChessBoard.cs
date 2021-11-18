using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Chess1
{
    class ChessBoard
    {
        public string patternString = "AaBbCcDdEeFfGgHh";
        string patternUpper = "ABCDEFGH";
        string patternInt = "12345678";

        public BoardSquare[,] board = new BoardSquare[8, 8];

        public BoardSquare lastWhiteStep;
        public BoardSquare lastBlackStep;

        // builds the board and inserts the pieses and each square color
        public ChessBoard()                             
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        board[i, j] = new BoardSquare("BLACK", new Position(i, j));
                    }
                    else
                    {
                        board[i, j] = new BoardSquare("WHITE", new Position(i, j));
                    }
                }
            }
            this.PlacePieces();
        }

        // deploy the pieces on the board
        private void PlacePieces()
        {
            for (int i = 0; i < 8; i++)
            {
                board[i, 1].piece = new Pawn("WHITE");
                board[i, 6].piece = new Pawn("BLACK");
            }
            board[0, 0].piece = new Rook("WHITE");
            board[1, 0].piece = new Knight("WHITE");
            board[2, 0].piece = new Bishop("WHITE");
            board[3, 0].piece = new Queen("WHITE");
            board[4, 0].piece = new King("WHITE");
            board[5, 0].piece = new Bishop("WHITE");
            board[6, 0].piece = new Knight("WHITE");
            board[7, 0].piece = new Rook("WHITE");

            board[0, 7].piece = new Rook("BLACK");
            board[1, 7].piece = new Knight("BLACK");
            board[2, 7].piece = new Bishop("BLACK");
            board[3, 7].piece = new Queen("BLACK");
            board[4, 7].piece = new King("BLACK");
            board[5, 7].piece = new Bishop("BLACK");
            board[6, 7].piece = new Knight("BLACK");
            board[7, 7].piece = new Rook("BLACK");
        }

        // prints the board
        public static void Print(BoardSquare[,] board)
        {
            string print = "";
            string print_ = "";

            Console.WriteLine("           A(0)   B(1)   C(2)   D(3)   E(4)   F(5)   G(6)   H(7)   ");
            Console.WriteLine();
            Console.WriteLine("         ________________________________________________________");

            for (int j = 7; j >= 0; j--)
            {
                string printCell = "";
                string printCell_ = "";
                string printEmpty = "         ";
                print = " " + (j + 1) + "  (" + j + ")  ";
                print_ = "         ";

                for (int i = 0; i < 8; i++)
                {
                    if (board[i, j].piece != null)
                    {
                        printCell = printCell + "| " + board[i, j].piece.GetColorPiece() + " " + board[i, j].piece.name + "  ";
                        printCell_ = printCell_ + "|______";
                    }
                    else
                    {
                        printCell = printCell + "|  ... ";
                        printCell_ = printCell_ + "|______";
                    }
                    printEmpty = printEmpty + "|      ";
                }
                print_ = print_ + printCell_ + "|";
                print = print + printCell + "|";
                Console.WriteLine(printEmpty + "|");
                Console.WriteLine(print);
                Console.WriteLine(print_);
            }
            Console.WriteLine();

            Console.WriteLine("Pawn - P");
            Console.WriteLine("Queen - Q");
            Console.WriteLine("King - K");
            Console.WriteLine("Rook - R");
            Console.WriteLine("Knight - N");
            Console.WriteLine("Bishop - B");

            Console.WriteLine();
        }

        // getting the input and checking for invalid inputs 
        public Move getMoveInput(Player player)
        {
            Console.WriteLine("'{0}' player, please insert 'From' cordinations in this pattern 'a4' (letter first and then number)", player.colorName);
            string moveFrom = Console.ReadLine();

            if (inputValidation(moveFrom) == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("'From' Input was invalid, please try again");
                Console.ForegroundColor = ConsoleColor.White;
                return getMoveInput(player);
            }

            Console.WriteLine("'{0}' player, please insert 'To' cordinations in this pattern 'a4' (letter first and then number)", player.colorName);
            string moveTo = Console.ReadLine();

            if (inputValidation(moveTo) == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("'To' Input was invalid, please try again");
                Console.ForegroundColor = ConsoleColor.White;
                return getMoveInput(player);
            }
            return convertMove(moveFrom, moveTo);
        }

        // checks the validity of the input
        public bool inputValidation(string input)
        {
            string str = input.Replace(" ", "");

            if (str.Length != 2)
                return false;
            if (!this.patternString.Contains(str[0]))
                return false;
            if (!this.patternInt.Contains(str[1]))
                return false;

            return true;
        }

        // checks the validity of the input of the pawn promoation 
        public static string inputValidationPromotion(string input)
        {
            string result = input.Replace(" ", "");
            result = result.ToUpper();
            if (result.Length != 1)
                return "";
            if (result != "R" && result != "B" && result != "N" && result != "Q") 
                return "";

            return result;
        }

        // convert the move form from moveFrom and moveTo --> (x,y) (x,y)
        private Move convertMove(string moveFrom, string moveTo)
        {
            int fromX = this.patternUpper.IndexOf(moveFrom[0].ToString().ToUpper());            // convert string input to int for easy convertion
            int fromY = int.Parse("" + moveFrom[1]) - 1;

            int toX = this.patternUpper.IndexOf(moveTo[0].ToString().ToUpper());
            int toY = int.Parse("" + moveTo[1]) - 1;

            Move pos = new Move(fromX, fromY, toX, toY);
            return pos;
        }

        // getter of the square position 
        public BoardSquare getSquare(Position position)
        {
            return board[position.x, position.y];
        }

        // function that all the pieces that are alive of a certain color
        public static BoardSquare[] getPiecesByColor(BoardSquare[,] board, string colour)
        {
            BoardSquare[] result = new BoardSquare[16];
            int counter = 0;
            for (int j = 7; j >= 0; j--)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (board[i, j].piece != null && board[i, j].piece.colour == colour)
                    {
                        if (board[i, j].piece.weight != Piece.king)  // always puts the king the index = 0 of the array
                        {
                            counter++;
                            result[counter] = board[i, j];
                        }
                        else
                            result[0] = board[i, j];
                    }
                }
            }
            return result;
        }

        // function that checks if is there check or mate
        public static int CheckMate(ChessBoard chBoard, string colour, bool testMyPiecesCheck)  // testMyPiecesCheck == true --> by self 
        {
            int result, resultshield=0;
            int xOponent = 0, yOponent = 0;
            BoardSquare[,] board = chBoard.board;

            // activePieces is a list of all the pieces of the same chosen colour
            BoardSquare[] activePieces = colour == "BLACK" ? ChessBoard.getPiecesByColor(board, "BLACK") :ChessBoard.getPiecesByColor(board, "WHITE");

            //Extract dangeries Piaces from opponent
            result = IfPresentsCheck(chBoard, colour, ref xOponent, ref yOponent);             
            
            // testing that the move that the active player just did doesnt open a check situation
            if (testMyPiecesCheck)
                return result;

            // check if is there a "CHECK" situation or "Stale" situation (result = 0 --> no check) (result = -1 --> stale)
            else if (result <= 0) 
            {
                result = IsStaleMate(chBoard, activePieces);
                return result;
            }
            // there a is "CHECK" situation from only one enemy piece and checks if possible to shield the check with by self piece
            if (result == 1)
                resultshield = find_Shield(chBoard, activePieces, colour, xOponent, yOponent);

            // checks if is there any self piece to shield the check
            if (resultshield == 0)                                                                                 
                return result;                                                                                                                                                                         

            // there is no way to shield the king and the king have to run (1--> can run and avoid check) (2--> mate and the game ends)
            result = ifPossableKingMoving(chBoard, activePieces);              
            return result;
        }

        // the function is getting a cloned board
        public static BoardSquare[,] GetClonedBoard(BoardSquare[,] board)
        {
            BoardSquare[,] boardClone = new BoardSquare[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        boardClone[i, j] = new BoardSquare("BLACK", new Position(i, j));
                    }
                    else
                    {
                        boardClone[i, j] = new BoardSquare("WHITE", new Position(i, j));
                    }
                }
            }

            for (int j = 7; j >= 0; j--)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (board[i, j].piece != null)
                    {
                        boardClone[i, j].piece = (Piece)board[i, j].piece.Clone();
                        boardClone[i, j].position.x = board[i, j].position.x;
                        boardClone[i, j].position.y = board[i, j].position.y;
                        boardClone[i, j].SquereColor = board[i, j].SquereColor;
                    }
                    else
                        boardClone[i, j].piece = null;
                }
            }
            return boardClone;
        }

        // the function is checking if we can avoid the check with putting a different figure to block the check
        private static int find_Shield(ChessBoard chBoard, BoardSquare[] activePieces, string colour, int xOponent, int yOponent)
        {
            int result = 1;
            int step_x, step_y, indexX, indexY,
                xKing = activePieces[0].position.x,
                yKing = activePieces[0].position.y;

            BoardSquare[,] tempBackUp;
            BoardSquare square;
            Position v_From = new Position(0, 0);
            Position v_To = new Position(0, 0);
            Move v_Move = new Move(0, 0, 0, 0);
            bool v_shield = false;
            int ifcheck = 1;

            // which side the figure pointing (places)
            step_x = Math.Sign(xOponent - xKing);
            step_y = Math.Sign(yOponent - yKing);

            indexX = xKing + step_x;
            indexY = yKing + step_y;

            while (indexX != xOponent || indexY != yOponent)
            {
                for (int i = 0; i < activePieces.Length; i++)
                {
                    if (activePieces[i] == null) 
                        continue;

                    square = activePieces[i];
                    v_From.x = square.position.x;
                    v_From.y = square.position.y;
                    v_To.x = indexX;
                    v_To.y = indexY;
                    v_Move.from = v_From;
                    v_Move.to = v_To;

                    // this part is trying to find the figure that can sheild the king from the check
                    // and checks if the move to the new position (v_Move) is valid
                    if (square.piece.weight == Piece.knight)
                        v_shield = (activePieces[i].piece as Knight).validateMove(v_Move, chBoard);
                    else if (square.piece.weight == Piece.bishop)
                        v_shield = (activePieces[i].piece as Bishop).validateMove(v_Move, chBoard);
                    else if (square.piece.weight == Piece.rook)
                        v_shield = (activePieces[i].piece as Rook).validateMove(v_Move, chBoard);
                    else if (square.piece.weight == Piece.queen)
                        v_shield = (activePieces[i].piece as Queen).validateMove(v_Move, chBoard);
                    else if (square.piece.weight == Piece.pawn)
                        v_shield = (activePieces[i].piece as Pawn).validateMove(v_Move, chBoard);

                    if (v_shield)           // if true so the valid is true and the figure can move there
                        goto Shield_test;
                }
                indexX = indexX + step_x;
                indexY = indexY + step_y;
            }

            return result;

        Shield_test:

            if (v_shield)       // if true
            {
                // after we moved the piece to the new position to avoid the check we need to check if any check or mate are 
                // happening due to this move
                tempBackUp = ChessBoard.GetClonedBoard(chBoard.board);
                chBoard.getSquare(v_Move.to).piece = square.piece;
                chBoard.getSquare(v_Move.from).piece = null;
                ifcheck = CheckMate(chBoard, colour, false);
                chBoard.board = ChessBoard.GetClonedBoard(tempBackUp);
            }
            if (ifcheck == 0)   // there is no check any more
                result = 0;
            return result;
        }

        private static int IfPresentsCheck(ChessBoard chBoard, string colour, ref int xOponent, ref int yOponent)
        {
            int result;
            BoardSquare[,] board = chBoard.board;
            BoardSquare[] v_WhiteP, v_BlackP;

            v_WhiteP = ChessBoard.getPiecesByColor(board, "WHITE");
            v_BlackP = ChessBoard.getPiecesByColor(board, "BLACK");


            Position kingPosition = (colour == "BLACK" ? v_BlackP[0].position : v_WhiteP[0].position);
            BoardSquare[] opponentPieces = colour == "BLACK" ? v_WhiteP : v_BlackP;
            BoardSquare[] activePieces = colour == "BLACK" ? v_BlackP : v_WhiteP;

            result = 0;
            int xKing = kingPosition.x, yKing = kingPosition.y;
            int step_x, step_y,
                Mem_Ox = 0, Mem_Oy = 0,
                xOp = 0,
                yOp = 0;

            int fig, ox, oy;

            // we are checking if the opposite color pieces can do a check on our king
            for (int i = 1; i < opponentPieces.Length; i++)
            {
                if (result == 2)        // 2 checks
                    return result;

                if (opponentPieces[i] == null || opponentPieces[i].piece == null) 
                    continue;

                xOp = opponentPieces[i].position.x;
                yOp = opponentPieces[i].position.y;
                xOponent = xOp;
                yOponent = yOp;
                fig = opponentPieces[i].piece.weight;
                if
                (
                    // check if thats the king
                    activePieces[0].piece is King && opponentPieces[1].piece is King
                    && Math.Abs(xKing - xOp) < 2 && Math.Abs(yKing - yOp) < 2 
                )
                {
                    result = -100;
                    return result;
                }

                else if
                (
                // check for the knight if he is able to chekc the king
                    fig == Piece.knight &&
                    (
                    Math.Abs(xKing - xOp) == 2 && Math.Abs(yKing - yOp) == 1 ||
                    Math.Abs(xKing - xOp) == 1 && Math.Abs(yKing - yOp) == 2
                    )
                )
                    result++;


                else if
                (
                // check for the pawn if he is able to chekc the king
                    fig == Piece.pawn &&
                    (
                        colour == "BLACK" && xKing - xOp == 1 && yKing - yOp == 1 ||
                        colour == "WHITE" && xKing - xOp == -1 && yKing - yOp == -1
                    )
                )
                    result++;

                // what is the piece the doing the chcek
                else if (fig == Piece.knight || fig == Piece.pawn || fig == Piece.king)
                    continue;
                // if is there any figure that can make a check (danger for check)
                else if (fig == Piece.rook && Math.Abs(xKing - xOp) != 0 && Math.Abs(yKing - yOp) != 0)
                    continue;

                else if (fig == Piece.bishop && Math.Abs(xKing - xOp) != Math.Abs(yKing - yOp))
                    continue;

                else if (fig == Piece.queen && Math.Abs(xKing - xOp) != Math.Abs(yKing - yOp)
                    && Math.Abs(xKing - xOp) != 0 && Math.Abs(yKing - yOp) != 0)
                    continue;

                step_x = Math.Sign(xOp - xKing);
                step_y = Math.Sign(yOp - yKing);
                ox = xKing + step_x;
                oy = yKing + step_y;


                // in the while loop we are checing the cells between the suspected figure and the king and if is there any possible check
                while (Math.Abs(ox) < 8 && Math.Abs(oy) < 8 && Math.Abs(ox) >= 0 && Math.Abs(oy) >= 0)
                {
                    if (board[ox, oy].piece != null)
                    {
                        // no one was found on the cells 
                        if (ox == xOp && oy == yOp)
                            goto NewOxOy;

                        // we found one of our piece so we are blocking the check
                        else if (board[ox, oy].piece.colour == colour)
                        {
                            result--;
                            goto NewOxOy;
                        }
                        else
                        {
                            // we didnt found and dnager figure or our figure
                            for (int j = 0; j < opponentPieces.Length; j++)
                            {
                                // Going by the "traectoria" of the Check
                                if (opponentPieces[j] != null && opponentPieces[j].position.x == ox && opponentPieces[j].position.y == oy)
                                {
                                    if (fig == Piece.pawn)
                                    {
                                        if
                                        (
                                        // check for the pawn of the opposite side so its a check
                                            (colour == "BLACK" && xKing - xOp == 1 && yKing - yOp == 1) ||
                                            (colour == "WHITE" && xKing - xOp == -1 && yKing - yOp == -1)
                                        )
                                            goto NewOxOy;
                                        else
                                        {
                                            result = -1;
                                            goto NewOxOy;
                                        }
                                    }
                                    // we are checking for the other pieces if is there suspected check figure for our king
                                    if (fig == Piece.queen && (step_x == 0 || step_y == 0) &&
                                        opponentPieces[j].piece.weight == Piece.rook
                                       ) goto NewOxOy;
                                    else if (fig == Piece.rook && (step_x == 0 || step_y == 0) &&
                                        opponentPieces[j].piece.weight == Piece.queen
                                       ) goto NewOxOy;
                                    else if (fig == Piece.queen && step_x != 0 && step_y != 0 &&
                                        opponentPieces[j].piece.weight == Piece.bishop
                                       ) goto NewOxOy;
                                    else if (fig == Piece.bishop && step_x != 0 && step_y != 0 &&
                                        opponentPieces[j].piece.weight == Piece.queen
                                       ) goto NewOxOy;
                                }
                            }
                        }
                    }
                    ox = ox + step_x;
                    oy = oy + step_y;
                }
            NewOxOy:
                {
                    Mem_Ox = ox;
                    Mem_Oy = oy;
                    result++;
                }
            }
            // return of the suspected figure
            xOponent = Mem_Ox;
            yOponent = Mem_Oy;
            return result;
        }
        
        // the function is checking if the king can run away from a check that is already exists !!!
        private static int ifPossableKingMoving(ChessBoard chBoard, BoardSquare[] activePieces)
        {
            /////////////////  Check King moving  ///////////////////
            string colourKing = activePieces[0].piece.colour;  // index = 0 always the king
            int result = 2;

            Position v_To = new Position(0, 0);
            Move v_Move = new Move(0, 0, 0, 0);

            King king = (activePieces[0].piece as King);

            int xKing = activePieces[0].position.x,
                yKing = activePieces[0].position.y,
                xOponent=0, yOponent =0, flagCheck;

            v_Move.from = new Position(xKing, yKing);

            // checks if the king can move to a certain positions and doesn't fall out of board bounds
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    v_To.x = xKing + i;
                    v_To.y = yKing + j;

                    if (v_To.x >= 0 && v_To.x <= 7 && v_To.y >= 0 && v_To.y <= 7)   // checks bounds of board
                    {
                        v_Move.to = v_To;           // the valid new position "TO"

                        if (king.validateMove(v_Move, chBoard))
                        {
                            BoardSquare[,] boardClone = ChessBoard.GetClonedBoard(chBoard.board);

                            chBoard.getSquare(v_Move.to).piece = king;
                            chBoard.getSquare(v_Move.from).piece = null;

                            // this part is moving the king to the clone board to check if the new position is compromised by enemy pieces

                            flagCheck = IfPresentsCheck(chBoard, colourKing, ref xOponent, ref yOponent);
                            // if flagCheck = 0 (there is no check) ,1 (there is one check figure),2 (there is two check figures)
                            chBoard.board = ChessBoard.GetClonedBoard(boardClone);

                            if (flagCheck > 0)
                                result = 2;             // the king cant move any where to run away from the check
                            else
                            {
                                result = 1;             // the king can run away from 1 figure
                                goto EndKingMoving;
                            }
                        }
                    }
                }
            EndKingMoving:
            return result;      // either 1 or 2 becuase we came here with 1 check
        }
        // this function checks if is there any figure of ours that can move without exposing the king for check
        private static int IsStaleMate(ChessBoard chBoard, BoardSquare[] active)
        {
            ///////////////// if presents  stag check  ///////////////////
            ChessBoard ChB = new ChessBoard();
            ChB.board = ChessBoard.GetClonedBoard(chBoard.board);
            Piece pc;
            string p_colour = active[0].piece.colour;
            int result = -1;
            bool v_res = false;

            Position v_From = new Position(0, 0);
            Position v_To = new Position(0, 0);
            Move v_Move = new Move(0, 0, 0, 0);
            int Kx, Ky, i1, j1, Ox = 0, Oy = 0, m;

            v_Move.from = v_From;

            // we are checking all our figures if we can move without exposing our king to check
            for (int k = 0; k < active.Length; k++)
            {
                if (active[k] == null)
                    continue;

                pc = active[k].piece;
                Kx = active[k].position.x;
                Ky = active[k].position.y;
                v_From.x = Kx;
                v_From.y = Ky;
                v_Move.from = v_From;

                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (i == 0 && j == 0) continue;
                        v_To.x = Kx + i;
                        v_To.y = Ky + j;
                        if (!(v_To.x >= 0 && v_To.y >= 0 && v_To.x <= 7 && v_To.y <= 7))
                            continue;

                        // checking each figure if they can move and validation of each of them
                        if (pc is King)
                        {
                            v_Move.to = v_To;
                            if (!(pc as King).validateMove(v_Move, chBoard)) continue;
                        }
                        if (pc is Queen)
                        {
                            v_Move.to = v_To;
                            if (!(pc as Queen).validateMove(v_Move, chBoard)) continue;
                        }
                        else if (pc is Rook)
                        {
                            v_Move.to = v_To;
                            if (!(pc as Rook).validateMove(v_Move, chBoard)) continue;
                        }
                        else if (pc is Bishop)
                        {
                            v_Move.to = v_To;
                            if (!(pc as Bishop).validateMove(v_Move, chBoard)) continue;
                        }
                        else if (pc is Knight)
                        {
                            if (i == 0 || j == 0) continue;
                            else
                                for (int n = 1; n <= 2; n++)
                                {
                                    m = n == 1 ? 1 : 2;
                                    i1 = i * m;
                                    m = n == 1 ? 2 : 1;
                                    j1 = j * m;
                                    v_To.x = Kx + i1;
                                    v_To.y = Ky + j1;

                                    v_Move.to = v_To;
                                    v_res = (pc as Knight).validateMove(v_Move, chBoard);
                                    if (v_res) break;
                                }
                            if (!v_res) continue;
                        }
                        else if (pc is Pawn)
                        {
                            if (!(pc as Pawn).validateMove(v_Move, chBoard)) continue;
                        }


                        BoardSquare[,] boardClone = ChessBoard.GetClonedBoard(ChB.board);

                        ChB.getSquare(v_Move.to).piece = pc;
                        ChB.getSquare(v_Move.from).piece = null;

                        int flgCheck = IfPresentsCheck(ChB, p_colour, ref Ox, ref Oy);

                        ChB.board = ChessBoard.GetClonedBoard(boardClone);


                        if (flgCheck == 0)
                        {
                            result = 0;
                            return result;
                        }
                    }
                }
            }
            // we are returning the first figure that can move without exposing the king to check
            return result;
        }

        // function that checks if 2 boards are the same
        public static bool isEqualBoards(BoardSquare[,] board, BoardSquare[,] board1)
        {
            bool result=true;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    BoardSquare board_square = board[i, j];
                    BoardSquare board1_square = board1[i, j];

                    if (board_square.piece == null && board1_square.piece == null)
                        continue;
                    if (board_square.piece == null && board1_square.piece != null)
                    {
                        result = false;
                        break;
                    } 
                    if (board_square.piece != null && board1_square.piece == null)
                    {
                        result = false;
                        break;
                    }
                    if (board_square.piece != null && board1_square.piece != null)
                        if (
                           (board_square.piece.colour != board1_square.piece.colour) ||
                           (board_square.piece.weight != board1_square.piece.weight))
                        {
                            result = false;
                            break;
                        }
                }
            }
            return result;
        }
    }

}


