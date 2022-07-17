using DatabaseLayer.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        public AddAddress AddAddress(int UserId,AddAddress addAddress);
    }
}
