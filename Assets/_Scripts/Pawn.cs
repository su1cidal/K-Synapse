using System;
using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class Pawn : MonoBehaviour
    {
        [SerializeField] public int health = Constants.PLAYER_MAX_HEALTH;
        [SerializeField] public int keys = 0;
        [SerializeField] public int answeredQuestions = 0;
        [SerializeField] public int rolledDice = -1;
        [SerializeField] public bool isFinalStep = true;
        [SerializeField] public MapTile currentMapTile;
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
            rolledDice = Random.Range(Constants.ROLL_MIN_VALUE, Constants.ROLL_MAX_VALUE);
            Debug.Log($"{this.name} roll: {rolledDice}");
            return rolledDice;
        }

        public IEnumerator MoveToNextTile()
        {
            isFinalStep = false;
            
            var nextTile = currentMapTile.adjacentTiles;

            if (nextTile != null)
            {
                currentMapTile.RemovePawn(this);
                nextTile[0].AddPawn(this); //0 because there can be multiple units in adjacentTiles
                currentMapTile = nextTile[0];
                //GetComponentInChildren<Transform>().position = currentMapTile.transform.position;

                GetComponentInChildren<Transform>().DOMove(currentMapTile.transform.position, 0.5f); //todo or *speed*Time.deltatime
                rolledDice -= 1;
            }
            
            yield return new WaitForSeconds (1);
            
            if (rolledDice == 0)
            {
                isFinalStep = true;
                currentMapTile.DoAction(this);
            }
            else
            {
                MoveToNextTile();
            }
        }

        public void SetLastPositionEdit(int editValue) => _lastPositionEdit = editValue;
        
        public int GetLastPositionEdit() => _lastPositionEdit;
    }
}