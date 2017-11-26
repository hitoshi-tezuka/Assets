using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene { 
	public abstract class Card : MonoBehaviour {

		[SerializeField] RawImage CardImage;
		[SerializeField] Text CardDescription;

		private int m_purchaseMoney = 0;
		public int PurchaseMoney { get { return m_purchaseMoney; } }

		/// <summary>
		/// カード設定
		/// </summary>
		/// <param name="image">画像イメージ</param>
		/// <param name="description">説明文</param>
		/// <param name="purchaseMoney">購入金額</param>
		public void Setup(string description, int purchaseMoney, RawImage image = null)
		{
			CardDescription.text = description;
			m_purchaseMoney = purchaseMoney;
		}

		/// <summary>
		/// カード効果取得
		/// </summary>
		public abstract Effect GetEffect();

	}
} 