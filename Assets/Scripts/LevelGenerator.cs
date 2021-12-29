using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Cylinder Attributes")]
    [SerializeField]
    private GameObject cylinder;

    private GameObject previousCylinder;

    [SerializeField]
    private Color deadZone;

    [SerializeField]
    private float minRadius, maxRadius;
    

    
    #region Functions
    private float FindRadius(float minR, float maxR)
    {
        float radius = Random.Range(minR, maxR);

        if(previousCylinder != null)
        {
            while (Mathf.Abs(radius - previousCylinder.transform.localScale.x) < 0.4f)
            {
                radius = Random.Range(minR, maxR);
            }
        }
               
        return radius;
    }

    public void SpawnCylinder()
    {
        // Find a random radius and height
        float radius = FindRadius(1f, 3f);
        float height = Random.Range(1f, 3f);

        //Apply radius and height to prefab
        cylinder.transform.localScale = new Vector3(radius, height, radius);

        //First Cylinder
        if (previousCylinder == null)
        {

            previousCylinder = Instantiate(cylinder, Vector3.zero, Quaternion.identity);

        }
        //All other cylinders
        else
        {

            float spawnPoint = previousCylinder.transform.position.z + previousCylinder.transform.localScale.y + cylinder.transform.localScale.y;

            previousCylinder = Instantiate(cylinder, new Vector3(0, 0, spawnPoint), Quaternion.identity);

            //Creating dead zone cylinders
            if (Random.value < 0.1f)
            {
                previousCylinder.GetComponent<Renderer>().material.color = deadZone;
                previousCylinder.tag = "DeadZone";
            }
        }
        previousCylinder.transform.Rotate(90, 0, 0);
    }
}
#endregion