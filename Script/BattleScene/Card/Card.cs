using UnityEngine;
using UnityEngine.UI;
using Database;
using UnityEngine.EventSystems;

namespace BattleScene { 
	public abstract class Card : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler{

		[SerializeField] private RawImage m_CardImage;
		[SerializeField] private Text m_CardDescription;


		private int m_purchaseMoney = 0;
		public int CostCoin { get { return m_Data.CostCoin; } }
        public int TreaserCoin { get { return m_Data.Treasure; } }
        public int PlusAction { get {return m_Data.PlusAction; } }
        public int PlusCoin { get { return m_Data.PlusCoin; } }
        public int PlusCard { get { return m_Data.PlusCard; } }
        public int PlusPurchase { get { return m_Data.PlusPurchase; } }
        public int PlusVictoryPointToken { get { return m_Data.PlusVictoryPointToken; } }

        private CardMasterData m_Data;
        private ScrollRect m_ScrollRect;

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


        // 操作処理
        public void OnDrag(PointerEventData pointerEventData)
        {
            GetParentScrollRect().OnDrag(pointerEventData);
        }

        public void OnBeginDrag(PointerEventData pointerEventData)
        {
            GetParentScrollRect().OnBeginDrag(pointerEventData);
        }

        public void OnEndDrag(PointerEventData pointerEventData)
        {
            
            if (GetParentScrollRect() != null)
                GetParentScrollRect().OnEndDrag(pointerEventData);
        }


        // 親階層のScrollRectを取得する
        private ScrollRect GetParentScrollRect()
        {
            // 2階層上のScrollRectを取得
            if(m_ScrollRect == null)
                m_ScrollRect = this.transform.parent.parent.GetComponent<ScrollRect>();
            return m_ScrollRect;
        }
    }
} 