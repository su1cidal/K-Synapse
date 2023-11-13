using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettingsSO", menuName = "SO/GameSettingsSO")]
public class GameSettingsSO : ScriptableObject
{
    public int PlayerCount = 4;
    public int TurnCount = 15;
    public int CupsToWin = 1;
    public int TurnLength = 60;
    public int TimeToAnswer = 10;

    // public Enum QuestionTheme;
    // public Enum Map;

}