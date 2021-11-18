using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum Types
{
    Melee,
    Range, 
    Support,
    Energy
}

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/Ally")]
public class AllyObject : ScriptableObject
{
    public Types type;
    //public string name;
    //public Sprite art;
    //public int health;
    //public int damage;
    //public int speed;
    //public float rangeAttack;

}
