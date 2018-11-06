using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public Text HighScoreText;
    public Text ScoreText;
    public Text DistanceText;

    private DataManager dataManager;
    private int highScore;
    private float distance;

	// Use this for initialization
	void Start () 
    {
        dataManager = FindObjectOfType<DataManager>();
	}
	
	// Update is called once per frame
	void Update () 
    {        
        ScoreText.text = dataManager.GetScoreString();
        DistanceText.text = dataManager.GetDistanceString();
	}

}
