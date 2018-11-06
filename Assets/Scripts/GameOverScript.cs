using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

    public Text HighScoreText;
    public Text ScoreText;
    public Text DistanceText;
    public Text HighDistanceText;

    private DataManager dataManager;
    private int highScore;
    private float distance;


	// Use this for initialization
	void Start () {
        dataManager = FindObjectOfType<DataManager>();
        
        ScoreText.text = dataManager.GetScoreString();
        DistanceText.text = dataManager.GetDistanceString();
        HighScoreText.text = dataManager.GetHighScoreString();
        HighDistanceText.text = dataManager.GetHighDistanceString();

        dataManager.SetScore(0);
        dataManager.SetDistance(0.0f);
	}
	
}
