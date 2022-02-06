using System;
using UnityEngine;

namespace Svnvav.SoftBody
{
    public class CatController : MonoBehaviour
    {
        [SerializeField] private CatBody _catBody;

        [SerializeField] private Vector2 _pushForce;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Push();
            }
        }

        private void Push()
        {
            _catBody.AddForce(_pushForce, ForceMode2D.Impulse);
        }
    }
}