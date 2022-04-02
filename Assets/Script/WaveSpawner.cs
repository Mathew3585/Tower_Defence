using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;
using TMPro;

public class WaveSpawner: MonoBehaviour
{
    //Fonctions
    [SerializeField]
    [Header("Prefabs Ennemies"), Tooltip("Cette Variable permet de g�re les Prefabs des ennemies")]
    private Transform ennemyPrefabs;
    [SerializeField]
    [Header("Temps entre chaque spawns"), Tooltip("Cette Variable permet de g�re le Temps entre chaque spawn")]
    private float TimesBetweenspawn;
    [SerializeField]
    [Header("Point de spawn"), Tooltip("Cette Variable permet de g�re les point de spawn")]
    private Transform SpawnPoint;
    [SerializeField]
    [Header("Temp de chaque vagues "), Tooltip("Cette Variable permet de g�re le Temp de Chaque vagues")]
    private TMP_Text waveCountdoawnTimer;
    [SerializeField]
    [Header("Temp entre chaque vagues "), Tooltip("Cette Variable permet de g�re le Temp entre chaque vagues")]
    private float timeBetweenWaves = 5f;
    [SerializeField]
    private float countdown = 5f;
    private int waveIndex = 0;

    void Update()
    {
        //Si le compteur de d�but de partie et = 0 alors on lance la game
        if (countdown <= 0f) 
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        //Chaque seconde qui passe on retire 1 �  countdown
        countdown -= Time.deltaTime;

        // modifer le text Countdown 
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdoawnTimer.text = string.Format("{0:00.00}", countdown);
    }
    
    //Lance la game
    IEnumerator SpawnWave()
    {
        waveIndex++; //Changer de manche
        Debug.Log("Apparitions d'une nouvelle vague");
        Player_Stat.Rounds++;

        //Delay entre chaque spawn
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(TimesBetweenspawn);
        }
    }

    //Faire Spawn les ennemies 
    void SpawnEnemy()
    {
        Instantiate(ennemyPrefabs, SpawnPoint.position, SpawnPoint.rotation);
    }
}
