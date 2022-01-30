using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Svnvav.SoftBody.Editor
{
    public class SoftCircleSetSprings : EditorWindow
    {
        private Transform _softBody;
        [SerializeField] private float _colliderRadius = 0.3f;
        [SerializeField] private float _colliderRadiusGap = 0.03f;
        [SerializeField] private float _springDampingRatio = 0.03f;
        [SerializeField] private float _springFrequency = 5f;
        

        [MenuItem("Tools/SoftCircleSetSprings")]
        private static void OnMenuClick()
        {
            GetWindow<SoftCircleSetSprings>();
        }

        private void OnGUI()
        {
            _softBody = (Transform)EditorGUILayout.ObjectField("Soft body", _softBody, typeof(Transform));
            _colliderRadius = EditorGUILayout.FloatField("Collider Radius", _colliderRadius);
            _colliderRadiusGap = EditorGUILayout.FloatField("Collider Radius Gap", _colliderRadiusGap);
            _springDampingRatio = EditorGUILayout.FloatField(nameof(_springDampingRatio), _springDampingRatio);
            _springFrequency = EditorGUILayout.FloatField(nameof(_springFrequency), _springFrequency);
            

            EditorGUILayout.Separator();
            if (GUILayout.Button("Button"))
            {
                Generate();
            }
        }

        private void Generate()
        {
            var parts = new GameObject[_softBody.childCount];
            for (int i = 0; i < parts.Length; i++)
            {
                var part = _softBody.GetChild(i).gameObject;
                var collider = part.GetComponent<CircleCollider2D>();
                if (collider == null)
                {
                    collider = part.AddComponent<CircleCollider2D>();
                }

                part.transform.localScale = Vector3.one * _colliderRadius;
                collider.radius = 0.5f;//_colliderRadius;
                collider.offset = Vector2.zero;//Vector2.right * (_colliderRadius - _colliderRadiusGap);
                
                var rb2d = part.GetComponent<Rigidbody2D>();
                if (rb2d == null)
                {
                    rb2d = part.AddComponent<Rigidbody2D>();
                }
                
                parts[i] = part;
            }

            for (int i = 0; i < parts.Length; i++)
            {
                var from = parts[i];
                var springJoints = from.GetComponents<SpringJoint2D>();
                for (var index = 0; index < springJoints.Length; index++)
                {
                    DestroyImmediate(springJoints[index]);
                }
                
                for (int j = 0; j < parts.Length; j++)
                {
                    if(i == j) continue;

                    var connectedBody = parts[j].GetComponent<Rigidbody2D>();

                    var springJoint = from.AddComponent<SpringJoint2D>();
                    springJoint.connectedBody = connectedBody;
                    springJoint.dampingRatio = _springDampingRatio;
                    springJoint.frequency = _springFrequency;
                    springJoint.autoConfigureConnectedAnchor = true;
                    springJoint.anchor = Vector2.zero;//Vector2.right * (_colliderRadius - _colliderRadiusGap);
                    //springJoint.connectedAnchor = Vector2.zero;//Vector2.right * (_colliderRadius - _colliderRadiusGap);
                }
            }
        }
    }
}