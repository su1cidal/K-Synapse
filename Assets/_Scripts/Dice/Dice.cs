using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _dice;
    [SerializeField] private GameObject _number;
    [SerializeField] private TMP_Text _numberText;
    [SerializeField] private GameObject _vfx;
    
    [SerializeField] private float speedOfRotation = 100f;

    private int _rolledNumber;
    private bool _isWaitingForInput = false;
    private bool _isStopPressed = false;
    private bool _isEnded = false;
    private bool _isPlayer = false;
    
    private bool IsEnded() => _isEnded;
    private bool IsStopPressed() => _isStopPressed;
    private bool IsWaitingForInput() => _isWaitingForInput;

    public static event EventHandler OnConfetti;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsWaitingForInput() && _isPlayer)
        {
            _isStopPressed = true;
            
            _isWaitingForInput = false;
        }

        if (_dice.activeSelf)
        {
            _dice.transform.Rotate(Vector3.up, speedOfRotation * Time.deltaTime);
            _dice.transform.Rotate(Vector3.right, -speedOfRotation * Time.deltaTime);
        }
    }

    public IEnumerator Roll(Pawn pawn)
    {
        _isPlayer = pawn.IsPlayer;
        _isStopPressed = false;
        _isEnded = false;
        
        _prefab.gameObject.SetActive(true);
        _isWaitingForInput = true;
        
        if (!_isPlayer)
        {
            yield return new WaitForSeconds(1f);
            RollForBot();
        }
            
        yield return new WaitUntil(IsStopPressed); // while?
        pawn.MakeJump();
        
        pawn.rolledDice = GetANumber();
        StartCoroutine(ShowNumber(_rolledNumber, pawn));
        
        yield return new WaitUntil(IsEnded);
    }

    private void RollForBot()
    {
        _isStopPressed = true;

        _isWaitingForInput = false;
    }

    private int GetANumber()
    {
        _rolledNumber = Random.Range(Constants.ROLL_MIN_VALUE, Constants.ROLL_MAX_VALUE);
        return _rolledNumber;
    }

    private IEnumerator ShowNumber(int value, Pawn pawn)
    {
        if(pawn.IsPlayer)
            yield return new WaitForSeconds(0.5f);
        else 
            yield return new WaitForSeconds(0.5f);
        
        _dice.SetActive(false);

        _numberText.text = value.ToString();
        
        _number.SetActive(true);
        _vfx.SetActive(true);
        OnConfetti?.Invoke(this, EventArgs.Empty);
        _isEnded = true;
        
        
        yield return new WaitForSeconds(2f);
        _prefab.gameObject.SetActive(false);
    }

    private void HideAll()
    {
        _dice.SetActive(false);
        _number.SetActive(false);
        _vfx.SetActive(false);
    }

    private void OnEnable()
    {
        _dice.SetActive(true);
    }

    private void OnDisable()
    {
        HideAll();
    }
}