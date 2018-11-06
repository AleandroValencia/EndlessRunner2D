using UnityEngine;
using System.Collections;

public class BGMScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);

        // Singleton 
        // if there is more than one of this type of object, destroy it
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
	
}
