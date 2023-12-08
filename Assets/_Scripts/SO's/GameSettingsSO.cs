using UnityEngine;

[CreateAssetMenu(fileName = "GameSettingsSO", menuName = "SO/GameSettingsSO")]
public class GameSettingsSO : ScriptableObject
{
    public int playerCount = 4;
    public int turnCount = 15;
    public int cupsToWin = 2;
    public float timeToAnswer = 10;
    public QuestionClassification classification = QuestionClassification.GeneralKnowledge;
    
    public string playerName;
    public PlayerMaterialsSO playerMaterials;
    // public Enum Map;

}