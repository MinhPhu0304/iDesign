using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Data;
using System.IO;

public class ItemManager : MonoBehaviour
{
    private static ItemManager _instance;
    private string dbURL, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    private IDataReader reader;
    public GameObject ObjectToPlace;
    public List<GameObject> selectableModels;
    public List<Item> selectableItems;
    
    public static ItemManager Instance;
    private List<Item> itemList;
    private string[] dbReadOrder = { "Name", "url", "desc", "categories", "brands", "designer", "spec" };

    public void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    string DatabaseName = "users.s3db";
    // Start is called before the first frame update
    public void Start()
    {
        //Application database Path android
        string filepath = Application.persistentDataPath + "/" + DatabaseName;
        if (!File.Exists(filepath))
        {
            // If not found on android will create Tables and database

            Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" +
                             Application.dataPath + "!/assets/users");

            // UNITY_ANDROID
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/users.s3db");
            while (!loadDB.isDone) { }
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDB.bytes);
        }

        dbURL = "URI=file:" + filepath;

        Debug.Log("Stablishing connection to: " + dbURL);
        dbconn = new SqliteConnection(dbURL);
        dbconn.Open();

        string query;
        query = "CREATE TABLE IF NOT EXISTS item (" +
                                   "ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                   "Name varchar(100), " +
                                   "price REAL," +
                                   "url varchar(30)," +
                                   "desc varchar(200)," +
                                   "categories varchar(200)," +
                                   "brands varchar(100)," +
                                   "designer varchar(50)," +
                                   "spec varchar(100));";
        try
        {
            dbcmd = dbconn.CreateCommand(); // create empty command
            dbcmd.CommandText = query;
            dbcmd.ExecuteReader(); // execute command which returns a reader
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        ReadRecordFromDB();
    }

    private void ReadRecordFromDB()
    {
        int totalRecordInDB = CountTotalRecordInDB();
        if (totalRecordInDB > 0)
        {
            LoadItemFromDBIntoList();
        }
        else
        {
            PopulateDemoData();
        }

    }

    private int CountTotalRecordInDB()
    {
        int totalRecordInDB = 0;
        using (dbconn = new SqliteConnection(dbURL))
        {
            dbconn.Open();
            IDbCommand dbCommand = dbconn.CreateCommand();
            dbCommand.CommandText = "Select COUNT(*) Total from item;";
            IDataReader dbRecord = dbCommand.ExecuteReader();
            if(dbRecord.Read()) totalRecordInDB = dbRecord.GetInt32(0);
        }

        return totalRecordInDB;
    }

    private void LoadItemFromDBIntoList()
    {

    }

    private void LoadItemIntoList(IDataReader dbRecord)
    {
        while (dbRecord.Read())
        {

        }
    }

    private void PopulateDemoData()
    {
        List<Item> demoData = new List<Item>();
        demoData.Add(new Item(1, "Chair", 50.00f, "https://google.com", "You sit on it or can be used for fighting"));
        demoData.Add(new Item(2, "Table", 50.00f, "https://google.com", "Good stuff"));
        demoData.Add(new Item(3, "Andy", 50.00f, "https://google.com", "Just for fun"));
        demoData.Add(new Item(4, "Couch", 50.00f, "https://google.com", "Cautions: heavy stuff"));

        itemList = demoData;
        AddDataIntoDB();
    }

    private void AddDataIntoDB()
    {
        using (dbconn = new SqliteConnection(dbURL))
        {
            dbconn.Open();
            IDbCommand dbCommand = dbconn.CreateCommand();
            foreach(Item item in itemList)
            {
                string itemName = item.GetName();
                string itemDesc = item.GetDesc();
                string sellerURL = item.GetURL();
                float price = item.GetPrice();
                int itemId = item.GetItemID();
                string insertRecord = string.Format("INSERT into item (ID, Name, price, url, desc, categories, brands, designer, spec)" +
                                                        "values (\"{0}\",\"{1}\",\"{2}\", \"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\")",
                                                        itemId,
                                                        itemName,
                                                        price,
                                                        sellerURL,
                                                        itemDesc,
                                                        "",
                                                        "",
                                                        "",
                                                        "");
                dbCommand.CommandText = insertRecord;
                dbCommand.ExecuteScalar();
            }
        }
        Debug.Log("Done populating item data to database");
    }

    //https://www.youtube.com/watch?v=5p2JlI7PV1w
    //https://www.youtube.com/watch?v=AvuuX4qxC_0
}
