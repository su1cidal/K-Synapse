using System.Collections;
using UnityEngine;

public class QuestionTile : Tile
{
    public override IEnumerator DoAction(Pawn player)
    {
        ShowVFX();
        
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(QuizHandler.Instance.StartQuiz());
        HideVFX();
    }
}