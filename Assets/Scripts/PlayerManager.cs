using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region constants
    private const float sizeStandart = 0.35f;
    private const float checkerRadius = 0.2f;
    private const float skinOffset = 0.05f;

    #endregion

    #region Serialized Fields
    [SerializeField]
    private Vector3 defaultSize;

    [SerializeField]
    private LayerMask cylinderLayer;

    [HideInInspector]
    public bool canCollect = false;

    [SerializeField]
    private float health = 5.0f;

    [SerializeField]
    AudioClip deathSound;

    

    #endregion

    #region Unity
    private void Update()
    {
        //defining cylinder and radius

        Transform cyl = Physics.OverlapSphere(transform.position, checkerRadius, cylinderLayer)[0].transform;
        float cyl_rad = cyl.localScale.x * sizeStandart;


        //Check Death Status
        if (cyl_rad > transform.localScale.y)
        {
            Death();
        }

        if(health <= 0)
        {
            Death();
        }

        if (cyl.CompareTag("DeadZone"))
        {
            if(cyl_rad + skinOffset > transform.localScale.y)
            {
                Death();
            }
        }

        //check can collectable objects status
        if (cyl_rad + skinOffset > transform.localScale.y)
        {
            canCollect = true;
        }
        else
        {
            canCollect = false;
        }

        ChangeRingRadius(cyl_rad);
        HealthCount();
    }
    #endregion

    #region Functions

    private void Death()
    {
        //Stop camera controller
        if(Camera.main != null)
        {
            Camera.main.GetComponent<CameraController>().enabled = false;
        }

        //oepn game over panel
        UIManager.uiManager.OpenGameOverUI();


        GameManager.gm.isPlayerAlive = false;

        //Play death sound
        Camera.main.GetComponent<AudioSource>().PlayOneShot(deathSound);

        //Save high score
        if(GameManager.gm.distance > PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", GameManager.gm.distance);

        }

        //Set High Score Text
        UIManager.uiManager.SetHighScoreText();


        //destroy the player
        Destroy(this.gameObject);              
    }

    private void ChangeRingRadius(float cyl_rad)
    {
        //change ring radius
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Stationary)
            {

                Vector3 targetVector = new Vector3(cyl_rad, cyl_rad, defaultSize.z);

                transform.localScale = Vector3.Slerp(transform.localScale, targetVector, 0.125f);

            }
            
        }
        else
        {
            transform.localScale = Vector3.Slerp(transform.localScale, defaultSize, 0.125f);
        }


    }

    private void HealthCount()
    {
        health = Mathf.Clamp(health,-1, 10.0f);

        if(health >= 0)
        {
            health -= Time.deltaTime;
            UIManager.uiManager.SetPlayerHealth(health);
        }
        
    }

    public void IncreaseHealth(float value)
    {
        health += value;
    }
    #endregion

}
