using UnityEngine;
using System.Collections;
using Assets;

public class Player : MonoBehaviour {

    GameObject player;
    private static float speed = 0.25f;
    private DirectionEnum direction;
    private Level1 game;
    
    public GameObject currentWeapon;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("player");
        player.transform.rotation = new Quaternion();
        direction = DirectionEnum.North;
        game = GetComponentInParent<Level1>();
        
        if (currentWeapon == null || (currentWeapon.GetComponent<Weapon>() == null))
        {
            Debug.Log("invalid game object set as player current weapon");
        }
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPosition = player.transform.position;

        if (game.alienKillCount >= 5)
        {
            player.GetComponent<SpriteRenderer>().color = Color.green;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            newPosition = new Vector3(player.transform.position.x, player.transform.position.y + speed, player.transform.position.z);

            if (direction != DirectionEnum.North)
            {
                player.transform.rotation = new Quaternion();
                direction = DirectionEnum.North;
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            newPosition = new Vector3(player.transform.position.x, player.transform.position.y -speed, player.transform.position.z);

            if (direction != DirectionEnum.South)
            {
                player.transform.rotation = new Quaternion();
                player.transform.Rotate(0.0f, 0.0f, -180.0f);
                direction = DirectionEnum.South;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition = new Vector3(player.transform.position.x - speed, player.transform.position.y, player.transform.position.z);

            if (direction != DirectionEnum.West)
            {
                player.transform.rotation = new Quaternion();
                player.transform.Rotate(0.0f, 0.0f, 90.0f);
                direction = DirectionEnum.West;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            newPosition = new Vector3(player.transform.position.x + speed, player.transform.position.y, player.transform.position.z);

            if (direction != DirectionEnum.East)
            {
                player.transform.rotation = new Quaternion();
                player.transform.Rotate(0.0f, 0.0f, -90.0f);
                direction = DirectionEnum.East;
            }
        }

        if (Input.GetKey(KeyCode.Z))
        {
            //Debug.Log(Time.unscaledTime - lastFireTime);

            if (currentWeapon != null && (currentWeapon.GetComponent<Weapon>() != null))
            {
                currentWeapon.GetComponent<Weapon>().FireBullet(game, this, direction);
            }
            else
            {
                Debug.Log("can't fire bullet!!!");
            }
        }

        player.transform.position = newPosition;
    }

    public void Hit(GameObject other)
    {
        if (other.tag == "Basic Gun")
        {
            if (other.GetComponent<Weapon>() != null)
            {
                currentWeapon = other.GetComponent<Weapon>().gameObject;
                other.GetComponent<Weapon>().ObtainWeapon();
                Debug.Log("New weapon obtained");
            }
            else
            {
                Debug.Log("no weapon component found");
            }
        }

        //alienHitSound.PlayOneShot(alienHitSound.clip);
        //healthLevel -= damage;
        //level.shouldWobble = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Hit(other.gameObject);
        //Debug.Log("Collision");
        //Destroy(other.gameObject);
    }
}
