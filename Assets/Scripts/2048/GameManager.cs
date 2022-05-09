using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject tile;
    public int size;
    private TileManager[,] arr;
    private int[,] map;

    public int score = 0;

    private System.Random random = new System.Random();

    private Dictionary<KeyCode, Action> dictionary;
    void Start()
    {
        dictionary = new Dictionary<KeyCode, Action>()
        {
            {KeyCode.LeftArrow, MoveLeft},
            {KeyCode.RightArrow, MoveRight},
            {KeyCode.DownArrow, MoveDown},
            {KeyCode.UpArrow, MoveUp},
            {KeyCode.R, restart}
        };

        arr = new TileManager[size, size];
        map = new int[size, size];

        Camera.main.transform.position = new Vector3(size / 2f - 0.5f, size / 2f + 0.5f, -10);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                var tmp = Instantiate(tile);
                TileManager manager = tmp.GetComponent<TileManager>();

                arr[i, j] = manager;

                tmp.name = "" + j + "," + i;
                tmp.transform.position = new Vector2(j, i);
            }
        }

        SpawnTile();
        SpawnTile();

        TileUpdate();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (var keyActionPair in dictionary)
            {
                if (Input.GetKeyDown(keyActionPair.Key))
                {
                    keyActionPair.Value();
                    SpawnTile();
                    break;
                }
            }
        }

        TileUpdate();
    }

    private void SpawnTile()
    {
        var list = GetEmptyPos();

        if (list.Count == 0)
        {
            return;
        }

        var pos = list[random.Next() % list.Count];

        var i = random.Next() % 10;

        if (i == 0)
        {
            map[pos.y, pos.x] = 4;
            score += 4;
        }
        else
        {
            map[pos.y, pos.x] = 2;
            score += 2;
        }
    }

    private void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    class pos
    {
        public int x;
        public int y;

        public pos()
        {
            x = 0;
            y = 0;
        }

        public pos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    private List<pos> GetEmptyPos()
    {
        List<pos> list = new List<pos>();

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (map[i, j] == 0)
                    list.Add(new pos(j, i));
            }
        }

        return list;
    }

    private void TileUpdate()
    {
        int max_value = GetMax();

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                arr[i, j].Show(map[i, j] != 0);
                arr[i, j].SetNumber(map[i, j]);
                arr[i, j].SetColor(max_value);
            }
        }
    }

    private int GetMax()
    {
        int result = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                result = map[i, j] > result ? map[i, j] : result;
            }
        }
        return result;
    }

    private void MoveLeft()
    {
        for (int i = 0; i < size; i++)
        {
            int[] tmp = new int[size];
            int x = 0;

            int last = 0;
            for (int j = 0; j < size; j++)
            {
                if (map[i, j] == 0) continue;
                if (last == 0)
                {
                    last = map[i, j];
                }
                else
                {
                    if (last == map[i, j])
                    {
                        tmp[x++] = last * 2;
                        last = 0;
                    }
                    else
                    {
                        tmp[x++] = last;
                        last = map[i, j];
                    }
                }
            }
            tmp[x] = last;

            for (int j = 0; j < size; j++)
            {
                map[i, j] = tmp[j];
            }
        }

        TileUpdate();
    }

    private void MoveRight()
    {
        for (int i = 0; i < size; i++)
        {
            int[] tmp = new int[size];
            int x = size - 1;

            int last = 0;
            for (int j = size - 1; j >= 0; j--)
            {
                if (map[i, j] == 0) continue;
                if (last == 0)
                {
                    last = map[i, j];
                }
                else
                {
                    if (last == map[i, j])
                    {
                        tmp[x--] = last * 2;
                        last = 0;
                    }
                    else
                    {
                        tmp[x--] = last;
                        last = map[i, j];
                    }
                }

            }
            tmp[x] = last;

            for (int j = 0; j < size; j++)
            {
                map[i, j] = tmp[j];
            }
        }

        TileUpdate();
    }

    private void MoveDown()
    {
        for (int i = 0; i < size; i++)
        {
            int[] tmp = new int[size];
            int y = 0;

            int last = 0;
            for (int j = 0; j < size; j++)
            {
                if (map[j, i] == 0) continue;
                if (last == 0)
                {
                    last = map[j, i];
                }
                else
                {
                    if (last == map[j, i])
                    {
                        tmp[y++] = last * 2;
                        last = 0;
                    }
                    else
                    {
                        tmp[y++] = last;
                        last = map[j, i];
                    }
                }
            }
            tmp[y] = last;

            for (int j = 0; j < size; j++)
            {
                map[j, i] = tmp[j];
            }
        }

        TileUpdate();
    }

    private void MoveUp()
    {
        for (int i = 0; i < size; i++)
        {
            int[] tmp = new int[size];
            int y = size - 1;

            int last = 0;
            for (int j = size - 1; j >= 0; j--)
            {
                if (map[j, i] == 0) continue;
                if (last == 0)
                {
                    last = map[j, i];
                }
                else
                {
                    if (last == map[j, i])
                    {
                        tmp[y--] = last * 2;
                        last = 0;
                    }
                    else
                    {
                        tmp[y--] = last;
                        last = map[j, i];
                    }
                }
            }
            tmp[y] = last;

            for (int j = 0; j < size; j++)
            {
                map[j, i] = tmp[j];
            }
        }

        TileUpdate();
    }
}
