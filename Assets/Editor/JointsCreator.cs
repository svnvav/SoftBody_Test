using System;
using UnityEditor;
using UnityEngine;

namespace Svnvav.SoftBody.Editor
{
    [Serializable]
    public abstract class JointsCreator
    {
        protected enum JointType
        {
            None,
            Spring,
            Distance,
            Hinge
        }
        
        protected abstract string Title { get; }
        
        [SerializeField] protected JointType _jointType;
        [SerializeField] protected float _springDampingRatio = 0f;
        [SerializeField] protected float _springFrequency = 1f;

        public void OnGUI()
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField(Title);
            _jointType = (JointType)EditorGUILayout.EnumPopup(_jointType);
            if(_jointType == JointType.None) return;
            
            switch (_jointType)
            {
                case JointType.Spring:
                    _springDampingRatio = EditorGUILayout.FloatField(nameof(_springDampingRatio), _springDampingRatio);
                    _springFrequency = EditorGUILayout.FloatField(nameof(_springFrequency), _springFrequency);
                    break;
                case JointType.Distance:
                    break;
                case JointType.Hinge:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public abstract void Generate(GameObject[] nodes);

        protected void CreateJointBetween(GameObject from, Rigidbody2D connected)
        {
            switch (_jointType)
            {
                case JointType.Spring:
                    var springJoint = from.AddComponent<SpringJoint2D>();
                    springJoint.connectedBody = connected;
                    springJoint.dampingRatio = _springDampingRatio;
                    springJoint.frequency = _springFrequency;
                    break;
                case JointType.Distance:
                    var distanceJoint = from.AddComponent<DistanceJoint2D>();
                    distanceJoint.connectedBody = connected;
                    break;
                case JointType.Hinge:
                    var hingeJoint = from.AddComponent<HingeJoint2D>();
                    hingeJoint.connectedBody = connected;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}