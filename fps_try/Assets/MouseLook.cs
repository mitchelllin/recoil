using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseLook : MonoBehaviour
{
    public float mouseSens = 100f;

    public Transform playerBody;

    float xRotation = 0f;
    float recVert = 0f;
    float recHoriz = 0f;

    public float recSpeed = 5f;
    public float recoverSpeed = 1f;

    float totalrecV = 0f;
    float totalrecH = 0f;
    float recovery = .1f;
    float timeSinceShot = 0f;
    float allRecoilMultiplier = .5f;
    float startingRecover = .1f;

    public shake camParent;

    bool moveDown = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void Recoil (float vert, float horiz) {
        recVert += vert;
        recHoriz += Random.Range(-1f * horiz, 1f * horiz);
        StartCoroutine(camParent.Shake(.1f,.3f));
    }
    void Update()
    {
        //Adding to total Vertical and Horizontal Recoil.
        totalrecV += recVert;
        totalrecH += recHoriz;
        //Adding Recoil to aim
        float mouseX = recHoriz + Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = recVert + Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        //Calculation for whether mouse was moved down
        float mouseInputY = Input.GetAxis("Mouse Y");
        if (mouseInputY < 0){
            moveDown = false;
            totalrecV = 0;
        }

        //Reducing recVert over time to reduce amount of vertical climb over time after a shot
        recVert -= recSpeed * Time.deltaTime;
        if (recVert <0) {
            recVert = 0;
        }
        //Same but for Horizontal Recoil
        recHoriz -= recSpeed * Time.deltaTime;
        if (recHoriz <0) {
            recHoriz = 0;
        }

        //Recovery Calculations -
        //Recovery only happens if mouse has not been moved down during shooting.
        //
        if (!Input.GetButton("Fire1") && moveDown == true) {
            timeSinceShot += Time.deltaTime;
            if (timeSinceShot > .01f){
                //totalrec is the total distance moved up by recoil
                if (totalrecV > 0) {
                    //recovery slowly ramps up
                    mouseY -= recovery;
                    totalrecV -= recovery;
                    recovery += recoverSpeed * Time.deltaTime;
                }   
                if (totalrecH > 0) {
                    mouseX -= recovery;
                    totalrecH -= recovery;
                }
            }
        }
        else {
            if (timeSinceShot != 0f)
            {
                recovery = startingRecover;
                timeSinceShot = 0f;
                
            }
            if (Input.GetButton("Fire1"))
            {
                moveDown = true;
            }
        }
        if (totalrecV > 0 && moveDown == true && timeSinceShot < .5f) {
            mouseY -= totalrecV*allRecoilMultiplier *Time.deltaTime;
            totalrecV -= totalrecV*allRecoilMultiplier * Time.deltaTime;
        }

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    
}
