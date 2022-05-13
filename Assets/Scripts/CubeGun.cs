using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGun : PrimaryWeapon
{
    // Start is called before the first frame update
    void Start()
    {
        Name = "Cube Gun";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void Activate() {
        Debug.Log("Cube Fired!");
    }
}
