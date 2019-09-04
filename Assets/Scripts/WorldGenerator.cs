using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
	[SerializeField] float IslandWidth = 50;
	[SerializeField] float IslandLength = 50;

	[Header("Water")]
	[SerializeField] float WaterWidth = 50;
	[SerializeField] float WaterLength = 50;
	[SerializeField] float BaseWaterHeight = -1f;
	public List<Transform> Water = new List<Transform>();
	public float WaterHeighScale, WaterWaveSpeed;

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
				float height = Mathf.PerlinNoise(((float)i + RandomSeed) / NoiseScale, ((float)j + RandomSeed) / NoiseScale) * HeightScale;

				GameObject newHex = Instantiate(Hex, new Vector3((j % 2 == 0) ? (i * 1.7321f) + 0.86603f : i * 1.7321f, height, j * 1.5f), Quaternion.identity);

				newHex.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
			}
		}

		GenerateWater();
	}

	void GenerateWater()
	{
		//for (int i = 0; i > -IslandLength; i--)
		//{
		//	for (int j = 0; j > -IslandWidth; j--)
		//	{
		//		if (i == 0 && j == 0)
		//			continue;

		//		GameObject newHex = Instantiate(Hex, new Vector3((j % 2 == 0) ? (i * 1.7321f) + 0.86603f : i * 1.7321f, -3, j * 1.5f), Quaternion.identity);

		//		newHex.GetComponent<MeshRenderer>().material.color = Color.cyan;
		//	}
		//}

		//for (int i = 0; i < IslandLength * 2; i++)
		//{
		//	for (int j = 0; j > -IslandWidth; j--)
		//	{
		//		if (i == 0 || j == 0)
		//			continue;

		//		GameObject newHex = Instantiate(Hex, new Vector3((j % 2 == 0) ? (i * 1.7321f) + 0.86603f : i * 1.7321f, -3, j * 1.5f), Quaternion.identity);

		//		newHex.GetComponent<MeshRenderer>().material.color = Color.cyan;
		//	}
		//}

		//for (int i = 0; i > -IslandLength; i--)
		//{
		//	for (int j = 0; j < IslandWidth * 2; j++)
		//	{
		//		if (i == 0 || j == 0)
		//			continue;

		//		GameObject newHex = Instantiate(Hex, new Vector3((j % 2 == 0) ? (i * 1.7321f) + 0.86603f : i * 1.7321f, -3, j * 1.5f), Quaternion.identity);

		//		newHex.GetComponent<MeshRenderer>().material.color = Color.cyan;
		//	}
		//}

		for (int i = (int)-WaterLength; i < WaterLength * 2; i++)
		{
			for (int j = (int)-WaterWidth; j < WaterWidth * 2; j++)
			{
				if ((i >= 0 && i < IslandLength) && (j >= 0 && j < IslandWidth))
					continue;

				GameObject newHex = Instantiate(Hex, new Vector3((j % 2 == 0) ? (i * 1.7321f) + 0.86603f : i * 1.7321f, BaseWaterHeight, j * 1.5f), Quaternion.identity);

				newHex.GetComponent<MeshRenderer>().material.color = Color.cyan;

				Water.Add(newHex.transform);
			}
		}
	}
}
