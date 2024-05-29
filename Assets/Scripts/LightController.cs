using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light redLight;
    public Light greenLight;

    void Start()
    {
        redLight.enabled = false;
        greenLight.enabled = false;
    }

    public void SetGameResult(bool isSuccess)
    {
        if (isSuccess)
        {
            greenLight.enabled = true;
            redLight.enabled = false;
        }
        else
        {
            greenLight.enabled = false;
            redLight.enabled = true;
        }
    }
}