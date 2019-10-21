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
    
    private List<Item> itemList = new List<Item>();
    private string[] dbReadOrder = { "Name", "url", "desc", "categories", "brands", "designer", "spec" };
    private static readonly string DatabaseName = "users.s3db"; // Do not change db or the consequences are bad

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

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void Start()
    {
        //Application database Path android
        string filepath = Application.persistentDataPath + "/" + DatabaseName;
        
        CreateDBFileIfNotExist(filepath);
        EstablishDBConnection(filepath);
        ReadRecordFromDB();
    }

    private void CreateDBFileIfNotExist(string location)
    {
        if (!File.Exists(location))
        {
            // If not found on android will create Tables and database

            Debug.LogWarning("File \"" + location + "\" does not exist. Attempting to create from \"" +
                             Application.dataPath + "!/assets/users");

            // UNITY_ANDROID
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/users.s3db");
            while (!loadDB.isDone) { }
            // then save to Application.persistentDataPath
            File.WriteAllBytes(location, loadDB.bytes);
        }
    }

    private void EstablishDBConnection(string filePath)
    {
        dbURL = "URI=file:" + filePath;

        Debug.Log("Stablishing connection to: " + dbURL);
        dbconn = new SqliteConnection(dbURL);
        dbconn.Open();

        string query = "CREATE TABLE IF NOT EXISTS item (" +
                                   "ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                   "Name varchar(100), " +
                                   "price REAL," +
                                   "url varchar(30)," +
                                   "desc varchar(200)," +
                                   "categories varchar(200)," +
                                   "brands varchar(100)," +
                                   "designer varchar(50)," +
                                   "spec varchar(100)," +
                                   "noClick INT);";
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
        dbconn.Close();
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
        using (dbconn = new SqliteConnection(dbURL))
        {
            dbconn.Open();
            IDbCommand dbQuery = dbconn.CreateCommand();
            dbQuery.CommandText = "Select * from item;";
            IDataReader dbRecord = dbQuery.ExecuteReader();
            LoadDBRecordFrom(dbRecord);
            dbQuery.Cancel();
        }
    }

    private void LoadDBRecordFrom(IDataReader dbRecord)
    {
        while (dbRecord.Read())
        {
            int itemId = dbRecord.GetInt32(0);
            string name = dbRecord.GetString(1);
            double price = dbRecord.GetFloat(2);
            string itemSiteURL = dbRecord.GetString(3);
            string itemDesc = dbRecord.GetString(4);
            int numberClick = dbRecord.GetInt32(9);
            itemList.Add(new Item(itemId, name, (float)price, itemSiteURL, itemDesc, numberClick));
        }
        dbRecord.Close();
    }

    private void PopulateDemoData()
    {
        itemList.Add(new Item(1, "Chair", 30.00f, "https://www.trademe.co.nz/business-farming-industry/office-furniture/desk-chairs/listing-2356237609.htm?rsqid=3512401f2c8a4cffad08f4acf7c7ab30-001", "Adjustable seat height Height adjustable back/lumbar\n Independently adjustable seat tilt - free floating or lockable"));
        itemList.Add(new Item(2, "Table", 60.00f, "https://www.trademe.co.nz/business-farming-industry/office-furniture/desk-chairs/listing-2357653157.htm?rsqid=148a18ec29374beeafeeab8f14940dcc-001", "Good stuff"));
        itemList.Add(new Item(3, "Couch", 90.00f, "https://google.com", "Cautions: heavy stuff"));
        AddDemoDataToDB();
    }

    private void AddDemoDataToDB()
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
                int noClick = item.getNumberOfClick();
                string insertRecord = string.Format("INSERT into item (ID, Name, price, url, desc, categories, brands, designer, spec, noClick)" +
                                                        "values (\"{0}\",\"{1}\",\"{2}\", \"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\", \"{9}\")",
                                                        itemId,
                                                        itemName,
                                                        price,
                                                        sellerURL,
                                                        itemDesc,
                                                        "",
                                                        "",
                                                        "",
                                                        "",
                                                        noClick);
                dbCommand.CommandText = insertRecord;
                dbCommand.ExecuteScalar();
            }
            dbCommand.Cancel();
        }
    }
    //https://www.youtube.com/watch?v=5p2JlI7PV1w
    //https://www.youtube.com/watch?v=AvuuX4qxC_0
}
