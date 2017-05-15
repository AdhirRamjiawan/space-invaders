using UnityEngine;
using System.Collections;
using Assets;

public class Player : MonoBehaviour {

    GameObject player;
    private static float speed = 0.25f;
    private DirectionEnum direction;
    private Level1 game;
    private GameObject bullet;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("player");
        player.transform.rotation = new Quaternion();
        direction = DirectionEnum.North;
        game = GetComponentInParent<Level1>();
        bullet = GameObject.Find("bullet");
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
            GameObject tmpBullet = GameObject.Instantiate<GameObject>(bullet);
            tmpBullet.GetComponent<Bullet>().direction = direction;
            tmpBullet.transform.position = player.transform.position;
            tmpBullet.transform.parent = game.gameObject.transform;
            game.bullets.Add(tmpBullet);
        }

        player.transform.position = newPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision");
        //Destroy(other.gameObject);
    }
}
