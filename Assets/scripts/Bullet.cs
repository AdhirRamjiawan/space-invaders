using UnityEngine;
using System.Collections;
using Assets;

public class Bullet : MonoBehaviour {
    
    public float speed;
    public DirectionEnum direction;
    private Level1 game;

	// Use this for initialization
	void Start () {
        speed = 1f;
        game = GetComponentInParent<Level1>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.tag);
        if (other.tag == "Alien")
        {
            if (other.gameObject.GetComponent<Alien>().healthLevel <= 0)
            {
                game.aliensToRemove.Add(other.gameObject);
                other.gameObject.GetComponent<Alien>().killedBy = "bullet";
            }
        }
    }
}
