using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed = 10;

    public GameObject projectile;

    public float fireRate = 0.5F;
    private float nextFire = 0.0F;

    public GameObject fuel;

    private SoundController soundController;

    // Start is called before the first frame update
    void Start()
    {
        soundController = GameObject.Find("SoundObject").GetComponent<SoundController>();
    }

    // Update is called once per frame
    void Update() {
        handlePlayerMovement();
        handlePlayerBoundaries();
        shootProjectile();
    }

    private void OnCollisionEnter(Collision collision) {
        if(!collision.gameObject.CompareTag("Fuel")) {
            handlePlayerCollision(collision);
        } else {
            handleFuelCollision(collision);
        }
    }

    private static void handleFuelCollision(Collision collision) {
        Debug.Log("Award player fuel and points");
        Destroy(collision.gameObject);
    }

    private void handlePlayerCollision(Collision collision) {
        soundController.audioSource.PlayOneShot(soundController.explosionSound, 1);
        handleGameOverSequence(collision);
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    private void handleGameOverSequence(Collision collision) {
        Debug.Log("Game Over");

        SceneManager.LoadScene("Challenge 1");
    }

    private void shootProjectile() {
        var keyCodeIsPressedAndNextFireIsReady = (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && Time.time > nextFire;

        if (keyCodeIsPressedAndNextFireIsReady) {
            nextFire = Time.time + fireRate;

            var projectilePositionRelativeToPlayerPosition = transform.position + transform.TransformDirection(new Vector3(0, 1.1f, 2));

            Instantiate(projectile, projectilePositionRelativeToPlayerPosition, projectile.transform.rotation);
            soundController.audioSource.PlayOneShot(soundController.projectileSound, 1);
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
        var horizontalMovement = Vector3.forward * Time.deltaTime * MovementSpeed * horizontalInput;

        transform.Translate(horizontalMovement);
    }

    private void handleVerticalMovementByPlayerInput() {
        var verticalInput = Input.GetAxis("Vertical");
        var verticalMovement = Vector3.up * Time.deltaTime * MovementSpeed * verticalInput;

        transform.Translate(verticalMovement);
    }
}
