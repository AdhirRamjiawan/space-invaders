using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Level1 : MonoBehaviour {

    public List<GameObject> bullets;
    public List<GameObject> aliens;
    public List<GameObject> bulletsToRemove;
    public List<GameObject> aliensToRemove;
    public List<GameObject> weaponPackages;
    public List<GameObject> weaponPackagesToRemove;
    public int alienKillCount;

    private GameObject gameCamera;
    private GameObject alienTemplate;
    private GameObject weaponPackageTemplate;
    private float alienSpeed = 0.05f;
    private float weaponPackageSpeed = 0.05f;

    // shakey camera params
    public float minMaxBounds;
    public int maxWobbleCount;
    public float wobbleForce;
    public bool shouldWobble;
    private float cameraOriginalX;
    private int currentWobbleCount;
    private float sinAngleForWobble;

	// Use this for initialization
	void Start () {
        Debug.Log("Starting game");
        aliensToRemove = new List<GameObject>();
        bulletsToRemove = new List<GameObject>();
        bullets = new List<GameObject>();
        aliens = new List<GameObject>();
        weaponPackages = new List<GameObject>();
        weaponPackagesToRemove = new List<GameObject>();

        alienTemplate = GameObject.Find("alien");
        weaponPackageTemplate = GameObject.Find("basic gun"); // instead of "basic gun" we need to find a way to allow for inheritence of Weapon type GameObjects
        gameCamera = GameObject.Find("Main Camera");

        initShakeCamera();
    }
	
	// Update is called once per frame
	void Update () {

        applyShakeCameraUpdate();

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
        
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

        foreach (GameObject package in weaponPackages)
        {
            if (package != null && package.GetComponent<Weapon>() != null && package.GetComponent<Weapon>().isObtained)
                continue;

            if (package == null)
            {
                weaponPackagesToRemove.Add(package);
                continue;
            }

            package.transform.position = new Vector3(package.transform.position.x, package.transform.position.y - weaponPackageSpeed, package.transform.position.z);

            if (isOutsideUniverse(package.transform.position))
                weaponPackagesToRemove.Add(package);
        }

        spawnAliens();
        spawnWeaponPackages();

        bulletsToRemove.ForEach(b => {
            bullets.Remove(b);
            GameObject.Destroy(b);
        });

        bulletsToRemove.Clear();

        weaponPackagesToRemove.ForEach(p => {
            weaponPackages.Remove(p);
            GameObject.Destroy(p);
        });

        weaponPackagesToRemove.Clear();

        aliensToRemove.ForEach(a => {
            if (a.gameObject.GetComponent<Alien>().killedBy == "bullet")
            {
                alienKillCount++;
            }

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
                
                GameObject tmpAlien = GameObject.Instantiate<GameObject>(alienTemplate);
                tmpAlien.transform.position = new Vector3((float)random.Next(-30, 30), (float)random.Next(20, 30), 0f);
                aliens.Add(tmpAlien);
            }
        }
    }

    private void spawnWeaponPackages()
    {
        if (weaponPackages.Count < 10)
        {
            for (int i = 0; i < 3; i++)
            {
                System.Random random = new System.Random();

                GameObject weaponPackage = GameObject.Instantiate<GameObject>(weaponPackageTemplate);
                weaponPackage.transform.position = new Vector3((float)random.Next(-30, 30), (float)random.Next(20, 30), 0f);
                weaponPackages.Add(weaponPackage);
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

    private void initShakeCamera()
    {
        cameraOriginalX = gameCamera.transform.position.x;

        if (minMaxBounds == 0)
            minMaxBounds = 5f;

        if (maxWobbleCount == 0)
            maxWobbleCount = 7;

        if (wobbleForce == 0)
            wobbleForce = 0.25f;
    }

    private void applyShakeCameraUpdate()
    {
        if (currentWobbleCount < maxWobbleCount && shouldWobble)
        {
            float delta = Mathf.Sin(sinAngleForWobble) * wobbleForce;

            //Debug.Log("delta " + delta  );
            //Debug.Log("sin angle " + sinAngleForWobble);

            gameCamera.transform.position = new Vector3(gameCamera.transform.position.x + delta, gameCamera.transform.position.y + delta, gameCamera.transform.position.z);
            currentWobbleCount++;
            sinAngleForWobble += 45;
        }
        else
        {
            currentWobbleCount = 0;
            shouldWobble = false;
            sinAngleForWobble = 0;
        }
    }
}
