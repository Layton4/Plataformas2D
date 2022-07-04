using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState //posibles estados del videojuego
{
    menu, inGame,gameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;

    public GameState currentGameState = GameState.menu; //variables para saber en que estado del juego estamos

    public int CollectableCounter = 0;  

    public Canvas MenuCanvas, gameCanvas, gameOverCanvas;
    private void Awake()
    {
        if (sharedInstance == null)
        {
            // Configuramos la instancia
            sharedInstance = this;
            // Nos aseguramos de que no sea destruida con el cambio de escena
            DontDestroyOnLoad(sharedInstance);
        }
        else
        {
            // Como ya existe una instancia, destruimos la copia
            Destroy(this);
        }
    }
    private void Start()
    {
        BackToMenu();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Start") && currentGameState != GameState.inGame)
        {
            StartGame();
        }

        if(Input.GetButtonDown("Menu"))
        {
            BackToMenu();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
    public void StartGame()
    {
        SetGameState(GameState.inGame);
        MenuCanvas.enabled = false;
        

        if (PlayerController.sharedInstance.transform.position.x > 5)
        {
            LevelGenerator.sharedInstance.RemoveAllTheBlocks();
            LevelGenerator.sharedInstance.GenerateInitialBlocks();
        }
        PlayerController.sharedInstance.StartGame();
        CameraFollow.sharedInstance.ResetCameraPosition();
        CollectableCounter = 0;
    }
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
        MenuCanvas.enabled = false;
    }
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
        MenuCanvas.enabled = true;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit(); //en un juego mobil no iría bien
        #endif
    }

    void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu)
        {
            MenuCanvas.enabled = true;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
        }
        else if(newGameState == GameState.inGame)
        {
            MenuCanvas.enabled = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
        }
        else if(newGameState == GameState.gameOver)
        {
            MenuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = true;
        }
        currentGameState = newGameState; //asignamos el estado de juego actual al que introducimos por parámetro

    }

    public void CollectObject(int objectValue)
    {
        CollectableCounter += objectValue;
        Debug.Log("Llevamos " + CollectableCounter + " Diamantes");
    }
}
