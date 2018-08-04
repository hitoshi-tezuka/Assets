using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Database
{ 
	public class DataBaseController : MonoBehaviour, MyDatabaseListener {

		// Use this for initialization
		void Awake () {
            // データベース初期化、ゲーム起動時などで1回だけ実行すればOK
            MyDatabase.Init(this,this);
        }

        public void OnDatabaseInit() {
            // データベースの初期化の後に行いたい処理を実装する、例えばデータベース内の値を使った他の何かの初期化とか
        }

		/// <summary>
		/// カードマスタテーブルから1件取得
		/// </summary>
		/// <param name="id">id</param>
		/// <returns></returns>
		public CardMasterData SelectCardMaster(string id)
		{
			// 以下は実際に使用する時の使い方の例
			MyDatabase db = MyDatabase.Instance;

			// データベースのテーブルを取得
			CardMasterTable cardMasterTable = db.GetCardMasterTable();

			return cardMasterTable.SelectFromPrimaryKey(id);
		}

		/// <summary>
		/// カードマスタテーブルから全件取得
		/// </summary>
		/// <param name="id">shop id</param>
		/// <returns></returns>
		public List<CardMasterData> SelectCardMaster()
		{
			// 以下は実際に使用する時の使い方の例
			MyDatabase db = MyDatabase.Instance;

			// データベースのテーブルを取得
			CardMasterTable cardMasterTable = db.GetCardMasterTable();

			return cardMasterTable.SelectAll();
		}


	}
}