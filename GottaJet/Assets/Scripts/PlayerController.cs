﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float playerMovementSpeed = 10;

    public GameObject projectile;
    private bool fireRateIsIncreased = false;
    private float fireRate = 0.5F;
    private float nextFire = 0.0F;

    public GameObject fuel;

    private SoundController soundController;

    public GameObject explosionParticleEffect;

    public GameObject gemParticleEffect;

    private Vector3 playerStartingPosition;

    private MeshCollider meshCollider;

    private MeshRenderer meshRenderer;

    private bool playerIsDead = false;

    private MeshRenderer childrenMeshRenderer;

    private GameManager gameManager;

    public ParticleSystem playerSpeedBoostParticleEffect;

    public ParticleSystem playerFireBoostParticleEffect;


    // Start is called before the first frame update
    void Start()
    {
        soundController = GameObject.Find("SoundObject").GetComponent<SoundController>();

        meshCollider = gameObject.GetComponent<MeshCollider>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        childrenMeshRenderer = GameObject.Find("Propeller").GetComponent<MeshRenderer>();

        playerStartingPosition = transform.position;

    }

    // Update is called once per frame
    void Update() {
        HandlePlayerMovement();
        HandlePlayerBoundaries();
        ShootProjectile();
        HandlePlayerFireBoostParticleEffect();
        Debug.Log(fireRateIsIncreased);
    }

    private void ShootProjectile() {
        handleFireRate();
        var projectilePositionRelativeToPlayerPosition = transform.position + transform.TransformDirection(new Vector3(0, .7f, 2));
        
        if (!playerIsDead) {
            var nextFireIsREady = Time.time > nextFire;
            var keyCodeIsPressedAndNextFireIsReady = (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && nextFireIsREady;
                
            if (keyCodeIsPressedAndNextFireIsReady) {

                nextFire = Time.time + fireRate;

                Instantiate(projectile, projectilePositionRelativeToPlayerPosition, projectile.transform.rotation);

                soundController.audioSource.PlayOneShot(soundController.projectileSound, 1);
            }
        }
    }

    private void handleFireRate() {
        if (fireRateIsIncreased) {
            fireRate = 0;
        } else {
            fireRate = 0.5f;
        }

    }

    #region collision

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Gem")) {
            HandleGemCollision(other);
        } 
        else if (other.CompareTag("FireBooster")) {
            HandleFireBoosterCollision(other);
        } 
        else if (other.CompareTag("SpeedBoost")) {
            HandleSpeedBoostCollision(other); 
        } 
        else {
            HandlePlayerCollision(other);
        }
    }

    private void HandleSpeedBoostCollision(Collider other) {
        Destroy(other.gameObject);

        StartCoroutine(HandleIncreaedPlayerMovement());
    }

    IEnumerator HandleIncreaedPlayerMovement() {
        playerSpeedBoostParticleEffect.gameObject.SetActive(true);
        playerMovementSpeed = 18;

        yield return new WaitForSeconds(10);

        playerSpeedBoostParticleEffect.gameObject.SetActive(false);
        playerMovementSpeed = 10;
    }

    private void HandleFireBoosterCollision(Collider other) {
        Destroy(other.gameObject);

        StartCoroutine(HandleFireRateChange());
    }

    IEnumerator HandleFireRateChange() {
        fireRateIsIncreased = true;
        //Todo:figure out how to cancel a coroutine or another way of handling this effect
        yield return new WaitForSeconds(10);

        fireRateIsIncreased = false;


    }

    private void HandlePlayerFireBoostParticleEffect() {
        if(fireRateIsIncreased) {
            playerFireBoostParticleEffect.gameObject.SetActive(true);
        } else {
            playerFireBoostParticleEffect.gameObject.SetActive(false);
        }
    }

    private void HandleGemCollision(Collider other) {
        soundController.audioSource.PlayOneShot(soundController.fuelCollectionSound, 1);

        var gemPoints = 1200;

        gameManager.UpdateScore(gemPoints);
        Instantiate(gemParticleEffect, other.transform.position, gemParticleEffect.transform.rotation);
        Destroy(other.gameObject);
    }

    private void HandlePlayerCollision(Collider other) {
        soundController.audioSource.PlayOneShot(soundController.explosionSound, 1);

        Instantiate(explosionParticleEffect, transform.position, transform.rotation);

        if(other.gameObject.CompareTag("EnemyAirplane")) {
            Instantiate(explosionParticleEffect, other.transform.position, other.transform.rotation);
        }

        Destroy(other.gameObject);
        HandlePlayerDeath(other);
    }

    private void HandlePlayerDeath(Collider other) {
        childrenMeshRenderer.enabled = false;
        meshCollider.enabled = false;
        meshRenderer.enabled = false;
        playerIsDead = true;
        playerSpeedBoostParticleEffect.gameObject.SetActive(false);
        playerMovementSpeed = 10;
        fireRateIsIncreased = false;

        gameManager.DecreaseLives();

        if (gameManager.lives != 0) {
            StartCoroutine(PlayerDeathRoutine(other));
        }
    }

    IEnumerator PlayerDeathRoutine(Collider other) {
        yield return new WaitForSeconds(3);
        transform.position = playerStartingPosition;
        childrenMeshRenderer.enabled = true;
        meshRenderer.enabled = true;
        playerIsDead = false;

        yield return new WaitForSeconds(3);
        meshCollider.enabled = true;
    }

    private void OnCollisionEnter(Collision other) {
        var gameObjectTagIsEnemy = other.gameObject.CompareTag("EnemyAirplane");

        if (gameObjectTagIsEnemy) {
            soundController.audioSource.PlayOneShot(soundController.explosionSound);
            Instantiate(explosionParticleEffect, gameObject.transform.position, gameObject.transform.rotation);

            Destroy(other.gameObject);
            HandlePlayerCollision(other);
        }
    }

    private void HandlePlayerCollision(Collision other) {
        soundController.audioSource.PlayOneShot(soundController.explosionSound, 1);

        Instantiate(explosionParticleEffect, transform.position, transform.rotation);

        if (other.gameObject.CompareTag("EnemyAirplane")) {
            Instantiate(explosionParticleEffect, other.transform.position, other.transform.rotation);
        }

        Destroy(other.gameObject);
        HandlePlayerDeath(other);
    }

    private void HandlePlayerDeath(Collision other) {
        childrenMeshRenderer.enabled = false;
        meshCollider.enabled = false;
        meshRenderer.enabled = false;
        playerIsDead = true;

        playerSpeedBoostParticleEffect.gameObject.SetActive(false);
        playerMovementSpeed = 10;

        fireRateIsIncreased = false;

        gameManager.DecreaseLives();

        if (gameManager.lives != 0) {
            StartCoroutine(PlayerDeathRoutine(other));
        } 
    }

    IEnumerator PlayerDeathRoutine(Collision other) {
        yield return new WaitForSeconds(3);
        transform.position = playerStartingPosition;
        childrenMeshRenderer.enabled = true;
        meshRenderer.enabled = true;
        playerIsDead = false;

        yield return new WaitForSeconds(3);
        meshCollider.enabled = true;
    }

    #endregion

    #region player boundaries
    private void HandlePlayerBoundaries() {
        HandlePlayerLeftBoundary();
        HandlePlayerRightBoundary();
        HandlePlayerTopBoundary();
        HandlePlayerBottomBoundary();
    }

    private void HandlePlayerLeftBoundary() {
        var leftBoundary = -14;
        var playerHasReachLeftBoundary = transform.position.z < leftBoundary;

        if (playerHasReachLeftBoundary) {
            transform.position = new Vector3(transform.position.x, transform.position.y, leftBoundary);
        }
    }

    private void HandlePlayerRightBoundary() {
        var rightBoundary = 14;
        var playerHasReachedRightBoundary = transform.position.z > rightBoundary;

        if (playerHasReachedRightBoundary) {
            transform.position = new Vector3(transform.position.x, transform.position.y, rightBoundary);
        }
    }

    private void HandlePlayerTopBoundary() {
        var TopBoundary = 8;
        var playerHasReachedTopBoundary = transform.position.y > TopBoundary;

        if (playerHasReachedTopBoundary) {
            transform.position = new Vector3(transform.position.x, TopBoundary, transform.position.z);
        }
    }

    private void HandlePlayerBottomBoundary() {
        var BottomBoundary = -7;
        var playerHasReachedBottomBoundary = transform.position.y < BottomBoundary;

        if (playerHasReachedBottomBoundary) {

            transform.position = new Vector3(transform.position.x, BottomBoundary, transform.position.z);
        }
    }

    #endregion

    #region player movement
    private void HandlePlayerMovement() {
        if(!playerIsDead) {
            HandleHorizontalMovementByPlayerInput();
            HandleVerticalMovementByPlayerInput();
        }
    }

    private void HandleHorizontalMovementByPlayerInput() {
        var horizontalInput = Input.GetAxis("Horizontal");
        var horizontalMovement = Vector3.forward * Time.deltaTime * playerMovementSpeed * horizontalInput;

        transform.Translate(horizontalMovement);
    }

    private void HandleVerticalMovementByPlayerInput() {
        var verticalInput = Input.GetAxis("Vertical");
        var verticalMovement = Vector3.up * Time.deltaTime * playerMovementSpeed * verticalInput;

        transform.Translate(verticalMovement);
    }
    #endregion
}
