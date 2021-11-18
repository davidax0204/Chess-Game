using System;


namespace Chess1
{
    // Position class is creating the cords of a ceration cell in the board with x position and y positon
    class Position                              
    {
        public int x;                           
        public int y;                            

        public Position (int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
