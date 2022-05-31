using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public float power = 0.2f;
    public float duration = 0.2f;
    public float slowDownAmount = 1f;
    private bool shouldShake;
    private float initialDuration;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        //transform.localPosition -> parent transform position, transform.position -> root transform position
        startPosition = transform.localPosition; 
        initialDuration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        Shake();
    }

    //shake
    private void Shake()
    {
        //if we should shake the camera
        if (shouldShake)
        {
            if(duration > 0f)
            {
                //yeni pozisyona = önceki konumu + 1 radius luk bir sphere deki 
                //herhangi bir noktanın konumu * powerı ekliyor.
                transform.localPosition = startPosition + Random.insideUnitSphere * power;
                //smooth bir hareket için süreyi yavaşlatıyor
                duration -= Time.deltaTime * slowDownAmount;
            }
            else // her şeyi eski haline getiriyor.
            {
                shouldShake = false;
                duration = initialDuration;
                transform.localPosition = startPosition;
            }
        } //if we should shake the camera
    }//shake


    //shouldShake is private, to reach this variable we should use this below;
    public bool ShouldShake
    {
        get
        {
            return shouldShake;
        }
        set
        {
            shouldShake = value;
        }
    }
}
