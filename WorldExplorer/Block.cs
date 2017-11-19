namespace WorldExplorer
{
    struct Block
    {
        byte x;
        byte y;
        byte z;
        byte id;
        byte type;

        public Block(byte x, byte y, byte z, byte id, byte type)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.id = id;
            this.type = type;
        }
    }
}
