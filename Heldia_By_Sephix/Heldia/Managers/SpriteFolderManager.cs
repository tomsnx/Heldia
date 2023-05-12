using Heldia.Objects;

namespace Heldia.Managers;
public class SpriteFolderManager
{
    enum e_IdFile // Int by default
    {
        Characters = ObjectId.Player,
        Block = ObjectId.Block,
        Tree = ObjectId.Tree,
        Grass = ObjectId.Grass,
        BaseGrass = ObjectId.BaseGrass
    }

    private e_IdFile _id;

    // get
    public string GetSfManager(int id)
    {
        _id = (e_IdFile)id;

        //determine the folder of the texture
        if (_id == e_IdFile.Characters)
        {
            return "characters";
        }
        else if (_id == e_IdFile.Grass)
        {
            return "world";
        }
        else if (_id == e_IdFile.BaseGrass)
        {
            return "world";
        }
        else
        {
            return "";
        }
    }
}
