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

        public IEnumerable<MapTile> GetMap() => _tiles;
        public MapTile GetStartTile() => _startTile;
        public List<MapTile> GetRespawnTiles() => _respawnTiles;
    }
}