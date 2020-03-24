using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float movementSpeed;
    public GameObject playerProjectile;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(playerProjectile.GetComponent<BoxCollider>(), GetComponent<BoxCollider>());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
    }
}
