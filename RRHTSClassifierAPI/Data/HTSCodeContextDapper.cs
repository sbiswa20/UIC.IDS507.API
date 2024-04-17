using System.Data;
using System.Security.Cryptography;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using RRHTSCLASSIFIERAPI.Models;
using RRHTSCLASSIFIERAPI.SQL;
using RRHTSCLASSIFIERAPI.Utilities;


namespace RRHTSCLASSIFIERAPI
{
    class HTSCodeContextDapper
    {
        private readonly IConfiguration _config;


        public HTSCodeContextDapper(IConfiguration config)
        {


            _config = config;
        }


        public string LoadHTSCodes(string searchParam)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("HTSDBConnection"));

            string sqlQuery = Queries.GetHTSCodesSQLCommandText();

            var parameters = new DynamicParameters();
            parameters.Add("queryParam", searchParam, DbType.String);

            List<HTSCodeDumpData> hTSCodeDumpDatas = dbConnection.Query<HTSCodeDumpData>(sqlQuery, parameters).ToList();

            string resultJson = Utility.GetJsonHTSCode(hTSCodeDumpDatas);

            if(resultJson.StartsWith("{"))
            {
                resultJson = "["+resultJson+"]";
            }

            return resultJson;
        }

    }

}