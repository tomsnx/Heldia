using System;
using Heldia.Managers;
using Heldia.Objects;
using Microsoft.Xna.Framework;
using static Heldia.Engine.Singleton.GameManager;
using static Heldia.Objects.ObjectId;

namespace Heldia.Engine;

public class Map
{
    private static int _sizeX = 250;
    private static int _sizeY = 250;

    private GameObject[,] _map;
    private float[,] _noiseMap = new float[_sizeX, _sizeY];

    // Tiles size in pixels
    public static int tileSize = 32;

    private Random _rand;
    
    private static int[] _permutation = { 151, 160, 137, 91, 90, 15, 131, 13, 201, 95, 96, 53, 194, 233, 
                                            7, 225, 140, 36, 103, 30, 69, 142, 8, 99, 37, 240, 21, 10, 23, 
                                            190, 6, 148, 247, 120, 234, 75, 0, 26, 197, 62, 94, 252, 219, 
                                            203, 117, 35, 11, 32, 57, 177, 33, 88, 237, 149, 56, 87, 174, 
                                            20, 125, 136, 171, 168, 68, 175, 74, 165, 71, 134, 139, 48, 27, 
                                            166, 77, 146, 158, 231, 83, 111, 229, 122, 60, 211, 133, 230, 220, 
                                            105, 92, 41, 55, 46, 245, 40, 244, 102, 143, 54, 65, 25, 63, 161, 
                                            1, 216, 80, 73, 209, 76, 132, 187, 208, 89, 18, 169, 200, 196, 135, 
                                            130, 116, 188, 159, 86, 164, 100, 109, 198, 173, 186, 3, 64, 52, 217, 
                                            226, 250, 124, 123, 5, 202, 38, 147, 118, 126, 255, 82, 85, 212, 207, 
                                            206, 59, 227, 47, 16, 58, 17, 182, 189, 28, 42, 223, 183, 170, 213, 119, 
                                            248, 152, 2, 44, 154, 163, 70, 221, 153, 101, 155, 167, 43, 172, 9, 129, 
                                            22, 39, 253, 19, 98, 108, 110, 79, 113, 224, 232, 178, 185, 112, 104, 218, 
                                            246, 97, 228, 251, 34, 242, 193, 238, 210, 144, 12, 191, 179, 162, 241, 81, 
                                            51, 145, 235, 249, 14, 239, 107, 49, 192, 214, 31, 181, 199, 106, 157, 184, 
                                            84, 204, 176, 115, 121, 50, 45, 127, 4, 150, 254, 138, 236, 205, 93, 222, 
                                            114, 67, 29, 24, 72, 243, 141, 128, 195, 78, 66, 215, 61, 156, 180 };

    // Constructor
    public Map(int mapScale)
    {
        //_map = new int[_sizeX, _sizeY];
        Instance.MapScale = mapScale;
        Instance.TileSize = tileSize;
        _rand = new Random();
    }

    // Init
    public void Init(ObjectManager objMgr, Main g)
    {
        _noiseMap = GeneratePerlinMap(_sizeX, _sizeY, 1f);
        _noiseMap = Remap(_noiseMap, 0, 255);
        _map = GenerateGameObjects(_noiseMap);

        for (int i = 0; i < _map.GetLength(0); i++)
        {
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                objMgr.Add(_map[i,j], g);
            }
        }
    }

    public void Update(GameTime gt, Main g)
    {
        // Convertir la position du joueur en coordonnées de la grille
        int playerTileX = (int)Math.Floor(Instance.PlayerX / (tileSize * Instance.MapScale));
        int playerTileY = (int)Math.Floor(Instance.PlayerY / (tileSize * Instance.MapScale));

        // Désactiver toutes les tiles
        for (int x = 0; x < _sizeX; x++)
        {
            for (int y = 0; y < _sizeY; y++)
            {
                _map[x, y].Active = false;
            }
        }

        // Activer les tiles autour de la caméra du joueur
        ActivateTilesAroundCamera(playerTileX, playerTileY);
    }
    
    private void ActivateTilesAroundCamera(int playerTileX, int playerTileY)
    {
        int cameraLeftTile = playerTileX - (Instance.Camera.CameraBounds.Width / tileSize / Instance.MapScale) / 2 - 2;
        int cameraTopTile = playerTileY - (Instance.Camera.CameraBounds.Height / tileSize / Instance.MapScale) / 2 - 2;
        int cameraRightTile = playerTileX + (Instance.Camera.CameraBounds.Width / tileSize / Instance.MapScale) / 2 + 4;
        int cameraBottomTile = playerTileY + (Instance.Camera.CameraBounds.Height / tileSize / Instance.MapScale) / 2 + 4;

        int minX = Math.Max(cameraLeftTile, 0);
        int maxX = Math.Min(cameraRightTile, _sizeX - 1);
        int minY = Math.Max(cameraTopTile, 0);
        int maxY = Math.Min(cameraBottomTile, _sizeY - 1);

        // Active les tuiles dans la zone définie par la caméra
        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                _map[x, y].Active = true;
            }
        }
    }
    
    private GameObject[,] GenerateGameObjects(float[,] noiseMap)
    {
        GameObject[,] map = new GameObject[_sizeX, _sizeY];

        for (int x = 0; x < _sizeX; x++)
        {
            for (int y = 0; y < _sizeY; y++)
            {
                float noiseValue = noiseMap[x, y];

                // Déterminez le type d'objet en fonction de la valeur de bruit
                EObjectId objectType = DetermineObjectType(noiseValue);

                // Créez le GameObject correspondant et placez-le dans le tableau map
                map[x, y] = CreateGameObject(objectType, x, y);
            }
        }

        return map;
    }
    
    private EObjectId DetermineObjectType(float noiseValue)
    {
        // Implémentez ici votre propre logique pour déterminer le type d'objet en fonction de la valeur de bruit
        // Vous pouvez utiliser des seuils pour classer les valeurs de bruit en catégories et assigner les types d'objets correspondants.
        // Par exemple :
        if (noiseValue < 5f)
        {
            return EObjectId.BaseGrass;
        }
        else if (noiseValue < 100f)
        {
            return EObjectId.Grass;
        }
        else
        {
            return EObjectId.BaseGrass;
        }
    }
    
    private GameObject CreateGameObject(EObjectId objectType, int x, int y)
    {
        GameObject obj = null;
        
        if (objectType == EObjectId.BaseGrass)
        {
            BaseGrass grass = new BaseGrass(tileSize * Instance.MapScale * x, tileSize * Instance.MapScale * y);
            grass.SetScale(Instance.MapScale);
            obj = grass;
        } else if (objectType == EObjectId.Grass)
        {
            Grass grass = new Grass(tileSize * Instance.MapScale * x, tileSize * Instance.MapScale * y, _rand.Next(0, 6));
            grass.SetScale(Instance.MapScale);
            obj = grass;
        }
        else
        {
            Grass grass = new Grass(tileSize * Instance.MapScale * x, tileSize * Instance.MapScale * y, _rand.Next(0, 6));
            grass.SetScale(Instance.MapScale);
            obj = grass;
        }

        return obj;
    }

    public float[,] GeneratePerlinMap(int width, int height, float scale)
    {
        float[,] perlinMap = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float sampleX = (float)x / width * scale;
                float sampleY = (float)y / height * scale;

                float noiseValue = GeneratePerlinNoise(sampleX, sampleY);

                perlinMap[x, y] = noiseValue;
            }
        }

        return perlinMap;
    }

    private float GeneratePerlinNoise(float x, float y)
    {
        int x0 = (int)x;
        int x1 = x0 + 1;
        int y0 = (int)y;
        int y1 = y0 + 1;

        float sx = x - x0;
        float sy = y - y0;

        float n0 = DotGridGradient(x0, y0, x, y);
        float n1 = DotGridGradient(x1, y0, x, y);
        float ix0 = Lerp(n0, n1, sx);

        n0 = DotGridGradient(x0, y1, x, y);
        n1 = DotGridGradient(x1, y1, x, y);
        float ix1 = Lerp(n0, n1, sx);

        float value = Lerp(ix0, ix1, sy);
        return value;
    }

    private float DotGridGradient(int ix, int iy, float x, float y)
    {
        float dx = x - ix;
        float dy = y - iy;

        int index = _permutation[(ix + _permutation[iy % _permutation.Length]) % _permutation.Length];

        float random = (float)((index & 1) == 0 ? dx : dy);
        float gradient = (float)((index & 2) == 0 ? -dx : -dy);

        return random + gradient;
    }

    private float Lerp(float a, float b, float t)
    {
        return a + t * (b - a);
    }
    
    public float InverseLerp(float a, float b, float value)
    {
        return (value - a) / (b - a);
    }
    
    public float[,] Remap(float[,] perlinMap, float minValue, float maxValue)
    {
        float minNoise = float.MaxValue;
        float maxNoise = float.MinValue;

        // Trouver les valeurs min et max de la carte de bruit Perlin
        for (int x = 0; x < perlinMap.GetLength(0); x++)
        {
            for (int y = 0; y < perlinMap.GetLength(1); y++)
            {
                float noise = perlinMap[x, y];
                if (noise < minNoise)
                    minNoise = noise;
                if (noise > maxNoise)
                    maxNoise = noise;
            }
        }

        // Remapper les valeurs entre la plage souhaitée
        float[,] remappedMap = new float[perlinMap.GetLength(0), perlinMap.GetLength(1)];

        for (int x = 0; x < perlinMap.GetLength(0); x++)
        {
            for (int y = 0; y < perlinMap.GetLength(1); y++)
            {
                float noise = perlinMap[x, y];
                float remappedValue = Lerp(minValue, maxValue, InverseLerp(minNoise, maxNoise, noise));
                remappedMap[x, y] = (int)remappedValue;
            }
        }

        return remappedMap;
    }
}