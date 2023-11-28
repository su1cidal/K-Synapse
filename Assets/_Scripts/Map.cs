using System;
using System.Collections.Generic;
using _Scripts.Tiles;
using UnityEngine;

namespace _Scripts
{
    public class Map : MonoBehaviour
    {
        [SerializeField] public Tile _startTile;
        [SerializeField] public List<Tile> _respawnTiles;
        [SerializeField] public List<Tile> _tiles;

        public IEnumerable<Tile> GetMap() => _tiles;
        public Tile GetStartTile() => _startTile;
        public List<Tile> GetRespawnTiles() => _respawnTiles;
    }
}