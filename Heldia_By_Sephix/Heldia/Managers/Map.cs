using System;
using Heldia.Objects;
using static Heldia.Objects.ObjectId;

namespace Heldia.Managers;

public class Map
{
    private int[,] _map = new int[,]
    {
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        { 2, 2, 2, 2, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2},
        { 2, 2, 2, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        { 2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        { 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
    };

    // Tiles size in pixels
    public static int tileSize = 16;
    
    private int _mapScale;
    private Random _rand;

    // Constructor
    public Map(int mapScale = 4)
    {
        _mapScale = mapScale;
        _rand = new Random();
    }

    // Init
    public void Init(ObjectManager objMgr, Main g)
    {
        for (int line = 0; line < _map.GetLength(0); line++)
        {
            for (int column = 0; column < _map.GetLength(1); column++)
            {
                // Verify if the number (which is in array) are 
                if ((e_ObjectId)_map[line, column] == e_ObjectId.BaseGrass)
                {
                    BaseGrass grass = new BaseGrass(tileSize * _mapScale * column, tileSize * _mapScale * line);
                    grass.SetScale(_mapScale);
                    objMgr.Add(grass, g);
                } else if ((e_ObjectId)_map[line, column] == e_ObjectId.Grass)
                {
                    Grass grass = new Grass(tileSize * _mapScale * column, tileSize * _mapScale * line, _rand.Next(0, 4));
                    grass.SetScale(_mapScale);
                    objMgr.Add(grass, g);
                } else if ((e_ObjectId)_map[line, column] == e_ObjectId.Block)
                {
                    Block block = new Block(tileSize * _mapScale * column, tileSize * _mapScale * line);
                    block.SetScale(_mapScale);
                    objMgr.Add(block, g);
                } else if ((e_ObjectId)_map[line, column] == e_ObjectId.Tree)
                {
                    Tree tree = new Tree(tileSize * _mapScale * column, tileSize * _mapScale * line);
                    tree.SetScale(_mapScale);
                    objMgr.Add(tree, g);
                }
            }
        }
    }
}