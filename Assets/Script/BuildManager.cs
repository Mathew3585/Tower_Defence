using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    #region Singleton
    //Faire une reference � un autre sript
    public static BuildManager instance;

    //Chercher un BuildManager Sur la sc�ne 
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Il y a des d�ja un Build Manager dans la sc�ne !!!");
        }
        instance = this;
    }
    #endregion

    //Fonctions
    public GameObject StandarTurretPrefab;


    private GameObject turretToBluid;

    private void Start()
    {
        turretToBluid = StandarTurretPrefab;
    }

    //Mettre tun tourelle
    public GameObject GetTurretToBuild()
    {
        return turretToBluid;
    }



}
