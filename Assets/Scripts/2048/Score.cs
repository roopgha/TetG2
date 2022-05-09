using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private GameManager gm;
    private Text label;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        label = GetComponent<Text>();
    }

    float score = 0;
    float speed = 0;

    void Update()
    {
        if (score < gm.score)
        {
            speed += Time.deltaTime;
            score += speed;
        }
        else
        {
            speed = 0;
            score = gm.score;
        }
        label.text = "Score\n" + (int)score;
    }
}
