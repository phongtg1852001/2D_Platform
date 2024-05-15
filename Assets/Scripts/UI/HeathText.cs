using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HeathText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0, 75, 0);
    public float timeToFade = 1f;

    RectTransform textTransfrom;
    TextMeshProUGUI textMeshPro;

    private float timeToElapse = 0f;
    private Color startColor;

    private void Awake()
    {
        textTransfrom = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    private void Update()
    {
        textTransfrom.position += moveSpeed * Time.deltaTime;
        timeToElapse += Time.deltaTime;

        if(timeToElapse < timeToFade) 
        {
            float fadeAlpha = startColor.a * (1 - timeToElapse / timeToFade);
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }else
        {
            Destroy(gameObject);
        }
    }
}
