using BusinessLayer.Interface;
using DatabaseLayer.Address;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL :IAddressBL
    {
        IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public AddAddress AddAddress(int UserId, AddAddress addAddress)
        {
            try
            {
                return this.addressRL.AddAddress(UserId,addAddress);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public AddressModel UpdateAddress(AddressModel addressModel, int UserId)
        {
            try
            {
                return addressRL.UpdateAddress(addressModel, UserId);
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
                return addressRL.DeleteAddress(addressId, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AddressModel> GetAllAddresses(int UserId)
        {
            try
            {
                return addressRL.GetAllAddresses(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
