using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text healthText;
    public Text armorText;
    public Text ammoText;
    public Text cdText;
    public Text ItemName;
    public Text ROFText;
    public Text gunDmgText;
    public Text reloadTimeText;
    public Text CDText;
    public Text amuletDmgText;
    public Text description;
    public Image img;
    public Image cdImg;
    public Image ammoImg;
    PlayerController player;
    PrimaryWeapon weapon;
    SecondaryWeapon artifact;
    public GameObject ItemPanel;
    public GameObject primaryWeaponPanel;
    public GameObject secondaryWeaponPanel;
    public GameObject ammoPanel;
    public GameObject CDPanel;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        weapon = GameObject.Find("PrimarySpot").GetComponentInChildren<PrimaryWeapon>();
        artifact = GameObject.Find("SecondarySpot").GetComponentInChildren<SecondaryWeapon>();
        healthText.text = player.Health + "/" + player.MaxHealth;
        armorText.text = player.Armor.ToString();

        if (weapon != null)
        {
            ammoPanel.SetActive(true);
            ammoImg.sprite = weapon.GetComponent<PrimaryWeapon>().Img;
            if (weapon.ReloadCount > 0)
            {
                ammoText.text = "Reloading...";
            }
            else
            {
                ammoText.text = weapon.ClipAmmo + "/" + weapon.MaxClipAmmo;
            }
        }
        else {
            ammoPanel.SetActive(false);
        }

        if (artifact != null)
        {
            CDPanel.SetActive(true);
            cdImg.sprite = artifact.GetComponent<SecondaryWeapon>().Img;
            if (artifact.CdCount > 0)
            {
                cdText.text = artifact.CdCount.ToString("F1");
            }
            else
            {
                cdText.text = "READY";
            }
        }
        else {
            CDPanel.SetActive(false);
        }

        if (player.ItemInRange != null) {
            ItemName.text = player.ItemInRange.GetComponent<IWeapon>().Name;
            img.sprite = player.ItemInRange.GetComponent<IWeapon>().Img;
            if (player.ItemInRange.GetComponent<PrimaryWeapon>() != null) {
                ROFText.text = (1f / player.ItemInRange.GetComponent<PrimaryWeapon>().RateOfFire) + "/sec";
                gunDmgText.text = player.ItemInRange.GetComponent<PrimaryWeapon>().Damage.ToString();
                reloadTimeText.text = player.ItemInRange.GetComponent<PrimaryWeapon>().ReloadTime +" sec";
                primaryWeaponPanel.SetActive(true);
                ItemPanel.SetActive(true);
                secondaryWeaponPanel.SetActive(false);
            } 
            else if (player.ItemInRange.GetComponent<SecondaryWeapon>() != null)
            {
                CDText.text = player.ItemInRange.GetComponent<SecondaryWeapon>().Cooldown + " sec";
                amuletDmgText.text = player.ItemInRange.GetComponent<SecondaryWeapon>().Damage.ToString();
                description.text = player.ItemInRange.GetComponent<SecondaryWeapon>().Description;
                secondaryWeaponPanel.SetActive(true);
                ItemPanel.SetActive(true);
                primaryWeaponPanel.SetActive(false);
            }
        }
        else
        {
            ItemPanel.SetActive(false);
        }
       
    }
}
