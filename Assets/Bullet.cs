using UnityEngine;
using System.Collections;
using Assets;

public class Bullet : MonoBehaviour {
    
    public float speed;
    public DirectionEnum direction;
    private Game game;

	// Use this for initialization
	void Start () {
        speed = 1f;
        game = GetComponentInParent<Game>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.tag);
        if (other.tag == "Alien")
        {
            Destroy(other.gameObject);
        }
    }
}
