using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System.IO;
using System;

public class DBTest : MonoBehaviour {
	public SqliteConnection conn = null;
	public Text text;
	string path;
	string dbpath;
	// Use this for initialization
	void Start () {
		//测试文件拷贝
		path  = Application.persistentDataPath + "/test.txt";
		TextAsset text = Resources.Load<TextAsset> ("test");
		File.WriteAllBytes (path, text.bytes);

		//拷贝数据库到沙盒区
		dbpath = Application.persistentDataPath + "/mydb.db";
		TextAsset dbText = Resources.Load<TextAsset> ("mydb");
		File.WriteAllBytes (dbpath, dbText.bytes);
	}
	
	void OnGUI()
	{
		if(GUILayout.Button("测试数据库"))
		{
			//测试文件读取
			StreamReader sr = new StreamReader (path);
			string str = sr.ReadToEnd ();
			Debug.Log (str);
			//数据库连接
			string url = "URI=file:" + dbpath;
			try
			{
				conn = new SqliteConnection (url);
				conn.Open ();
				SqliteCommand cmd = new SqliteCommand (conn);
				cmd.CommandText = "select * from usr;";
				SqliteDataReader reader = cmd.ExecuteReader ();
				while(reader.Read())
				{
					text.text += reader.GetString(0) +" "+reader.GetString(1) +"\n";
				}
				reader.Close();
			}
			catch (Exception e)
			{
				text.text = e.ToString();
			}
			finally{
				conn.Close ();
			}
		}
	}
}
