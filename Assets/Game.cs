using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;

public class Game : MonoBehaviour {

    public List<GameObject> bullets;
    public List<GameObject> aliens;
    public List<GameObject> bulletsToRemove;
    public List<GameObject> aliensToRemove;

    private GameObject alientTemplate;
    private float alienSpeed = 0.05f;

	// Use this for initialization
	void Start () {
        Debug.Log("Starting game");
        aliensToRemove = new List<GameObject>();
        bulletsToRemove = new List<GameObject>();
        bullets = new List<GameObject>();
        aliens = new List<GameObject>();
        alientTemplate = GameObject.Find("alien");
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        // AFTER A CERTAIN DISTANCE BULLET INSTANCES SHOULD BE DESTROYED TO IMRPOVE PERFORMANCE
	    foreach (GameObject bullet in bullets)
        {
            Bullet script = bullet.GetComponent<Bullet>();
            switch (script.direction)
            {
                case DirectionEnum.North:
                    bullet.transform.position = new Vector3(bullet.transform.position.x, bullet.transform.position.y + script.speed, bullet.transform.position.z);
                    break;
                case DirectionEnum.South:
                    bullet.transform.position = new Vector3(bullet.transform.position.x, bullet.transform.position.y - script.speed, bullet.transform.position.z);
                    break;
                case DirectionEnum.East:
                    bullet.transform.position = new Vector3(bullet.transform.position.x + script.speed, bullet.transform.position.y, bullet.transform.position.z);
                    break;
                case DirectionEnum.West:
                    bullet.transform.position = new Vector3(bullet.transform.position.x - script.speed, bullet.transform.position.y, bullet.transform.position.z);
                    break;
            }

            if (isOutsideUniverse(bullet.transform.position))
                bulletsToRemove.Add(bullet);
            
        }

        foreach (GameObject alien in aliens)
        {
            if (alien == null)
            {
                aliensToRemove.Add(alien);
                continue;
            }

            alien.transform.position = new Vector3(alien.transform.position.x, alien.transform.position.y - alienSpeed, alien.transform.position.z);

            if (isOutsideUniverse(alien.transform.position))
                aliensToRemove.Add(alien);
        }

        spawnAliens();

        bulletsToRemove.ForEach(b => {
            bullets.Remove(b);
            GameObject.Destroy(b);
        });

        bulletsToRemove.Clear();

        aliensToRemove.ForEach(a => {
            aliens.Remove(a);
            GameObject.Destroy(a);
        });

        aliensToRemove.Clear();
    }

    private void spawnAliens()
    {
        if (aliens.Count < 10)
        {
            for (int i = 0; i < 3; i++)
            {
                System.Random random = new System.Random();
                
                GameObject tmpAlien = GameObject.Instantiate<GameObject>(alientTemplate);
                tmpAlien.transform.position = new Vector3((float)random.Next(-30, 30), (float)random.Next(20, 30), 0f);
                aliens.Add(tmpAlien);
            }
        }
    }

    private bool isOutsideUniverse(Vector3 position)
    {
        if (position.x < -50 || position.x > 50)
            return true;

        if (position.y < -20 || position.y > 20)
            return true;

        return false;
    }
}
