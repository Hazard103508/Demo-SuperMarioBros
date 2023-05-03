namespace UnityShared.Files
{
    public static class Json
    {
        /// <summary>
        /// Serialize an object to Json
        /// </summary>
        /// <param name="obj">Object to Serialize</param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            return Serialize(obj, null);
        }
        /// <summary>
        /// Serialize an object to Json
        /// </summary>
        /// <param name="obj">Object to Serialize</param>
        /// <param name="jsonConverters">custom json converters</param>
        /// <returns></returns>
        public static string Serialize(object obj, params Newtonsoft.Json.JsonConverter[] jsonConverters)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, jsonConverters);
        }
        /// <summary>
        /// Deserialize a json to an object
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize</typeparam>
        /// <param name="json">json text to deserialize </param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
    }
}
