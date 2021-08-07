using System;
using UnityEngine;

namespace Svnvav.SoftBody.Editor
{
    [Serializable]
    public class NeighbourJointCreator : JointsCreator
    {
        protected override string Title => "Neighbours";

        public override void Generate(GameObject[] nodes)
        {
            if(_jointType == JointType.None) return;
            
            for (int i = 0; i < nodes.Length; i++)
            {
                var prevIndex = (i + nodes.Length - 1) % nodes.Length;
                var prevRigidbody = nodes[prevIndex].GetComponent<Rigidbody2D>();
                var nextIndex = (i + 1) % nodes.Length;
                var nextRigidbody = nodes[nextIndex].GetComponent<Rigidbody2D>();

                CreateJointBetween(nodes[i], prevRigidbody);
                CreateJointBetween(nodes[i], nextRigidbody);
            }
        }
    }
}