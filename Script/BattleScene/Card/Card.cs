using UnityEngine;
using UnityEngine.UI;
using Database;
using UnityEngine.EventSystems;

namespace BattleScene { 
	public abstract class Card : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler{

		[SerializeField] private RawImage m_CardImage;
		[SerializeField] private Text m_CardDescription;
        [SerializeField] private GameObject m_ShowCard;


		private int m_purchaseMoney = 0;
		public int CostCoin { get { return m_Data.CostCoin; } }
        public int TreaserCoin { get { return m_Data.Treasure; } }
        public int PlusAction { get {return m_Data.PlusAction; } }
        public int PlusCoin { get { return m_Data.PlusCoin; } }
        public int PlusCard { get { return m_Data.PlusCard; } }
        public int PlusPurchase { get { return m_Data.PlusPurchase; } }
        public int PlusVictoryPointToken { get { return m_Data.PlusVictoryPointToken; } }

        private Vector3 m_dragIconScale = new Vector3(0.7f, 0.7f, 0.7f);
        private CardMasterData m_Data;
        private ScrollRect m_ScrollRect;
        private GameObject m_DragIcon;

		/// <summary>
		/// カード設定
		/// </summary>
		/// <param name="image">画像イメージ</param>
		/// <param name="description">説明文</param>
		/// <param name="purchaseMoney">購入金額</param>
		public void Setup(CardMasterData data)
		{
            m_Data = data;
            m_CardDescription.text = m_Data.CardName;
			m_purchaseMoney = m_Data.CostCoin;
        }

		/// <summary>
		/// カード効果取得
		/// </summary>
		public abstract Effect GetEffect();

        // 各ドラッグ処理
        public void OnBeginDrag(PointerEventData pointerEventData)
        {
            CreateDragIcon();
            m_ShowCard.SetActive(false);
            GetParentScrollRect().OnBeginDrag(pointerEventData);
        }
        public void OnDrag(PointerEventData pointerEventData)
        {
            GetParentScrollRect().OnDrag(pointerEventData);
            m_DragIcon.transform.position = pointerEventData.position;
        }
        public void OnEndDrag(PointerEventData pointerEventData)
        {
            // ドラッグ終了時、場に出していない場合は前の状態に戻す
            if (GetParentScrollRect() != null)
            { 
                GetParentScrollRect().OnEndDrag(pointerEventData);
                CancelDrag();
            }
            Destroy(m_DragIcon);
        }

        // 2階層上のScrollRectを取得する
        private ScrollRect GetParentScrollRect()
        {
            if(m_ScrollRect == null)
                m_ScrollRect = this.transform.parent.parent.GetComponent<ScrollRect>();
            return m_ScrollRect;
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
    }
} 