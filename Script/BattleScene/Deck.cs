using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene { 

	/// <summary>
	/// デッキ
	/// </summary>
	public class Deck {
        [SerializeField]
        private Card m_Card;            // カードプレファブ
		private List<Card> m_CardList;	// デッキ情報

		/// <summary>
		/// カード追加処理
		/// </summary>
		/// <param name="card"></param>
		public void AddCard(Card card)
		{
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
		public void Initialize() 
		{
			for(int i=0;i<3;i++)
			{
                
                Card copperCoin = new CoinCard();
                copperCoin.Setup("test", 1);
                AddCard(copperCoin);
			}
            for(int i=0;i<7;i++)
            {
                Card terrytory = new TerrytoryCard();
                AddCard(terrytory);
            }
		}

	}
}