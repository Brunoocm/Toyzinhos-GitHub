using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/AllyMelee")]
public class AllyMelee : AllyObject
{
    public string nameOBJMELEE;
    public Sprite art;
    public int health;
    public int damage;
    public int speed;
    public float rangeAttack;

    //public AllyObject allyScriptable;

    public void Awake()
    {
        type = Types.Melee;
    }
}
