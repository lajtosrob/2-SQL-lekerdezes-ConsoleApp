using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; // nuget MySQL.DATA telepítés szükséges
using MySql.Data;

string kapcsolatleiro = "datasource=127.0.0.1;port=3306;database=hardver;username=root;password=;";

MySqlConnection SQLkapcsolat = new MySqlConnection(kapcsolatleiro);
try
{
	SQLkapcsolat.Open();
}
catch (MySqlException hiba)
{
	Console.WriteLine(hiba.Message);
	Environment.Exit(1);
}

//Kategóriák listázása a kategóriában előforduló termékfajták számával 
// csak a legalább 100 termékfajtát tartalmazó kategória szerepel
//termékfajták száma szerint csökkenő sorrendben. 

string SQLselect =
	"SELECT Kategória, COUNT(Kategória) as Darab FROM termékek " +
	"GROUP BY 'Kategória' " +
	"HAVING Darab>=100 " +
	"ORDER BY Darab DESC";

MySqlCommand SQLparancs = new MySqlCommand(SQLselect, SQLkapcsolat);
MySqlDataReader eredmenyOlvaso = SQLparancs.ExecuteReader();
while (eredmenyOlvaso.Read())
{
	//Itt már két mező van. Megj. A második egy alias
	Console.Write(eredmenyOlvaso.GetString("Kategória") + " - ");
	Console.WriteLine(Convert.ToInt32(eredmenyOlvaso.GetString("Darab")));
}
eredmenyOlvaso.Close();
SQLkapcsolat.Close();
