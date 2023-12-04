using UnityEngine;

[CreateAssetMenu(fileName = "GameSettingsSO", menuName = "SO/GameSettingsSO")]
public class GameSettingsSO : ScriptableObject
{
    public int playerCount = 2;
    public int turnCount = 15;
    public int cupsToWin = 1;
    public int turnLength = 60;
    public float timeToAnswer = 10;
    public QuestionClassification classification = QuestionClassification.GeneralKnowledge;
    // public Enum Map;

}