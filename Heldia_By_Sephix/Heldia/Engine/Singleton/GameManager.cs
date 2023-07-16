using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Heldia.Engine.Singleton;

/// <summary>
/// Singleton Class for all game
/// </summary>
public class GameManager
{
    public static GameManager Instance { get; private set; }
    
    // Global
    public string GameTitle { get; private set; } = "Heldia";
    public int GameScale { get; private set; } = 3;
    public bool IsFullScreen { get; set; }
    public bool DebugMode { get; set; } = true;
    public bool SettingsMode { get; set; } = false;
    
    // FPS
    public bool FixedFps { get; } = false;
    public float GoalFps { get; set; } = 144;

    // Camera
    public Camera Camera { get; set; }
    public float CameraDelay { get; set; } = 10.0f;
    public Vector2 CameraPos { get; set; } // Camera destination position (2var system)
    
    // input
    public KeyboardState Kb { get; set; }

    // Map
    public int MapScale { get; set; }
    public int TileSize { get; set; }
    
    // Player Position
    public float PlayerX { get; set; }
    public float PlayerY { get; set; }
    public bool sprint { get; set; }
    
    // Player life
    public float PlayerHealth { get; set; }
    public float PlayerMaxHealth { get; set; } = 100f;
    public float PlayerCoefLostHealth { get; set; }
    public float PlayerCoefRegenHealth { get; set; }
    public double PlayerDelayRegenHealth { get; set; }
    
    // Player Stamina
    public float PlayerStamina { get; set; }
    public float PlayerMaxStamina { get; set; } = 100f;
    public float PlayerCoefLostStamina { get; set; }
    public float PlayerCoefRegenStamina { get; set; }
    public double PlayerDelayRegenStamina { get; set; }
    
    // Elapsed Time
    public double TotalGameTime { get; set; }

    public int Day { get; set; }

    // Constructor
    private GameManager()
    {
        // Private to prevent external instantiation
    }

    /**
     * Init the instance of the Singleton
     */
    public static void Init()
    {
        if (Instance != null)
        {
            Console.Error.WriteLine("More of one instance of GameManager in the scene !");
            return;
        }
        Instance = new GameManager();
    }
}