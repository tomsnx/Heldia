using Heldia.Engine.Enum;
using Heldia.Objects;

namespace Heldia.Managers;
public class SpriteFolderManager
{
    enum EIdFile // Int by default
    {
        Characters = EObjectId.Player,
        Block = EObjectId.Block,
        Tree = EObjectId.Tree,
        Grass = EObjectId.Grass,
        BaseGrass = EObjectId.BaseGrass
    }

    private EIdFile _id;

    // get
    public string GetSfManager(int id)
    {
        _id = (EIdFile)id;

        //determine the folder of the texture
        if (_id == EIdFile.Characters)
        {
            return "characters";
        }
        else if (_id == EIdFile.Grass)
        {
            return "world";
        }
        else if (_id == EIdFile.BaseGrass)
        {
            return "world";
        }
        else
        {
            return "";
        }
    }
}
