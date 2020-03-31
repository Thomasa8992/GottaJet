﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject projectile;
    private float movementSpeed = 10;

    private SoundController soundController;

    public GameObject explosionParticleEffect;

    // Start is called before the first frame update
    void Start()
    {
        soundController = GameObject.Find("SoundObject").GetComponent<SoundController>();

        InvokeRepeating("ShootProjectile", .5f, 2f);
    }

    // Update is called once per frame
    void Update() {
        HandleEnemyMovement();
    }

    private void HandleEnemyMovement() {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }

    void ShootProjectile() {
        Instantiate(projectile, transform.position + transform.TransformDirection(new Vector3(0, 1.1f, 2)), projectile.transform.rotation);
        soundController.audioSource.PlayOneShot(soundController.projectileSound);
    }

    private void OnCollisionEnter(Collision collision) {
        HandlePlayerBulletCollision(collision);
    }

    private void HandlePlayerBulletCollision(Collision other) {
        var gameObjectTagIsPlayerBullet = other.gameObject.CompareTag("PlayerBullet");
        var incomingGameObject = other.gameObject;
        if (gameObjectTagIsPlayerBullet) {
            Debug.Log("Award player points");
            Instantiate(explosionParticleEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
