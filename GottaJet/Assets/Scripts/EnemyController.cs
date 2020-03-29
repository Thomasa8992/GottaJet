using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject projectile;
    private float movementSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootProjectile", .5f, 2f);
    }

    // Update is called once per frame
    void Update() {
        handleEnemyMovement();
    }

    private void handleEnemyMovement() {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }

    void ShootProjectile() {
        Instantiate(projectile, transform.position + transform.TransformDirection(new Vector3(0, 1.1f, 2)), projectile.transform.rotation);
    }

    private void OnCollisionEnter(Collision collision) {

        handlePlayerBulletCollision(collision);
    }

    private void handlePlayerBulletCollision(Collision collision) {
        var gameObjectTagIsPlayerBullet = collision.gameObject.CompareTag("PlayerBullet");
        if (gameObjectTagIsPlayerBullet) {
            Debug.Log("Award player points");

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
