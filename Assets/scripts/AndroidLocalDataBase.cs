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
    public InputField textInputUsername, textInputPassword;
    private static readonly string DatabaseName = "users.s3db";

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
        CreateUserTableIfNotExist(dbconn);
        
    }

    private void CreateUserTableIfNotExist(IDbConnection dbConnection)
    {
        string query = "CREATE TABLE user (ID INTEGER PRIMARY KEY  AUTOINCREMENT, Name varchar(100), Password varchar(200))";
        try
        {
            dbcmd = dbconn.CreateCommand(); // create empty command
            dbcmd.CommandText = query; // fill the command
            reader = dbcmd.ExecuteReader(); // execute command which returns a reader
            dbcmd.Dispose();
            dbconn.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        finally
        {
            dbconn.Close();
        }
    }
    
    public void signUpRountine()
    {
        InsertNewUsernameToDatabase(textInputUsername.text, textInputPassword.text);
    }

    private void InsertNewUsernameToDatabase(string name, string password)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("insert into user (name, Password) values (\"{0}\",\"{1}\")", name, password);//yeah yeah, sql injection is real....bite me
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbcmd.Dispose();
        }
        SceneManager.LoadScene("Menu");
        ReadAllUsernameRecord();
    }

    /**
     * <summary>DO NOT USE THIS. ONLY USED FOR DEV ENVIRONMENT</summary>
     */
    public void ReadAllUsernameRecord()
    {
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
                Debug.Log(" name =" + Namereaders + "Password=" + Addressreaders);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
        }
    }

    public void executeLogin()
    {
        string username = textInputUsername.text;
        string passwordInput = textInputPassword.text;

        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open();
            string queryUserCommand = GetQueryUserCommand();
            SqliteParameter usernameSQLParam = new SqliteParameter("@name", username);
            IDbCommand dbcmd = dbconn.CreateCommand();
            dbcmd.Parameters.Add(usernameSQLParam);
            dbcmd.CommandText = queryUserCommand;
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

    private string GetQueryUserCommand()
    {
        return string.Format("SELECT name, password from user where name = \"@name\"");
    }

    private void showWrongCredentialMessage()
    {
        textInputUsername.placeholder.GetComponent<Text>().text = "Wrong username or password";
        textInputUsername.text = "";
        textInputPassword.placeholder.GetComponent<Text>().text = "Wrong username or password";
        textInputPassword.text = "";
    }
}