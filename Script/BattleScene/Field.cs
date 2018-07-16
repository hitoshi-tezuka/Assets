using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace BattleScene { 

    public class Field : MonoBehaviour,IDropHandler {

	    // Use this for initialization
	    void Start () {
		
	    }
	
	    // Update is called once per frame
	    void Update () {
		
	    }

        public void AddCard(Card card)
        {
            if (card.IsField || card.IsSupply) return;
            card.transform.SetParent(this.transform);
            card.transform.localPosition = Vector3.zero;
            card.UpdateState(Card.CardState.FIELD);
        }


        public void OnDrop(PointerEventData pointerEventData)
        {
            Card dragCard = pointerEventData.pointerDrag.GetComponent<Card>();
            if(dragCard!=null) AddCard(dragCard);
        }
    }
}