using Microsoft.VisualStudio.TestTools.UnitTesting;
using PassStorage3.DataCryptoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassStorage3.Tests.DataCryptoLayer
{
    [TestClass]
    public class CrossCryptoTests
    {
        public CrossCryptoTests()
        {

        }

        [DataTestMethod]
        [DataRow("HelloWorld", "Key1", "Key2")]
        public async Task Should_DecodeEncode_ReturnSameValue(string data, string key1, string key2)
        {
            
        }
    }
}
