using UnityEngine;
using System;
using System.Text;

namespace Database
{ 
    public class CardMasterData : AbstractData
    {
        public string Card = string.Empty;
        public string ImageName = string.Empty;
        public string CardName = string.Empty;
        public int SetType = 0;
        public int CostCoin = 0;
        public int CostPotion = 0;
        public int CostLiabilities = 0;
        public int Classification = 0;
        public int CardType = 0;
        public int Treasure = 0;
        public int VictoryPoint = 0;
        public int PlusCard = 0;
        public int PlusAction = 0;
        public int PlusPurchase = 0;
        public int PlusCoin = 0;
        public int PlusVictoryPointToken = 0;


        public override void DebugPrint()
        {
            Debug.Log("CardMasterData Card =" + Card + ", CardName =" + CardName);
        }
    }

    public class CardMasterTable : AbstractDbTable<CardMasterData>
    {
        private const string COL_CARD = "Card";
        private const string COL_IMAGENAME = "ImageName";
        private const string COL_CARDNAME = "CardName";
        private const string COL_SETTYPE = "SetType";
        private const string COL_COSTCOIN = "CostCoin";
        private const string COL_COSTPOTISION = "CostPotion";
        private const string COL_COSTLIABILITIES = "CostLiabilities";
        private const string COL_CLASSIFICATION = "Classification ";
        private const string COL_CARDTYPE = "CardType";
        private const string COL_TREASURE = "Treasure";
        private const string COL_VICTORYPOINT = "VictoryPoint";
        private const string COL_PLUSCARD = "PlusCard";
        private const string COL_PLUSACTION = "PlusAction";
        private const string COL_PLUSPURCHASE = "PlusPurchase";
        private const string COL_PLUSCOIN = "PlusCoin";
        private const string COL_PLUSVICTORYPOINTTOKEN = "PlusVictoryPointToken";

        protected override string[] PrimaryKeyName { get { string[] str = { "Card" }; return str; } }

        public CardMasterTable(ref SqliteDatabase db) : base(ref db)
        {
        }

        protected override string TableName {get {return "CardMaster";} }

        public override void MargeData(ref SqliteDatabase oldDb)
        {
        }

        public override void Update(CardMasterData data)
        {
        }

        protected override CardMasterData PutData(DataRow row)
        {
            CardMasterData card = new CardMasterData();
            card.Card = GetStringValue(row, COL_CARD);
            card.ImageName = GetStringValue(row, COL_IMAGENAME);
            card.CardName = GetStringValue(row, COL_CARDNAME);
            card.SetType = GetIntValue(row, COL_SETTYPE);
            card.CostCoin = GetIntValue(row, COL_COSTCOIN);
            card.CostPotion = GetIntValue(row, COL_COSTPOTISION);
            card.CostLiabilities = GetIntValue(row, COL_COSTLIABILITIES);
            card.Classification = GetIntValue(row, COL_CLASSIFICATION);
            card.CardType = GetIntValue(row, COL_CARDTYPE);
            card.Treasure = GetIntValue(row, COL_TREASURE);
            card.VictoryPoint = GetIntValue(row, COL_VICTORYPOINT);
            card.PlusCard = GetIntValue(row, COL_PLUSCARD);
            card.PlusAction = GetIntValue(row, COL_PLUSACTION);
            card.PlusPurchase = GetIntValue(row, COL_PLUSPURCHASE);
            card.PlusCoin = GetIntValue(row, COL_PLUSCOIN);
            card.PlusVictoryPointToken = GetIntValue(row, COL_PLUSVICTORYPOINTTOKEN);
            return card;
        }


    }

}