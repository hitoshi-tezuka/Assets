using UnityEngine;
using UnityEngine.UI;
using Database;
using UnityEngine.EventSystems;
using Coffee.UIExtensions;

namespace BattleScene { 
	public abstract class Card : Photon.MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler{

		[SerializeField] private RawImage m_CardImage;
		[SerializeField] private Text m_CardDescription;
        [SerializeField] private GameObject m_ShowCard;
        [SerializeField] private GameObject m_CardNum;
        [SerializeField] private Text m_CardSupply;
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

        public CardMasterData Data { get; private set; }
        public string CardId { get { return Data.Card; } }
        public int CostCoin { get { return Data.CostCoin; } }
        public int TreaserCoin { get { return Data.Treasure; } }
        public int PlusAction { get {return Data.PlusAction; } }
        public int PlusCoin { get { return Data.PlusCoin; } }
        public int PlusCard { get { return Data.PlusCard; } }
        public int PlusPurchase { get { return Data.PlusPurchase; } }
        public int PlusVictoryPointToken { get { return Data.PlusVictoryPointToken; } }
        public CardState State { get; private set; } = CardState.SUPPLY;
        public bool IsHand {get { return State.Equals(CardState.HAND); }}
        public bool IsSupply { get { return State.Equals(CardState.SUPPLY); } }
        public bool IsField { get { return State.Equals(CardState.FIELD); } }
        public bool IsDeck { get { return State.Equals(CardState.DECK); } }
        public bool IsDiscard { get { return State.Equals(CardState.DISCARD); } }
        public bool IsRevocation { get { return State.Equals(CardState.REVOCATION); } }
        private bool IsScroll { get { return IsHand || IsSupply || IsField; } }
        public int Supply { get { return m_Supply; } set { UpdateSupply(value); } }

        private Vector3 m_dragIconScale = new Vector3(0.7f, 0.7f, 0.7f);
        private ScrollRect m_HandScrollRect;
        private GameObject m_DragIcon;
        private int m_purchaseMoney = 0;
        private int m_Supply = 0;
        private UIShiny m_UiShiny;

        // ドラッグ作るのにphotonで同期するか考慮が必要
        private bool IsCreateDrag { get { return /* IsHand || IsField;*/ false; } }

        /// <summary>
        /// カード設定
        /// </summary>
        /// <param name="image">画像イメージ</param>
        /// <param name="description">説明文</param>
        /// <param name="purchaseMoney">購入金額</param>
        public void Setup(CardMasterData data)
        {
            Debug.Log("-------------  SyncSetup -------------");
            photonView.RPC("SyncSetup", PhotonTargets.AllBuffered, data);
        }

        /// <summary>
        /// カード状態
        /// </summary>
        /// <param name="state"></param>
        public void UpdateState(CardState state)
        {
            Debug.Log("-------------  SyncState -------------");
            photonView.RPC("SyncState", PhotonTargets.AllBuffered, state);
        }

        /// <summary>
        /// 残りカードのサプライを確認
        /// </summary>
        /// <param name="num"></param>
        private void UpdateSupply(int num)
        {
            Debug.Log("------------ SyncSupply ------------ " + num.ToString());
            if (!IsSupply) return;
            photonView.RPC("SyncSupply", PhotonTargets.AllBuffered, num);
        }

        /// <summary>
        /// カード効果取得
        /// </summary>
        public abstract Effect GetEffect();

        // 各ドラッグ処理
        public void OnBeginDrag(PointerEventData pointerEventData)
        {
            if (IsScroll) GetParentScrollRect()?.OnBeginDrag(pointerEventData);

            if (IsCreateDrag)
            {
                CreateDragIcon();
                m_ShowCard.SetActive(false);
            }
        }

        public void OnDrag(PointerEventData pointerEventData)
        {
            if (IsScroll) GetParentScrollRect()?.OnDrag(pointerEventData);

            if (IsCreateDrag) m_DragIcon.transform.position = pointerEventData.position;
        }

        public void OnEndDrag(PointerEventData pointerEventData)
        {
            if (IsScroll) GetParentScrollRect()?.OnEndDrag(pointerEventData);

            if (IsHand)
            {
                CancelDrag();
            }
            else
            {
                m_ShowCard.SetActive(true);
                m_HandScrollRect = null;    // 親スクロール情報をリセット
            }

            Destroy(m_DragIcon);
        }

        private void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            //Debug.Log("OnPhotonInstantiate : card");
        }

        public void SetupTransform()
        {
            var ownerId = photonView.owner.ID;
            switch(State)
            {
                case CardState.HAND:
                    transform.SetParent(BattleSceneManager.SceneManager.PlayersTransform.Find(ownerId.ToString()+"/Hand/Content"));
                    transform.localPosition = Vector3.zero;
                    transform.localScale = Vector3.one;
                    break;
                case CardState.DECK:
                    transform.SetParent(BattleSceneManager.SceneManager.PlayersTransform.Find(ownerId.ToString() + "/Deck"));
                    transform.localPosition = Vector3.zero;
                    transform.localScale = Vector3.one;
                    break;
                case CardState.FIELD:
                    transform.SetParent(BattleSceneManager.SceneManager.FieldTransform);
                    transform.localPosition = Vector3.zero;
                    transform.localScale = m_dragIconScale;
                    break;
                case CardState.SUPPLY:
                    transform.SetParent(BattleSceneManager.SceneManager.SupplyTransform);
                    transform.localPosition = Vector3.zero;
                    transform.localScale = Vector3.one;
                    break;
                case CardState.DISCARD:
                    transform.SetParent(BattleSceneManager.SceneManager.PlayersTransform.Find(ownerId.ToString() + "/DisCard"));
                    transform.localPosition = Vector3.zero;
                    transform.localScale = Vector3.one;
                    break;
            }
        }

        // 2階層上のScrollRectを取得する
        private ScrollRect GetParentScrollRect()
        {
            if (m_HandScrollRect == null)
                m_HandScrollRect = GetComponentInParent<ScrollRect>();
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
            Data = data;
            m_CardDescription.text = Data.CardName;
            m_purchaseMoney = Data.CostCoin;
            m_CardCost.text = m_purchaseMoney.ToString();
            m_UiShiny = m_ShowCard.GetComponent<UIShiny>();
        }

        [PunRPC]
        public void SyncState(CardState state)
        {
            State = state;
            m_CardNum.SetActive(State.Equals(CardState.SUPPLY));
            SetupTransform();
        }

        [PunRPC]
        public void SyncSupply(int num)
        {
            m_Supply = num;
            m_CardSupply.text = m_Supply.ToString();
        }

        public void OnClick()
        {
            if (m_UiShiny.play)
            {
                var player =  BattleSceneManager.SceneManager.NowPlayer;
                player.PurchaseCard(this);
                return;
            }
            if (IsSupply)
            {
                m_UiShiny.Play();
            }
        }


    }
} 