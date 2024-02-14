using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public static Action<float> OnClimb;
    public static Action OnReachedFinalStair;

    #region Serialized Fields
    [SerializeField] private Stair[] stairs;
    [SerializeField] private float stairClimbCD = .5f;
    [SerializeField] private Transform jumpPoint;

    [SerializeField] private AnimationCurve jumpAnimCurve;
    #endregion

    #region Privates
    private PlayerHealth _playerHealth;
    private PlayerAnimationController _playerAnimationController;

    private float _lastClimbedStairTime = 0f;
    private float _climbedDistance;
    private float _playerJumpPointY;
    private float _totalDistance;
    private float _jumpSpeed;

    private int stairIndex;

    private bool _isClimbing = false;
    private bool _isTouching = false;
    private bool _climbedAllStairs = false;
    private bool _reachedJumpPoint = false;

    private Vector3 _climbPointPosition;
    private Vector3 _positionXZ;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        stairIndex = 0;
        _climbedDistance = 0f;
        _playerHealth = GetComponent<PlayerHealth>();
        _playerAnimationController = GetComponent<PlayerAnimationController>();
    }
    private void Start()
    {
        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        PlayerHealth.OnDeath += OnDeath;
        LevelManager.OnTryAgain += OnRestartScene;
        ShopPanelController.onPlay += OnPlay;
    }
    private void OnDeath()
    {
        if (!PlayerPrefs.HasKey("lastClimbedStair"))
        {
            PlayerPrefs.SetInt("lastClimbedStair", stairIndex + 1);
        }
        else
        {
            if (stairIndex + 1 > PlayerPrefs.GetInt("lastClimbedStair"))
            {
                PlayerPrefs.SetInt("lastClimbedStair", stairIndex + 1);
            }
        }
    }
    private void OnRestartScene()
    {
        foreach (Stair stair in stairs)
        {
            if(stair != null)
            {
                stair.gameObject.GetComponent<Rigidbody>().useGravity = true;
                stair.gameObject.GetComponent<BoxCollider>().enabled = true;
            }            
        }
    }
    private void OnPlay() => stairClimbCD = FeatureManager.Instance.GetClimbSpeed();
    private void OnDisable() => UnSubscribeEvents();
    private void UnSubscribeEvents()
    {
        PlayerHealth.OnDeath -= OnDeath;
        LevelManager.OnTryAgain -= OnRestartScene;
        ShopPanelController.onPlay -= OnPlay;
    }
    #endregion
    void Update()
    {
        if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
        {
            if (CanClimbStair())
            {
                ClimbStair();
            }
            
            _isTouching = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _playerAnimationController.StopClimbingStairsAnim();
            _isTouching = false;
        }

        if (!_isTouching || !_climbedAllStairs)
        {
            if (_playerHealth.IsPlayerFullHealth())
                return;
            _playerHealth.RegenerateHealth();
        }

        if (_climbedAllStairs)
        {
            if (_reachedJumpPoint)
                return;

            _positionXZ = new Vector3(transform.position.x,jumpPoint.position.y,transform.position.z);
            float distance = Vector3.Distance(_positionXZ, jumpPoint.position);
            float distanceNormalized = 1 - (distance / _totalDistance);
            float positionY = jumpAnimCurve.Evaluate(distanceNormalized) + _playerJumpPointY;
            transform.position = new Vector3(transform.position.x,positionY,transform.position.z);
            if(Vector3.Distance(transform.position,jumpPoint.position) < .1f)
            {
                _reachedJumpPoint = true;
            }
        }
    }
    #region Climb Mechanic
    private bool CanClimbStair()
    {
        bool canClimb = Time.time >= _lastClimbedStairTime && stairIndex < stairs.Length
            && !_isClimbing /*&& stairIndex != stairs.Length - 1*/;
        if (canClimb)
        {
            _lastClimbedStairTime = Time.time + stairClimbCD;
        }
        return canClimb;
    }
    private void ClimbStair()
    {
        if (!LeanTween.isTweening(gameObject))
        { 
            _isClimbing = true;
            _playerAnimationController.PlayClimbingStairsAnim();
            if (!stairs[stairIndex].gameObject.activeSelf)
                stairs[stairIndex].gameObject.SetActive(true);

            _climbPointPosition = stairs[stairIndex].GetClimbPoint().position;
            if (stairIndex >= stairs.Length / 3)
            {
                _playerHealth.TakeDamage(20f);
            }
            LeanTween.rotateAround(gameObject, Vector3.up, 15f, stairClimbCD);
            _climbedDistance = (stairIndex + 1) * 1.73f;
            if (_climbedDistance > 100f)
                _climbedDistance = 100f;
            ScoreManager.OnMoneyIncreased?.Invoke();
            SignBoardController.OnDistanceClosed?.Invoke(_climbedDistance);
            LeanTween.move(gameObject, _climbPointPosition, stairClimbCD).setOnComplete(ClimbNextStair);
            OnClimb?.Invoke(((float) stairIndex / (stairs.Length - 1)));
        }
    }
    private void ClimbNextStair()
    {
        if (stairIndex < stairs.Length)
            stairIndex++;
        _isClimbing = false;

        if (stairIndex == stairs.Length)
        {
            _playerAnimationController.StopClimbingStairsAnim();;
            OnReachedFinalStair?.Invoke();
            SetupJump();
            LeanTween.moveX(gameObject, jumpPoint.position.x, 1f);
            LeanTween.moveZ(gameObject, jumpPoint.position.z, 1f);
        }
    } 
    #endregion
    private bool IsPointerOverUIElement()
    {
        var eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
    private void SetupJump()
    {
        _playerJumpPointY = transform.position.y;
        _climbedAllStairs = true;
        _positionXZ = transform.position;
        _positionXZ.y = jumpPoint.position.y;
        _totalDistance = Vector3.Distance(_positionXZ, jumpPoint.position);
        _jumpSpeed = 15f;
    }
}
