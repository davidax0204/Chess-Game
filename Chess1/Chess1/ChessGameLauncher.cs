using System;

namespace Chess1
{
    class ChessGameLauncher
    {
        static int Main(string[] args)
        {
            ChessGame game = new ChessGame();     // start the game
            game.play();
            return 0;
        }
    }

}
