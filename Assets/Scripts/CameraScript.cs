using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject Player;
    public float springConstant = 0.03f;

	// Update is called once per frame
	void Update () {
        float xPos = GetComponent<Transform>().position.x;
        float playerXPos = Player.GetComponent<Transform>().position.x;
        float distanceBetween = playerXPos - xPos;

        if (distanceBetween > 0.0f || distanceBetween < 0.0f)
        {
            float force = springConstant * distanceBetween;
            xPos += force;
            GetComponent<Transform>().position = new Vector3(xPos, GetComponent<Transform>().position.y, GetComponent<Transform>().position.z);
        }
    }
}
