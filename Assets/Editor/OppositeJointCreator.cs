using System;
using UnityEngine;

namespace Svnvav.SoftBody.Editor
{
    [Serializable]
    public class OppositeJointCreator : JointsCreator
    {
        protected override string Title => "Opposite";
        
        public override void Generate(GameObject[] nodes)
        {
            if(_jointType == JointType.None) return;
            
            for (int i = 0; i < nodes.Length; i++)
            {
                var oppositeIndex = (i + nodes.Length / 2) % nodes.Length;
                var oppositeRigidbody = nodes[oppositeIndex].GetComponent<Rigidbody2D>();
                
                CreateJointBetween(nodes[i], oppositeRigidbody);
            }
        }
    }
}