using System;
namespace Chess1
{
    class Bishop :Piece   
    {
        public Bishop (string color) :base(color)
        {
            this.name = "B";
            weight = Piece.bishop; 
        }
        public override bool validateMove(Move move, ChessBoard board)   // move validation for class bishop which can
        {                                                                // move only diagonaly back and foward 
            if (base.validateMove(move, board) == false)
                return false;                                            // base validation

            if (Math.Abs(move.to.x - move.from.x) == Math.Abs(move.to.y - move.from.y))     // check if the move is diagonal or not
            {

                int numberOfMoves = Math.Abs(move.to.x - move.from.x) - 1;                  // checks how many cells the figure is moving       
                int moveYPosition = move.from.y;
                int moveXPosition = move.from.x;
                int Xdir = move.to.x - move.from.x > 0 ? 1 : -1;
                int Ydir = move.to.y - move.from.y > 0 ? 1 : -1;
                for (int i = 0; i < numberOfMoves; i++)                                     // the for loop is checking if the cells of the 
                {                                                                           // Bishop are all empty and if not so its invalid
                    moveXPosition += Xdir;
                    moveYPosition += Ydir;
                    if (board.board[moveXPosition, moveYPosition].piece != null)
                    {
                        return false;
                    }
                }
                return board.getSquare(move.to).piece == null || board.getSquare(move.to).piece.colour != colour;       // checks if the final destination of the bishop is an enemy figure 
            }                                                                                                           // or just empty cell (if one of them is true so the move is valid)
            return false;
        }
    }
}
