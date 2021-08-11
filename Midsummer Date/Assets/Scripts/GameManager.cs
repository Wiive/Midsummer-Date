using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public enum GameState { start, inGame}
    public GameState currentState;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "Start")
        {
            ChangeGameState(GameState.start);
        }
        else
        {
            ChangeGameState(GameState.inGame);
        }
    }

    public void ChangeGameState(GameState gameState)
    {
        currentState = gameState;
    }

    public void ChangeGameState(int gameState)
    {
        currentState = (GameState)gameState;
    }

}
