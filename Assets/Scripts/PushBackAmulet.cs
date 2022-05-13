using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBackAmulet : SecondaryWeapon
{
    float actionRadius = 5f;
    float speed = 5f;
    float distance = 30f;
    public Sprite img;
    float force = 1f;
    float pasiveArmorGiven = 1f;
    float maxArmorGranted = 20f;
    float timer = 5f;
    float count = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Name = "Pushback Amulet";
        Cooldown = 7f;
        CdCount = 0f;
        Damage = 0;
        Description = "Pushes back nearby enemies." + "\n" + "Pasive: Grants " + pasiveArmorGiven +" Armor every " + timer + " sec (Max: " + maxArmorGranted + ")";
        Img = img;
    }

    // Update is called once per frame
    void Update()
    {
        if (Equipped && GetComponentInParent<PlayerController>().Armor < maxArmorGranted && count <= 0){
            GetComponentInParent<PlayerController>().GiveArmor(pasiveArmorGiven);
            count = timer;
        }
        if (count > 0) {
            count -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Mouse1) && CdCount == 0) {
            Activate();
        }
        if (CdCount > 0) {
            CdCount -= Time.deltaTime;
        }
    }

    override public void Activate() {
        if (CdCount <= 0) {
            Collider[] colls = Physics.OverlapSphere(transform.position, actionRadius);
            foreach (Collider c in colls)
            {
                if (c.gameObject.layer == 9)
                {
                    c.gameObject.AddComponent<PushBackAmuletEffect>();
                }
            }
            CdCount = Cooldown;
        }  
    }
}
