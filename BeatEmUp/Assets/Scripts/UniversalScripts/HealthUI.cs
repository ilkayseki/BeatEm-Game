using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Image healthUI;

    private void Awake()
    {
        healthUI = GameObject.FindWithTag(Tags.HEALTH_UI).GetComponent<Image>();
    }

    public void DisplayHealth(float value)
    {
        //because slider value is between 0 and 1 -> (99/100 = 0.99)
        value /= 100;
        
        if (value < 0f)
            value = 0f;

        healthUI.fillAmount = value;
    }
}
