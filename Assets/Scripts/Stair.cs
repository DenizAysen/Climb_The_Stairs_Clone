using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stair : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private Transform standPoint;
    //[SerializeField] private TextMeshPro moneyText;

    #endregion
    public Transform GetClimbPoint()
    {
        return standPoint;
    }
    //public void PlayMoneyTextAnim()
    //{
    //    GameObject moneyTextGameObject = moneyText.gameObject;
    //    LeanTween.moveLocalY(moneyTextGameObject, moneyTextYLocalMoveValue, .1f).setOnComplete(() =>
    //    {
    //        moneyTextGameObject.SetActive(false);
    //        moneyText.GetComponent<LookAtCamera>().enabled  = false;
    //    });
    //}
}
