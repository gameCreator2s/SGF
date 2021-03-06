﻿/*
 * Copyright (C) 2018 Slicol Tang. All rights reserved.
 * 
 * 服务器（模块）管理器
 * Server (module) manager
 * 
 * Licensed under the MIT License (the "License"); 
 * you may not use this file except in compliance with the License. 
 * You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, 
 * software distributed under the License is distributed on an "AS IS" BASIS, 
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, 
 * either express or implied. 
 * See the License for the specific language governing permissions and limitations under the License.
*/

using System.Collections.Generic;
using SGF.Common;
using SGF.Utils;

namespace SGF.Server
{
    public class ServerModuleInfo
    {
        public int id;
        public string name;
        public string assembly;
        public int port;
    }

    public class ServerConfig
    {
        internal static string Namespace = "SGF.Server";
        internal readonly static string Path = "./ServerConfig.json";

        private readonly static MapList<int, ServerModuleInfo> MapServerModuleInfo = new MapList<int, ServerModuleInfo>();


        public static ServerModuleInfo GetServerModuleInfo(int id)
        {
            if (MapServerModuleInfo.Count == 0)
            {
                ReadConfig();
            }

            return MapServerModuleInfo[id];
        }

        private static void ReadConfig()
        {
            Debuger.Log();
            string jsonStr = FileUtils.ReadString(Path);
            var obj = MiniJSON.Json.Deserialize(jsonStr) as List<object>;
            for (int i = 0; i < obj.Count; i++)
            {
                var infoJson = obj[i] as Dictionary<string, object>;
                ServerModuleInfo info = new ServerModuleInfo();
                info.id = (int)(long)infoJson["id"];
                info.name = (string)infoJson["name"];
                info.assembly = (string)infoJson["assembly"];
                info.port = (int)(long)infoJson["port"];
                MapServerModuleInfo.Add(info.id, info);
            }
        }

    }
}