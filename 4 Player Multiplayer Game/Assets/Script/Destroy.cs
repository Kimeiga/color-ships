using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

    public float destroyTime;
    private float startTime;

	// Use this for initialization
	void Start () {

        startTime = Time.time;

	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time > startTime + destroyTime) {
            Destroy(gameObject);
        }

	}
}
