using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/UpgradeCostList")]
public class UpgradeCostList : ScriptableObject
{
    public List<int> nextlevelCostList;
}
