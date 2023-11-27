using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEditor.MPE;
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
        [SerializeField] public Tile currentMapTile;
        [Header("----")]
        [SerializeField] public Material color;
        [SerializeField] public GameObject visual;
        private int _lastPositionEdit;
        
        [SerializeField] public bool isFinalStep = true;
        public event EventHandler OnFinalStep;
        [SerializeField] public bool tileEventEnded = true;
        public event EventHandler OnTileEventEnded;
        
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

        public List<Vector3> GetPath()
        {
            List<Vector3> path = new List<Vector3>();
            List<Tile> nextTiles;

            var dice = rolledDice;
            var firstTile = currentMapTile;
            var tempCurrent = currentMapTile;
            
            for (int i = 0; i < rolledDice; ++i)
            {
                nextTiles = tempCurrent.adjacentTiles;
                if (nextTiles.Count == 1)
                {
                    tempCurrent = nextTiles[0];
                    path.Add(tempCurrent.transform.position);
                    
                    dice -= 1;
                }
                else
                {
                    Debug.LogError("More or less than one tile!");
                    // todo logic when there are more or less than 1 tile
                    break;
                }

            }
            
            currentMapTile = tempCurrent;
            return path;
        }
        
        public IEnumerator MoveByPath()
        {
            currentMapTile.RemovePawn(this);
            List<Vector3> path = GetPath();
            var delay = 0.6f;
            
            isFinalStep = false;
            
            for (var index = 0; index < path.Count; index++)
            {
                var point = path[index];
                GetComponentInChildren<Transform>().DOMove(point, 0.5f).onComplete(); //todo or *speed*Time.deltatime
                rolledDice -= 1;
                yield return new WaitForSeconds(delay);
            }

            if (rolledDice == 0)
            {
                isFinalStep = true;
                
                currentMapTile.AddPawn(this);
                currentMapTile.DoAction(this); // if tile is QUESTION tile, so what?
                
                // we can try to fire event only on last player
                OnFinalStep?.Invoke(this, EventArgs.Empty);
                OnTileEventEnded?.Invoke(this, EventArgs.Empty); // need to receive callback from DoAction
            }
        }

        public void SetLastPositionEdit(int editValue) => _lastPositionEdit = editValue;
        
        public int GetLastPositionEdit() => _lastPositionEdit;
    }
}