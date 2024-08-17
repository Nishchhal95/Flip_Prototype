using UnityEngine;

public static class PersistanceManager
{
    public static string GAME_STATE_KEY = "GameState";
    public static bool loadGame = false;
    
    public static void Save(GameState gameState)
    {
        PlayerPrefs.SetString(GAME_STATE_KEY, JsonUtility.ToJson(gameState));
        PlayerPrefs.Save();
    }

    public static GameState Load()
    {
        string jsonGameState = PlayerPrefs.GetString(GAME_STATE_KEY, "");
        if (string.IsNullOrEmpty(jsonGameState))
        {
            // Nothing to load
            Debug.Log("Nothing to load");
            return null;
        }

        GameState gameState = JsonUtility.FromJson<GameState>(jsonGameState);
        return gameState;
    }
}
