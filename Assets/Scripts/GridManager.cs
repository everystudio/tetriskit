using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tetriskit
{
    public class GridManager : Singleton<GridManager>
    {
        public GameObject m_prefGrid;
        public Transform m_tfPivot;

        public Transform[,] grid = new Transform[Defines.GridHeightMax,Defines.GridWidthMax];

        public void Setup()
        {
            foreach(Transform t in m_tfPivot.GetComponentsInChildren<Transform>())
            {
                if (m_tfPivot.gameObject != t.gameObject)
                {
                    DestroyImmediate(t.gameObject);
                }
            }

            for( int y = 0; y < Defines.GridHeightMax; y++)
            {
                for( int x = 0; x < Defines.GridWidthMax; x++)
                {
                    GameObject gridassist = Instantiate(m_prefGrid, m_tfPivot);
                    gridassist.transform.localPosition = new Vector3(x, y);
                }
            }
        }

        public bool InsideBorder(Vector2 pos)
        {
            return ((int)pos.x >= 0 && (int)pos.x < Defines.GridWidthMax && (int)pos.y >= 0);
        }

        public bool IsRowFull(int _iRow)
        {
            for (int x = 0; x < Defines.GridWidthMax; ++x)
            {
                if (grid[_iRow,x] == null)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsValidGridPosition(Transform _obj)
        {
            foreach (Transform child in _obj)
            {
                if (child.gameObject.tag.Equals("Block"))
                {
                    Vector2 v = Defines.roundVec2(child.position- m_tfPivot.position);
                    Debug.Log(v);
                    if (!InsideBorder(v))
                    {
                        return false;
                    }

                    if (grid[(int)v.y,(int)v.x] != null &&
                        grid[(int)v.y, (int)v.x].parent != _obj)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void UpdateGrid(Transform _obj)
        {
            // 一度対象のオブジェクトのgridデータを削除
            for (int y = 0; y < Defines.GridHeightMax; y++)
            {
                for (int x = 0; x < Defines.GridWidthMax; x++)
                {
                    if (grid[y,x] != null)
                    {
                        if (grid[y, x].parent == _obj)
                        {
                            grid[y, x] = null;
                        }
                    }
                }
            }

            // 対象のオブジェクトのグリッドを位置から
            foreach (Transform child in _obj)
            {
                if (child.gameObject.tag.Equals("Block"))
                {
                    Vector2 v = Defines.roundVec2(child.position - m_tfPivot.position);
                    grid[(int)v.y, (int)v.x] = child;
                }
            }
        }



    }
}



