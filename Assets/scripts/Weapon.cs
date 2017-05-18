using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets;

public class Weapon : MonoBehaviour, IShootable {
    private float bulletDamage;
    private float lastFireTime;
    private GameObject bullet;

    public float bulletFireRate;
    // Use this for initialization
    void Start () {
        if (bulletFireRate == 0)
        {
            bulletFireRate = 0.02f;
        }

        bullet = GameObject.Find("bullet");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // will need to change Level1 to a more generic type so can be used in multiple levels.
    public void FireBullet(Level1 game, Player player, DirectionEnum direction)
    {
        if (Time.unscaledTime - lastFireTime > bulletFireRate)
        {
            lastFireTime = Time.unscaledTime;
            GameObject tmpBullet = GameObject.Instantiate<GameObject>(bullet);
            tmpBullet.GetComponent<Bullet>().direction = direction;
            tmpBullet.transform.position = player.transform.position;
            tmpBullet.transform.parent = game.gameObject.transform;
            game.bullets.Add(tmpBullet);

            tmpBullet.GetComponent<Bullet>().FireBullet();
        }
    }

    public void SetBulletDamage(float damage)
    {
        this.bulletDamage = damage;
    }

    public void SetBulletSpeed(float speed)
    {
        throw new NotImplementedException();
    }
}
