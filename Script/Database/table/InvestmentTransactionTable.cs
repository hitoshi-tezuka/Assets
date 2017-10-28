using UnityEngine;
using System.Text;

public class InvestmentTransactionData : AbstractData {
	public int id = 0;
	public int shopid = 0;
	public int machineid = 0;
	public int machinenumber = 0;
	public int investment = 0;
	public int collection = 0;


	public override void DebugPrint() {
		Debug.Log("InvestmentTransactionData id=" + id  + "shopid = " + shopid + ", machineid=" + machineid + ", machinenumber=" + machinenumber + ", investment=" + investment + ", collection=" + collection);
	}

}

public class InvestmentTransactionTable : AbstractDbTable<InvestmentTransactionData> {
	private static readonly string COL_ID = "id";
	private static readonly string COL_SHOPID = "shopid";
	private static readonly string COL_MACHINEID = "machineid";
	private static readonly string COL_MACHINENUMBER = "machinenumber";
	private static readonly string COL_INVESTMENT = "investment";
	private static readonly string COL_COLLECTION = "collection";

	protected override string[] PrimaryKeyName { get { string[] str = { COL_ID}; return str; } }
	
	public InvestmentTransactionTable(ref SqliteDatabase db) : base(ref db) {
	}

	protected override string TableName {
		get {
			return "InvestmentTransaction";
		}
	}

	public override void MargeData(ref SqliteDatabase oldDb) {
	}

	public override void Update(InvestmentTransactionData data) {
		if (data.shopid <= DbDefine.DB_INVALID_PRIMARY_ID) {
			return;
		}

		StringBuilder query = new StringBuilder();

		query.Append("INSERT INTO ");
		query.Append(TableName);
		query.Append(" VALUES(");
		query.Append("null ,");			// id‚ðŽw’è
		query.Append(data.shopid);
		query.Append(",");
		query.Append(data.machineid);
		query.Append(","); 
		query.Append(data.machinenumber);
		query.Append(",");
		query.Append(data.investment);
		query.Append(",");
		query.Append(data.collection);
		query.Append(");");
		mDb.ExecuteNonQuery(query.ToString());

		Debug.Log(query.ToString());
		Debug.Log("Insert Success");
		
	}

	protected override InvestmentTransactionData PutData(DataRow row) {
		InvestmentTransactionData data = new InvestmentTransactionData();
		data.id = GetIntValue(row, "id");
		data.shopid = GetIntValue(row, "shopid");
		data.machineid = GetIntValue(row, "machineid");
		data.machinenumber = GetIntValue(row, "machinenumber");
		data.investment = GetIntValue(row, "investment");
		data.collection = GetIntValue(row, "collection");
		return data;
	}
}
