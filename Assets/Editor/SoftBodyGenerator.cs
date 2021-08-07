using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Svnvav.SoftBody.Editor
{
    public class SoftBodyGenerator : EditorWindow
    {
        private Transform _root;
        private GameObject _partTemplate;
        private int _partCount = 6;
        private float _radius = 1f;

        private AllOtherJointCreator _allOtherJointCreator = new AllOtherJointCreator();
        private NeighbourJointCreator _neighbourJointCreator = new NeighbourJointCreator();
        private OppositeJointCreator _oppositeJointCreator = new OppositeJointCreator();

        [MenuItem("Tools/SoftBodyGenerator")]
        private static void OnMenuClick()
        {
            GetWindow<SoftBodyGenerator>();
        }

        private void OnGUI()
        {
            _root = (Transform)EditorGUILayout.ObjectField(_root, typeof(Transform));
            _partTemplate = (GameObject)EditorGUILayout.ObjectField(_partTemplate, typeof(GameObject));
            _partCount = EditorGUILayout.IntField("Parts count", _partCount);
            _radius = EditorGUILayout.FloatField("Distance", _radius);

            EditorGUILayout.Separator();

            _allOtherJointCreator.OnGUI();
            _neighbourJointCreator.OnGUI();
            _oppositeJointCreator.OnGUI();

            EditorGUILayout.Separator();
            if (GUILayout.Button("Button"))
            {
                ClearRoot();
                Generate();
            }
        }

        private void ClearRoot()
        {
            var chldren = new List<Transform>();
            for (int i = 0; i < _root.childCount; i++)
            {
                chldren.Add(_root.GetChild(i));
            }

            foreach (var child in chldren)
            {
                DestroyImmediate(child.gameObject);
            }
        }
        
        private void Generate()
        {
            var parts = new GameObject[_partCount];
            var angleStep = 2 * Mathf.PI / _partCount;
            for (int i = 0; i < _partCount; i++)
            {
                var part = Instantiate(_partTemplate, _root);
                var localPosition = new Vector2(Mathf.Sin(i * angleStep), Mathf.Cos(i * angleStep)) * _radius;
                part.transform.localPosition = localPosition;
                parts[i] = part;
            }

            _allOtherJointCreator.Generate(parts);
            _neighbourJointCreator.Generate(parts);
            _oppositeJointCreator.Generate(parts);
        }
    }
}