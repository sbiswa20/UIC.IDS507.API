using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using RRHTSCLASSIFIERAPI.Models;

namespace RRHTSCLASSIFIERAPI
{
    class DataContextDapper
    {

        private readonly IConfiguration _config;

        public DataContextDapper(IConfiguration config)
        {

            _config = config;
        }

        public IEnumerable<T> LoadData<T>(String sql)
        {
            

                IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("HTSDBConnection"));

                return dbConnection.Query<T>(sql);

            
        }

         public T LoadDataSingle<T>(String sql)
        {

                String sqlQuery = @"
                select * from HTS_CODE_DUMP 
                where HTS_CD1 = (select HTS_CD1 from HTS_CODE_DUMP where Pr_Def like '%Motor starters%')
                and 
                (HTS_CD2 = ((select HTS_CD2 from HTS_CODE_DUMP where Pr_Def like '%Motor starters%'))
                or HTS_CD2 is null)
                ";
           

                IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("HTSDBConnection"));

                List<HTSCodeDumpData> hTSCodeDumpDatas = dbConnection.Query<HTSCodeDumpData>(sqlQuery).ToList();

                foreach (var item in hTSCodeDumpDatas)
                {
                    Console.WriteLine(item.HTS_CD1+ "    "+item.HTS_CD1+"  "+item.HTS_CD2+"  "+item.HTS_CD3+"  "+item.Pr_Def);
                }

                return dbConnection.QuerySingle<T>(sql);

            
        }

    }
}