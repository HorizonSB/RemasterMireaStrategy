using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWaves : MonoBehaviour
{
    [Header("Spawn enemy attributes")]
    public float spawnFrequency;
    GameObject[] enemyBuildings;


    [Header("End Game Settings")]
    public int portalHP = 20;
    private GameObject endGamePanel;
    [SerializeField] private Text hpText;
    [SerializeField] private float gameTimer = 300f;
    [SerializeField] private Text timerText;

    void Start()
    {
        _timeLeft = gameTimer;
        StartCoroutine(StartTimer());

        endGamePanel = GameObject.FindGameObjectWithTag("GameOverPanel");
        enemyBuildings = GameObject.FindGameObjectsWithTag("EnemyBuilding");
        Invoke("BuildEnemySpawn", 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            portalHP -= 1;
            hpText.text = portalHP.ToString() + "/20";
            if (portalHP <= 0)
            {
                LevelFailed();
            }
        }
    }

    private void Update()
    {
        gameTimer -= Time.deltaTime;
        timerText.text = gameTimer.ToString();

        if(gameTimer <= 0 && portalHP > 0)
        {
            LevelPassedSuccesfully();
        }
    }

    public void LevelFailed()
    {
        EventsBus.LevelFailed?.Invoke();
        endGamePanel.transform.GetChild(0).gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void LevelPassedSuccesfully()
    {
        EventsBus.LevelPassedSuccesfully?.Invoke();
    }

    private void BuildEnemySpawn()
    {
        StartCoroutine(EnemyBuildingSpawn());      
    }

    IEnumerator EnemyBuildingSpawn()
    {
        foreach(GameObject building in enemyBuildings)
        {
            building.transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(spawnFrequency);
        }
    }

    public float _timeLeft = 0f;

    private IEnumerator StartTimer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            UpdateTimeText();
            yield return null;
        }
    }

    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
            _timeLeft = 0;

        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
