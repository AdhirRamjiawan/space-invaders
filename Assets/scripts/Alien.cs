using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {

    public bool isKilled;
    public string killedBy;
    public float healthLevel;
    public float maxHealthLevel;

    private Level1 level;
    

	// Use this for initialization
	void Start () {
        level = GameObject.Find("Game").GetComponent<Level1>();

        if (maxHealthLevel == 0)
            maxHealthLevel = 100;

        healthLevel = maxHealthLevel;
	}
	
	// Update is called once per frame
	void Update () {
        Material material = GetComponent<SpriteRenderer>().material;

        material.color = new Color(1.0f- (healthLevel / maxHealthLevel), (healthLevel / maxHealthLevel), (healthLevel / maxHealthLevel));
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        level.shouldWobble = true;
        healthLevel -= 20;

        //Debug.Log("Collision");
        //Destroy(other.gameObject);
    }
}
