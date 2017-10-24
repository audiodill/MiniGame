using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project.Web.DAL
{
    public class StoreSQL_DAL : IStoreDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DoomSlayer"].ConnectionString;
        private const string SQL_GetStoreInventory = "SELECT * FROM item";
        private const string SQL_SortInventory = "SELECT * FROM item WHERE type = @category_id";
        private const string SQL_GetPlayerInventory = "SELECT * FROM player join item_player on item_player.player_id = player.player_id join item on item_player.item_id = item.item_id WHERE player.player_id = @player_id;";
        private const string SQL_SortPlayerInventory = "SELECT * FROM item join item_player on item_player.item_id = item.item_id join player on player.player_id = item_player.player_id WHERE item.type = @category_id and player.player_id = @player_id;";
        public List<ItemModel> GetStoreInventory(int sort, int inventoryId)
        {
            List<ItemModel> output = new List<ItemModel>();
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd;
                    if(sort == 0)
                    {
                        if(inventoryId == 0)
                        {
                            cmd = new SqlCommand(SQL_GetStoreInventory, conn);
                        }
                        else
                        {
                            cmd = new SqlCommand(SQL_GetPlayerInventory, conn);
                            cmd.Parameters.AddWithValue("player_id", inventoryId);
                        }
                        
                    }
                    else
                    {
                        if(inventoryId == 0)
                        {
                            cmd = new SqlCommand(SQL_SortInventory, conn);
                            cmd.Parameters.AddWithValue("@category_id", sort);
                        }
                        else
                        {
                            cmd = new SqlCommand(SQL_SortPlayerInventory, conn);
                            cmd.Parameters.AddWithValue("@player_id", sort);
                        }
                        
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ItemModel s = new ItemModel();

                        s.Name = Convert.ToString(reader["name"]);
                        s.Price = Convert.ToInt32(reader["price"]);
                        s.Weight = Convert.ToDecimal(reader["weight"]);
                        s.Description = Convert.ToString(reader["description"]);
                        s.ImageName = Convert.ToString(reader["image"]);

                        output.Add(s);
                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return output;
        }
    }
}