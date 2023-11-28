using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Tiles;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class Pawn : MonoBehaviour
    {
        [SerializeField] public int health = Constants.PLAYER_MAX_HEALTH;
        [SerializeField] public int keys = 0;
        [SerializeField] public int answeredQuestions = 0;
        [SerializeField] public int rolledDice = -1;
        [SerializeField] public Tile currentMapTile;
        [Header("----")]
        [SerializeField] public Material color;
        [SerializeField] public GameObject visual;
        private int _lastPositionEdit;
        
        public void DoDamage(int amount)
        {
            if (health - amount <= Constants.PLAYER_MIN_HEALTH)
            {
                health = Constants.PLAYER_MIN_HEALTH;
            }
            else
            {
                health -= amount;
            }
        }
        
        public void AddHealth(int amount)
        {
            if (health + amount >= Constants.PLAYER_MAX_HEALTH)
            {
                health = Constants.PLAYER_MAX_HEALTH;
            }
            else
            {
                health += amount;
            }
        }
        
        public void AddKeys(int amount)
        {
            keys += amount;
        }
        
        public int RollADice()
        {
            rolledDice = Random.Range(Constants.ROLL_MIN_VALUE, Constants.ROLL_MAX_VALUE+1);
            Debug.Log($"{this.name} roll: {rolledDice}");
            return rolledDice;
        }

        public void SetLastPositionEdit(int editValue) => _lastPositionEdit = editValue;
        
        public int GetLastPositionEdit() => _lastPositionEdit;
    }
}