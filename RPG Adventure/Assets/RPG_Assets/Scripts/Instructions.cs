using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    public Button start_button;

    void Start()
    {
        Button start = start_button.GetComponent<Button>();
        GameObject.Find("Timer");

    }

    // Update is called once per frame
    public void game_start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}