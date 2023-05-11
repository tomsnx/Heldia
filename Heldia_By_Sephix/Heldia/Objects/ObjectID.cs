namespace Heldia.Objects;
public static class ObjectId
{
    public enum e_ObjectId // Int by default
    {
        Player = ObjectId.Player,
        Block = ObjectId.Block,
        Tree = ObjectId.Tree,
        Grass01 = ObjectId.Grass01,
        Grass02 = ObjectId.Grass02
    }
    
    public const int Player = 0;
    public const int Grass01 = 1;
    public const int Grass02 = 2;
    public const int Tree = 3;
    public const int Block = 4;
    
    // Folder list
    public static readonly int[] GrassTab = {Grass01, Grass02};
}
