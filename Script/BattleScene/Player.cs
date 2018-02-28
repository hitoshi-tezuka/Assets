using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Database;

namespace BattleScene { 
	public class Player : MonoBehaviour {

		[SerializeField]
		private ScrollRect m_Hand;
        [SerializeField]
		private Status m_Status;
        [SerializeField]
        private Deck m_Deck;

		private BattleSceneManager.PlayerTurn m_ProcessingTurn;
        private const int CLEANUPCARDNUM = 5;
        private PlayerStatus m_PlayerStatus;
        private List<Card> m_PositionCard;
		/// <summary>
		/// プレイヤーステータス情報
		/// </summary>
		public struct PlayerStatus
		{
			public int Money;
			public int Scrore;
			public int Action;
			public int Purchase;
		}
	
		// Update is called once per frame
		void Update () {
			
			switch(m_ProcessingTurn)
			{
				case BattleSceneManager.PlayerTurn.SelectCard:
					break;
				case BattleSceneManager.PlayerTurn.Purchase:
					break;
				case BattleSceneManager.PlayerTurn.Action:
					break;
				case BattleSceneManager.PlayerTurn.CleanUp:
					break;
			}
		}

		public void Initialize(List<CardMasterData> cardList)
		{
            m_PlayerStatus = new PlayerStatus();
            m_PositionCard = new List<Card>();
            m_Deck.Initialize(cardList);
            DrawCard(CLEANUPCARDNUM);
            m_Hand.horizontalNormalizedPosition = 0;
            m_Status.UpdateStatus(m_PlayerStatus);
        }

        private void DrawCard(int addNum)
        {
            foreach(var card in m_Deck.GetCard(addNum))
            {
                m_PositionCard.Add(card);
                card.transform.SetParent(m_Hand.content);
                UpdateStatus(card);
            }
        }

        public void UpdateStatus(Card card)
        {
            m_PlayerStatus.Money += card.TreaserCoin;
            m_PlayerStatus.Money += card.PlusCoin;
            m_PlayerStatus.Purchase += card.PlusPurchase;
            m_PlayerStatus.Action += card.PlusAction;
            m_Status.UpdateStatus(m_PlayerStatus);
        }
	}
}