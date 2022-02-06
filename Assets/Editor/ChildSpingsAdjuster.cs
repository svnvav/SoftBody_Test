using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Svnvav.SoftBody.Editor
{
    public class ChildSpringAdjuster : EditorWindow
    {
        private Transform _root;
        [SerializeField] private float _dampingRatio = 0.03f;
        [SerializeField] private float _frequency = 5f;
        

        [MenuItem("Tools/ChildSpringAdjuster")]
        private static void OnMenuClick()
        {
            GetWindow<ChildSpringAdjuster>();
        }

        private void OnGUI()
        {
            _root = (Transform)EditorGUILayout.ObjectField(nameof(_root), _root, typeof(Transform));
            _dampingRatio = EditorGUILayout.FloatField(nameof(_dampingRatio), _dampingRatio);
            _frequency = EditorGUILayout.FloatField(nameof(_frequency), _frequency);
            

            EditorGUILayout.Separator();
            if (GUILayout.Button("Button"))
            {
                Adust();
            }
        }

        private void Adust()
        {
            var springs = _root.GetComponentsInChildren<SpringJoint2D>();
            foreach (var spring in springs)
            {
                spring.frequency = _frequency;
                spring.dampingRatio = _dampingRatio;
            }
        }
    }
}