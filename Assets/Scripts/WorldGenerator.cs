using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
	[Header("Island")]
	[SerializeField] float IslandWidth = 50;
	[SerializeField] float IslandLength = 50;

	[Header("Colours")]
	public Color PlainsColour;
	public Color ForestColour;
	public Color DesertColour;
	public Gradient WaterGradient;

	[Header("Water")]
	[SerializeField] float WaterWidth = 50;
	[SerializeField] float WaterLength = 50;
	[SerializeField] float BaseWaterHeight = -1f;
	public List<Transform> Water = new List<Transform>();

	[SerializeField] float HeightScale = 1.5f;
	[SerializeField] float NoiseScale = 3f;
	float perlinMinMax = 5.0f;

	[SerializeField] GameObject Hex;

	void Start()
	{
		Generate();
	}

	void Update()
	{
	}

	void Generate()
	{
		float RandomSeed = Random.Range(0, 100000);

		for (int i = 0; i < IslandLength; i++)
		{
			for (int j = 0; j < IslandWidth; j++)
			{
				float height = (Mathf.PerlinNoise(((float)i + RandomSeed) / NoiseScale, ((float)j + RandomSeed) / NoiseScale) * HeightScale) + 0.5f;

				GameObject newHex = Instantiate(Hex, new Vector3((j % 2 == 0) ? (i * 1.7321f) + 0.86603f : i * 1.7321f, height, j * 1.5f), Quaternion.identity);

				newHex.transform.SetParent(transform);

				int rand = Random.Range(0, 3);

				if (rand == 0)
					newHex.GetComponent<HexManager>().thisBiome = HexManager.BiomeType.Plains;
				else if (rand == 1)
					newHex.GetComponent<HexManager>().thisBiome = HexManager.BiomeType.Forest;
				else if (rand == 2)
					newHex.GetComponent<HexManager>().thisBiome = HexManager.BiomeType.Desert;

				if (newHex.GetComponent<HexManager>().thisBiome == HexManager.BiomeType.Plains)
					newHex.GetComponent<MeshRenderer>().material.color = PlainsColour;
				else if (newHex.GetComponent<HexManager>().thisBiome == HexManager.BiomeType.Forest)
					newHex.GetComponent<MeshRenderer>().material.color = ForestColour;
				else if (newHex.GetComponent<HexManager>().thisBiome == HexManager.BiomeType.Desert)
					newHex.GetComponent<MeshRenderer>().material.color = DesertColour;

			}
		}

		GenerateWater();
	}

	void GenerateWater()
	{
		for (int i = (int)-WaterLength; i < WaterLength * 2; i++)
		{
			for (int j = (int)-WaterWidth; j < WaterWidth * 2; j++)
			{
				if ((i >= 0 && i < IslandLength) && (j >= 0 && j < IslandWidth))
					continue;

				GameObject newHex = Instantiate(Hex, new Vector3((j % 2 == 0) ? (i * 1.7321f) + 0.86603f : i * 1.7321f, BaseWaterHeight, j * 1.5f), Quaternion.identity);

				newHex.GetComponent<MeshRenderer>().material.color = WaterGradient.Evaluate(Random.Range(0f, 1f));

				newHex.transform.SetParent(transform);

				Water.Add(newHex.transform);
				newHex.GetComponent<HexManager>().isWater = true;
			}
		}
	}
}
