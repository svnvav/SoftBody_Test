using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Svnvav.SoftBody.Editor
{
    public class SoftCircleGenerator : EditorWindow
    {
        private Transform _partsRoot;
        private GameObject _softBody;
        private GameObject _partTemplate;
        private int _partCount = 12;
        private float _radius = 0.5f, _partRadius = 0.25f;

        private AllOtherJointCreator _allOtherJointCreator = new AllOtherJointCreator();
        private NeighbourJointCreator _neighbourJointCreator = new NeighbourJointCreator();
        private OppositeJointCreator _oppositeJointCreator = new OppositeJointCreator();

        [MenuItem("Tools/SoftCircleGenerator")]
        private static void OnMenuClick()
        {
            GetWindow<SoftCircleGenerator>();
        }

        private void OnGUI()
        {
            _partsRoot = (Transform)EditorGUILayout.ObjectField("Parts root", _partsRoot, typeof(Transform));
            _softBody = (GameObject)EditorGUILayout.ObjectField("Soft body", _softBody, typeof(GameObject));
            _partTemplate = (GameObject)EditorGUILayout.ObjectField("Part prefab", _partTemplate, typeof(GameObject));
            _partCount = EditorGUILayout.IntField("Parts count", _partCount);
            _radius = EditorGUILayout.FloatField("Radius", _radius);
            _partRadius = EditorGUILayout.FloatField("Part radius to fit", _partRadius);

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
            for (int i = 0; i < _partsRoot.childCount; i++)
            {
                chldren.Add(_partsRoot.GetChild(i));
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
            var actualRadius = Mathf.Max(_radius - _partRadius, 0f);
            for (int i = 0; i < _partCount; i++)
            {
                var part = Instantiate(_partTemplate, _partsRoot);
                var localPosition = new Vector2(Mathf.Sin(i * angleStep), Mathf.Cos(i * angleStep)) * actualRadius;
                part.transform.localPosition = localPosition;
                parts[i] = part;
            }

            _allOtherJointCreator.Generate(parts);
            _neighbourJointCreator.Generate(parts);
            _oppositeJointCreator.Generate(parts);

            var softCircle = _softBody.GetComponent<SoftCircle>();
            if (softCircle == null)
            {
                softCircle = _softBody.AddComponent<SoftCircle>();
            }
            softCircle.SetParts(parts.Select(part => part.transform).ToArray());
        }
    }
}