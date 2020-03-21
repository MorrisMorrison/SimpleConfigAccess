using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EazyMoney.Config
{

    public class Config
    {
        private JObject _config { get; set; }
        private string _separator { get; set; }
        public string this[string p_path]
        {
            get
            {
                string[] pathParameters = p_path.Split(_separator);
                JToken? currentNode = null;
                foreach (var pathParamter in pathParameters)
                {
                    if (currentNode != null)
                    {
                        currentNode = currentNode[pathParamter];
                    }
                    else
                    {
                        currentNode = _config[pathParamter];
                    }
                }
                
                return currentNode.Value<string>();
            }
        }
        public Config(string p_pathToConfigFile, string p_separator = ":")
        {
            _config = JObject.Parse(File.ReadAllText(p_pathToConfigFile));
            _separator = p_separator;
        }
    }
}