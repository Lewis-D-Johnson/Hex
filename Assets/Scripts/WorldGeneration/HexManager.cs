using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexManager : MonoBehaviour
{
	[Header("Statistics")]
	public int HexPopulation;
	public enum HexType { None, Mining, Forest, Farming, Housing }
	public HexType thisType;

	public bool isWater;

    [Header("Components")]
    public Transform HexerHolder;
    public GameObject Hexer;
	public GameObject TypeParent;
	public GameObject MiningType;
	public GameObject ForestType;
	public GameObject FarmingType;
	public GameObject HousingType;
    public Transform InteractCanvas;

	[Header("Water")]
	[SerializeField] float WaterWaveHeight = 1f;
	float randomHeight, randomSpeed;
	public float minWaterWaveSpeed, maxWaterWaveSpeed;
	bool moveUp;

	[Header("UI")]
	[SerializeField] GameObject SelectedIcon;
    [SerializeField] Text Title;
    [SerializeField] Text Description;
	[SerializeField] Button ColonizeBttn;
	[SerializeField] Button AttackBttn;
	[SerializeField] Button DeforestBttn;
	[SerializeField] Button FarmBttn;
	[SerializeField] Button MineBttn;

	private void Start()
	{
        ColonizeBttn.onClick.AddListener(ColonizeHex);

        if (!isWater)
		{
            InteractCanvas.GetComponent<Canvas>().worldCamera = Camera.main;//.transform.GetChild(0).GetComponent<Camera>();

			int randRotation = Random.Range(0, 7);

			TypeParent.transform.eulerAngles = new Vector3(0, Mathf.RoundToInt(randRotation) * 60, 0);

			int RandType = Random.Range(0, 5);

			switch (RandType)
			{
				case 0:
					{
						thisType = HexType.None;
						ColonizeBttn.gameObject.SetActive(true);
						AttackBttn.gameObject.SetActive(false);
						DeforestBttn.gameObject.SetActive(false);
						FarmBttn.gameObject.SetActive(false);
						MineBttn.gameObject.SetActive(false);
						break;
					}
				case 1:
					{
						thisType = HexType.Mining;
						MiningType.SetActive(true);
						ColonizeBttn.gameObject.SetActive(false);
						AttackBttn.gameObject.SetActive(false);
						DeforestBttn.gameObject.SetActive(false);
						FarmBttn.gameObject.SetActive(false);
						MineBttn.gameObject.SetActive(true);
						break;
					}
				case 2:
					{
						thisType = HexType.Forest;
						ForestType.SetActive(true);
						ColonizeBttn.gameObject.SetActive(false);
						AttackBttn.gameObject.SetActive(false);
						DeforestBttn.gameObject.SetActive(true);
						FarmBttn.gameObject.SetActive(false);
						MineBttn.gameObject.SetActive(false);
						break;
					}
				case 3:
					{
						thisType = HexType.Farming;
						FarmingType.SetActive(true);
						ColonizeBttn.gameObject.SetActive(false);
						AttackBttn.gameObject.SetActive(false);
						DeforestBttn.gameObject.SetActive(false);
						FarmBttn.gameObject.SetActive(true);
						MineBttn.gameObject.SetActive(false);
						break;
					}
				case 4:
					{
						thisType = HexType.Housing;
						HousingType.SetActive(true);
						ColonizeBttn.gameObject.SetActive(false);
						AttackBttn.gameObject.SetActive(true);
						DeforestBttn.gameObject.SetActive(false);
						FarmBttn.gameObject.SetActive(false);
						MineBttn.gameObject.SetActive(false);
						break;
					}
			}
		}
		else
		{
			randomHeight = Random.Range(0, WaterWaveHeight);
			randomSpeed = Random.Range(minWaterWaveSpeed, maxWaterWaveSpeed);

			CheckWaterInit();
		}
	}

	private void Update()
	{
		if (moveUp)
		{
			transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, -randomHeight, transform.localPosition.z), randomSpeed);
		}
		else
		{
			transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, randomHeight, transform.localPosition.z), randomSpeed);
		}

		InteractCanvas.LookAt(Camera.main.transform.GetChild(0));
	}

	IEnumerator CheckWater()
	{
		if (transform.localPosition.y >= randomHeight - 0.5f)
		{
			moveUp = true;
		}
		else
		{
			moveUp = false;
		}

		yield return new WaitForSeconds(1f);

		CheckWaterInit();
	}

	void CheckWaterInit()
	{
		StartCoroutine(CheckWater());
	}

	private void OnMouseEnter()
	{
        if(!isWater)
		    SelectedIcon.SetActive(true);		
	}
	private void OnMouseExit()
    {
        if (!isWater)
            SelectedIcon.SetActive(false);
	}

    private void OnMouseDown()
    {
        if(!isWater)
		    InteractionManager.instance.ChangeInteraction(this);
    }

    public void ToggleInteractCanvas()
    {
        InteractCanvas.gameObject.SetActive(!InteractCanvas.gameObject.activeInHierarchy);

        //Title.text = BiomeName.ToUpper() + " - " + thisType.ToString().ToUpper();
        Description.text = "This description will be filled in very shortly!";
    }

    #region ButtonMethods

    public void ColonizeHex()
    {
        if (GameManager.instance.CurPopulation > 0)
        {
            HexPopulation = GameManager.instance.CurPopulation;

            for (int i = 0; i < GameManager.instance.CurPopulation; i++)
            {
                GameObject newHexer = Instantiate(Hexer, HexerHolder);
                newHexer.transform.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            }

            GameManager.instance.CurPopulation = 0;
        }
    }

    #endregion
}
