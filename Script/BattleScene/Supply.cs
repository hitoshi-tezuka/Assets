using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScene
{ 
    public class Supply : MonoBehaviour {

        [SerializeField]
        private ScrollRect m_SupplyScroll;
        private List<Card> m_Card = new List<Card>();

        public void AddSupply(Card card)
        {
            card.UpdateState(Card.CardState.SUPPLY);
            card.Supply = 10;
            m_Card.Add(card);
        }

        public Card GetSupplyCard(string id)
        {
            return m_Card.Find(x => x.CardId == id);
        }
    }
}