using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        DestroyOutOfBoundsObjectOnRightBoundary();
        DestroyGameObjectsOnLeftBoundary();
        DestroyOutOfBoundsOnLowerScreenBoundary();
    }

    private void DestroyOutOfBoundsOnLowerScreenBoundary() {
        var outOfBoundsYPosition = -8;

        if (transform.position.y < outOfBoundsYPosition) {
            Destroy(gameObject);
        }
    }

    private void DestroyOutOfBoundsObjectOnRightBoundary() {
        var rightOutOfBoundsZPosition = 20;
         
        if (transform.position.z > rightOutOfBoundsZPosition) {
            Destroy(gameObject);
        }
    }

    private void DestroyGameObjectsOnLeftBoundary() {
        var leftOutOfBoundsZPosition = -20;

        if (transform.position.z < leftOutOfBoundsZPosition) {
            Destroy(gameObject);
        }
    }
}
