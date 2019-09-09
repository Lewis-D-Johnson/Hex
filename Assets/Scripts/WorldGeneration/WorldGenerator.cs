using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

                newHex.AddComponent<MeshCollider>();

				int randBiome = Random.Range(0, BiomeManager.instance.Biomes.Count);

				//newHex.GetComponent<HexManager>().BiomeName = BiomeManager.instance.Biomes[randBiome].Name;
				newHex.GetComponent<MeshRenderer>().material.color = BiomeManager.instance.Biomes[randBiome].HexColor;

			}
		}

		GenerateWater();

        GameManager.instance.GetComponent<NavMeshSurface>().BuildNavMesh();
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

                newHex.layer = 4;

				Water.Add(newHex.transform);
				newHex.GetComponent<HexManager>().isWater = true;
			}
		}
	}
}
