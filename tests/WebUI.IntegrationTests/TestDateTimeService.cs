using Golobal_IMC_Task.Application.Common.Interfaces;
using System;

namespace Golobal_IMC_Task.WebUI.IntegrationTests
{
    public class TestDateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
