using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene
{
    public class Player : Photon.MonoBehaviour {

		[SerializeField]
		private ScrollRect m_Hand;
        [SerializeField]
		private Status m_Status;
        [SerializeField]
        private Deck m_Deck;
        [SerializeField] private ScrollRect m_PublicCardSheet;
        [SerializeField] private ScrollRect m_PrivateCardSheet;

        private BattleSceneManager.PlayerTurn m_ProcessingTurn;
        private const int CLEANUPCARDNUM = 5;
        private PlayerStatus m_PlayerStatus;
        private List<Card> m_HandCard;

        public Action<Player> PhotonInstantiateCallback { get; set; }
        public PhotonPlayer OwnerPlayer { get; private set; }
        public int Money => m_PlayerStatus.Money;

		/// <summary>
		/// プレイヤーステータス情報
		/// </summary>
        [Serializable]
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

        public void Initialize(List<Entity_CardMaster.CardMasterData> cardList)
        {
            m_PlayerStatus = new PlayerStatus();
            m_HandCard = new List<Card>();
            m_Deck.Initialize(cardList);
            m_Deck.Shuffle();
            DrawCard(CLEANUPCARDNUM);
            CleanUpStatus();
        }

        public void UpdateStatus(Card card)
        {
            m_PlayerStatus.Money += card.TreaserCoin;
            m_PlayerStatus.Money += card.PlusCoin;
            m_PlayerStatus.Purchase += card.PlusPurchase;
            m_PlayerStatus.Action += card.PlusAction;
            SyncStatus();
        }


        private void SyncStatus()
        {
            photonView.RPC("SyncStatus", PhotonTargets.AllBuffered, m_PlayerStatus);
        }

        [PunRPC]
        private void SyncStatus(PlayerStatus status)
        {
            m_Status.UpdateStatus(status);
        }

        public void PurchaseCard(Card card)
        {
            if (card.CostCoin>m_PlayerStatus.Money || m_PlayerStatus.Purchase <= 0) return;
            var cardCont = m_Deck.AddCard(card.Data);
            cardCont.UpdateState(Card.CardState.DISCARD);
            m_PlayerStatus.Purchase -= 1;
            m_PlayerStatus.Money -= card.CostCoin;
            card.Supply = card.Supply - 1;
            photonView.RPC("SyncPurchaseCard", PhotonTargets.AllBuffered,m_PlayerStatus);
        }

        [PunRPC]
        private void SyncPurchaseCard(PlayerStatus status)
        {
            m_Status.UpdateStatus(status);
        }

        public void SetActivePublicDrawCard(bool flg)
        {
            photonView.RPC("SyncPublicDrawCard", PhotonTargets.AllBuffered, flg);
        }

        [PunRPC]
        private void SyncPublicDrawCard(bool active)
        {
            m_PublicCardSheet.gameObject.SetActive(active);
        }

        public void AddCardToDeck(Card card)
        {
            var cardCont = m_Deck.AddCardToFirst(card.Data);
            cardCont.UpdateState(Card.CardState.DECK);
        }

        public void EndTurn()
        {
            CleanUpStatus();
            HandAllDiscard();
            DrawCard(CLEANUPCARDNUM);
        }

        public List<Card> DrawCard(int addNum)
        {
            var cardList = new List<Card>();
            foreach (var card in m_Deck.GetCard(addNum))
            {
                card.UpdateState(Card.CardState.HAND);
                m_HandCard.Add(card);
                cardList.Add(card);
            }
            return cardList;
        }

        public async UniTask<List<Card>> GetSelectCard()
        {
            var selectCard = new List<Card>();

            // カード選択されるまで待機
            await SelectCard();

            foreach(Card card in m_HandCard)
            {
                if (card.IsSelect) selectCard.Add(card);
            }

            return selectCard;
        }

        private async UniTask SelectCard()
        {
            m_PrivateCardSheet.gameObject.SetActive(true);

            

            m_PrivateCardSheet.gameObject.SetActive(false);
        }

        private void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            //Debug.Log("OnPhotonInstantiate : Player");
            OwnerPlayer = info.sender;
            name = OwnerPlayer.ID.ToString();
            SetupTransform();
            BattleSceneManager.SceneManager.PlayerAdd(this);
        }

        // プレイヤーの位置を初期化
        private void SetupTransform()
        {
            transform.SetParent(BattleSceneManager.SceneManager.PlayersTransform);
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
        }


        private void CleanUpStatus()
        {
            m_PlayerStatus = new PlayerStatus();
            m_PlayerStatus.Purchase = 1;
            m_PlayerStatus.Action = 1;
            SyncStatus();
        }

        private void HandAllDiscard()
        {
            foreach (var card in m_HandCard)
            {
                card.UpdateState(Card.CardState.DISCARD);
            }
            m_HandCard.Clear();
        }
    }
}