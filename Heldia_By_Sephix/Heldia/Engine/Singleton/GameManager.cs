using System;

namespace Heldia.Engine.Singleton;

/// <summary>
/// Singleton Class for all game
/// </summary>
public class GameManager
{
    public static GameManager Instance { get; private set; }
    
    // Camera
    public float CameraDelay { get; set; } = 10.0f;
    
    // Player Position
    public float PlayerX { get; set; }
    public float PlayerY { get; set; }
    
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