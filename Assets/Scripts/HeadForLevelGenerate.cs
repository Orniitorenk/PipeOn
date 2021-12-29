using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadForLevelGenerate : MonoBehaviour
{
    [SerializeField]
    private LevelGenerator levelGenerator;

    [SerializeField]
    private LayerMask cyl_layer;

    private void Update()
    {
        Collider[] cyl = Physics.OverlapSphere(transform.position, 1f, cyl_layer);
        if(cyl.Length <= 0)
        {
            levelGenerator.SpawnCylinder();
        }
    }
}
