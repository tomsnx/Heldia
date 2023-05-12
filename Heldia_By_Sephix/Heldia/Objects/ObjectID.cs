namespace Heldia.Objects;
public static class ObjectId
{
    public enum e_ObjectId // Int by default
    {
        Player = ObjectId.Player,
        Block = ObjectId.Block,
        Tree = ObjectId.Tree,
        Grass = ObjectId.Grass,
        BaseGrass = ObjectId.BaseGrass
    }
    
    public const int Player = 0;
    public const int Grass = 1;
    public const int BaseGrass = 2;
    public const int Tree = 3;
    public const int Block = 4;
}
