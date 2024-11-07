using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Knonkaty
{
    static public class Model
    {
        static private SQLiteConnection db;
        static public string tableName = "Person";
        static public int limit = 2;

        static public void connect()
        {
            if (db != null)
                return;
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "bazaDanych.db");
            db = new SQLiteConnection(dbPath);
            db.Execute($"CREATE TABLE IF NOT EXISTS {tableName} (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, surname TEXT NOT NULL, gender TEXT NOT NULL, email TEXT NOT NULL, state TEXT NOT NULL, number INTEGER NOT NULL)");
        }
        static public List<Person> getPersons(int page, string searchBarText)
        {
            string where = "";

            if (searchBarText != null && searchBarText.Length != 0)
            {
                where = $"WHERE name LIKE '%{searchBarText}%' OR surname LIKE '%{searchBarText}%'";
            }

            var data = db.Query<Person>($"SELECT * FROM {tableName} {where} LIMIT {page * limit}, {limit}");

            return data.ToList<Person>();
        }
        static public int countPersons(string searchBarText)
        {
            string where = "";

            if (searchBarText != null && searchBarText.Length != 0)
            {
                where = $"WHERE name LIKE '%{searchBarText}%' OR surname LIKE '%{searchBarText}%'";
            }

            var data = db.ExecuteScalar<int>($"SELECT COUNT(*) FROM {tableName} {where}");

            return data;
        }
        static public void addPerson(string name, string surname, string gender, string email, string state, int number)
        {
            db.Execute($"INSERT INTO {tableName} (name, surname, gender, email, state, number) VALUES ('{name}', '{surname}', '{gender}', '{email}', '{state}', '{number}')");
        }
        static public void modifyPerson(int id, string name, string surname, string gender, string email, string state, int number)
        {
            db.Execute($"UPDATE {tableName} SET name='{name}', surname='{surname}', gender='{gender}', email='{email}', state='{state}', number='{number}' WHERE id={id}");
        }
        static public void removePerson(int id)
        {
            db.Execute($"DELETE FROM {tableName} WHERE id = {id}");
        }
        static public void addTable(string name)
        {
            db.Execute($"CREATE TABLE IF NOT EXISTS {name} (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, surname TEXT NOT NULL, gender TEXT NOT NULL, email TEXT NOT NULL, state TEXT NOT NULL, number INTEGER NOT NULL)");
            tableName = name;
        }
    }
}
