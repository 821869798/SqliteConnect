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
	public Button btn;
	// Use this for initialization
	void Start () {

        //拷贝数据库到沙盒区
        dbpath = FilePathHelper.Instance.GetPersistentDataPath("mydb.sqlite");
		TextAsset dbText = Resources.Load<TextAsset> ("mydb");
        string dir = Path.GetDirectoryName(dbpath);
        if(!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
		File.WriteAllBytes (dbpath, dbText.bytes);

		//添加事件
		btn.onClick.AddListener (DBConnect);
	}

	void DBConnect()
	{
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
            cmd.Dispose();
        }
		catch (Exception e)
		{
			text.text = e.ToString();
		}
		finally{
			conn.Close ();
		}
	}

    private void OnApplicationQuit()
    {
        if (conn == null) return;
        conn.Close();
        conn.Dispose();
    }
}
