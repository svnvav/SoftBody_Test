using UnityEngine;
using UnityEngine.U2D;

public class SoftSpriteShape : MonoBehaviour
{
    [SerializeField] private SpriteShapeController _spriteShapeController;
    [SerializeField] private Transform[] _points;

    // Update is called once per frame
    private void Update()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            var pointPosition = (Vector2)_points[i].localPosition;

            var radius = _points[i].GetComponent<CircleCollider2D>().radius;
            
            _spriteShapeController.spline.SetPosition(i, pointPosition + pointPosition.normalized * radius);

            var leftTangent = (Vector2)_spriteShapeController.spline.GetLeftTangent(i);

            var newRightTangent = Vector2.Perpendicular(-pointPosition) * leftTangent.magnitude;
            var newLeftTangent = -newRightTangent;
            
            _spriteShapeController.spline.SetLeftTangent(i, newLeftTangent);
            _spriteShapeController.spline.SetRightTangent(i, newRightTangent);
        }
    }
}
