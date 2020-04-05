using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelKeeperController : MonoBehaviour
{
    public int fuelLevel = 100;
    public int curretLevel = 50;
    //private float decreasePerMinute = .5f;
    public Vector3 startingScale;

    // Start is called before the first frame update
    void Start()
    {

        startingScale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        //startingScale.x = (startingScale.x * fuelLevel) / 100;
        //fuelLevel = multipliedX -= Time.time * decreasePerMinute / 60f;
        //startingScale.x = (curretLevel / fuelLevel);
        Debug.Log(curretLevel / fuelLevel);
        //transform.localScale = new Vector3(startingScale.x, 0.7f, 1);
    }
}
