using System;
using Heldia.Managers;
using Heldia.Objects;
using static Heldia.Objects.ObjectId;

namespace Heldia.Engine;

public class Map
{
    private int _sizeX = 50;
    private int _sizeY = 50;
    private int[,] _map =
    {
        {2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        {2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        {2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        {2, 2, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        {2, 2, 2, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        {2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
        {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
    };

    // Tiles size in pixels
    public static int tileSize = 16;
    
    private int _mapScale;
    private Random _rand;

    // Constructor
    public Map(int mapScale = 4)
    {
        //_map = new int[_sizeX, _sizeY];
        _mapScale = mapScale;
        _rand = new Random();

        /*for (int i = _sizeX - 1; i >= 0; i--)
        {
            for (int j = _sizeY - 1; j >= 0; j--)
            {
                _map[i, j] = 2;
            }
        }*/
    }

    // Init
    public void Init(ObjectManager objMgr, Main g)
    {
        for (int line = 0; line < _map.GetLength(0); line++)
        {
            for (int column = 0; column < _map.GetLength(1); column++)
            {
                // Verify if the number (which is in array) are 
                if ((EObjectId)_map[line, column] == EObjectId.BaseGrass)
                {
                    BaseGrass grass = new BaseGrass(tileSize * _mapScale * column, tileSize * _mapScale * line);
                    grass.SetScale(_mapScale);
                    objMgr.Add(grass, g);
                } else if ((EObjectId)_map[line, column] == EObjectId.Grass)
                {
                    Grass grass = new Grass(tileSize * _mapScale * column, tileSize * _mapScale * line, _rand.Next(0, 4));
                    grass.SetScale(_mapScale);
                    objMgr.Add(grass, g);
                } else if ((EObjectId)_map[line, column] == EObjectId.Block)
                {
                    Block block = new Block(tileSize * _mapScale * column, tileSize * _mapScale * line);
                    block.SetScale(_mapScale);
                    objMgr.Add(block, g);
                } else if ((EObjectId)_map[line, column] == EObjectId.Tree)
                {
                    Tree tree = new Tree(tileSize * _mapScale * column, tileSize * _mapScale * line);
                    tree.SetScale(_mapScale);
                    objMgr.Add(tree, g);
                }
            }
        }
    }
}