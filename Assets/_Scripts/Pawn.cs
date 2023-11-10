using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class Pawn : MonoBehaviour
    {
        [SerializeField] public int health;
        [SerializeField] public int keys;
        [SerializeField] public int answeredQuestions;
        [SerializeField] public int rolledDice = 0;
        [SerializeField] public bool isFinalStep = true;
        [SerializeField] public MapTile currentMapTile;
        [Header("----")]
        [SerializeField] public Material color;
        [SerializeField] public GameObject visual;
        private int _lastPositionEdit;
        
        
        public int RollADice()
        {
            rolledDice = Random.Range(Constants.MIN_ROLL_VALUE, Constants.MAX_ROLL_VALUE);
            Debug.Log($"{this.name} roll: {rolledDice}");
            return rolledDice;
        }

        public void SetLastPositionEdit(int editValue) => _lastPositionEdit = editValue;
        
        public int GetLastPositionEdit() => _lastPositionEdit;
    }
}