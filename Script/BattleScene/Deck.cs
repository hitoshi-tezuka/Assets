using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;
using System.Linq;

namespace BattleScene {

	/// <summary>
	/// デッキ
	/// </summary>
	public class Deck:MonoBehaviour {
		[SerializeField] private CoinCard   m_CoinCard;         // コインカード
		[SerializeField] private ActionCard m_ActionCard;       // アクションカード
		[SerializeField] private PointCard  m_PointCard;        // 領地カード

        private List<Card> m_CardList = new List<Card>();
		private CardBuilder m_CardBulider;

		/// <summary>
		/// カード追加処理
		/// </summary>
		/// <param name="card"></param>
		public void AddCard(Card card)
		{
            card.UpdateState(Card.CardState.DECK);
			m_CardList.Add(card);
            
		}

		/// <summary>
		/// カード取得処理
		/// </summary>
		/// <returns></returns>
		public Card[] GetCard(int num)
		{
			List<Card> cards = new List<Card>();
            int i = 0;
            while (i<num)
            {
                cards.Add(m_CardList[i]);
                i++;
            }
            return cards.ToArray();
		}
   
		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize(List<CardMasterData> cardList) 
		{
			m_CardBulider = new CardBuilder();

			foreach (var card in cardList)
			{
				var content = m_CardBulider.CreateCardObject(card,true);
                content.transform.SetParent(this.transform);
                content.transform.localScale = Vector3.one;
				AddCard(content);
			}
		}

        public void Shuffle()
        {
            int i = m_CardList.Count;
            var rand = new System.Random();
            while (i > 1)
            {
                i--;
                var k = rand.Next(i + 1);
                var card = m_CardList[k];
                m_CardList[k] = m_CardList[i];
                m_CardList[i] = card;
            }
        }
	}
}