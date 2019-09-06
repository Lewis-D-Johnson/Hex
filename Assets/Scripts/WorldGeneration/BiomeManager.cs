using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeManager : MonoBehaviour
{
	#region Singleton
	public static BiomeManager instance;

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

	public List<Biome> Biomes = new List<Biome>();
}

[System.Serializable]
public class Biome
{
	[Header("Statistics")]
	public string Name;

	[Header("Colours")]
	public Color HexColor;
	public Color InteractPanelColor;
	public Color InteractTextColor;

	public enum HexType { NoType, Mining, Forest, Farming, Housing }
	[Header("Types"), MultiSelect(typeof(HexType))]
	public HexType thisType;
}

public class MultiSelectAttribute : PropertyAttribute
{
	public System.Type propType;
	public MultiSelectAttribute(System.Type aType)
	{
		propType = aType;
	}
}