using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReplaySelectScript : MonoBehaviour {
    
    private DataManager dataManager;

	// Use this for initialization
	void Start () {
        dataManager = FindObjectOfType<DataManager>();
        dataManager.SetScore(0);
        dataManager.SetDistance(0.0f);
        Destroy(dataManager);
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void LoadNewGame()
    {
        if (dataManager != null)
        {
        }
        SceneManager.LoadScene("Gameplay");
    }

    public void CloseGame()
    {
        print("Close Game");
        Application.Quit();
    }
}
