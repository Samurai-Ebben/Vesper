using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeStats : MonoBehaviour
{
    #region StatsLists

    List<float> statsSmall;
    List<float> statsMedium;
    List<float> statsLarge;

    #endregion

    #region SizeParameters

    [Header("Size Parameters")]
    [Space(10)]
    [Range(0, 3)] public float sizeSmall = 0.25f;
    [Range(0, 3)] public float sizeMedium = 0.75f;
    [Range(0, 3)] public float sizeLarge = 1.25f;

    #endregion

    #region MovementStats

    [Header("Movement Stats")]
    [Space(10)]
    [Range(0, 100)] public float speedSmall = 10;
    [Range(0, 100)] public float speedMedium = 6;
    [Range(0, 100)] public float speedLarge = 4;

    [Space(10)]
    [Range(0, 100)] public float accelerationSmall = 30;
    [Range(0, 100)] public float accelerationMedium = 20;
    [Range(0, 100)] public float accelerationLarge = 10;

    [Space(10)]
    [Range(0, 100)] public float deaccelerationSmall = 10;
    [Range(0, 100)] public float deaccelerationMedium = 20;
    [Range(0, 100)] public float deaccelerationLarge = 30;

    #endregion

    #region JumpStats

    [Header("Jump Stats")]
    [Space(10)]
    [Range(0, 50)] public float jumpHeightSmall = 4;
    [Range(0, 50)] public float jumpHeightMedium = 6;
    [Range(0, 50)] public float jumpHeightLarge = 8;

    [Space(10)]
    [Range(0, 100)] public float fallSpeedSmall = 1;
    [Range(0, 100)] public float fallSpeedMedium = 2;
    [Range(0, 100)] public float fallSpeedLarge = 3;

    [Space(10)]
    [Range(0, 1)] public float jumpCutOffSmall = .5f;
    [Range(0, 1)] public float jumpCutOffMedium = .1f;
    [Range(0, 1)] public float jumpCutOffLarge = .005f;

    #endregion

    #region GroundCheckSizes

    [Header("Ground Check Sizes")]
    [Space(10)]
    float groundCheckSizeSmallX;
    float groundCheckSizeMediumX;
    float groundCheckSizeLargeX;

    [Space(10)]
    float groundCheckSizeSmallY;
    float groundCheckSizeMediumY;
    float groundCheckSizeLargeY;

    #endregion

    #region AirMultipliers

    [Header("Air Multipliers")]
    [Space(10)]
    [Range(0, 10)] public float airSpeedMultiSmall = .9f;
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

    #endregion

    #region LandingSfxOffsets

    [Header("Landing SFX Offsets")]
    [Space(10)]
    public float landingSfxOffsetSmall = 0.05f;
    public float landingSfxOffsetMedium = 0.1f;
    public float landingSfxOffsetLarge = 0.5f;

    #endregion

    void Start()
    {
        UpdateStatValues();
    }

    public List<float> ReturnStats(Sizes size)
    {

        if (size == Sizes.SMALL)
            return statsSmall;

        else if (size == Sizes.BIG)
            return statsLarge;

        else if (size == Sizes.MEDIUM)
            return statsMedium;
        UpdateStatValues();

        return statsMedium;
    }

    private void UpdateStatValues()
    {
        groundCheckSizeSmallX   =   sizeSmall;
        groundCheckSizeMediumX  =   sizeMedium;
        groundCheckSizeLargeX   =   sizeLarge;        
        groundCheckSizeSmallY   =   0.007f;
        groundCheckSizeMediumY  =   0.01f;
        groundCheckSizeLargeY   =   0.009f;

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
            groundCheckSizeLargeX,
            groundCheckSizeLargeY,
            airSpeedMultiLarge,
            airAccMultiLarge,
            airDecMultiLarge,
            landingSfxOffsetLarge

        };
    }
}

