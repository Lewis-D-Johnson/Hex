using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
	#region Singleton
	public static InteractionManager instance;

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
	public List<GameObject> InteractionTabs = new List<GameObject>();

	public HexManager thisInteraction;

	public void ChangeInteraction(HexManager newInteraction)
	{
		if (!newInteraction.InteractCanvas.gameObject.activeInHierarchy)
		{
			foreach (GameObject inter in InteractionTabs)
			{
				inter.SetActive(false);
			}

			thisInteraction = newInteraction;
			newInteraction.ToggleInteractCanvas();

			InteractionTabs.Add(newInteraction.InteractCanvas.gameObject);
		}
		else
		{
			foreach (GameObject inter in InteractionTabs)
			{
				inter.SetActive(false);
			}
		}
	}
}
