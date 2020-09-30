using MySql.Data.MySqlClient;
using SugoiAirServer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SugoiAirServer.Querys
{
    public class UserQuery
    {
        public AppDb Db { get; }

        public UserQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<List<User>> ReadAllAsync()
        {
            var users = new List<User>();
            using var cmd = Db.Connection.CreateCommand();

            cmd.CommandText = @"SELECT u_no
                                        ,u_id
                                        ,u_password
                                        ,u_name
                                FROM tbl_users;";

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var user = new User(Db)
                    {
                        uNo = reader.GetInt32(0),
                        uId = reader.GetString(1),
                        uName = reader.GetString(2),
                        uPassword = reader.GetString(3),
                        
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        public async Task<User> FindOneAsync(string uId)
        {
            User user = null;
            using var cmd = Db.Connection.CreateCommand();

            cmd.CommandText = @"SELECT u_no
                                        ,u_id
                                        ,u_password
                                        ,u_name
                                FROM tbl_users
                                WHERE u_id = @uId;";

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@uId",
                DbType = DbType.String,
                Value = uId,
            });

            try { 
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    
                    user = new User(Db)
                    {
                        uNo = reader.GetInt32(0),
                        uId = reader.GetString(1),
                        uName = reader.GetString(2),
                        uPassword = reader.GetString(3),

                    };
                }
            }
            }
            catch (Exception e){
                Console.WriteLine(e.StackTrace);
            }
            return user;
        }

        //public async Task InsertAsync()
        //{
        //    using var cmd = Db.Connection.CreateCommand();
        //    cmd.CommandText = @"INSERT INTO `BlogPost` (`Title`, `Content`) VALUES (@title, @content);";
        //    BindParams(cmd);
        //    await cmd.ExecuteNonQueryAsync();
        //    Id = (int)cmd.LastInsertedId;
        //}

        //public async Task UpdateAsync()
        //{
        //    using var cmd = Db.Connection.CreateCommand();
        //    cmd.CommandText = @"UPDATE `BlogPost` SET `Title` = @title, `Content` = @content WHERE `Id` = @id;";
        //    BindParams(cmd);
        //    BindId(cmd);
        //    await cmd.ExecuteNonQueryAsync();
        //}
    }
}
