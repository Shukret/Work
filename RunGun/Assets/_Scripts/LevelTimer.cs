using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    [Header("BeforeBoss")]
    [SerializeField] private float startTime;
    private float time;
    [SerializeField] private Slider sl;

    [Header("AfterBoss")]
    [SerializeField] private Image fill;
    [SerializeField] private float bossHealth;
    [SerializeField] private GameObject levelNumber;
    [SerializeField] private GameObject bossSprite;
    public bool boss;
    bool first;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        sl.maxValue = startTime;
        StartCoroutine(MinusTime());
    }   

    IEnumerator MinusTime()
    {
        while (time < startTime)
        {
            yield return new WaitForSeconds(0.05f);
            time += 0.05f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!boss)
        {
            sl.value = time;
        }
        if (boss)
        {
            if (!first)
                StartCoroutine(StartBoss());
            sl.value = bossHealth;
        }
    }

    IEnumerator StartBoss()
    {
        first = true;
        yield return null;
        time = 0;
        levelNumber.SetActive(false);
        bossSprite.SetActive(true);
        fill.color = Color.red;
        startTime = bossHealth;
        sl.maxValue = startTime;
    }
}
