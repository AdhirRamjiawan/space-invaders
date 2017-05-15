using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {

    public bool isKilled;
    public string killedBy;
    private Level1 level;

	// Use this for initialization
	void Start () {
        level = GameObject.Find("Game").GetComponent<Level1>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        level.shouldWobble = true;
        //Debug.Log("Collision");
        //Destroy(other.gameObject);
    }
}
