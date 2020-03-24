using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float movementSpeed;
    public GameObject enemyProjectile;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(enemyProjectile.GetComponent<BoxCollider>(), GetComponent<BoxCollider>());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }
}
