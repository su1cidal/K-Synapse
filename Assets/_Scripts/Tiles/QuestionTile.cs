using System;
using System.Collections;
using UnityEngine;

public class QuestionTile : Tile
{
    public static event EventHandler OnQuizStart;
    
    public override IEnumerator DoAction(Pawn player)
    {
        ShowVFX();
        
        OnQuizStart?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(QuizHandler.Instance.StartQuiz());
        
        HideVFX();
    }
}