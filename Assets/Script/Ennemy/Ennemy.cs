using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class Ennemy : MonoBehaviour
{
    //Fonction
    [Header("Vitesse"), Tooltip("Cette Variable permet de g�re la vitesse de l'ennemies")]
    public float StartSpeed = 10;
    [HideInInspector]
    public float speed;
    [Header("Vie"), Tooltip("Cette Variable permet de g�re la vie de l'ennemies")]
    public float StartHealth = 100f;
    private float Health;
    [Header("Argent drop"), Tooltip("Cette Variable permet de g�re l'argent drop")]
    public int worth = 50;
    [SerializeField]
    [Header("D�gat"), Tooltip("Cette Variable permet de g�rer les d�gat des ennmies")]
    private int dammage;
    [Header("Particule de mort"), Tooltip("Cette Variable permet de cr�er la particule de mort de l'�nnemies")]
    public GameObject deadEffect;
    Player_Stat Player;
    [Header("Image de la vie des �nnemies"), Tooltip("Cette Variable permet de cr�er l'ui qui montre la vie des ennmies")]
    public Image healthbar;
    private bool isDead = false;
    [Header("G�rer le Camera Shake"), Tooltip("Cette Variable permet de g�rer le camera shake")]
    public CameraShaker camerashake;
    private GameManager gameManager;
    public float DurationShake = 0.15f;
    public float MagnitueShake = 0.4f;


    //Permet d'apliquer une vitesse a speed 
    public void Start()
    {
        camerashake = Camera.main.gameObject.GetComponent<CameraShaker>();
        speed = StartSpeed;
        Health = StartHealth;
    }

    //Permet de prendre des d�gat
    public void TakeDommage(float amount)
    {
        Health -= amount;
        healthbar.fillAmount = Health / StartHealth;

        if (Health <= 0 && !isDead)
        {
            Die();
        }
    }

    //Permet de ralentir l'ennemy
    public void Slow(float amount)
    {
        speed = StartSpeed * (1 - amount);
    }


    //Permet de mourrir
    private void Die()
    {
        isDead = true;
        Player_Stat.money += worth;

        GameObject deathParticule = (GameObject)Instantiate(deadEffect, transform.position, Quaternion.identity);
        Destroy(deathParticule, 2f);

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }

    //permet de mettre des d�gat au joueur quand il arrive a la fin des waypoints
    public void EndOfPath()
    {
        CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        Player_Stat.lives -= dammage;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}