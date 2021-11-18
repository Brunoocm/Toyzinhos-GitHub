using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/AllyRange")]
public class AllyRange : AllyObject
{
    public string nameobjobj;
    public Sprite art;
    public int health;
    public int damage;
    public int speed;
    public float rangeAttack;
    public void Awake()
    {
        type = Types.Range;
    }
}
