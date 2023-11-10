using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
    public class Map : MonoBehaviour
    {
        [SerializeField] public MapTile _startTile;
        [SerializeField] public List<MapTile> _respawnTiles;
        [SerializeField] public List<MapTile> _tiles;

        public List<MapTile> GetRespawnTiles() => _respawnTiles;
        public MapTile GetStartTile() => _startTile;
        
        public List<MapTile> GetMap()
        {
            return _tiles;
        }
    }
}
