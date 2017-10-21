using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour {

	[SerializeField]
	Text Money;
	[SerializeField]
	Text Score;
	[SerializeField]
	Text Action;
	[SerializeField]
	Text Purchase;

	/// <summary>
	/// ステータスを初期化
	/// </summary>
	/// <param name="money">手札の所持金</param>
	/// <param name="score"></param>
	/// <param name="action"></param>
	/// <param name="purchase"></param>
	public void Setup(int money, int score, int action, int purchase)
	{

	}

	public void UpdateStatus(int money, int score, int action, int purchase)
	{

	}
}
