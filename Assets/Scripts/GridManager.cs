using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tetriskit
{
    public class GridManager : Singleton<GridManager>
    {
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

            GameObject prefGrid = Resources.Load("grid_assist") as GameObject;
            for ( int y = 0; y < Defines.GridHeightMax; y++)
            {
                for( int x = 0; x < Defines.GridWidthMax; x++)
                {
                    GameObject gridassist = Instantiate(prefGrid, m_tfPivot);
                    gridassist.transform.localPosition = new Vector3(x, y);
                }
            }
            transform.position = new Vector3(
                Mathf.Round(transform.position.x),
                Mathf.Round(transform.position.y));
        }

        public bool InsideBorder(Vector2 pos)
        {
            return ((int)pos.x >= 0 && (int)pos.x < Defines.GridWidthMax && (int)pos.y >= 0);
        }

        public bool IsValidGridPosition(Transform _obj)
        {
            foreach (Transform child in _obj)
            {
                if (child.gameObject.tag.Equals("Block"))
                {
                    Vector2 v = Defines.roundVec2(child.position- m_tfPivot.position);
                    //Debug.Log(v);
                    if (!InsideBorder(v))
                    {
                        //Debug.Log(child.gameObject.name);
                        //Debug.Log(child.position);
                        //Debug.Log(m_tfPivot.position);
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

        public bool Fall(Mino _mino)
        {
            bool bRet = false;

            _mino.transform.position += Vector3.down;
            if (IsValidGridPosition(_mino.transform))
            {
                // アップデート
                UpdateGrid(_mino.transform);
                bRet = true;
            }
            else
            {
                Debug.Log("revert");
                _mino.transform.position += Vector3.up;
            }
            return bRet;
        }

        public void MoveHorizontal(Mino _mino , bool _bIsLeft)
        {
            float deltaMovement = _bIsLeft ? -1.0f : 1.0f;

            // Modify position
            _mino.transform.position += new Vector3(deltaMovement, 0, 0);

            // Check if it's valid
            if ( IsValidGridPosition(_mino.transform))// It's valid. Update grid.
            {
                UpdateGrid(_mino.transform);
            }
            else // It's not valid. revert movement operation.
            {
                _mino.transform.position += new Vector3(-deltaMovement, 0, 0);
            }
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

        public void PlaceMinos()
        {
            StartCoroutine(DeleteRows(0));
        }
        private bool IsRowFull(int _iRow)
        {
            for (int x = 0; x < Defines.GridWidthMax; ++x)
            {
                if (grid[_iRow, x] == null)
                {
                    return false;
                }
            }
            return true;
        }

        private void DecreaseRowsAbove(int y)
        {
            for (int i = y; i < Defines.GridHeightMax; ++i)
            {
                DecreaseRow(i);
            }
        }

        private void DecreaseRow(int y)
        {
            for (int x = 0; x < Defines.GridWidthMax; ++x)
            {
                if (grid[y,x] != null)
                {
                    // Move one towards bottom
                    grid[y - 1, x] = grid[y,x];
                    grid[y,x] = null;

                    // Update Block position
                    grid[y - 1,x].position += Vector3.down;
                }
            }
        }


        private IEnumerator DeleteRows(int _iRowStart)
        {
            for( int y = _iRowStart; y < Defines.GridHeightMax; y++)
            {
                if(IsRowFull(y))
                {
                    DeleteRowLine(y);
                    DecreaseRowsAbove(y + 1);
                    y -= 1;
                    yield return new WaitForSeconds(0.5f);
                }
            }
            yield break;
        }

        private void DeleteRowLine( int _iRow)
        {
            for( int x = 0; x < Defines.GridWidthMax; x++)
            {
                Destroy(grid[_iRow, x].gameObject);
                grid[_iRow, x] = null;
            }
        }



    }
}



