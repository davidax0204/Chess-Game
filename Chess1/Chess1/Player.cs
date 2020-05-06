using System;


namespace Chess1
{
    class Player
    {
        public string colorName;
        public bool isActive;
        public Player(string colorname, bool isActive)
        {
            this.colorName = colorname;
            this.isActive = isActive;
        }
    }
}
