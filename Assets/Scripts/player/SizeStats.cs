using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeStats : MonoBehaviour
{
    List<float> propertiesSmall;
    List<float> propertiesMedium;
    List<float> propertiesLarge;

    public float sizeSmall = 0.25f;
    public float sizeMedium = 0.75f;
    public float sizeLarge = 1.25f;

    public float speedSmall = 10;
    public float speedMedium = 6;
    public float speedLarge = 4;
    
    public float accelerationSmall = 30;
    public float accelerationMedium = 20;
    public float accelerationLarge = 10;
     
    public float deaccelerationSmall = 10;
    public float deaccelerationMedium = 20;
    public float deaccelerationLarge = 30;
    
    public float jumpHeightSmall = 4;
    public float jumpHeightMedium = 6;
    public float jumpHeightLarge = 8;
    
    public float fallSpeedSmall = 1;
    public float fallSpeedMedium = 2;
    public float fallSpeedLarge = 3;

    void Start()
    {
        propertiesSmall = new List<float>
        {
            sizeSmall,
            speedSmall,
            accelerationSmall,
            deaccelerationSmall,
            jumpHeightSmall,
            fallSpeedSmall
        };

        propertiesMedium = new List<float>
        {
            sizeMedium,
            speedMedium,
            accelerationMedium,
            deaccelerationMedium,
            jumpHeightMedium,
            fallSpeedMedium
        };

        propertiesLarge = new List<float>
        {
            sizeLarge,
            speedLarge,
            accelerationLarge,
            deaccelerationLarge,
            jumpHeightLarge,
            fallSpeedLarge
        };
    }
}

