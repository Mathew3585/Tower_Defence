using UnityEngine;

public class Shop : MonoBehaviour
{
    //Fonction
    private BuildManager buildManager;
    public TourelleBleuprint standarTourelle;
    public TourelleBleuprint MissileLancherTurrette;
    public TourelleBleuprint LazerBeamerTurrette;
    public MineBleuprint MineExplosive;
    public GameObject TurretBasicGosth;

    //Chercher le build manager
    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    //Acheter la tourelle Standar
    public void SelectStandarTuret()
    {
        Debug.Log("S�l�ction de la tourelle standar!!");
        buildManager.SelectTurretToBuild(standarTourelle);
    }

    //Acheter une tourelle lance missile 
    public void SelectMissileLancherTuret()
    {
        Debug.Log("S�l�ction de la tourelle Lace missile!!");
        buildManager.SelectTurretToBuild(MissileLancherTurrette);
    }

    //Acheter une tourelle lance missile 
    public void SelectLazerBeamer()
    {
        Debug.Log("S�l�ction de la tourelle Lazer !!");
        buildManager.SelectTurretToBuild(LazerBeamerTurrette);
    }
    public void SelectMine()
    {
        Debug.Log("S�l�ction de la Mine!!");
        buildManager.SelectMineTobuild(MineExplosive);
    }
}
