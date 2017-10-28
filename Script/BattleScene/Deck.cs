using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene { 

	/// <summary>
	/// デッキ
	/// </summary>
	public class Deck:MonoBehaviour {
        [SerializeField] private CoinCard   m_CoinCard;         // コインカード
        [SerializeField] private ActionCard m_ActionCard;       // アクションカード
        [SerializeField] private PointCard  m_PointCard;        // 領地カード

        private List<Card> m_CardList;	                        // デッキ情報

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
            m_CardList = new List<Card>();

            for (int i=0;i<3;i++)
			{
                var coin = Instantiate<Card>(m_CoinCard);
                coin.Setup("coin", 1);
                coin.transform.SetParent(this.transform);
                coin.transform.localPosition = Vector3.zero;
                AddCard(coin);
			}
            for(int i=0;i<7;i++)
            {
                var point = Instantiate<Card>(m_PointCard);
                point.transform.SetParent(this.transform);
                point.transform.localPosition = Vector3.zero;
                point.Setup("point",1);
                AddCard(point);
            }
		}

	}
}