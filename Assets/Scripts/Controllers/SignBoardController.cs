using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class SignBoardController : MonoBehaviour
{
    #region Actions
    public static Action<float> OnDistanceClosed; 
    #endregion

    #region Serialized Fields
    [SerializeField] private Transform signBoard;
    [SerializeField] private Transform lastClimbedDistanceSignBoard;

    [SerializeField] private Transform woodsParent;
    [SerializeField] private Transform lastClimbedDistanceSignBoardWoodsParent;
    [SerializeField] private GameObject woodPrefab;
    #endregion

    #region Privates
    private List<GameObject> _woodList;
    private GameObject _previousWood;

    private TextMeshPro _signBoardText;

    private Vector3 woodPos;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        _woodList = new List<GameObject>();
        _signBoardText = signBoard.GetComponentInChildren<TextMeshPro>();
        _signBoardText.SetText(0.ToString("F1") + "m");
        woodPos = Vector3.zero;
        CreateLastClimbedPosSignBoard();
    }
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        OnDistanceClosed += DistanceClosed;
        LevelManager.OnNextLevel += OnNextLevel;
    }

    private void OnNextLevel()
    {
        PlayerPrefs.DeleteKey("lastClimbedStair");
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        OnDistanceClosed -= DistanceClosed;
        LevelManager.OnNextLevel -= OnNextLevel;
    } 
    #endregion
    private void CreateLastClimbedPosSignBoard()
    {
        if (!PlayerPrefs.HasKey("lastClimbedStair"))
        {
            lastClimbedDistanceSignBoard.gameObject.SetActive(false);
            return;
        }

        int lastClimbedStairIndex = PlayerPrefs.GetInt("lastClimbedStair");
        lastClimbedDistanceSignBoard.gameObject.SetActive(true);
        lastClimbedDistanceSignBoard.position = new Vector3(lastClimbedDistanceSignBoard.position.x,
            lastClimbedDistanceSignBoard.position.y + (lastClimbedStairIndex * 0.295f), lastClimbedDistanceSignBoard.position.z);

        for (int i = 0; i< lastClimbedStairIndex; i++)
        {
            GameObject wood = Instantiate(woodPrefab, lastClimbedDistanceSignBoardWoodsParent);
            wood.GetComponentInChildren<TextMeshPro>().enabled = false;
            if(i == 0)
            {
                woodPos = new Vector3(2.028f, .15f, 0.87f);
                wood.transform.position = woodPos;
            }
            else
            {
                woodPos = new Vector3(2.028f, (0.295f * i) + .15f, 0.87f);
                wood.transform.position = woodPos;
            }           
        }

        TextMeshPro climbedSignBoardText = lastClimbedDistanceSignBoard.GetComponentInChildren<TextMeshPro>();
        climbedSignBoardText.SetText((lastClimbedStairIndex * 1.73f).ToString("F1") + "m");
    }
    private void DistanceClosed(float climbedDistance)
    {
        signBoard.position = new Vector3(signBoard.position.x, signBoard.position.y + 0.295f, signBoard.position.z);//Scoreboardýn pozisyonunu 0.3f yükseltir
        _previousWood = Instantiate(woodPrefab,woodsParent);
        _previousWood.GetComponentInChildren<TextMeshPro>().text = FeatureManager.Instance.GetIncome() + "$";
        //Eðer listede eleman yoksa varsayýlan pozisyonda odun oluþur varsa kendinden önceki 0.3f üstünde oluþur
        if (_woodList.Count == 0)
        {
            _previousWood.transform.position = new Vector3(0f, 0.15f, 0.87f);
        }

        else
        {
            _previousWood.transform.position = new Vector3(0f, (0.295f * _woodList.Count) + 0.15f, 0.87f);
        }
        //  previousWood = Instantiate(wood, new Vector3(2.02f, 0.3f*(woods.Count-1)+0.15f, 0.81f), Quaternion.identity);
        _signBoardText.SetText(climbedDistance.ToString("F1") + "m");

        _woodList.Add(_previousWood);
    }
   
}
