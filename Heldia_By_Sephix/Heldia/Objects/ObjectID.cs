namespace Heldia;
public static class ObjectID
{
    public enum e_ObjectId : int
    {
        Player = ObjectID.Player,
        Block = ObjectID.Block,
        Tree = ObjectID.Tree,
        Grass01 = ObjectID.Grass01,
        Grass02 = ObjectID.Grass02
    }
    
    public const int Player = 0;
    public const int Grass01 = 1;
    public const int Grass02 = 2;
    public const int Tree = 3;
    public const int Block = 4;
    
    // Folder list
    public static readonly int[] GrassTab = {Grass01, Grass02};
}
