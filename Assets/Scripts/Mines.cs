using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : SecondaryWeapon
{
    public Sprite img;
    public GameObject proximityMine;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Name = "Proximity Mines";
        Cooldown = 10f;
        CdCount = 0f;
        Damage = 20f;
        Description = "Places a proximity mine that explode on enemy contact and inflicts " +Damage+ " damage in an area (Lasts 120 sec)";
        Img = img;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CdCount > 0) {
            CdCount -= Time.deltaTime;
        }
    }
    public override void Activate()
    {
        if (CdCount <= 0) {
            animator.SetTrigger("SetMine");
            Vector3 pos = new Vector3(transform.position.x, 0.05f, transform.position.z);
            proximityMine.GetComponent<ProximityMine>().Damage = Damage;
            Instantiate<GameObject>(proximityMine, pos, Quaternion.identity);
            CdCount = Cooldown;
        } 
    }
}
