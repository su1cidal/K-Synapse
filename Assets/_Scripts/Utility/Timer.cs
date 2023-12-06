using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameSettingsSO _gameSettingsSo;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private float _timeRemaining;
    [SerializeField] private bool _timerIsRunning = false;

    public event Action TimeEnded;

    private void Awake()
    {
        _timeRemaining = _gameSettingsSo.timeToAnswer;
    }

    public void StartTimer()
    {
        _timeRemaining = _gameSettingsSo.timeToAnswer;
        _timerIsRunning = true;
    }
    
    public void StopTimer()
    {
        _timeRemaining = 0;
        _timerIsRunning = false;
    }
    
    private void Update()
    {
        if (_timerIsRunning)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
                DisplayTime(_timeRemaining);
            }
            else
            {
                StopTimer();
                TimeEnded?.Invoke();
            }
        }
    }
    
    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}