using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool isPlayerAlive = true;

    
    public static GameManager gm;

    private GameObject player;

    [SerializeField]
    private Transform playerStartPoint;

    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    private float difficulty;

    public float distance;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        gm = this;
    }

    private void Update()
    {
        if (!isPlayerAlive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }


        //Check player distance

        if(player != null)
        {
            distance = Vector3.Distance(player.transform.position, playerStartPoint.position);
            UIManager.uiManager.SetDistanceValue(distance);
        }

        
        cameraController.speed += Time.timeSinceLevelLoad / 10000 * difficulty;
        cameraController.speed = Mathf.Clamp(cameraController.speed, 5, 20);
    }
}
