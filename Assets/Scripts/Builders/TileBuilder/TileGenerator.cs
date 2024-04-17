
    using UnityEngine;

    public sealed class TileGenerator: MonoBehaviour
    {
        public static TileGenerator current;
        private TileDirector _tileDirector;

        private void Awake()
        {
            if (current == null)
            {
                current = this;
            }
            if (current != this)
            {
                Destroy(transform);
            }
        }
        
        public Tile CreateTile(RoomSO roomData)
        {
            _tileDirector = new TileDirector();
            Tile tile = _tileDirector.CreateTile(roomData);
            return tile;
        }
    }
