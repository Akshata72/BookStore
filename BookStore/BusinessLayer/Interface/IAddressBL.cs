using DatabaseLayer.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAddressBL
    {
        public AddAddress AddAddress(int UserId, AddAddress addAddress);
        public AddressModel UpdateAddress(AddressModel addressModel, int UserId);
        public string DeleteAddress(int addressId, int UserId);
        List<AddressModel> GetAllAddresses(int UserId);
    }
}
