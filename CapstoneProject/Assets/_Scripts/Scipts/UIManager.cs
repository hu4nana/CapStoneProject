using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject Panel;
    // Start is called before the first frame update

    public GameObject Boss_HP_Panel;
    public GameObject Boss_BreakPoint_Panel;
    //public GameObject Player_HP_Panel;
    public GameObject GameOver_Panel;
    public GameObject GameClear_Panel;



    public void OnClickStartGame()
    {
        SceneManager.LoadScene("StageA_Scene");
    }

    public void OnClickEndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit();
#endif
    }

    public void OnClickReloadGame()
    {
        SceneManager.LoadScene("StageA_Scene");
        Time.timeScale = 1;
    }


    public void OnClickGameSetting()
    {
        Panel.SetActive(!Panel.activeSelf);
    }

    public void Boss_HP_Panel_On()
    {
        Boss_HP_Panel.SetActive(true);
        Boss_BreakPoint_Panel.SetActive(true);
    }
    public void GameOver_Panel_On()
    {
        GameOver_Panel.SetActive(true);
    }
    public void Gameclear_Panel_On()
    {
        GameClear_Panel.SetActive(true);
    }


}
