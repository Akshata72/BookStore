using DatabaseLayer.Address;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AddressRL :IAddressRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectionString;

        public AddressRL(IConfiguration configuration)
        {
            sqlConnectionString = configuration.GetConnectionString("BookStore");
            connection.ConnectionString = sqlConnectionString;
        }

        public AddAddress AddAddress(int UserId, AddAddress addAddress)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Add_Address", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Address", addAddress.Address);
                cmd.Parameters.AddWithValue("@City", addAddress.City);
                cmd.Parameters.AddWithValue("@State", addAddress.State);
                cmd.Parameters.AddWithValue("@AdTypeId", addAddress.AdTypeId);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return addAddress;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AddressModel UpdateAddress(AddressModel addressModel, int UserId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Update_Address", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AddressId", addressModel.AddressId);
                cmd.Parameters.AddWithValue("@Address", addressModel.Address);
                cmd.Parameters.AddWithValue("@City", addressModel.City);
                cmd.Parameters.AddWithValue("@State", addressModel.State);
                cmd.Parameters.AddWithValue("@AdTypeId", addressModel.AdTypeId);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return addressModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string DeleteAddress(int addressId, int UserId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Delete_Address", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AddressId", addressId);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return "Address Deleted Successfully";
                }
                else
                {
                    return "Failed to Delete Address";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public AddressModel ReadData(AddressModel address, SqlDataReader rdr)
        {
            address.Address = Convert.ToString(rdr["Address"] == DBNull.Value ? default : rdr["Address"]);
            address.City = Convert.ToString(rdr["City"] == DBNull.Value ? default : rdr["City"]);
            address.State = Convert.ToString(rdr["State"] == DBNull.Value ? default : rdr["State"]);
            address.AdTypeId = Convert.ToInt32(rdr["AdTypeId"] == DBNull.Value ? default : rdr["AdTypeId"]);
            address.AddressId = Convert.ToInt32(rdr["AddressId"] == DBNull.Value ? default : rdr["AddressId"]);

            return address;
        }

        public List<AddressModel> GetAllAddresses(int UserId)
        {
            try
            {
                List<AddressModel> addressResponse = new List<AddressModel>();
                SqlCommand cmd = new SqlCommand("SP_Get_AllAddress", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);

                connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        AddressModel address = new AddressModel();
                        AddressModel temp;
                        temp = ReadData(address, rdr);
                        addressResponse.Add(temp);
                    }
                    connection.Close();
                    return addressResponse;
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
