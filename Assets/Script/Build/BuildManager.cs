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

    private TourelleBleuprint turretToBluid;
    private Node selectedNode;
    [Header("Node")]
    public NodeUi nodeUi;
    [Header("Particule de Construction")]
    public GameObject ParticuleBuild;
    [Space(5)]
    public GameObject ParticuleSell;
    [Header("Audio de Construction")]
    [Space(5)]
    public AudioClip AudioBuild;
    [Space(5)]
    public AudioClip AudioSell;
    public bool canBuild { get { return turretToBluid != null; } }
    public bool hasMoney { get { return Player_Stat.money >= turretToBluid.cost; } }

    //Mettre un tourelle 


    public void SelectTurretToBuild(TourelleBleuprint turret) 
    {
        turretToBluid = turret;
        selectedNode = null;

        DeselecetNode();
    }

    //Recuper les valeur de turretToBuild 
    public TourelleBleuprint GetTuretToBuild()
    {
        return turretToBluid;
    }

    //Pemermet de savoir si une tourelle et selectionner et afficher Le nodeUi
    public  void SelectedNode(Node node)
    {

        if(node == selectedNode)
        {
            DeselecetNode();
            return;
        }
        Debug.Log("Ok tourelle s�l�ctioner");
        selectedNode = node;
        turretToBluid = null;
        nodeUi.SetTarget(node);
    }

    //Permet de d�select un node
    public void DeselecetNode()
    {
        selectedNode = null;
        nodeUi.Hide();
    }
} 
