using RRHTSCLASSIFIERAPI.Models;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.CompilerServices;


namespace RRHTSCLASSIFIERAPI.Utilities;

class Utility
{
    public Utility()
    {

    }

    public static List<HTSCodeSequence> GetHTSCodeSequenceData(List<HTSCodeDumpData> hTSCodeDumpDatas)
    {
        List<HTSCodeSequence> hTSCodes = new List<HTSCodeSequence>();

        foreach(var data in hTSCodeDumpDatas)
        {
            int Level = data.HTS_CD_SEQ;

            string ID = data.HTS_CD1+"_"+data.HTS_CD2+"_"+data.HTS_CD3+"_"+data.HTS_CD4+"_"+data.HTS_CD5+"_"+data.HTS_CD6+"_"+data.HTS_CD7+"_"+data.HTS_CD8+"_"+data.HTS_CD9+"_"+data.HTS_CD10;

            string HTSDefination = data.Pr_Def;

            string cleanedID = Regex.Replace(ID, @"(\d+)_{2,}", "$1");

            HTSCodeSequence hTSCodeSequence = new HTSCodeSequence();
            hTSCodeSequence.HTSDefination = HTSDefination;
            hTSCodeSequence.Id = cleanedID;
            hTSCodeSequence.Level = Level;



            Console.WriteLine(Level+","+cleanedID+","+HTSDefination);

            hTSCodes.Add(hTSCodeSequence);
        }





        return hTSCodes;

    }


    public static string GetJsonHTSCode(List<HTSCodeDumpData> hTSCodeDumpDatas)
    {
        string jsonHtsCode = "";

        try{

            List<HTSCodeSequence> hTSCodeRec = GetHTSCodeSequenceData(hTSCodeDumpDatas);
            Console.WriteLine(hTSCodeRec.Count());

            hTSCodeRec = hTSCodeRec.OrderBy(x => x.Level).ThenBy(x => x.Id).ToList();

            Node root = null;
            
            Dictionary<string, Node> nodeLookup = new Dictionary<string, Node>();

            foreach(var dataRecords in hTSCodeRec)
            {
                int level = dataRecords.Level;
                string id = dataRecords.Id;
                string description = dataRecords.HTSDefination;

                Node newNode = new Node(id, description);

                if (level == 1)
                {
                    root = newNode;
                }
                else
                {
                    // Find the parent node to add this new node
                    string parentId = id.Substring(0, id.LastIndexOf('_'));
                    if (nodeLookup.ContainsKey(parentId))
                    {
                        nodeLookup[parentId].AddChild(newNode);
                    }
                }
                nodeLookup[id] = newNode;


            }

            

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            jsonHtsCode = JsonSerializer.Serialize(root, options);
            Console.WriteLine("JSON = "+jsonHtsCode);

        }
        catch(Exception ex)
        {
            Console.WriteLine("Exception  = "+ex);
        }

    


        return jsonHtsCode;

    }
}