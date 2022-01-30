using System;
using UnityEngine;
using UnityEngine.U2D;

public class SoftSpriteShape : MonoBehaviour
{
    [SerializeField] private SpriteShapeController _spriteShapeController;
    [SerializeField] private float _pointsScale = 1f;
    [SerializeField] private Transform[] _points;
    private Vector2[] _normals;

    private void Start()
    {
        _normals = new Vector2[_points.Length];
        for (int i = 0; i < _points.Length; i++)
        {
            _normals[i] = ((Vector2)_points[i].localPosition).normalized;
        }
    }
    
    private void Update()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            var pointPosition = (Vector2)_points[i].localPosition;

            var radius = _points[i].GetComponent<CircleCollider2D>().radius * _pointsScale;
            
            _spriteShapeController.spline.SetPosition(i, pointPosition + _normals[i] * radius);

            var leftTangent = (Vector2)_spriteShapeController.spline.GetLeftTangent(i);

            var newRightTangent = Vector2.Perpendicular(-pointPosition) * leftTangent.magnitude;
            var newLeftTangent = -newRightTangent;
            
            _spriteShapeController.spline.SetLeftTangent(i, newLeftTangent);
            _spriteShapeController.spline.SetRightTangent(i, newRightTangent);
        }
    }
}
