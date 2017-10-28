using UnityEngine;
using System.Text;

public class MachineReferenceTransactionData : AbstractData {
	public int id = 0;
	public int machineid = 0;
	public int machineidref = 0;
	public int rank = 0;
	public int gamenum = 0;

	public override void DebugPrint() {
		Debug.Log("MachineReferenceTransactionData id=" + id + ", machineid=" + machineid + ", machineidref=" + machineidref + ", rank=" + rank + ", gamenum=" + gamenum);
	}
}

public class MachineReferenceTransactionTable : AbstractDbTable<MachineReferenceTransactionData> {
	private static readonly string COL_ID = "id";
	private static readonly string COL_MACHINEID = "machineid";
	private static readonly string COL_MACHINEIDREF = "machineidref";
	private static readonly string COL_RANK = "rank";
	private static readonly string COL_GAMENUM = "gamenum";

	public MachineReferenceTransactionTable(ref SqliteDatabase db) : base(ref db) {
	}

	protected override string TableName {
		get {
			return "MachineReferenceTransaction";
		}
	}

	public override void MargeData(ref SqliteDatabase oldDb) {
	}

	public override void Update(MachineReferenceTransactionData data) {
		if (data.id <= DbDefine.DB_INVALID_PRIMARY_ID) {
			return;
		}

		StringBuilder query = new StringBuilder();
		MachineReferenceTransactionData selectData = SelectFromPrimaryKey(data.id);

		query.Append("INSERT INTO ");
		query.Append(TableName);
		query.Append(" VALUES(");
		query.Append(data.id);
		query.Append(",");
		query.Append(data.machineid);
		query.Append(",");
		query.Append("null");
		query.Append(",");
		query.Append(data.rank);
		query.Append(",");
		query.Append(data.gamenum);
		query.Append(");");

		mDb.ExecuteNonQuery(query.ToString());
	}

	protected override MachineReferenceTransactionData PutData(DataRow row) {
		MachineReferenceTransactionData data = new MachineReferenceTransactionData();
		data.id = GetIntValue(row, "id");
		data.machineid = GetIntValue(row, "machineid");
		data.machineidref = GetIntValue(row, "machineidref");
		data.rank = GetIntValue(row, "rank");
		data.gamenum = GetIntValue(row, "gamenum");
		return data;
	}
}
