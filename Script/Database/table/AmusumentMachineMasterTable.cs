using UnityEngine;
using System.Text;

public class AmusumentMachineMasterData : AbstractData {
	public int machinenumber = 0;
	public string machineName = "";

	public override void DebugPrint() {
		Debug.Log("AmusumentMachineMasterData machinenumber=" + machinenumber + ", machineName=" + machineName);
	}
}

public class AmusumentMachineMasterTable : AbstractDbTable<AmusumentMachineMasterData> {
	private static readonly string COL_MACHINENUMBER = "machinenumber";
	private static readonly string COL_MACHINENAME = "machineName";

	public AmusumentMachineMasterTable(ref SqliteDatabase db) : base(ref db) {
	}

	protected override string TableName {
		get {
			return "AmusumentMachineMaster";
		}
	}

	public override void MargeData(ref SqliteDatabase oldDb) {
	}

	public override void Update(AmusumentMachineMasterData data) {
		if (data.machinenumber <= DbDefine.DB_INVALID_PRIMARY_ID) {
			return;
		}

		StringBuilder query = new StringBuilder();
		AmusumentMachineMasterData selectData = SelectFromPrimaryKey(data.machinenumber);
		if (selectData == null) {
			query.Append("INSERT INTO ");
			query.Append(TableName);
			query.Append(" VALUES(");
			query.Append(data.machinenumber);
			query.Append(",");
			query.Append("'");
			query.Append(data.machineName);
			query.Append("'");
			query.Append(");");
		} else {
			query.Append("UPDATE ");
			query.Append(TableName);
			query.Append(" SET ");
			query.Append(COL_MACHINENAME);
			query.Append("=");
			query.Append("'");
			query.Append(data.machineName);
			query.Append("'");
			query.Append(" WHERE ");
			query.Append(COL_MACHINENUMBER);
			query.Append("=");
			query.Append(data.machinenumber);
			query.Append(";");
		}
		mDb.ExecuteNonQuery(query.ToString());
	}

	protected override AmusumentMachineMasterData PutData(DataRow row) {
		AmusumentMachineMasterData data = new AmusumentMachineMasterData();
		data.machinenumber = GetIntValue(row, "machinenumber");
		data.machineName = GetStringValue(row, "machineName");
		return data;
	}
}
