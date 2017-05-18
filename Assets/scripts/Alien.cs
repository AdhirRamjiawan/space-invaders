using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {

    public bool isKilled;
    public string killedBy;
    public float healthLevel;
    public float maxHealthLevel;

    private Level1 level;

    public static AudioSource alienDieSound;
    public static AudioSource alienHitSound;

    // Use this for initialization
    void Start () {
        level = GameObject.Find("Game").GetComponent<Level1>();

        if (maxHealthLevel == 0)
            maxHealthLevel = 100;

        healthLevel = maxHealthLevel;

        if (alienDieSound == null)
            alienDieSound = GetComponents<AudioSource>()[0];

        if (alienHitSound == null)
            alienHitSound = GetComponents<AudioSource>()[1];

    }
	
	// Update is called once per frame
	void Update () {
        Material material = GetComponent<SpriteRenderer>().material;

        material.color = new Color(1.0f- (healthLevel / maxHealthLevel), (healthLevel / maxHealthLevel), (healthLevel / maxHealthLevel));
	}

    public void Die()
    {
        alienDieSound.PlayOneShot(alienDieSound.clip);
    }

    public void Hit(GameObject other)
    {
        float damage = 0f;

        if (other.GetComponent<Bullet>() != null)
            damage = other.GetComponent<Bullet>().damage;

        alienHitSound.PlayOneShot(alienHitSound.clip);
        healthLevel -= damage;
        level.shouldWobble = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Hit(other.gameObject);
        //Debug.Log("Collision");
        //Destroy(other.gameObject);
    }
}
