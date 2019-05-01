using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //private Image healthbarImage;
    private RawImage healthbarImage;
    private RectTransform barMaskTransform;
    private float barMaskWidth;
    private float onePercent;
    private int healthValue = 100;

    void Awake()
    {
        barMaskTransform = transform.Find("BarMask").GetComponent<RectTransform>();
        healthbarImage = transform.Find("BarMask").Find("Bar").GetComponent<RawImage>();

        barMaskWidth = barMaskTransform.sizeDelta.x;
        onePercent = barMaskWidth / 100;
    }

    public void UpdateHealth(int health)
    {
        healthValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        Rect uvRect = healthbarImage.uvRect;
        uvRect.x -= 0.1f * Time.deltaTime;
        healthbarImage.uvRect = uvRect;

        Vector2 barMaskSizeDelta = barMaskTransform.sizeDelta;
        barMaskSizeDelta.x = (float)healthValue * onePercent;
        barMaskTransform.sizeDelta = barMaskSizeDelta;
    }
}
