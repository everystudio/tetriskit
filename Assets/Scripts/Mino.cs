using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tetriskit
{
    [RequireComponent(typeof(tetriskit.MinoMovementController))]
    public class Mino : MonoBehaviour
    {
        public static readonly int GRID_SIZE = 4;

        private int m_iGridNum;
        private int m_iGridCount;

        [HideInInspector]
        public MinoMovementController movementController;

        private void Awake()
        {
            movementController = GetComponent<MinoMovementController>();
            m_iGridNum = 0;
            m_iGridCount = 0;
            foreach( Transform t in transform)
            {
                if( t.tag == "Block")
                {
                    m_iGridNum += 1;
                    t.gameObject.AddComponent<MinoPiece>().OnDestroyed.AddListener(() =>
                    {
                        m_iGridCount += 1;
                        if( m_iGridNum <= m_iGridCount)
                        {
                            Destroy(gameObject);
                        }
                    });
                }
            }
        }
    }
}