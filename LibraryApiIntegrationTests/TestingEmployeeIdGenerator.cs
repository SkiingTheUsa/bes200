using LibraryApi.Services;
using System;

namespace LibraryApiIntegrationTests
{
    public class TestingEmployeeIdGenerator : IGenerateEmployeeIds
    {
        public Guid GetNewEmployeeId()
        {
            return new Guid();
        }
    }
}
