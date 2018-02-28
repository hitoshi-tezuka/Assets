using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace BattleScene { 

    public class Stage : MonoBehaviour,IDropHandler {

        [SerializeField]
        private RectTransform m_Field;

	    // Use this for initialization
	    void Start () {
		
	    }
	
	    // Update is called once per frame
	    void Update () {
		
	    }

        public void AddCard(Card card)
        {
            card.transform.SetParent(m_Field);
            card.transform.localPosition = Vector3.zero;
        }


        public void OnDrop(PointerEventData pointerEventData)
        {
            Card dragCard = pointerEventData.pointerDrag.GetComponent<Card>();
            AddCard(dragCard);
        }
    }
}