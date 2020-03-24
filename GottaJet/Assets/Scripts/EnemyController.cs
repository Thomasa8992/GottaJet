using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootProjectile", 2, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShootProjectile() {
        Instantiate(projectile, transform.position, projectile.transform.rotation);
    }
}
