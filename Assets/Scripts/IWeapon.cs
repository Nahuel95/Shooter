using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IWeapon
{
    bool Equipped { get; set; }
    string Name { get; set; }
    Sprite Img { get; set; }
    bool Shooting { get; set; }
    void Activate();
}
