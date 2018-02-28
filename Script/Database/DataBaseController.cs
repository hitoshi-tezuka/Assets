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
			MyDatabase.Init(this, this);
		}

		public void OnDatabaseInit() {
			// データベースの初期化の後に行いたい処理を実装する、例えばデータベース内の値を使った他の何かの初期化とか
		}

		private void Sample() {
			// 以下は実際に使用する時の使い方の例
			MyDatabase db = MyDatabase.Instance;

			// データベースのテーブルを取得
			DummyMasterTable dummyMasterTable = db.GetDummyMasterTable();
			// 1行分のデータを格納するクラス
			DummyMasterData dummyMasterData;

			// 主キーによるSelect
			dummyMasterData = dummyMasterTable.SelectFromPrimaryKey(1);

			// 1行分のデータのデバッグログ出力もできます
			dummyMasterData.DebugPrint();

			// 全行取得
			foreach (DummyMasterData data in dummyMasterTable.SelectAll()) {
				// ループ処理
				data.DebugPrint();
			}

			// InsertまたはUpdate
			// ※同一の主キーのデータがあればUpdate、無ければInsertとなる
			dummyMasterData.id = 1;
			dummyMasterData.dummyText = "hogehoge";
			dummyMasterData.dummyBool = false;
			dummyMasterTable.Update(dummyMasterData);

			// 主キーによるDelete
			dummyMasterTable.DeleteFromPrimaryKey(1);

			// 全行削除
			dummyMasterTable.DeleteAll();
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

		/// <summary>
		/// 店舗テーブルから全件取得
		/// </summary>
		/// <returns></returns>
		public List<ShopMasterData> SelectShopMaster()
		{
			// 以下は実際に使用する時の使い方の例
			MyDatabase db = MyDatabase.Instance;

			// データベースのテーブルを取得
			ShopMasterTable shopMasterTable = db.GetShopMasterTable();

			return shopMasterTable.SelectAll();
		}

		/// <summary>
		/// 店舗テーブルの登録／更新
		/// </summary>
		/// <param name="id">主キー</param>
		/// <param name="name">店舗名</param>
		/// <returns></returns>
		public bool InsertUpdateShopMaster(int id, string name)
		{
			// 以下は実際に使用する時の使い方の例
			MyDatabase db = MyDatabase.Instance;

			// データベースのテーブルを取得
			ShopMasterTable shopMasterTable = db.GetShopMasterTable();

			ShopMasterData shopMasterData = shopMasterTable.SelectFromPrimaryKey(id);

			// InsertまたはUpdate
			// ※同一の主キーのデータがあればUpdate、無ければInsertとなる
			shopMasterData.id = id;
			shopMasterData.ShopName = name;

			shopMasterTable.Update(shopMasterData);
			return true;
		}

		/// <summary>
		/// 収支テーブルから全件取得
		/// </summary>
		/// <returns></returns>
		public List<InvestmentTransactionData> SelectInvestmentTransaction()
		{
			// 以下は実際に使用する時の使い方の例
			MyDatabase db = MyDatabase.Instance;

			// データベースのテーブルを取得
			InvestmentTransactionTable investmentTransactionTable = db.GetInvestmentTransactionTable();

			return investmentTransactionTable.SelectAll();
		}

		/// <summary>
		/// 収支テーブルの登録／更新
		/// </summary>
		/// <param name="id"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool InsertUpdateInvestmentTransaction(int shopId, int machineId, int machineNumber, int investment, int collection)
		{
			// 以下は実際に使用する時の使い方の例
			MyDatabase db = MyDatabase.Instance;

			// データベースのテーブルを取得
			InvestmentTransactionTable investmentTransaction = db.GetInvestmentTransactionTable();
			// トランザクションのためinsertしか行わない。更新はしない
			InvestmentTransactionData investmentData = new InvestmentTransactionData();

			// InsertまたはUpdate
			// ※同一の主キーのデータがあればUpdate、無ければInsertとなる
			investmentData.shopid = shopId;
			investmentData.machineid = machineId;
			investmentData.machinenumber = machineNumber;
			investmentData.investment = investment;
			investmentData.collection = collection;
			investmentTransaction.Update(investmentData);
			return true;
		}

		/// <summary>
		/// マシン詳細テーブルの登録／更新
		/// </summary>
		/// <param name="id"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool InsertMachineReferenceTransaction(int shopId, int machineId , int rank, int gameNum)
		{
			// 以下は実際に使用する時の使い方の例
			MyDatabase db = MyDatabase.Instance;

			// データベースのテーブルを取得
			MachineReferenceTransactionTable MachineReferenceTransaction = db.GetMachineReferenceTransactionTable();
			// トランザクションのためinsertしか行わない。更新はしない
			MachineReferenceTransactionData MachineReferenceData = new MachineReferenceTransactionData();

			// InsertまたはUpdate
			// ※同一の主キーのデータがあればUpdate、無ければInsertとなる
			MachineReferenceData.id = shopId;
			MachineReferenceData.machineid = machineId;
			MachineReferenceData.rank = rank;
			MachineReferenceData.gamenum= gameNum;
			MachineReferenceTransaction.Update(MachineReferenceData);
			return true;
		}
	}
}