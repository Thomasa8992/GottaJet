using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private ScoreKeeperController scoreKeeperController;
    // Start is called before the first frame update
    void Start()
    {
        scoreKeeperController = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeperController>();
    }

    // Update is called once per frame
    void Update() {
        DestroyOutOfBoundsObjectOnRightBoundary();
        DestroyGameObjectsOnLeftBoundary();
        DestroyOutOfBoundsOnLowerScreenBoundary();
    }

    private void DestroyOutOfBoundsOnLowerScreenBoundary() {
        var outOfBoundsYPosition = -10;

        if (transform.position.y < outOfBoundsYPosition) {
            Destroy(gameObject);
        }
    }

    private void DestroyOutOfBoundsObjectOnRightBoundary() {
        var rightOutOfBoundsZPosition = 35;
         
        if (transform.position.z > rightOutOfBoundsZPosition) {
            Destroy(gameObject);
        }
    }

    private void DestroyGameObjectsOnLeftBoundary() {
        var leftOutOfBoundsZPosition = -35;

        if (transform.position.z < leftOutOfBoundsZPosition) {
            Destroy(gameObject);
        }
    }
}
