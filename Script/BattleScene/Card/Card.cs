using UnityEngine;
using UnityEngine.UI;
using Database;
using UnityEngine.EventSystems;

namespace BattleScene { 
	public abstract class Card : Photon.MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler{

		[SerializeField] private RawImage m_CardImage;
		[SerializeField] private Text m_CardDescription;
        [SerializeField] private GameObject m_ShowCard;
        [SerializeField] private GameObject m_CardNum;
        [SerializeField] private Text m_CardCost;

        public enum CardState
        {
            SUPPLY,
            FIELD,
            HAND,
            DECK,
            DISCARD,
            REVOCATION,
        }

        public int CostCoin { get { return Data.CostCoin; } }
        public int TreaserCoin { get { return Data.Treasure; } }
        public int PlusAction { get {return Data.PlusAction; } }
        public int PlusCoin { get { return Data.PlusCoin; } }
        public int PlusCard { get { return Data.PlusCard; } }
        public int PlusPurchase { get { return Data.PlusPurchase; } }
        public int PlusVictoryPointToken { get { return Data.PlusVictoryPointToken; } }
        public CardState State {
            get { return m_state; }
        }

        private Vector3 m_dragIconScale = new Vector3(0.7f, 0.7f, 0.7f);
        public CardMasterData Data { get; private set; }
        private ScrollRect m_HandScrollRect;
        private GameObject m_DragIcon;
        private int m_purchaseMoney = 0;

        public  CardState m_state = CardState.SUPPLY;
        public bool IsHand {get { return State.Equals(CardState.HAND); }}
        public bool IsSupply { get { return State.Equals(CardState.SUPPLY); } }
        public bool IsField { get { return State.Equals(CardState.FIELD); } }
        public bool IsDeck { get { return State.Equals(CardState.DECK); } }
        public bool IsDiscard { get { return State.Equals(CardState.DISCARD); } }
        public bool IsRevocation { get { return State.Equals(CardState.REVOCATION); } }

        private bool IsScroll { get { return IsHand || IsSupply; } }
        private bool IsCreateDrag { get { return IsHand == IsField; } }

        /// <summary>
        /// カード設定
        /// </summary>
        /// <param name="image">画像イメージ</param>
        /// <param name="description">説明文</param>
        /// <param name="purchaseMoney">購入金額</param>
        public void Setup(CardMasterData data)
        {
            //PhotonView photonView = GetComponent<PhotonView>();
            //photonView.RPC("SyncSetup", PhotonTargets.AllBuffered, data);
            photonView.RPC("SyncSetup", PhotonTargets.All, data);
        }

        public void UpdateState(CardState state)
        {
            photonView.RPC("SyncState", PhotonTargets.All, state);
        }

		/// <summary>
		/// カード効果取得
		/// </summary>
		public abstract Effect GetEffect();

        // 各ドラッグ処理
        public void OnBeginDrag(PointerEventData pointerEventData)
        {
            if (IsScroll) GetParentScrollRect().OnBeginDrag(pointerEventData);

            if (IsCreateDrag)
            {
                CreateDragIcon();
                m_ShowCard.SetActive(false);
            }
        }
        public void OnDrag(PointerEventData pointerEventData)
        {
            if (IsScroll) GetParentScrollRect().OnDrag(pointerEventData);

            if (IsCreateDrag) m_DragIcon.transform.position = pointerEventData.position;
        }
        public void OnEndDrag(PointerEventData pointerEventData)
        {
            if (IsScroll) GetParentScrollRect().OnEndDrag(pointerEventData);

            if (IsHand)
            {
                CancelDrag();
            }
            else
            {
                m_ShowCard.SetActive(true);
                this.transform.localScale = m_dragIconScale;
            }

            Destroy(m_DragIcon);
        }

        private void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            Debug.Log("OnPhotonInstantiate : card");
        }

        public void SetupTransform()
        {
            var ownerId = photonView.ownerId;
            switch(m_state)
            {
                case CardState.HAND:
                    Debug.Log("HAND");
                    transform.SetParent(BattleSceneManager.SceneManager.PlayersTransform.Find(ownerId.ToString()+"/Hand/Content"));
                    break;
                case CardState.DECK:
                    Debug.Log("DECK");
                    transform.SetParent(BattleSceneManager.SceneManager.PlayersTransform.Find(ownerId.ToString() + "/Deck"));
                    break;
            }
            transform.localScale = Vector3.one;
        }

        // 2階層上のScrollRectを取得する
        private ScrollRect GetParentScrollRect()
        {
            if(m_HandScrollRect == null)
                m_HandScrollRect = this.transform.parent.parent.GetComponent<ScrollRect>();
            return m_HandScrollRect;
        }

        private void CreateDragIcon()
        {
            m_DragIcon = Instantiate(this.gameObject,GetParentScrollRect().transform.parent.parent);

            // アイコンでレイキャストがブロックされると正常にカード情報を取得できないため
            CanvasGroup canvasGroup = m_DragIcon.AddComponent<CanvasGroup>();
            canvasGroup.blocksRaycasts = false;

            m_DragIcon.transform.localScale = m_dragIconScale;
        }

        private void CancelDrag()
        {
            this.transform.localScale = Vector3.one;
            m_ShowCard.SetActive(true);
        }

        [PunRPC]
        public void SyncSetup(CardMasterData data)
        {
            Debug.Log("-------------  SyncSetup -------------");
            Data = data;
            m_CardDescription.text = Data.CardName;
            m_purchaseMoney = Data.CostCoin;
            m_CardCost.text = m_purchaseMoney.ToString();
        }

        [PunRPC]
        public void SyncState(CardState state)
        {
            Debug.Log("-------------  SyncState -------------");
            Debug.Log("view id : " + photonView.viewID );
            m_state = state;
            m_CardNum.SetActive(m_state.Equals(CardState.SUPPLY));
            SetupTransform();
        }


    }
} 