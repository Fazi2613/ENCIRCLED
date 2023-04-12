using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [SerializeField]
    private Text enemyKillCountTxt;
    public int enemyKillCount;

    private void Awake()
    {
        if(instance == null)
        instance = this;
    }

    public void EnemyKilled()
    {
        enemyKillCount++;
        Debug.Log("Zombies Defeated: " + enemyKillCount);
        if (enemyKillCount == 21)
        {
            StartCoroutine(LoadSceneWithDelay("Win Screen"));
        }
    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneName);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    public void Update(){
        PlayerPrefs.SetInt("enemyKillCount", enemyKillCount);
    }

}