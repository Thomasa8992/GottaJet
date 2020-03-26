using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
    }

    void OnTriggerEnter(Collider other) {
            Destroy(gameObject);
            Destroy(other.gameObject);
    }
}
