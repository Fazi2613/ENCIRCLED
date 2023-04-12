using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class KillTxtController : MonoBehaviour
{
    private int enemiesKilled;
    private int playerHealth;
    public TMP_Text enemiesKilledText;
    public GameplayController gpC;

    void Start() {
        enemiesKilled = PlayerPrefs.GetInt("enemyKillCount", 0);

        if (SceneManager.GetSceneByBuildIndex(2).IsValid()){
            enemiesKilledText.text = "Megölt ellenségek: " + enemiesKilled;
        }
    }
}