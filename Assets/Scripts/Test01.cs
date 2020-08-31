using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tetriskit
{
    public class Test01 : MonoBehaviour
    {
        Mino currentMino;
        public GameObject m_pref;
        public GameObject m_goBlockHolder;

        public void Spawn()
        {
            // Spawn Group at current Position
            GameObject temp = Instantiate(m_pref);
            currentMino = temp.GetComponent<Mino>();
            temp.transform.parent = m_goBlockHolder.transform;
            temp.transform.localPosition = Vector3.zero;
        }

        public void RotateClockWise(bool _bIsCw)
        {

            currentMino.movementController.Rotate(_bIsCw);
            /*
            //currentMino.movementController.RotateClockWise(_bIsCw);
            float rotationDegree = (_bIsCw) ? 90.0f : -90.0f;
            currentMino.transform.RotateAround(currentMino.movementController.transform.position, Vector3.forward, rotationDegree);
            */
        }

        public void Fall()
        {
            currentMino.transform.position += Vector3.down;
            if (GridManager.Instance != null)
            {
                if (GridManager.Instance.IsValidGridPosition(currentMino.transform))
                {
                    // アップデート
                    GridManager.Instance.UpdateGrid(currentMino.transform);
                }
                else
                {
                    currentMino.transform.position += Vector3.up;
                }
            }
        }
        public void MoveHorizontal(Vector2 direction)
        {
            float deltaMovement = (direction.Equals(Vector2.right)) ? 1.0f : -1.0f;

            // Modify position
            transform.position += new Vector3(deltaMovement, 0, 0);

            // Check if it's valid
            if (GridManager.Instance != null && GridManager.Instance.IsValidGridPosition(this.transform))// It's valid. Update grid.
            {
                GridManager.Instance.UpdateGrid(this.transform);
            }
            else // It's not valid. revert movement operation.
            {
                transform.position += new Vector3(-deltaMovement, 0, 0);
            }
        }



    }
}