using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
namespace SmartOffice.Class
{
    public class JsonFieldsCollector
    {
        private readonly Dictionary<string, JValue> fields;
        private string SType = "";
        private string SName = "";
        public JsonFieldsCollector(JToken token)
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
                        if (jToken.ToString() != "")
                        {
                            string text_type = jToken.Path.ToString().Substring(jToken.Path.ToString().LastIndexOf(@".") + 1);

                            if (text_type == "type" ) {
                                SType = jToken.ToString();
                            }

                            if ( text_type == "key")
                            {
                                SName = jToken.ToString();
                            }

                            if (SType != "" && SName != "")
                            {
                                fields.Add(SName, (JValue)SType);
                                SType = "";
                                SName = "";
                            }
                        }

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
