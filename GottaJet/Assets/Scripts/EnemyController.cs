using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject projectile;
    public float movementSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(projectile.GetComponent<BoxCollider>(), GetComponent<BoxCollider>());
        InvokeRepeating("ShootProjectile", 1.5f, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }

    void ShootProjectile() {
        Instantiate(projectile, transform.position + transform.TransformDirection(new Vector3(0, 1.1f, 2)), projectile.transform.rotation);
    }
}
