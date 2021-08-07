using System;
using UnityEngine;

namespace Svnvav.SoftBody.Editor
{
    [Serializable]
    public class AllOtherJointCreator : JointsCreator
    {
        protected override string Title => "AllOthers";

        public override void Generate(GameObject[] nodes)
        {
            if(_jointType == JointType.None) return;
            
            for (int i = 0; i < nodes.Length; i++)
            {
                for (int j = 0; j < nodes.Length; j++)
                {
                    if(i == j) continue;
                    
                    CreateJointBetween(nodes[i], nodes[j].GetComponent<Rigidbody2D>());
                }
            }
        }
    }
}