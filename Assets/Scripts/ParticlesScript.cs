using UnityEngine;
using System.Collections;

public class ParticlesScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float lifetime = GetComponent<ParticleSystem>().main.duration;
        Destroy(gameObject, lifetime);
	}
	
}
