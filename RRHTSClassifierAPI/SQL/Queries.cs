namespace RRHTSCLASSIFIERAPI.SQL;

public static class Queries{

    // public static string GetHTSCodesSQLCommandText()
    // {
    //     return $@"
    //         SELECT * FROM HTS_CODE_DUMP
    //         WHERE HTS_CD1 IN (SELECT HTS_CD1 FROM HTS_CODE_DUMP WHERE Pr_Def LIKE '%' + @queryParam + '%')
    //         AND (
    //             HTS_CD2 IN (SELECT HTS_CD2 FROM HTS_CODE_DUMP WHERE Pr_Def LIKE '%' + @queryParam + '%')
    //             OR HTS_CD2 IS NULL
    //         )";
    // }

    public static string GetHTSCodesSQLCommandText()
    {
        return $@"
            select * from HTS_CODE_DUMP 
                where HTS_CD1 in (select HTS_CD1 from HTS_CODE_DUMP where Pr_Def like '%' + @queryParam + '%')
                and 
                (HTS_CD2 in ((select HTS_CD2 from HTS_CODE_DUMP where Pr_Def like '%' + @queryParam + '%'))
                or HTS_CD2 is null
                )
                and 
                (HTS_CD3 in ((select HTS_CD3 from HTS_CODE_DUMP where Pr_Def like '%' + @queryParam + '%'))
                or HTS_CD3 is null
            )";
    }


     public static string GetHTSAllCodesSQLCommandText()
    {
        return $@"
            select * from HTS_CODE_DUMP";
    }




    
}