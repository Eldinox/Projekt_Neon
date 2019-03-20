using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    private RawImage dashbarImage;
    private RectTransform barMaskTransform;
    private float barMaskWidth;
    private float onePercent;
    private float dashValue;

    Vector2 barMaskSizeDelta;

    void Awake()
    {
        barMaskTransform = transform.Find("BarMask2").GetComponent<RectTransform>();
        dashbarImage = transform.Find("BarMask2").Find("Bar2").GetComponent<RawImage>();

        barMaskWidth = barMaskTransform.sizeDelta.x;

        dashValue = 430;
    }

    public bool CheckDash()
    {
        if(dashValue >= 215)return true;
        else return false;
    }

    public void UpdateDashbar()
    {
        dashValue -= 215;
        if(dashValue < 0) dashValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Rect uvRect = dashbarImage.uvRect;
        uvRect.x -= .1f * Time.deltaTime;
        dashbarImage.uvRect = uvRect;

        barMaskSizeDelta = barMaskTransform.sizeDelta;
        if(dashValue < 430) dashValue += 50 * Time.deltaTime;
        barMaskSizeDelta.x = dashValue;
        barMaskTransform.sizeDelta = barMaskSizeDelta;
    }
}
