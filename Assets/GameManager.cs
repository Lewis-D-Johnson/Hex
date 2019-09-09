using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(instance.gameObject);
            }
            else
            {
                return;
            }
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    [Header("Global Variables")]
    public int CurPopulation;

    [Header("UI")]
    public Text PopulationText;

    void Start()
    {
        StartNewGame();
    }

    void Update()
    {
        PopulationText.text = "POPULATION: " + CurPopulation.ToString();
    }

    void StartNewGame()
    {
        CurPopulation = 2;
    }
}
