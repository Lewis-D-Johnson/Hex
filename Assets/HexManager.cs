using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexManager : MonoBehaviour
{
    public enum BiomeType { Plains, Forest, Desert }
	[Header("Statistics")]
	public BiomeType thisBiome;
	public enum HexType { None, Mining, Forest, Farming, Housing }
	public HexType thisType;

	public bool isWater;

	[Header("Components")]
	public GameObject TypeParent;
	public GameObject MiningType;
	public GameObject ForestType;
	public GameObject FarmingType;
	public GameObject HousingType;

	[Header("Water")]
	[SerializeField] float WaterWaveHeight = 1f;
	float randomHeight, randomSpeed;
	public float minWaterWaveSpeed, maxWaterWaveSpeed;
	bool moveUp;

	[Header("UI")]
	[SerializeField] GameObject SelectedIcon;

	private void Start()
	{
		if (!isWater)
		{
			int randRotation = Random.Range(0, 7);

			TypeParent.transform.eulerAngles = new Vector3(0, Mathf.RoundToInt(randRotation) * 60, 0);

			int RandType = Random.Range(0, 5);

			switch (RandType)
			{
				case 0:
					{
						thisType = HexType.None;
						break;
					}
				case 1:
					{
						thisType = HexType.Mining;
						MiningType.SetActive(true);
						break;
					}
				case 2:
					{
						thisType = HexType.Forest;
						ForestType.SetActive(true);
						break;
					}
				case 3:
					{
						thisType = HexType.Farming;
						FarmingType.SetActive(true);
						break;
					}
				case 4:
					{
						thisType = HexType.Housing;
						HousingType.SetActive(true);
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
		if (!moveUp)
		{
			transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, -randomHeight, transform.localPosition.z), randomSpeed);
		}
		else
		{
			transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, randomHeight, transform.localPosition.z), randomSpeed);
		}
	}

	IEnumerator CheckWater()
	{
		if (transform.localPosition.y >= randomHeight - 0.05f)
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
		SelectedIcon.SetActive(true);		
	}
	private void OnMouseExit()
	{
		SelectedIcon.SetActive(false);
	}
}
