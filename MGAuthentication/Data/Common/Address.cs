using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MGAuthentication.Data.Common
{
    // Owned type is a type that does not have a id. search for owned type for the expalantion
    [Owned]
    public class Address : ValueObject
    {
        public string PostNo { get; private set; }
        public string Detail { get; private set; }

        private Address()
        {
        }

        public Address(string postNo, string detail)
        {
            PostNo = postNo;
            Detail = detail;
        }

        public Address(Address address)
        {
            PostNo = address.PostNo;
            Detail = address.Detail;
        }

        public override string ToString()
        {
            return $"{PostNo}, {Detail}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return PostNo;
            yield return Detail;
        }
    }
}