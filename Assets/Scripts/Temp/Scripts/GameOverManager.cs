using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public bool isDead;

    [SerializeField] private TextMeshProUGUI bannerText;
    [SerializeField] private GameObject gameOverPopup;

    void Start()
    {
        gameOverPopup.SetActive(true);

        if (isDead)
        {
            bannerText.text = "패배!";
        }
        else
        {
            bannerText.text = "승리!";
        }
    }

    void Update()
    {

    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene("");
    }

    public void OnClickEnd()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}   
