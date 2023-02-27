using System;
using System.Collections.Generic;
using System.Text;

namespace Heldia;
public class SpriteFolderManager
{
    enum e_IdFile : int
    {
        Characters = ObjectID.Player,
        Block = ObjectID.Block,
        Tree = ObjectID.Tree
    }

    private e_IdFile _id;

    // get
    public string GetSfManager(int id)
    {
        _id = (e_IdFile)id;

        foreach (var item in ObjectID.GrassTab)
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
