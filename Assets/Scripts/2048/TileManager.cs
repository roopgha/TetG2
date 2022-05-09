using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    private Text label;
    private SpriteRenderer spriteRenderer;
    private float size;
    private int inc = 1;
    private Coroutine sizing;

    private void Awake()
    {
        label = transform.Find("Canvas").Find("Text").GetComponent<Text>();
        label.text = "0";

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetNumber(int num)
    {
        label.text = "" + num;
    }

    public int GetNumber()
    {
        return Int32.Parse(label.text);
    }

    IEnumerator coroutine()
    {
        while (true)
        {
            if (inc > 0)
            {
                if (size > 1) break;
            }
            else
            {
                if (size <= 0) break;
            }

            size += Time.deltaTime * inc;
            transform.localScale = new Vector3(size, size, size);
            yield return null;
        }

        if (inc > 0) transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = Vector3.zero;
    }

    public void Show(bool value)
    {
        if (value)
        {
            inc = 1;
        }
        else
        {
            inc = -1;
        }

        if (sizing != null) StopCoroutine(sizing);
        sizing = StartCoroutine(coroutine());
    }

    public void SetColor(int max_value)
    {
        int n = GetNumber();
        n = (int)Mathf.Log(n, 2);
        max_value = (int)Mathf.Log(max_value, 2);

        float gray = (max_value - (float)n) / max_value;
        spriteRenderer.color = new Color(gray, gray, gray);
    }
}
