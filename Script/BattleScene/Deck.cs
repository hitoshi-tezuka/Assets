using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;

namespace BattleScene {

	/// <summary>
	/// デッキ
	/// </summary>
	public class Deck:MonoBehaviour {
		[SerializeField] private CoinCard   m_CoinCard;         // コインカード
		[SerializeField] private ActionCard m_ActionCard;       // アクションカード
		[SerializeField] private PointCard  m_PointCard;        // 領地カード

		private List<Card> m_CardList;	                        // デッキ情報

		private CardBuilder m_CardBulider;

		/// <summary>
		/// カード追加処理
		/// </summary>
		/// <param name="card"></param>
		public void AddCard(Card card)
		{
            card.State=Card.CardState.HAND;
			m_CardList.Add(card);
		}

		/// <summary>
		/// カード取得処理
		/// </summary>
		/// <returns></returns>
		public Card[] GetCard(int num)
		{
			List<Card> cards = new List<Card>();
			for(int i=0;i<num;i++)
			{ 
				var getCard = m_CardList[Random.Range(0,m_CardList.Count)];
				cards.Add(getCard);
				m_CardList.Remove(getCard);
			}
			return cards.ToArray();
		}
   
		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize(List<CardMasterData> cardList) 
		{
			m_CardBulider = new CardBuilder();
			m_CardList = new List<Card>();

			foreach (var card in cardList)
			{
				var content = m_CardBulider.CreateCard(card);
				content.transform.SetParent(this.transform);
				content.transform.localPosition = Vector3.zero;
				AddCard(content);
			}
		}
	}
}