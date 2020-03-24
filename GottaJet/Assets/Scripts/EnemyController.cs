using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(projectile.GetComponent<BoxCollider>(), GetComponent<BoxCollider>());
        InvokeRepeating("ShootProjectile", 2, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShootProjectile() {
        Instantiate(projectile, transform.position + transform.TransformDirection(new Vector3(0, 1.1f, 2)), projectile.transform.rotation);
    }
}
