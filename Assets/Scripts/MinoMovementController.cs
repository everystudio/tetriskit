using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tetriskit
{
    public class MinoMovementController : MonoBehaviour
    {
        public Transform m_tfPivot;
        public float transitionInterval = 0.8f;
        public void DeleteGuide()
        {
            List<Transform> delete_list = new List<Transform>();
            foreach (Transform t in transform)
            {
                if (t.tag == "guide")
                {
                    delete_list.Add(t);
                }
            }
            foreach( Transform t in delete_list)
            {
                DestroyImmediate(t.gameObject);
            }
        }
        public void ShowGuide()
        {
            DeleteGuide();
            for (int y = 0; y < Mino.GRID_SIZE; y++)
            {
                for (int x = 0; x < Mino.GRID_SIZE; x++)
                {
                    string guide_name = (x == 0 && y == 0) ? "mino_guide_anchor" : "mino_guide";

                    GameObject guide = Instantiate(Resources.Load(guide_name), transform) as GameObject;
                    guide.transform.localPosition = new Vector3(
                        (float)x - 1f, (float)y - 1f);
                }
            }
        }
        public void RotateClockWise( bool _bIsCw)
        {
            //currentMino.movementController.RotateClockWise(_bIsCw);
            float rotationDegree = (_bIsCw) ? 90.0f : -90.0f;
            m_tfPivot.RotateAround(
                m_tfPivot.position,// + transform.up * 0.5f + transform.right * 0.5f
                Vector3.forward,
                rotationDegree);

            /*
            transform.position = new Vector3(
                Mathf.Round(transform.position.x),
                Mathf.Round(transform.position.y));
                */
        }
        public void Fall()
        {
            transform.position += Vector3.down;
        }

        public void DeleteGhost()
        {
            List<Transform> ghost_list = new List<Transform>();
            foreach (Transform t in transform)
            {
                if (t.tag == "BlockGhost")
                {
                    ghost_list.Add(t);
                }
            }
            foreach (Transform t in ghost_list)
            {
                DestroyImmediate(t.gameObject);
            }
        }

        public void Ghost()
        {
            //Debug.Log(gameObject.GetComponentsInChildren<Transform>().Length);

            List<Transform> ghost_list = new List<Transform>();
            List<Transform> block_list = new List<Transform>();
            foreach ( Transform t in transform.GetComponentsInChildren<Transform>())
            {
                if( t.tag == "BlockGhost")
                {
                    ghost_list.Add(t);
                }
                else if( t.tag == "Block")
                {
                    block_list.Add(t);
                }
            }
            foreach (Transform t in ghost_list)
            {
                DestroyImmediate(t.gameObject);
            }
            foreach(Transform t in block_list)
            {
                GameObject ghost = Instantiate(t.gameObject, transform) as GameObject;
                ghost.tag = "BlockGhost";
                Color block_color = ghost.GetComponent<SpriteRenderer>().color;
                ghost.GetComponent<SpriteRenderer>().color = new Color(
                    block_color.r,
                    block_color.g,
                    block_color.b,
                    0.5f
                    );
                ghost.transform.position = t.position;
            }
        }
        public void GhostMove(bool _bDown)
        {
            Vector3 move = _bDown ? Vector3.down : Vector3.up;
            foreach (Transform t in transform)
            {
                if (t.tag == "BlockGhost")
                {
                    t.position += move;
                }
            }
        }

    }
}


