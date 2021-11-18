using System;

namespace Chess1
{
    class Queen : Piece
    {
        public Queen (string color) :base(color)
        {
            this.name = "Q";
            this.weight = Piece.queen;
        }
        public override bool validateMove(Move move, ChessBoard board)
        {
            if (base.validateMove(move, board) == false)
                return false;           // base validation

            if ((Math.Abs(move.to.x - move.from.x) > 0) ^ (Math.Abs(move.to.y - move.from.y) > 0))                  // if queen the move is horiztal (or) vertical  (true && true = false)
            {
                if (Math.Abs(move.to.x - move.from.x) > 0)          // horizontal move
                {
                    int numberOfMoves = Math.Abs(move.to.x - move.from.x) - 1;
                    int movePosition = move.from.x;
                    int diraction = move.to.x - move.from.x > 0 ? 1 : -1;
                    for (int i = 0; i < numberOfMoves; i++)                                                         // checks in which diraction the queen is move up or down 
                    {                                                                                               // checks if every cell in the way is empty 
                        movePosition += diraction;
                        if (board.board[movePosition, move.from.y].piece != null)
                        {
                            return false;
                        }
                    }
                    return board.getSquare(move.to).piece == null || board.getSquare(move.to).piece.colour != colour;
                }
                if (Math.Abs(move.to.y - move.from.y) > 0)          // vertical move
                {
                    int numberOfMoves = Math.Abs(move.to.y - move.from.y) - 1;
                    int movePosition = move.from.y;
                    int diraction = move.to.y - move.from.y > 0 ? 1 : -1;
                    for (int i = 0; i < numberOfMoves; i++)                                                         // checks in which diraction the queen is move up or down 
                    {                                                                                               // checks if every cell in the way is empty 
                        movePosition += diraction;
                        if (board.board[move.from.x, movePosition].piece != null)
                        {
                            return false;
                        }
                    }

                    return board.getSquare(move.to).piece == null || board.getSquare(move.to).piece.colour != colour;
                }
            }
            else if (Math.Abs(move.to.x - move.from.x) == Math.Abs(move.to.y - move.from.y))                           // if the queen move is diognal 
            {
                int numberOfMoves = Math.Abs(move.to.x - move.from.x) - 1;
                int moveYPosition = move.from.y;
                int moveXPosition = move.from.x;
                int Xdiraction = move.to.x - move.from.x > 0 ? 1 : -1;
                int Ydiraction = move.to.y - move.from.y > 0 ? 1 : -1;
                for (int i = 0; i < numberOfMoves; i++)                                                             // checks in which diraction the queen is move up or down
                {                                                                                                   // checks if every cell in the way is empty 
                    moveXPosition += Xdiraction;
                    moveYPosition += Ydiraction;
                    if (board.board[moveXPosition, moveYPosition].piece != null)
                    {
                        return false;
                    }
                }
                return board.getSquare(move.to).piece == null || board.getSquare(move.to).piece.colour != colour;       // queen can move only if the diraction cell is empty or enenmy piece
            }
            return false;
        }
    }
}
