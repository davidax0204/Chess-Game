using System;
using System.Drawing;
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

        public ChessBoard()
        {

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        board[i, j] = new BoardSquare("BLACK",new Position(i,j));
                    }
                    else
                    {
                        board[i, j] = new BoardSquare("WHITE", new Position(i, j));
                    }
                }
            }
            this.PlacePieces();
        }

        private void PlacePieces_()
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
        private void PlacePieces1()
        {
            for (int i = 0; i < 8; i++)
            {
                //board[i, 1].piece = new Pawn("WHITE");
                //board[i, 6].piece = new Pawn("BLACK");
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
            board[0, 6].piece = new Pawn("WHITE");

            board[1, 7].piece = new Knight("BLACK");
            board[2, 7].piece = new Bishop("BLACK");
            board[3, 7].piece = new Queen("BLACK");
            board[4, 7].piece = new King("BLACK");
            board[5, 7].piece = new Bishop("BLACK");
            board[6, 7].piece = new Knight("BLACK");
            board[7, 7].piece = new Rook("BLACK");
        }
        private void PlacePieces()
        {
            for (int i = 0; i < 8; i++)
            {
                //board[i, 1].piece = new Pawn("WHITE");
                //board[i, 6].piece = new Pawn("BLACK");
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
        public static void Print(BoardSquare[,] board)
        {
            string print = "";
            string print_ = "";
            //Console.WriteLine("      A      B      C      D      E      F      G      H     ");

            Console.WriteLine("           A(0)   B(1)   C(2)   D(3)   E(4)   F(5)   G(6)   H(7)   ");
            Console.WriteLine();
            for (int j = 7; j >= 0; j--)
            {
                string printCell = "";
                string printCell_ = "";
                print = " " + (j + 1) + "  ("+j+")  ";
                print_ = "         ";

                for (int i = 0; i < 8; i++)
                {
                    if (board[i, j].piece != null)
                    {
                        printCell = printCell + "[ " + board[i, j].piece.GetColorPiece() + " " + board[i, j].piece.name + " ]";
                        printCell_ = printCell_ + "[_____]";
                        //Console.Write( "[ " + board[i, j].piece.GetColorPiece() + " "+ board[i, j].piece.name + " ]");
                    }
                    else
                    {
                        printCell = printCell + "[ ... ]";
                        printCell_ = printCell_ + "[_____]";
                        //Console.Write("[ ... ]");
                    }
                }
                print_ = print_ + printCell_;
                print = print + printCell;
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
        public Move getMoveInput(Player player)
        {
            Console.WriteLine("'{0}' player, please insert 'From' cordinations in this pattern 'a4'", player.colorName);
            String moveFrom = Console.ReadLine();

            if (inputValidation(moveFrom) == false)
            {
                Console.WriteLine("'From' Input was invalid, please try again");
                return getMoveInput(player);
            }

            Console.WriteLine("'{0}' player, please insert 'To' cordinations in this pattern 'a4'", player.colorName);
            String moveTo = Console.ReadLine();

            if (inputValidation(moveTo) == false)
            {
                Console.WriteLine("'To' Input was invalid, please try again");
                return getMoveInput(player);
            }





            return convertMove(moveFrom, moveTo);
        }
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
        public static string inputValidationPromotion (string input)
        {
            string str = input.Replace(" ", "");
            str = str.ToUpper();
            if (str.Length != 1)
                return "";
            if (str !=  "K" && str != "B" && str != "N" && str != "Q")
                return "";

            return str;
        }
        private Move convertMove(string moveFrom, string moveTo)
        {
            int fromX = this.patternUpper.IndexOf(moveFrom[0].ToString().ToUpper());
            int fromY = int.Parse("" + moveFrom[1]) - 1;

            int toX = this.patternUpper.IndexOf(moveTo[0].ToString().ToUpper());
            int toY = int.Parse("" + moveTo[1]) - 1;


            //int x1 = moveFrom[0] - 97;
            //int y1 =  (int)char.GetNumericValue(moveFrom[1]) - 1;


            //int x2 = moveInput[3] - 97;
            //int y2 = (int)char.GetNumericValue(moveInput[4]) - 1;


            Move pos = new Move(fromX, fromY, toX, toY);
            return pos;
        }
        public BoardSquare getSquare(Position position)
        {
            return board[position.x, position.y];
        }
        public static BoardSquare[] getPiecesByColor(BoardSquare[,] board, string colour)
        {
            BoardSquare[] result = new BoardSquare[16];
            int cnt = 1;
            for (int j = 7; j >= 0; j--)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (board[i, j].piece != null && board[i,j].piece.colour==colour)
                    {
                        if (board[i,j].piece.weight != Piece.king)
                            result[cnt++] = board[i, j];
                        else
                            result[0] = board[i, j];
                    }
                }
            }
            return result;
        }
        public static bool CheckMate(BoardSquare[,] board, BoardSquare[] p_WhiteP, BoardSquare[] p_BlackP, string p_colour)
        {

            bool result = false;
            Position Kp = (p_colour == "BLACK" ? p_BlackP[0].position : p_WhiteP[0].position);
            BoardSquare[] opponent = new BoardSquare[32];
            BoardSquare[] active = new BoardSquare[32];
            BoardSquare[] opnt = p_colour == "BLACK" ? p_WhiteP : p_BlackP;
            BoardSquare[] act = p_colour == "BLACK" ? p_BlackP : p_WhiteP;
            int step_x, step_y,
                Kx = Kp.x,
                Ky = Kp.y;

            //Extract dangeries Piaces from opponent
            int fig, Ox, Oy, ox, oy;
            for (int i = 0; i < opnt.Length; i++)
            {
                if (opnt[i] == null || opnt[i].piece == null) continue;

                Ox = opnt[i].position.x;
                Oy = opnt[i].position.y;

                fig = opnt[i].piece.weight;

                if
                (
                    fig == Piece.king &&
                    (Math.Abs(Kx - Ox) < 2 && Math.Abs(Ky - Oy) <= 2) ||
                    (Math.Abs(Kx - Ox) <= 2 && Math.Abs(Ky - Oy) < 2)
                )
                    return true;

                else if
                (
                    fig == Piece.knight &&
                    (Math.Abs(Kx - Ox) == 2 && Math.Abs(Ky - Oy) == 1) ||
                    (Math.Abs(Kx - Ox) == 1 && Math.Abs(Ky - Oy) == 2)
                )
                    return true;

                else if
                (fig == Piece.pawn && Math.Abs(Kx - Ox) == 1 && Math.Abs(Ky - Oy) == 1)
                    return true;

                else if (fig == Piece.knight || fig == Piece.pawn || fig == Piece.king)
                    continue;

                else if (fig == Piece.rook && Math.Abs(Kx - Ox) != 0 && Math.Abs(Ky - Oy) != 0)
                    continue;

                else if (fig == Piece.bishop && Math.Abs(Kx - Ox) != Math.Abs(Ky - Oy))
                    continue;

                else if (fig == Piece.queen && Math.Abs(Kx - Ox) != Math.Abs(Ky - Oy)
                    && Math.Abs(Kx - Ox) != 0 && Math.Abs(Ky - Oy) != 0)
                    continue;

                step_x = Math.Sign(Ox - Kx);
                step_y = Math.Sign(Oy - Ky); 
                ox = Kx + step_x;
                oy = Ky + step_y;

                while (Math.Abs(ox) < 8 && Math.Abs(oy) < 8 && Math.Abs(ox) >= 0 && Math.Abs(oy) >= 0)
                {
                    if (board[ox, oy].piece != null)
                    {
                        if (ox == Ox && oy == Oy)
                            return true;
                        else if (board[ox, oy].piece.colour == p_colour)
                            return false;
                        else
                        {
                            for (int j = 0; j < opnt.Length; j++)
                            {
                                if (opnt[j] != null && opnt[j].position.x == ox && opnt[j].position.y == oy)
                                {
                                    if ((fig == Piece.queen || fig == Piece.rook) &&
                                    opnt[j].piece.weight != Piece.queen && opnt[j].piece.weight != Piece.rook)
                                        return false;
                                    else if ((fig == Piece.queen || fig == Piece.bishop) &&
                                    opnt[j].piece.weight != Piece.queen && opnt[j].piece.weight != Piece.bishop)
                                        return false;
                                    else
                                        return true;
                                }
                            }
                        }
                    }
                    ox = ox + step_x;
                    oy = oy + step_y;
                }
            }
            return result;
        }

        public static int CheckMate(ChessBoard chBoard, string p_colour, bool Shield)
        {

            int result;
            BoardSquare[,] board = chBoard.board;
            BoardSquare[,] BordBackup, TempBackup;
            BoardSquare[] v_WhiteP, v_BlackP;

            BordBackup = ChessBoard.GetClonedBoard(board);
            v_WhiteP = ChessBoard.getPiecesByColor(board, "WHITE");
            v_BlackP = ChessBoard.getPiecesByColor(board, "BLACK");


            Position Kp = (p_colour == "BLACK" ? v_BlackP[0].position : v_WhiteP[0].position);
            BoardSquare[] opponent = p_colour == "BLACK" ? v_WhiteP : v_BlackP;
            BoardSquare[] active = p_colour == "BLACK" ? v_BlackP : v_WhiteP;
            result = 0;

            int step_x, step_y,
                Kx = Kp.x,
                Ky = Kp.y,
                Ox = 0,
                Oy = 0;
            //Extract dangeries Piaces from opponent
            int fig, ox, oy, k = 0;
            for (int i = 0; i < opponent.Length; i++)
            {

                if (result == 2)
                    return result;

                if (opponent[i] == null || opponent[i].piece == null) continue;

                Ox = opponent[i].position.x;
                Oy = opponent[i].position.y;
                fig = opponent[i].piece.weight;

                if
                (
                    fig == Piece.king &&
                    (Math.Abs(Kx - Ox) < 2 && Math.Abs(Ky - Oy) <= 2) ||
                    (Math.Abs(Kx - Ox) <= 2 && Math.Abs(Ky - Oy) < 2)
                )
                {
                    result = -100;
                    return result;
                }

                else if
                (
                    fig == Piece.knight &&
                    (Math.Abs(Kx - Ox) == 2 && Math.Abs(Ky - Oy) == 1) ||
                    (Math.Abs(Kx - Ox) == 1 && Math.Abs(Ky - Oy) == 2)
                )
                    ++result;

                else if
                (fig == Piece.pawn && Math.Abs(Kx - Ox) == 1 && Math.Abs(Ky - Oy) == 1)
                    ++result;

                else if (fig == Piece.knight || fig == Piece.pawn || fig == Piece.king)
                    continue;

                else if (fig == Piece.rook && Math.Abs(Kx - Ox) != 0 && Math.Abs(Ky - Oy) != 0)
                    continue;

                else if (fig == Piece.bishop && Math.Abs(Kx - Ox) != Math.Abs(Ky - Oy))
                    continue;

                else if (fig == Piece.queen && Math.Abs(Kx - Ox) != Math.Abs(Ky - Oy)
                    && Math.Abs(Kx - Ox) != 0 && Math.Abs(Ky - Oy) != 0)
                    continue;

                step_x = Math.Sign(Ox - Kx);
                step_y = Math.Sign(Oy - Ky);
                ox = Kx + step_x;
                oy = Ky + step_y;

                while (Math.Abs(ox) < 8 && Math.Abs(oy) < 8 && Math.Abs(ox) >= 0 && Math.Abs(oy) >= 0)
                {
                    if (board[ox, oy].piece != null)
                    {
                        if (ox == Ox && oy == Oy)
                            goto NewOxOy;
                        else if (board[ox, oy].piece.colour == p_colour)
                        {
                            result = 0;
                            return result;
                        }
                        else
                        {
                            for (int j = 0; j < opponent.Length; j++)
                            {
                                // Going by the "traectoria" of the Check
                                if (opponent[j] != null && opponent[j].position.x == ox && opponent[j].position.y == oy)
                                {
                                    if (fig == Piece.queen && (step_x == 0 || step_y == 0) &&
                                        opponent[j].piece.weight == Piece.rook
                                       ) goto NewOxOy;
                                    if (fig == Piece.rook && (step_x == 0 || step_y == 0) &&
                                        opponent[j].piece.weight == Piece.queen
                                       ) goto NewOxOy;
                                    else if (fig == Piece.queen && step_x != 0 && step_y != 0 &&
                                        opponent[j].piece.weight == Piece.bishop
                                       ) goto NewOxOy;
                                    else if (fig == Piece.bishop && step_x != 0 && step_y != 0 &&
                                        opponent[j].piece.weight == Piece.queen
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
                    Ox = ox;
                    Oy = oy;
                    ++result;
                }
            }
            if (! Shield  || result == 2 )            /////////////////  find Check Shield  /////////////////// 

                return result;

            /////////////////  find Check Shield  /////////////////// 
            BoardSquare Asqu = null;
            Position v_Fom = new Position(0, 0);
            Position v_To = new Position(0, 0);
            Move v_Move = new Move(0, 0, 0, 0);
            int ifcheck;
            bool v_shield = false;

            step_x = Math.Sign(Ox - Kx);
            step_y = Math.Sign(Oy - Ky);
            ox = Kx + step_x;
            oy = Ky + step_y;
            while (ox <= Ox && oy <= Oy)
            {
                for (int i = 0; i < active.Length; i++)
                {
                    Asqu = active[i];
                    v_Fom.x = Asqu.position.x;
                    v_Fom.y = Asqu.position.y;
                    v_To.x = ox;
                    v_To.y = oy;
                    v_Move.from = v_Fom;
                    v_Move.to = v_To;
                    if (Asqu.piece.weight == Piece.knight)
                        v_shield = (active[i].piece as Knight).validateMove(v_Move, chBoard);
                    else if (Asqu.piece.weight == Piece.bishop)
                        v_shield = (active[i].piece as Bishop).validateMove(v_Move, chBoard);
                    else if (Asqu.piece.weight == Piece.rook)
                        v_shield = (active[i].piece as Rook).validateMove(v_Move, chBoard);
                    else if (Asqu.piece.weight == Piece.queen)
                        v_shield = (active[i].piece as Queen).validateMove(v_Move, chBoard);
                    else if (Asqu.piece.weight == Piece.pawn)
                        v_shield = (active[i].piece as Pawn).validateMove(v_Move, chBoard);
                    if (v_shield)
                        return result;
                }
                ox = Kx + step_x;
                oy = Ky + step_y;
            }
            TempBackup = ChessBoard.GetClonedBoard(chBoard.board);
            chBoard.getSquare(v_Move.to).piece = Asqu.piece;
            chBoard.getSquare(v_Move.from).piece = null;
            ifcheck = CheckMate(chBoard, p_colour, false);
            chBoard.board = ChessBoard.GetClonedBoard(TempBackup)
            
            if (ifcheck != 0 && ! v_shield) 
                ++result;
            return result;
        }


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
    }
}
