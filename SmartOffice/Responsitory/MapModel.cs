using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
namespace SmartOffice.Class
{
    public class MapModel
    {
        private readonly Dictionary<string, JValue> fields;
        public MapModel(JToken token)
        {
            fields = new Dictionary<string, JValue>();
            CollectFields(token);
        }

        private void CollectFields(JToken jToken)
        {          
           
            try
            {
                switch (jToken.Type)
                {
                    case JTokenType.Object:
                        foreach (var child in jToken.Children<JProperty>())
                            CollectFields(child);
                        break;
                    case JTokenType.Array:
                        foreach (var child in jToken.Children())
                            CollectFields(child);
                        break;
                    case JTokenType.Property:
                        CollectFields(((JProperty)jToken).Value);
                        break;
                    default:                     
                                fields.Add(jToken.Path.ToString(), (JValue)jToken.ToString());                      
                        break;
                }
              
            }
            catch (Exception ex)
            {
                //this.fields = null;
                Console.Write(ex.Message);
            }
            
          
        }

        public IEnumerable<KeyValuePair<string, JValue>> GetAllFields() => fields;
    }
}
