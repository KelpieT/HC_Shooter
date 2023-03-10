using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
	[SerializeField] private List<Image> projectileImages;
	[SerializeField] private TMP_Text timer;

	public void ShowBulets(int countHave)
	{
		for (int i = 0; i < projectileImages.Count; i++)
		{
			projectileImages[i].gameObject.SetActive(i < countHave);
		}
	}
	public void ShowTimer(float timeReverse)
	{
		timer.text = timeReverse.ToString("0.0");
	}
	public void ShowTimer(bool show)
	{
		timer.gameObject.SetActive(show);
	}

}
