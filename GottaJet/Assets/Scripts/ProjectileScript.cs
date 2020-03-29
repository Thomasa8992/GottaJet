using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        handleProjectileMovement();
    }

    private void handleProjectileMovement() {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }
}
