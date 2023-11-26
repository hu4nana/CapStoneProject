using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject Panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OnClickStartGame()
    {
        SceneManager.LoadScene("InGameScene");
    }

    public void OnClickEndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit();
#endif
    }
    public void OnClickGameSetting()
    {
        Panel.SetActive(!Panel.activeSelf);
    }
}
