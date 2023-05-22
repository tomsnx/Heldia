using System;
using Microsoft.Xna.Framework;

namespace Heldia.Engine;

/// <summary>
/// Singleton Class for all game
/// </summary>
public class GameManager
{
    public static GameManager Instance { get; private set; }
    
    // Player life
    public float PlayerLife { get; set; }
    public float PlayerMaxLife { get; set; }
    public float PlayerCoefLostLife { get; set; }
    public float PlayerCoefRegenLife { get; set; }
    public double PlayerDelayRegenLife { get; set; }
    
    // Player Stamina
    public float PlayerStamina { get; set; }
    public float PlayerMaxStamina { get; set; }
    public float PlayerCoefLostStamina { get; set; }
    public float PlayerCoefRegenStamina { get; set; }
    public double PlayerDelayRegenStamina { get; set; }

    // Elapsed Time
    public double TotalGameTime { get; set; }
    
    // True every second
    public bool PlayerRegenLifeClock { get; set; }
    public bool PlayerRegenStaminaClock { get; set; }

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

    /**
     * Calculate the time elapsed since the launch of the game 
     */
    public void ElapsedTimeUpdate(GameTime gt)
    {
        TotalGameTime = gt.TotalGameTime.TotalMilliseconds;

        if (Math.Abs(TotalGameTime % PlayerDelayRegenLife) <= 1f)
        {
            PlayerRegenLifeClock = true;
        }
        else
        {
            PlayerRegenLifeClock = false;
        }

        if (Math.Abs(TotalGameTime % PlayerDelayRegenStamina) <= 2.5f)
        {
            PlayerRegenStaminaClock = true;
        }
        else
        {
            PlayerRegenStaminaClock = false;
        }
    }
}