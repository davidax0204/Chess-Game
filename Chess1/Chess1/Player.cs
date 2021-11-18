using System;


namespace Chess1
{
    class Player
    {
        public string colorName;            // the class Player represnting the active player that playing 
        public bool isActive;
        public Player(string colorname, bool isActive)
        {
            this.colorName = colorname;
            this.isActive = isActive;
        }
    }
}
