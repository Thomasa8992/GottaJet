using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed = 10;
    public GameObject projectile;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    public GameObject fuel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        handlePlayerMovement();
        handlePlayerBoundaries();
        shootProjectile();
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.gameObject.tag != "Fuel") {
            Destroy(collision.collider.gameObject);
            Destroy(gameObject);
        } else {
            Destroy(collision.collider.gameObject);
        }


    }

    private void shootProjectile() {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(projectile, transform.position + transform.TransformDirection(new Vector3(0, 1.1f, 2)), projectile.transform.rotation);
        }
    }

    private void handlePlayerBoundaries() {
        handlePlayerLeftBoundary();
        handlePlayerRightBoundary();
        handlePlayerTopBoundary();
        handlePlayerBottomBoundary();
    }

    private void handlePlayerLeftBoundary() {
        var leftBoundary = -22;
        var playerHasReachLeftBoundary = transform.position.z < leftBoundary;

        if (playerHasReachLeftBoundary) {
            transform.position = new Vector3(transform.position.x, transform.position.y, leftBoundary);
        }
    }

    private void handlePlayerRightBoundary() {
        var rightBoundary = 22;
        var playerHasReachedRightBoundary = transform.position.z > rightBoundary;

        if (playerHasReachedRightBoundary) {
            transform.position = new Vector3(transform.position.x, transform.position.y, rightBoundary);
        }
    }

    private void handlePlayerTopBoundary() {
        var TopBoundary = 7;
        var playerHasReachedTopBoundary = transform.position.y > TopBoundary;

        if (playerHasReachedTopBoundary) {
            transform.position = new Vector3(transform.position.x, TopBoundary, transform.position.z);
        }
    }

    private void handlePlayerBottomBoundary() {
        var BottomBoundary = -8;
        var playerHasReachedBottomBoundary = transform.position.y < BottomBoundary;

        if (playerHasReachedBottomBoundary) {
            transform.position = new Vector3(transform.position.x, BottomBoundary, transform.position.z);
        }
    }

    private void handlePlayerMovement() {
        handleVerticalMovementByPlayerInput();
        handleHorizontalMovementByPlayerInput();
    }

    private void handleHorizontalMovementByPlayerInput() {
        var horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * MovementSpeed * horizontalInput);
    }

    private void handleVerticalMovementByPlayerInput() {
        var verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.up * Time.deltaTime * MovementSpeed * verticalInput);
    }
}
