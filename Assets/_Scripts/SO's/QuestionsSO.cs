using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionsSO", menuName = "SO/QuestionsSO")]
public class QuestionsSO : ScriptableObject
{
    public QuestionClassification classification;
    public Question[] questions;
}