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

        public void OnDrag(PointerEventData pointerEventData)
        {
            var rect = this.transform.parent.parent.GetComponent<ScrollRect>();
            rect.OnDrag(pointerEventData);
        }

        public void OnBeginDrag(PointerEventData pointerEventData)
        {
            var rect = this.transform.parent.parent.GetComponent<ScrollRect>();
            rect.OnBeginDrag(pointerEventData);
        }

        public void OnEndDrag(PointerEventData pointerEventData)
        {
            var rect = this.transform.parent.parent.GetComponent<ScrollRect>();
            if (rect != null)
                rect.OnEndDrag(pointerEventData);
        }
    }
} 