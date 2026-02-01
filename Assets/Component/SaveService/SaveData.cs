using System;

[Serializable]
public class SaveData
{
    
    // GameDisplayData
    public bool fullscreen = true;
    public int resolutionWidth = 1920;
    public int resolutionHeight = 1080;
    public float brightness = 1f;

    //GameInfoData
    public string gameName = "ChromAdventure";
    public string version = "1.0.0";

    //PlayerInfoData
    public string PlayerName = "Player";

    //ScoreData
    public int RunCount;
    public int LevelIndex;

    //UIButtonData
    public string label;
    public string menuTarget;

    //GameplayData
    public float Speed = 1f;
    //public float maxSpeed = 20f;
    //public float acceleration = 0.05f;
    public int chunksInstantiated = 5;
    public int chunksDestroyedBehind = 2;
    //public string currentMusic = "MainTheme";
   
    //PlayerAbilitiesData    
    public bool canMoveLeft = true;
    public bool canMoveRight = true;
    public bool canJump = true;
    public bool canSlide = true;
    public float jumpHeight = 2f;
    public float slideDuration = 0.5f;

    //MenuStateData
    public bool isPaused = false;
    public bool isGameOver = false;
}

