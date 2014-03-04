﻿using NUnit.Framework;

namespace Simple.Data.Mysql5.Test.Features
{
    [TestFixture]
    public class StoredProceduresTests
    {
        [Test]
        public void GetCustomersWithNoParameters()
        {
            var db = Database.Open();
            var list = db.GetAllCustomers().ToList();

            Assert.AreEqual(5, list.Count);
            Assert.AreEqual(1, list[0].CustomerId);
            Assert.AreEqual(2, list[1].CustomerId);
            Assert.AreEqual(3, list[2].CustomerId);
            Assert.AreEqual(4, list[3].CustomerId);
            Assert.AreEqual(5, list[4].CustomerId);
        }

        [Test]
        public void GetCustomerById()
        {
            var db = Database.Open();
            var customer = db.GetCustomerById(3).First();

            Assert.AreEqual(3, customer.CustomerId);
            Assert.AreEqual("Amy", customer.Name);
            Assert.AreEqual("3, Street", customer.Address);
        }

        [Test]
        public void GetCustomerByIdWithNamedParameter()
        {
            var db = Database.Open();
            var customer = db.GetCustomerById(id: 2).First();

            Assert.AreEqual(2, customer.CustomerId);
            Assert.AreEqual("Albert", customer.Name);
            Assert.AreEqual("2, Street", customer.Address);
        }

        [Test]
        public void GetCustomerByUnknownId()
        {
            var db = Database.Open();
            var customer = db.GetCustomerById(-1).FirstOrDefault();

            Assert.IsNull(customer);
        }

        [Test]
        public void GetCustomersByName()
        {
            var db = Database.Open();
            var list = db.GetCustomersByName("Amy").ToList();

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(3, list[0].CustomerId);
            Assert.AreEqual(5, list[1].CustomerId);
        }

        [Test]
        public void GetCustomersByNameAndNamedParameter()
        {
            var db = Database.Open();
            var list = db.GetCustomersByName(_name: "Amy").ToList();

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(3, list[0].CustomerId);
            Assert.AreEqual(5, list[1].CustomerId);
        }

        [Test]
        public void GetCountCustomersAsOutputParam()
        {
            var db = Database.Open();
            //NOTE Mysql does not support defaults on output parameters
            //  so you can't call just db.GetCountCustomersAsOutputParam();
            var result = db.GetCountCustomersAsOutputParam(0);

            Assert.AreEqual(5, result.OutputValues["answer"]);
        }

    }
}
