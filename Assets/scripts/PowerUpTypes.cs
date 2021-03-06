﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTypes : MonoBehaviour {

    public enum PowerUp_Types {IncreaseFireRate,DiagonalBullets,Rockets,MoreBullets}
    public PowerUp_Types puTypes;
    public float yAdjuster;
    public string playerTag;
    public GameObject player;
    public float fireRateIncrease;
    public int maxBulletNumb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
    }

    private void Update()
    {
        //distruzione se esce dal bordo sotto
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)) + Vector3.up * yAdjuster;
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {

            if(puTypes == PowerUp_Types.IncreaseFireRate)
            {
                IncreaseFireRate();
            }
            if(puTypes == PowerUp_Types.DiagonalBullets)
            {
                AddDiagonalBullets();
            }
            if(puTypes == PowerUp_Types.Rockets)
            {
                AddRockets();
            }
            if (puTypes == PowerUp_Types.MoreBullets)
            {
                AddBullets();
            }

            Destroy(this.gameObject);
        }
    }

    void IncreaseFireRate()
    {
        player.GetComponent<Player>().bullets.fireRate -= fireRateIncrease;
    }

    void AddBullets()
    {
        int bulletNumb = player.GetComponent<Player>().bullets.bulletNumber;

        if(bulletNumb < maxBulletNumb)
        {
            player.GetComponent<Player>().bullets.bulletNumber++;
        }
    }

    void AddDiagonalBullets()
    {

    }

    void AddRockets()
    {

    }

}
