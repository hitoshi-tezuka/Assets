using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Database;
using System;

namespace BattleScene {
    public class Player : Photon.MonoBehaviour {

		[SerializeField]
		private ScrollRect m_Hand;
        [SerializeField]
		private Status m_Status;
        [SerializeField]
        private Deck m_Deck;

		private BattleSceneManager.PlayerTurn m_ProcessingTurn;
        private const int CLEANUPCARDNUM = 5;
        private PlayerStatus m_PlayerStatus;
        private List<Card> m_HandCard;

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

        public void UpdateStatus()
        {
            photonView.RPC("SyncStatus", PhotonTargets.All);
        }

        [PunRPC]
        public void SyncStatus()
        {
            Debug.Log("UpdateStatus");
            foreach (Transform child in m_Hand.content)
            {
                var card = child.GetComponent<Card>();

                m_PlayerStatus.Money += card.TreaserCoin;
                m_PlayerStatus.Money += card.PlusCoin;
                m_PlayerStatus.Purchase += card.PlusPurchase;
                m_PlayerStatus.Action += card.PlusAction;
                m_Status.UpdateStatus(m_PlayerStatus);
            }
        }

        public void Initialize(List<CardMasterData> cardList)
		{
            m_PlayerStatus = new PlayerStatus();
            m_HandCard  = new List<Card>();
            m_Deck.Initialize(cardList);
            m_Deck.Shuffle();
            DrawCard(CLEANUPCARDNUM);
            UpdateStatus();
            m_Hand.horizontalNormalizedPosition = 0;
        }

        private void DrawCard(int addNum)
        {
            foreach (var card in m_Deck.GetCard(addNum))
            {
                 card.UpdateState(Card.CardState.HAND);
                m_HandCard.Add(card);
            }
        }

        private void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            Debug.Log("OnPhotonInstantiate : Player");
            // プレイヤーの位置を初期化
            SetupTransform();
        }

        private void SetupTransform()
        {
            name = GetComponent<PhotonView>().ownerId.ToString();
            transform.SetParent(BattleSceneManager.SceneManager.PlayersTransform);
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
        }
    }
}