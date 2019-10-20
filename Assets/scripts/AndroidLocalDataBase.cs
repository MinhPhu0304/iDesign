using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//References
using Mono.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AndroidLocalDataBase : MonoBehaviour
{ 
    private string conn, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    private IDataReader reader;
    public InputField textInputUsername, textInputPassword, t_id;
    public Text dataUser;

    string DatabaseName = "users.s3db";
    // Start is called before the first frame update
    void Start()
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

        conn = "URI=file:" + filepath;

        Debug.Log("Stablishing connection to: " + conn);
        dbconn = new SqliteConnection(conn);
        dbconn.Open();

        string query;
        query = "CREATE TABLE user (ID INTEGER PRIMARY KEY  AUTOINCREMENT, Name varchar(100), Password varchar(200))";
        try
        {
            dbcmd = dbconn.CreateCommand(); // create empty command
            dbcmd.CommandText = query; // fill the command
            reader = dbcmd.ExecuteReader(); // execute command which returns a reader
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        //  reader_function();
    }
    //Insert
    public void signUpRountine()
    {
        InsertNewUsernameToDatabase(textInputUsername.text, textInputPassword.text);
    }

    //Insert To Database
    private void InsertNewUsernameToDatabase(string name, string password)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("insert into user (name, Password) values (\"{0}\",\"{1}\")", name, password);// table name
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
        Debug.Log("Insert Done  ");
        SceneManager.LoadScene("Menu");
        ReadAllUsernameRecord();
    }
    //Read All Data For To Database
    public void ReadAllUsernameRecord()
    {
        // int idreaders ;
        string Namereaders, Addressreaders;
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT  Name, Password " + "FROM user";// table name
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                Namereaders = reader.GetString(0);
                Addressreaders = reader.GetString(1);

                dataUser.text += Namereaders + " - " + Addressreaders + " ";
                Debug.Log(" name =" + Namereaders + "Password=" + Addressreaders);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }
    }
    //Search on Database by ID
    private void SearchUsernameByID(string userNameID)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            string usernameRead, passwordRead;
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT name,password " + "FROM user where id =" + userNameID;// table name
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                //  string id = reader.GetString(0);
                usernameRead = reader.GetString(0);
                passwordRead = reader.GetString(1);
                dataUser.text += usernameRead + " - " + passwordRead + "\n";

                Debug.Log(" name =" + usernameRead + "Address=" + passwordRead);

            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
        }

    }

    public void executeLogin()
    {
        string username = textInputUsername.text;
        string passwordInput = textInputPassword.text;

        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open();
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT name,password " + "FROM user where Name =\"" + username +"\"";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                string usernameRecord = reader.GetString(0);
                string passwordInRecord = reader.GetString(1);

                if (passwordInRecord.Equals(passwordInput))
                {
                    SceneManager.LoadScene("Menu");
                }
            }
        }
        showWrongCredentialMessage();
    }

    private void showWrongCredentialMessage()
    {
        textInputUsername.placeholder.GetComponent<Text>().text = "Wrong username or password";
        textInputUsername.text = "";
        textInputPassword.placeholder.GetComponent<Text>().text = "Wrong username or password";
        textInputPassword.text = "";
    }

    //Search on Database by ID
    private void F_to_update_function(string Search_by_id)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            string Name_readers_Search, Address_readers_Search;
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT name,address " + "FROM user where id =" + Search_by_id;// table name
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {

                Name_readers_Search = reader.GetString(0);
                Address_readers_Search = reader.GetString(1);
                textInputUsername.text = Name_readers_Search;
                textInputPassword.text = Address_readers_Search;

            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
        }
    }

    private void UpdateUserRecord(string update_id, string update_name, string update_address)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("UPDATE Staff set name = @name ,address = @address where ID = @id ");

            SqliteParameter P_update_name = new SqliteParameter("@name", update_name);
            SqliteParameter P_update_address = new SqliteParameter("@address", update_address);
            SqliteParameter P_update_id = new SqliteParameter("@id", update_id);

            dbcmd.Parameters.Add(P_update_name);
            dbcmd.Parameters.Add(P_update_address);
            dbcmd.Parameters.Add(P_update_id);

            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
            SearchUsernameByID(t_id.text);
        }

    }
}