using UnityEngine;

namespace Core.Map
{
    public struct PresetObject : ISpawnObject
    {
        private GameObject _prefab;

        private Vector2 _position;

        private float _yRotation;

        public GameObject Spawn()
        {
            return Object.Instantiate(_prefab, new Vector3(_position.x, _position.y), 
                Quaternion.Euler(0, _yRotation, 0));
        }
    }
}