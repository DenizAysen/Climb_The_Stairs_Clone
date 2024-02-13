using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Fade : MonoBehaviour
{
    //private Color32 color;
    //private Color32 toColor;
    [SerializeField] private float fadeTime;
    TextMeshPro woodMoneyText;
    void Start()
    {
        woodMoneyText = GetComponent<TextMeshPro>();
        var color = woodMoneyText.color;
        var fadeOutColor = color;
        fadeOutColor.a = 0;
        LeanTween.value(gameObject, updateValueExampleCallback, color, fadeOutColor, fadeTime).setOnComplete(() =>
        {
            gameObject.SetActive(false);
            Destroy(this);
        });
        //LeanTween.alphaVertex(woodMoneyText.gameObject, 0f, 1f);
    }
    void updateValueExampleCallback(Color val)
    {
        woodMoneyText.color = val;
    }
}
