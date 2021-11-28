using System;
using UnityEngine;

namespace Svnvav.SoftBody
{
    public class SoftCircle : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform[] _parts;
        private Vector3[] _partsStartPositions;
        private Vector2[] _spriteStartVertices;

        private void Start()
        {
            CalculatePartsRelativePositions();

            _spriteStartVertices = _spriteRenderer.sprite.vertices;
        }

        private void Update()
        {
            PositionToPartsMassCenter();
        }

        public void SetParts(Transform[] parts)
        {
            _parts = parts;

            PositionToPartsMassCenter();
        }

        public void PositionToPartsMassCenter()
        {
            var massCenter = GetMassCenter();

            transform.position = massCenter;
        }

        private void CalculatePartsRelativePositions()
        {
            _partsStartPositions = new Vector3[_parts.Length];

            for (int i = 0; i < _parts.Length; i++)
            {
                _partsStartPositions[i] = _parts[i].position - transform.position;
            }

            for (int i = 0; i < _partsStartPositions.Length; i++)
            {
                Debug.Log(_partsStartPositions[i]);
            }
        }
        
        private void UpdateView()
        {
            var sprite = _spriteRenderer.sprite;
            var vertices = (Vector2[])_spriteStartVertices.Clone();
            for (int i = 0; i < vertices.Length; i++)
            {
                for (int j = 0; j < _partsStartPositions.Length; j++)
                {
                    //var partOffset = _parts[i]. - _partsStartPositions[i];
                }
            }
        }

        private Vector3 GetMassCenter()
        {
            var massCenter = Vector3.zero;
            for (int i = 0; i < _parts.Length; i++)
            {
                massCenter += _parts[i].position;
            }
            massCenter /= _parts.Length;

            return massCenter;
        }
    }
}