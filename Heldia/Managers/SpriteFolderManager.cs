using System;
using System.Collections.Generic;
using System.Text;

namespace Heldia
{
    public class SpriteFolderManager
    {
        enum e_IDFile : int
        {
            characters = ObjectID.player,
            block = ObjectID.block,
            tree = ObjectID.tree
        }

        e_IDFile id;

        public SpriteFolderManager() { }


        // get
        public string getSFManager(int id)
        {
            this.id = (e_IDFile)id;

            //determine the folder of the texture
            if (this.id == e_IDFile.characters)
            {
                return "characters";
            }
            if (this.id == e_IDFile.block)
            {
                return "world";
            }
            if (this.id == e_IDFile.tree)
            {
                return "world";
            }

            return "";
        }

    }
}
