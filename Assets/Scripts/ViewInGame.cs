using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ViewInGame : MonoBehaviour
{
    public TextMeshProUGUI collectableText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;
    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame || GameManager.sharedInstance.currentGameState == GameState.gameOver)
        {
            int currentCollectables = GameManager.sharedInstance.CollectableCounter;
            collectableText.text = currentCollectables.ToString();
        }

        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            float traveledDistance = PlayerController.sharedInstance.GetDistance();
            scoreText.text = "Score:\n" + traveledDistance.ToString("f1");
        }
    }
}
