using System.Reflection.Metadata.Ecma335;
using System.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SimpleConfigAccess.Config
{

    public class Config
    {
        private JObject _config { get; set; }
        private string _separator { get; set; }

        public object this[string p_path]
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
                Type t = currentNode.GetType();
                switch (t)
                {
                    case Type jValueType when jValueType == typeof(JValue):
                    return currentNode.Value<string>();
                    case Type jArrayType when jArrayType == typeof(JArray):
                    return currentNode.Value<JArray>().ToObject<IList<string>>();
                    default:
                    return currentNode.Value<object>();
                }
            }
        }
        public Config(string p_pathToConfigFile, string p_separator = ":")
        {
            _config = JObject.Parse(File.ReadAllText(p_pathToConfigFile));
            _separator = p_separator;
        }
    }
}