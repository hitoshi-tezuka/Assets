using UnityEngine;
using System;
using System.Text;

namespace Database
{ 
    [Serializable]
    public class CardMasterData : AbstractData
    {

        public enum CardType
        {
            TREASURE = 10,
            VICTORYPOINT = 20,
            CURSE = 30,
            ACTION = 40,
            ATTACKACTION = 41,
            REACTION = 42
        }

        public string Card = string.Empty;
        public string ImageName = string.Empty;
        public string CardName = string.Empty;
        public int SetType = 0;
        public int CostCoin = 0;
        public int CostPotion = 0;
        public int CostLiabilities = 0;
        public int Classification = 0;
        public CardType CardTypes = 0;
        public int Treasure = 0;
        public int VictoryPoint = 0;
        public int PlusCard = 0;
        public int PlusAction = 0;
        public int PlusPurchase = 0;
        public int PlusCoin = 0;
        public int PlusVictoryPointToken = 0;
        public string CardDescription = string.Empty;

        public override void DebugPrint()
        {
            Debug.Log("CardMasterData Card =" + Card + ", CardName =" + CardName);
        }
    }

    public class CardMasterTable : AbstractDbTable<CardMasterData>
    {
        private const string COL_CARD = "Card";                                     // カード名(英語）（主キー）
        private const string COL_IMAGENAME = "ImageName";                           // カード画像名
        private const string COL_CARDNAME = "CardName";                             // カード名
        private const string COL_SETTYPE = "SetType";                               // セット
        private const string COL_COSTCOIN = "CostCoin";                             // コストコイン
        private const string COL_COSTPOTISION = "CostPotion";                       // コストポーション
        private const string COL_COSTLIABILITIES = "CostLiabilities";               // コスト負債
        private const string COL_CLASSIFICATION = "Classification ";                // 分類
        private const string COL_CARDTYPE = "CardType";                             // カード種類
        private const string COL_TREASURE = "Treasure";                             // 財宝      
        private const string COL_VICTORYPOINT = "VictoryPoint";                     // 勝利点
        private const string COL_PLUSCARD = "PlusCard";                             // ＋カード
        private const string COL_PLUSACTION = "PlusAction";                         // ＋アクション
        private const string COL_PLUSPURCHASE = "PlusPurchase";                     // ＋購入
        private const string COL_PLUSCOIN = "PlusCoin";                             // ＋コイン
        private const string COL_PLUSVICTORYPOINTTOKEN = "PlusVictoryPointToken";   // ＋勝利点トークン
        private const string COL_DESCRIPTION = "CardDescription";                   // カード説明

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
            card.Treasure = GetIntValue(row, COL_TREASURE);
            card.VictoryPoint = GetIntValue(row, COL_VICTORYPOINT);
            card.PlusCard = GetIntValue(row, COL_PLUSCARD);
            card.PlusAction = GetIntValue(row, COL_PLUSACTION);
            card.PlusPurchase = GetIntValue(row, COL_PLUSPURCHASE);
            card.PlusCoin = GetIntValue(row, COL_PLUSCOIN);
            card.PlusVictoryPointToken = GetIntValue(row, COL_PLUSVICTORYPOINTTOKEN);
            card.CardDescription = GetStringValue(row, COL_DESCRIPTION);
            return card;
        }
    }

}