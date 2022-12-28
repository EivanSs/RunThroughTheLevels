using UnityEngine;

namespace Core.Map
{
    public struct QuadObject : ISpawnObject
    {
        private GameObject _prefab;

        private Vector2 _position;

        public QuadObject(GameObject prefab, Vector2 position)
        {
            _prefab = prefab;
            _position = position;
        }

        public GameObject Spawn()
        {
            return Object.Instantiate(_prefab, new Vector3(_position.x, 0, _position.y), Quaternion.identity);
        }
    }
}