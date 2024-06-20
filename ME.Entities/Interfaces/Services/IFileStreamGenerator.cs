using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME.Entities.Interfaces.Services
{
    public interface IFileStreamGenerator : IDisposable
    {
        System.Data.Common.DbConnection? cnn { get; set; }

        string? ConnectionString { get; set; }

        void Open();

        public void Generate(ME.Entities.Generals.FileStreamGeneratorArguments arguments);

        public string ConvertEFConnectionString(string cs);

        void Close();
    }
}
