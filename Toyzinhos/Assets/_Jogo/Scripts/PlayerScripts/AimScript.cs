using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AimScript : MonoBehaviour
{
    public Camera camerasuper;
    public LayerMask layerSpawn;
    public LayerMask layerDont;
    public float distanceRaycast;

    [HideInInspector] public bool canSpawn;
    [HideInInspector] public Vector3 spawnPos;

    void Update()
    {
        RaycastHit hit;
        Ray ray = camerasuper.ScreenPointToRay(Input.mousePosition);

        //Physics.Raycast(ray, out hit, distanceRaycast, layerSpawn);

        if (Physics.Raycast(ray, out hit, distanceRaycast, layerDont))
        {
            canSpawn = false;
        }
        else
        {
            if (Physics.Raycast(ray, out hit, distanceRaycast, layerSpawn))
            {
                spawnPos = new Vector3(hit.point.x, hit.point.y, hit.point.z);

                canSpawn = true;
                Debug.DrawRay(camerasuper.transform.position, camerasuper.transform.forward * distanceRaycast, Color.green);
            }
            else
            {
                canSpawn = false; 
                Debug.DrawRay(camerasuper.transform.position, camerasuper.transform.forward * distanceRaycast, Color.red);
            }
        }

    }
}

