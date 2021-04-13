using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Text pointsText;
    public int stickmanPoints;

    // Update is called once per frame
    void Update()
    {
        pointsText.text = (stickmanPoints+1).ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level");
    } 
}
