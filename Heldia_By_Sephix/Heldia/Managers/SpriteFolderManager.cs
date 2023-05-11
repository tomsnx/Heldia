using Heldia.Objects;

namespace Heldia.Managers;
public class SpriteFolderManager
{
    enum e_IdFile // Int by default
    {
        Characters = ObjectId.Player,
        Block = ObjectId.Block,
        Tree = ObjectId.Tree
    }

    private e_IdFile _id;

    // get
    public string GetSfManager(int id)
    {
        _id = (e_IdFile)id;

        foreach (var item in ObjectId.GrassTab)
        {
            if (id == item)
            {
                return "world";
            }
        }

        //determine the folder of the texture
        if (_id == e_IdFile.Characters)
        {
            return "characters";
        }
        else if (_id == e_IdFile.Tree)
        {
            return "world";
        }
        else if (_id == e_IdFile.Block)
        {
            return "world";
        }
        else
        {
            return "";
        }
    }
}
