using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stat : MonoBehaviour
{
    //Foncition

    public static int money;
    [Header("Argent du joueur de base"), Tooltip("Cette Variable permet de g�re l'argent  du joueur")]
    public int startmoney = 400;
    [Space(10)]
    private static int _lives;


    public static int lives
    {
        get { return _lives; }
        set { Debug.Log(value); _lives = value; }
    }

    [Header("Vie du joueur de base"), Tooltip("Cette Variable permet de g�re la vie du joueur")]
    public int startLife = 100;

    public static int Rounds;

    public void Start()
    {
        Rounds = 0;
        money = startmoney;
        lives = startLife;
    }

    public void LifeTimeUpagrade()
    {
        lives = lives + 20;
    }

}
