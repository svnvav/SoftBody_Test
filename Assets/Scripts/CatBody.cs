using System;
using UnityEngine;

namespace Svnvav.SoftBody
{
    public class CatBody : MonoBehaviour
    {
        private Rigidbody2D[] _parts;

        private void Start()
        {
            _parts = GetComponentsInChildren<Rigidbody2D>();
        }

        public void AddForce(Vector2 force, ForceMode2D forceMode2D)
        {
            foreach (var part in _parts)
            {
                part.AddForce(force, forceMode2D);
            }
        }
    }
}