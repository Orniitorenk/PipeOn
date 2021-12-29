using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField]
    private Vector3 axis;

    [SerializeField]
    private LayerMask playerLayer;

    [SerializeField]
    private Color collectableColor, nonCollectableColor;

    [SerializeField]
    private AudioClip pickUpSound;
    
    private PlayerManager playerManager;

    private void Awake()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        transform.Rotate(axis * Time.deltaTime);


        if (playerManager.canCollect)
        {
            //color and rotation speed
            axis.y = 270;

            GetComponent<MeshRenderer>().material.color = collectableColor;

            bool touchintToPlayer = Physics.CheckSphere(transform.position, 0.9f, playerLayer);

            if (touchintToPlayer)
            {
                playerManager.IncreaseHealth(2.0f);

                Camera.main.GetComponent<AudioSource>().PlayOneShot(pickUpSound);

                Destroy(this.gameObject);
            }
        }
        else
        {
            axis.y = 45;
            GetComponent<MeshRenderer>().material.color = nonCollectableColor;
        }

        
    }
}
