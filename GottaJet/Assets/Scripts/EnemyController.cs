using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject projectile;
    private float movementSpeed = 10;

    private SoundController soundController;

    public GameObject explosionParticleEffect;

    public ScoreKeeperController scoreKeeperController;

    public LifeKeeperController lifeKeeperController;

    // Start is called before the first frame update
    void Start()
    {
        soundController = GameObject.Find("SoundObject").GetComponent<SoundController>();
        scoreKeeperController = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeperController>();
        lifeKeeperController = GameObject.Find("LifeKeeper").GetComponent<LifeKeeperController>();

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
        Instantiate(projectile, transform.position + transform.TransformDirection(new Vector3(0, .7f, 2)), projectile.transform.rotation);
        soundController.audioSource.PlayOneShot(soundController.projectileSound);
    }

    private void OnTriggerEnter(Collider other) {
        HandlePlayerBulletCollision(other);
    }

    private void HandlePlayerBulletCollision(Collider other) {
        var gameObjectTagIsPlayerBullet = other.gameObject.CompareTag("PlayerBullet");
        scoreKeeperController.score += 300;

        if (gameObjectTagIsPlayerBullet) {

            soundController.audioSource.PlayOneShot(soundController.explosionSound);

            Instantiate(explosionParticleEffect, gameObject.transform.position, gameObject.transform.rotation);

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
