using System;

namespace Heldia;

public class Map
{
    private int[,] _map = new int[,]
    {
        { 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 1},
        { 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 1},
        { 1, 1, 2, 2, 2, 1, 1, 2, 1, 1, 2, 2, 2, 2, 2, 1},
        { 1, 1, 2, 2, 2, 1, 1, 2, 1, 1, 2, 2, 2, 2, 2, 1},
        { 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 1},
        { 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 1},
        { 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 1},
        { 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 1},
        { 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 1},
        { 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 1},
    };

    // Tiles size in pixels
    private int _mapScale;
    private int _tileSize = 64;

    // Constructor
    public Map(int mapScale = 1)
    {
        _mapScale = mapScale;
    }

    // Init
    public void Init(ObjectManager objMgr, Main g)
    {
        for (int line = 0; line < _map.GetLength(0); line++)
        {
            for (int column = 0; column < _map.GetLength(1); column++)
            {
                // Verify if the number (which is in array) are 
                if ((ObjectID.e_ObjectId)_map[line, column] == ObjectID.e_ObjectId.Grass01)
                {
                    Grass01 grass = new Grass01(_tileSize * _mapScale * column, _tileSize * _mapScale * line);
                    grass.SetScale(_mapScale);
                    objMgr.Add(grass, g);
                } else if ((ObjectID.e_ObjectId)_map[line, column] == ObjectID.e_ObjectId.Grass02)
                {
                    Grass02 grass = new Grass02(_tileSize * _mapScale * column, _tileSize * _mapScale * line);
                    grass.SetScale(_mapScale);
                    objMgr.Add(grass, g);
                } else if ((ObjectID.e_ObjectId)_map[line, column] == ObjectID.e_ObjectId.Block)
                {
                    Block block = new Block(_tileSize * _mapScale * column, _tileSize * _mapScale * line);
                    block.SetScale(_mapScale);
                    objMgr.Add(block, g);
                } else if ((ObjectID.e_ObjectId)_map[line, column] == ObjectID.e_ObjectId.Tree)
                {
                    Tree tree = new Tree(_tileSize * _mapScale * column, _tileSize * _mapScale * line);
                    tree.SetScale(_mapScale);
                    objMgr.Add(tree, g);
                }
            }
        }
    }
}