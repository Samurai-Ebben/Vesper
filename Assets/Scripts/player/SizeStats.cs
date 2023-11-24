using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeStats : MonoBehaviour
{
    List<float> statsSmall;
    List<float> statsMedium;
    List<float> statsLarge;

    [Space(10)]
    public float sizeSmall = 0.25f;
    public float sizeMedium = 0.75f;
    public float sizeLarge = 1.25f;

    [Space(10)]
    public float speedSmall = 10;
    public float speedMedium = 6;
    public float speedLarge = 4;

    [Space(10)]
    public float accelerationSmall = 30;
    public float accelerationMedium = 20;
    public float accelerationLarge = 10;

    [Space(10)]
    public float deaccelerationSmall = 10;
    public float deaccelerationMedium = 20;
    public float deaccelerationLarge = 30;

    [Space(10)]
    public float jumpHeightSmall = 4;
    public float jumpHeightMedium = 6;
    public float jumpHeightLarge = 8;

    [Space(10)]
    public float fallSpeedSmall = 1;
    public float fallSpeedMedium = 2;
    public float fallSpeedLarge = 3;

    [Space(10), Range(0, 1)]
    public float jumpCutOffSmall = .5f;
    public float jumpCutOffMedium = .1f;
    public float jumpCutOffLarge = .005f;

    [Space(10)]
    public float jumpCutOffSmall = 1;
    public float jumpCutOffMedium = 2;
    public float jumpCutOffLarge = 3;

    void Start()
    {
        statsSmall = new List<float>
        {
            sizeSmall,
            speedSmall,
            accelerationSmall,
            deaccelerationSmall,
            jumpHeightSmall,
            fallSpeedSmall,
            jumpCutOffSmall
        };

        statsMedium = new List<float>
        {
            sizeMedium,
            speedMedium,
            accelerationMedium,
            deaccelerationMedium,
            jumpHeightMedium,
            fallSpeedMedium,
            jumpCutOffMedium

        };

        statsLarge = new List<float>
        {
            sizeLarge,
            speedLarge,
            accelerationLarge,
            deaccelerationLarge,
            jumpHeightLarge,
            fallSpeedLarge,
            jumpCutOffLarge
        };
    }

    public List<float> ReturnStats(string size)
    {
        if (size.ToLower() == "small")
        {
            return statsSmall;
        }

        if (size.ToLower() == "large")
        {
            return statsLarge;
        }

        return statsMedium;
    }
    
}

