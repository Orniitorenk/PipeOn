using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject point;

    private void Start()
    {
        if (!this.gameObject.CompareTag("DeadZone"))
        {
            CreatePoints();
        }
        
    }

    private void CreatePoints()
    {
        float radiusCylinder = transform.localScale.x / 2;
        float radiusCube = transform.localScale.x / 2;

        float height = radiusCube + radiusCylinder;

        float minRange = transform.position.z - transform.localScale.y;
        float maxRange = transform.position.z + transform.localScale.y;

        Vector3 pos = new Vector3(transform.position.x, transform.position.y + height, Random.Range(minRange, maxRange));

        Instantiate(point, pos, Quaternion.identity);
    }

    
}
