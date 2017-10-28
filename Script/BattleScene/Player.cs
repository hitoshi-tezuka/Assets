using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

		public void Initialize()
		{
			m_Hand.horizontalNormalizedPosition = 0;
			m_Status.Setup(0, 0, 0, 0);
            m_Deck.Initialize();
            AddHand(CLEANUPCARDNUM);
		}

        private void AddHand(int addNum)
        {
            foreach(var card in m_Deck.GetCard(addNum))
            {
                card.transform.SetParent(m_Hand.content);
            }
        }
	}
}