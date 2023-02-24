using System;
using System.Collections.Generic;
using System.Text;

namespace Heldia;
public class SpriteFolderManager
{
    enum e_IdFile : int
    {
        Characters = ObjectID.player,
        Block = ObjectID.block,
        Tree = ObjectID.tree
    }

    e_IdFile _id;

    // get
    public string GetSfManager(int id)
    {
        _id = (e_IdFile)id;

        //determine the folder of the texture
        if (_id == e_IdFile.Characters)
        {
            return "characters";
        }
        else if (_id == e_IdFile.Block)
        {
            return "world";
        }
        else if (_id == e_IdFile.Tree)
        {
            return "world";
        }
        else
        {
            return "";
        }
    }
}
