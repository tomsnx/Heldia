namespace Heldia.Objects;
public static class ObjectId
{
    public enum EObjectId // Int by default
    {
        Player = ObjectId.Player,
        LifeBar = ObjectId.LifeBar,
        StaminaBar = ObjectId.StaminaBar,
        Block = ObjectId.Block,
        Tree = ObjectId.Tree,
        Grass = ObjectId.Grass,
        BaseGrass = ObjectId.BaseGrass
    }
    
    public const int Player = 0;
    public const int LifeBar = 10;
    public const int StaminaBar = 11;
    public const int Grass = 1;
    public const int BaseGrass = 2;
    public const int Tree = 3;
    public const int Block = 4;
}
