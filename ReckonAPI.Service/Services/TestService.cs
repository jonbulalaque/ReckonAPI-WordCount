using System;
using System.Collections.Generic;
using System.Text;
using ReckonAPI.Service.Interfaces;
namespace ReckonAPI.Service.Services
{
    public class TestService : ITestService
    {
        public string TestMessage()
        {
            return "Test Message";
        }
    }
}
