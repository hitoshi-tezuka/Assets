using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class CardData_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Terasurware/CreateExcelData/CardData.xlsx";
	private static readonly string exportPath = "Assets/Terasurware/CreateExcelData/CardData.asset";
	private static readonly string[] sheetNames = { "CardMaster", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			Entity_CardMaster data = (Entity_CardMaster)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Entity_CardMaster));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Entity_CardMaster> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						Entity_CardMaster.CardMasterData p = new Entity_CardMaster.CardMasterData();
						
					cell = row.GetCell(0); p.Card = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(1); p.ImageName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.CardName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.SetType = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.CostCoin = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.CostPotion = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.CostLiabilities = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.Classification = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.CardType = (BattleScene.Card.CardType)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.Treasure = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(10); p.VictoryPoint = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(11); p.PlusCard = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(12); p.PlusAction = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(13); p.PlusPurchase = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(14); p.PlusCoin = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(15); p.PlusVictoryPointToken = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(16); p.CardDescription = (cell == null ? "" : cell.StringCellValue);
						data.Card.Add (p);
					}
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
