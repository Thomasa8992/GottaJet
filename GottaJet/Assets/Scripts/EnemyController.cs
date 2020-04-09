using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject projectile;
    public float movementSpeed = 10;

    private SoundController soundController;

    public GameObject explosionParticleEffect;

    public float projectileInvokeTime = 2f;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        projectileInvokeTime = 2f;

        soundController = GameObject.Find("SoundObject").GetComponent<SoundController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        ShootProjectile();
    }

    // Update is called once per frame
    void Update() {
        HandleEnemyMovement();
    }

    private void HandleEnemyMovement() {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }

    void ShootProjectile() {
        Instantiate(projectile, transform.position + transform.TransformDirection(new Vector3(0, .7f, 2)), projectile.transform.rotation);
        soundController.audioSource.PlayOneShot(soundController.projectileSound);

        Invoke("ShootProjectile", projectileInvokeTime);
    }

    private void OnTriggerEnter(Collider other) {
        HandlePlayerBulletCollision(other);
    }

    private void HandlePlayerBulletCollision(Collider other) {
        var gameObjectTagIsPlayerBullet = other.gameObject.CompareTag("PlayerBullet");

        if (gameObjectTagIsPlayerBullet) {
            gameManager.UpdateScore(300);

            soundController.audioSource.PlayOneShot(soundController.explosionSound);

            Instantiate(explosionParticleEffect, gameObject.transform.position, gameObject.transform.rotation);

            Destroy(other.gameObject);
            Destroy(gameObject);

            gameManager.enemiesLeft--;
        }
    }
}
