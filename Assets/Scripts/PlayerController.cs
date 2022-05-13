using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MaxHealth { get; set; } = 200f;
    public float Health { get; set; } = 100f;
    public float MaxArmor { get; set; }
    public float Armor { get; set; }
    private Rigidbody body;
    private int h = 0;
    private int v = 0;
    public float Movespeed { get; set; } = 5f;
    public float CameraSensitivity { get; set; } = 5f;
    public Transform head;
    public Transform torso;
    public GameObject camera;
    float xAxisClamp = 0;
    GameObject armRight;
    GameObject armLeft;
    float pickUpRange = 5f;
    public GameObject ItemInRange { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        //camera = GetComponentInChildren<Camera>().gameObject;
        armRight = GameObject.Find("ArmRight");
        armLeft = GameObject.Find("ArmLeft");
        if (armLeft.GetComponent<PrimaryWeapon>() != null)
        {
            armLeft.GetComponent<PrimaryWeapon>().Equipped = true;
        }
        if (armLeft.GetComponent<SecondaryWeapon>() != null)
        {
            armLeft.GetComponent<SecondaryWeapon>().Equipped = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
        RotateCamera();
        ItemInRange = ShowNearbyItems();

        if (Input.GetKey(KeyCode.Mouse0)){
            ActivatePrimaryWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ActivateSecondaryWeapon();
        }
        if (Input.GetKeyDown(KeyCode.F) && ItemInRange != null) {
            EquipItem(ItemInRange);
        }
        if (Health > MaxHealth) {
            Health = MaxHealth;
        }
        if (Armor < 0) {
            Armor = 0;
        }
        if (Health <= 0) {
            Die();
            Health = 0;
        }
    }

    void Move() {
        if (Input.GetKey(KeyCode.W))
        {
            v = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            v = -1;
        }
        else
        {
            v = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            h = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            h = -1;
        }
        else
        {
            h = 0;
        }

        body.velocity = (body.transform.right * h + body.transform.forward * v).normalized * Movespeed + new Vector3(0, body.velocity.y, 0);
    }
    void RotateCamera() {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 rotCamera = head.rotation.eulerAngles;
        Vector3 rotTorso = torso.rotation.eulerAngles;


        rotCamera.x -= mouseY * CameraSensitivity;
        //rotCamera.y += mouseX * CameraSensitivity;
        rotTorso.y += mouseX * CameraSensitivity;


        xAxisClamp -= mouseY * CameraSensitivity;

        if (xAxisClamp > 90) {
            xAxisClamp = 90;
            rotCamera.x = 90;
        } else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            rotCamera.x = 270;
        }

        head.rotation = Quaternion.Euler(rotCamera);
        body.rotation = Quaternion.Euler(rotTorso);
    }
    void ActivatePrimaryWeapon() {
        GameObject spot = armRight.transform.Find("PrimarySpot").gameObject;
        PrimaryWeapon weapon = spot.GetComponentInChildren<PrimaryWeapon>();
        if (weapon != null) {
            weapon.Activate();
        }
    }
    void ActivateSecondaryWeapon(){
        GameObject spot = armLeft.transform.Find("SecondarySpot").gameObject;
        SecondaryWeapon weapon = spot.GetComponentInChildren<SecondaryWeapon>();
        if (weapon != null) {
            weapon.Activate();
        }
    }
    GameObject ShowNearbyItems() {
        Collider[] colls = Physics.OverlapSphere(new Vector3(transform.position.x, 0, transform.position.z), pickUpRange);
        Collider ret = colls[0];
        foreach (Collider c in colls)
        {
            if ((ret == null || ret.GetComponent<IWeapon>() == null) && c.GetComponent<IWeapon>() != null) {
                if (!c.GetComponent<IWeapon>().Equipped) {
                    ret = c;
                }
            }

            if (c != null && ret != null && c.GetComponent<IWeapon>() != null && ret.GetComponent<IWeapon>() != null) {
                if (((c.transform.position - transform.position).magnitude < (ret.transform.position - transform.position).magnitude && !c.GetComponent<IWeapon>().Equipped) || ret.GetComponent<IWeapon>().Equipped)
                {
                    ret = c;
                }
            }  
        }
        if (ret.GetComponent<IWeapon>() != null && !ret.GetComponent<IWeapon>().Equipped) {
            return ret.gameObject;
        }
        return null;
    }
    void EquipItem(GameObject item) {
        if (item.GetComponent<PrimaryWeapon>() != null) {
            PrimaryWeapon check = armRight.transform.Find("PrimarySpot").GetComponentInChildren<PrimaryWeapon>();
            if (check != null)
            {
                GameObject equiped = armRight.transform.Find("PrimarySpot").GetComponentInChildren<PrimaryWeapon>().gameObject;
                equiped.transform.SetParent(null);
                equiped.transform.position = item.transform.position;
                equiped.transform.rotation = Quaternion.identity;
                equiped.GetComponent<PrimaryWeapon>().Equipped = false;
                item.transform.SetParent(armRight.transform.Find("PrimarySpot"));
                item.transform.position = armRight.transform.Find("PrimarySpot").position;
                item.transform.rotation = armRight.transform.Find("PrimarySpot").rotation;
                item.GetComponent<PrimaryWeapon>().Equipped = true;
            }
            else {
                item.transform.SetParent(armRight.transform.Find("PrimarySpot"));
                item.transform.position = armRight.transform.Find("PrimarySpot").position;
                item.transform.rotation = armRight.transform.Find("PrimarySpot").rotation;
                item.GetComponent<PrimaryWeapon>().Equipped = true;
            }
        }
        if (item.GetComponent<SecondaryWeapon>() != null)
        {
            SecondaryWeapon check = armLeft.transform.Find("SecondarySpot").GetComponentInChildren<SecondaryWeapon>();
            if (check != null)
            {
                GameObject equiped = armLeft.transform.Find("SecondarySpot").GetComponentInChildren<SecondaryWeapon>().gameObject;
                equiped.transform.SetParent(null);
                equiped.transform.position = item.transform.position;
                equiped.transform.rotation = Quaternion.identity;
                equiped.GetComponent<SecondaryWeapon>().Equipped = false;
                item.transform.SetParent(armLeft.transform.Find("SecondarySpot"));
                item.transform.position = armLeft.transform.Find("SecondarySpot").position;
                item.transform.rotation = armLeft.transform.Find("SecondarySpot").rotation;
                item.GetComponent<SecondaryWeapon>().Equipped = true;

            }
            else
            {
                item.transform.SetParent(armLeft.transform.Find("SecondarySpot"));
                item.transform.position = armLeft.transform.Find("SecondarySpot").position;
                item.transform.rotation = armLeft.transform.Find("SecondarySpot").rotation;
                item.GetComponent<SecondaryWeapon>().Equipped = true;
            }
        }
    }
    public void GetDamage(float dmg)
    {
        if (Armor > 0)
        {
            if (Armor >= dmg)
            {
                Armor -= dmg;
            }
            else
            {
                Health -= (dmg - Armor);
                Armor = 0;
            }
        }
        else {
            Health -= dmg;
        }
    }
    public void GiveHeal(float heal) {
        Health += heal;
    }
    public void GiveArmor(float armor)
    {
        Armor += armor;
    }
    void Die() {
        body.velocity = Vector3.zero;
    }
    void OnDrawGizmos() {
    }
}
