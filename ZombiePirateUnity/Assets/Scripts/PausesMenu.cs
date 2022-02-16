using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausesMenu : MonoBehaviour
{
    public GameObject PauseUI;
    public bool gameIsPaused;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused == true)
            {
                Unpause();
            }
            if (gameIsPaused == false)
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
        PauseUI.SetActive(true);
    }
    public void Unpause()
    {
        gameIsPaused = false;
        Time.timeScale = 1;
        PauseUI.SetActive(false);
    }
}
