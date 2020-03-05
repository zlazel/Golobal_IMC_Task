using Golobal_IMC_Task.Application.Common.Interfaces;
using System;

namespace Golobal_IMC_Task.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
