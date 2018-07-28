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

        private void Start()
        {
            m_CardBulider = new CardBuilder();
        }

        /// <summary>
        /// カード追加処理
        /// </summary>
        /// <param name="card"></param>
        public Card AddCard(Card card)
		{
			m_CardList.Add(card);
            return card;
            
		}

        /// <summary>
        /// カード追加処理(カード作成して追加）
        /// </summary>
        /// <param name="card"></param>
        public Card AddCard(CardMasterData cardMasterData)
        {
            var card = m_CardBulider.CreateCardObject(cardMasterData, true);
            return AddCard(card);

        }

        /// <summary>
        /// カード取得処理
        /// </summary>
        /// <returns></returns>
        public Card[] GetCard(int num, List<Card> alreadyCards = null)
        {
            List<Card> cards = new List<Card>();
            int i = 0;
            var deckCardList = m_CardList.FindAll(x => x.IsDeck && !(alreadyCards != null && alreadyCards.Contains(x)));
            if (deckCardList.Count < num) 
            {
                cards.AddRange(deckCardList);
                var n = num - deckCardList.Count;
                CleanUp();
                cards.AddRange(GetCard(n, deckCardList));
            }
            else
            { 
                while (i<num)
                {
                    cards.Add(deckCardList[i]);
                    i++;
                }
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
                content.UpdateState(Card.CardState.DECK);
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

        private void  CleanUp()
        {
            foreach(var card in m_CardList)
            {
                if(card.IsDiscard)
                {
                    card.UpdateState(Card.CardState.DECK);
                }
            }
            Shuffle();
        }
	}
}