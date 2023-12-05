using System.Collections;
using UnityEngine;

public class QuestionTile : Tile
{
    public override IEnumerator DoAction(Pawn player)
    {
        Debug.Log("QuestionTile Action");
        
        yield return StartCoroutine(QuizHandler.Instance.StartQuiz());
    }
}