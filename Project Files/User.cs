using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace StartComputercraft
{
    public static class User
    {
        // Demo player used as default if no profile found 
        public static string UserName = "demo-b3fa14ff49a556c6fbd3352845b006c1";
        public static string UserID = "b3fa14ff49a556c6fbd3352845b006c1"; // found after: "profiles": {"<?>": {"displayName": "<?>"}} or "selectedUser";
        public static string AccessToken = "e3b132adbf4c4d319d2fbf8239bdce58";
        static Dictionary<string, string> dictUser;

        public static bool ReadUser(string filePathAndName)
        {
            bool success = true;
            dictUser = new Dictionary<string, string>();
            if (File.Exists(filePathAndName))
            {
                bool dataStarted = false;
                string key = string.Empty;
                string value = string.Empty;
                string dat = File.ReadAllText(filePathAndName);
                JsonTextReader reader = new JsonTextReader(new StringReader(dat));
                while (reader.Read())
                {
                    if (dataStarted)
                    {
                        if (reader.TokenType == JsonToken.PropertyName || reader.TokenType == JsonToken.String)
                        {
                            if (reader.TokenType == JsonToken.PropertyName)
                            {
                                if (key != string.Empty)
                                {
                                    value = reader.Value.ToString();
                                    if (dictUser.ContainsKey(key))
                                    {
                                        dictUser[key] = value;
                                    }
                                    else
                                    {
                                        dictUser.Add(key, value);
                                    }
                                    key = string.Empty;
                                }
                                else
                                {
                                    key = reader.Value.ToString();
                                }
                            }
                            else if (reader.TokenType == JsonToken.String)
                            {
                                if (key != string.Empty)
                                {
                                    value = reader.Value.ToString();
                                    if (dictUser.ContainsKey(key))
                                    {
                                        dictUser[key] = value;
                                    }
                                    else
                                    {
                                        dictUser.Add(key, value);
                                    }
                                    key = string.Empty;
                                }
                            }
                        }
                        if (dictUser.Count >= 3)
                        {
                            dataStarted = false; //Dictionary read
                        }
                    }
                    if (reader.TokenType == JsonToken.PropertyName && reader.Value.ToString() == "authenticationDatabase")
                    {
                        key = "userid"; // equates to userid on old profiles
                        dataStarted = true;
                    }
                }
                dictUser.TryGetValue("accessToken", out AccessToken);
                dictUser.TryGetValue("userid", out UserID);
                dictUser.TryGetValue("username", out UserName);
            }
            else
            {
                success = false;
                Console.WriteLine("Unable to locate 'launcher_profiles.json'");
            }
            return success;
        }
    }
}
