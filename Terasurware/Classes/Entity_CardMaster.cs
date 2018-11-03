using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BattleScene;

public class Entity_CardMaster : ScriptableObject
{
	public List<CardMasterData> Card = new List<CardMasterData> ();

	[System.SerializableAttribute]
	public class CardMasterData
    {
        public string Card;
		public string ImageName;
		public string CardName;
		public int SetType;
		public int CostCoin;
		public int CostPotion;
		public int CostLiabilities;
		public int Classification;
		public Card.CardType CardType;
		public int Treasure;
		public int VictoryPoint;
		public int PlusCard;
		public int PlusAction;
		public int PlusPurchase;
		public int PlusCoin;
		public int PlusVictoryPointToken;
		public string CardDescription;
	}
}

