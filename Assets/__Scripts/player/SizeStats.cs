using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeStats : MonoBehaviour
{
    List<float> statsSmall;
    List<float> statsMedium;
    List<float> statsLarge;

    [Space(10)]
    [Range(0, 3)]public float sizeSmall = 0.25f;
    [Range(0, 3)]public float sizeMedium = 0.75f;
    [Range(0, 3)]public float sizeLarge = 1.25f;

    [Space(10)]
    [Range(0, 100)]public float speedSmall = 10;
    [Range(0, 100)]public float speedMedium = 6;
    [Range(0, 100)]public float speedLarge = 4;

    [Space(10)]
    [Range(0, 100)]public float accelerationSmall = 30;
    [Range(0, 100)]public float accelerationMedium = 20;
    [Range(0, 100)]public float accelerationLarge = 10;

    [Space(10)]
    [Range(0, 100)]public float deaccelerationSmall = 10;
    [Range(0, 100)]public float deaccelerationMedium = 20;
    [Range(0, 100)]public float deaccelerationLarge = 30;

    [Space(10)]
    [Range(0, 50)]public float jumpHeightSmall = 4;
    [Range(0, 50)]public float jumpHeightMedium = 6;
    [Range(0, 50)]public float jumpHeightLarge = 8;

    [Space(10)]
    [Range(0, 100)]public float fallSpeedSmall = 1;
    [Range(0, 100)]public float fallSpeedMedium = 2;
    [Range(0, 100)]public float fallSpeedLarge = 3;

    [Space(10)]
    [Range(0, 1)]public float jumpCutOffSmall = .5f;
    [Range(0, 1)] public float jumpCutOffMedium = .1f;
    [Range(0, 1)] public float jumpCutOffLarge = .005f;

    [Space(10)]
    float groundCheckSizeSmallX;
    float groundCheckSizeMediumX;
    float groundCheckSizeBigX;

    [Space(10)]
    float groundCheckSizeSmallY;
    float groundCheckSizeMediumY;
    float groundCheckSizeBigY;

    [Space(10)]
    [Range(0,10)]  public float airSpeedMultiSmall = .9f;
    [Range(0, 10)] public float airSpeedMultiMedium = 1f;
    [Range(0, 10)] public float airSpeedMultiLarge = 1f;
                    
    [Space(10)]     
    [Range(0, 10)] public float airAccMultiSmall = 1f;
    [Range(0, 10)] public float airAccMultiMedium = 1f;
    [Range(0, 10)] public float airAccMultiLarge = 1f;
                   
    [Space(10)]    
    [Range(0, 10)] public float airDecMultiSmall = .9f;
    [Range(0, 10)] public float airDecMultiMedium = .9f;
    [Range(0, 10)] public float airDecMultiLarge = .9f;

    [Space(10)]
    public float landingSfxOffsetSmall = 0.05f;
    public float landingSfxOffsetMedium = 0.1f;
    public float landingSfxOffsetLarge = 0.5f;

    void Start()
    {
        UpdateStatValues();
    }

    public List<float> ReturnStats(Sizes size)
    {
        UpdateStatValues();

        if (size == Sizes.SMALL)
        {
            return statsSmall;
        }

        if (size == Sizes.LARGE)
        {
            return statsLarge;
        }

        if (size == Sizes.MEDIUM)
        {
            return statsMedium;
        }

        //TODO remove print
        print("Unknown argument, returning statsMedium");
        return statsMedium;
    }

    private void UpdateStatValues()
    {
        groundCheckSizeBigX     =   0.05f;
        groundCheckSizeSmallX   =   0.05f;
        groundCheckSizeMediumX  =   0.05f;

        groundCheckSizeBigY = 0.009f;
        groundCheckSizeSmallY = 0.007f;
        groundCheckSizeMediumY = 0.01f;
        statsSmall = new List<float>
        {
            sizeSmall,
            speedSmall,
            accelerationSmall,
            deaccelerationSmall,
            jumpHeightSmall,
            fallSpeedSmall,
            jumpCutOffSmall,
            groundCheckSizeSmallX,
            groundCheckSizeSmallY,
            airSpeedMultiSmall,
            airAccMultiSmall,
            airDecMultiSmall,
            landingSfxOffsetSmall
    };

        statsMedium = new List<float>
        {
            sizeMedium,
            speedMedium,
            accelerationMedium,
            deaccelerationMedium,
            jumpHeightMedium,
            fallSpeedMedium,
            jumpCutOffMedium,
            groundCheckSizeMediumX,
            groundCheckSizeMediumY,
            airSpeedMultiMedium,
            airAccMultiMedium,
            airDecMultiMedium,
            landingSfxOffsetMedium
        };

        statsLarge = new List<float>
        {
            sizeLarge,
            speedLarge,
            accelerationLarge,
            deaccelerationLarge,
            jumpHeightLarge,
            fallSpeedLarge,
            jumpCutOffLarge,
            groundCheckSizeBigX,
            groundCheckSizeBigY,
            airSpeedMultiLarge,
            airAccMultiLarge,
            airDecMultiLarge,
            landingSfxOffsetLarge

        };
    }
}

