using System;
using System.Collections.Generic;
using System.Linq;

namespace ShiptalkLogic.BusinessObjects
{
    [Serializable]
    public struct State
    {
        private static readonly IDictionary<string, KeyValuePair<string, string>> _stateFIPS
            = new Dictionary<string, KeyValuePair<string, string>>
                  {
                      {"01", new KeyValuePair<string, string>("AL", "Alabama")}
                      ,
                      {"02", new KeyValuePair<string, string>("AK", "Alaska")}
                      ,
                      {"04", new KeyValuePair<string, string>("AZ", "Arizona")}
                      ,
                      {"05", new KeyValuePair<string, string>("AR", "Arkansas")}
                      ,
                      {"06", new KeyValuePair<string, string>("CA", "California")}
                      ,
                      {"08", new KeyValuePair<string, string>("CO", "Colorado")}
                      ,
                      {"09", new KeyValuePair<string, string>("CT", "Connecticut")}
                      ,
                      {"10", new KeyValuePair<string, string>("DE", "Delaware")}
                      ,
                      {"11", new KeyValuePair<string, string>("DC", "District of Columbia")}
                      ,
                      {"12", new KeyValuePair<string, string>("FL", "Florida")}
                      ,
                      {"13", new KeyValuePair<string, string>("GA", "Georgia")}
                      ,
                      {"15", new KeyValuePair<string, string>("HI", "Hawaii")}
                      ,
                      {"16", new KeyValuePair<string, string>("ID", "Idaho")}
                      ,
                      {"17", new KeyValuePair<string, string>("IL", "Illinois")}
                      ,
                      {"18", new KeyValuePair<string, string>("IN", "Indiana")}
                      ,
                      {"19", new KeyValuePair<string, string>("IA", "Iowa")}
                      ,
                      {"20", new KeyValuePair<string, string>("KS", "Kansas")}
                      ,
                      {"21", new KeyValuePair<string, string>("KY", "Kentucky")}
                      ,
                      {"22", new KeyValuePair<string, string>("LA", "Louisiana")}
                      ,
                      {"23", new KeyValuePair<string, string>("ME", "Maine")}
                      ,
                      {"24", new KeyValuePair<string, string>("MD", "Maryland")}
                      ,
                      {"25", new KeyValuePair<string, string>("MA", "Massachusetts")}
                      ,
                      {"26", new KeyValuePair<string, string>("MI", "Michigan")}
                      ,
                      {"27", new KeyValuePair<string, string>("MN", "Minnesota")}
                      ,
                      {"28", new KeyValuePair<string, string>("MS", "Mississippi")}
                      ,
                      {"29", new KeyValuePair<string, string>("MO", "Missouri")}
                      ,
                      {"30", new KeyValuePair<string, string>("MT", "Montana")}
                      ,
                      {"31", new KeyValuePair<string, string>("NE", "Nebraska")}
                      ,
                      {"32", new KeyValuePair<string, string>("NV", "Nevada")}
                      ,
                      {"33", new KeyValuePair<string, string>("NH", "New Hampshire")}
                      ,
                      {"34", new KeyValuePair<string, string>("NJ", "New Jersey")}
                      ,
                      {"35", new KeyValuePair<string, string>("NM", "New Mexico")}
                      ,
                      {"36", new KeyValuePair<string, string>("NY", "New York")}
                      ,
                      {"37", new KeyValuePair<string, string>("NC", "North Carolina")}
                      ,
                      {"38", new KeyValuePair<string, string>("ND", "North Dakota")}
                      ,
                      {"39", new KeyValuePair<string, string>("OH", "Ohio")}
                      ,
                      {"40", new KeyValuePair<string, string>("OK", "Oklahoma")}
                      ,
                      {"41", new KeyValuePair<string, string>("OR", "Oregon")}
                      ,
                      {"42", new KeyValuePair<string, string>("PA", "Pennsylvania")}
                      ,
                      {"44", new KeyValuePair<string, string>("RI", "Rhode Island")}
                      ,
                      {"45", new KeyValuePair<string, string>("SC", "South Carolina")}
                      ,
                      {"46", new KeyValuePair<string, string>("SD", "South Dakota")}
                      ,
                      {"47", new KeyValuePair<string, string>("TN", "Tennessee")}
                      ,
                      {"48", new KeyValuePair<string, string>("TX", "Texas")}
                      ,
                      {"49", new KeyValuePair<string, string>("UT", "Utah")}
                      ,
                      {"50", new KeyValuePair<string, string>("VT", "Vermont")}
                      ,
                      {"51", new KeyValuePair<string, string>("VA", "Virginia")}
                      ,
                      {"53", new KeyValuePair<string, string>("WA", "Washington")}
                      ,
                      {"54", new KeyValuePair<string, string>("WV", "West Virginia")}
                      ,
                      {"55", new KeyValuePair<string, string>("WI", "Wisconsin")}
                      ,
                      {"56", new KeyValuePair<string, string>("WY", "Wyoming")}
                      ,
                      {"60", new KeyValuePair<string, string>("AS", "American Samoa")}
                      ,
                      {"64", new KeyValuePair<string, string>("FM", "Fed States of Micronesa")}
                      ,
                      {"66", new KeyValuePair<string, string>("GU", "Guam")}
                      ,
                      {"68", new KeyValuePair<string, string>("MH", "Marshall Islands")}
                      ,
                      {"69", new KeyValuePair<string, string>("MP", "Northern Mariana Island")}
                      ,
                      {"70", new KeyValuePair<string, string>("PW", "Palau")}
                      ,
                      {"72", new KeyValuePair<string, string>("PR", "Puerto Rico")}
                      ,
                      {"78", new KeyValuePair<string, string>("VI", "Virgin Islands of the US")}
                       ,
                      {"99", new KeyValuePair<string, string>("CM", "CMS")}
                  };

        public State(string fips)
        {
            _code = null;
            _stateAbbr = null;
            _stateName = null;

            if (string.IsNullOrEmpty(fips))
                return;

            var pair = new KeyValuePair<string, string>();
            var code = FindCode(fips);

            if (code != null)
                pair = GetState(code);
            else
            {
                var state = FindState(fips);

                if (state.Key != null)
                {
                    code = GetCode(state.Key);
                    pair = GetState(code);
                }
            }

            _code = code;
            _stateAbbr = pair.Key;
            _stateName = pair.Value;
        }

        private readonly string _code;
        public string Code { get { return _code;} }

        private readonly string _stateName;
        public string StateName { get { return _stateName; } }
        
        private readonly string _stateAbbr;
        public string StateAbbr { get { return _stateAbbr; } }

        public static KeyValuePair<string, string> GetState(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentException("code");

            var pair = (from stateFips in _stateFIPS
                        where stateFips.Key == code
                        select stateFips.Value).FirstOrDefault();

            //TODO: Confirm if this logic is correct.
            if (pair.Key == null)
                throw new ArgumentOutOfRangeException("code");

            return pair;
        }

        public static IEnumerable<KeyValuePair<string, string>> GetStates()
        {
            var states = new List<KeyValuePair<string, string>>();
            
            foreach (var entry in _stateFIPS)
                if (entry.Key != "99") states.Add(entry.Value);

            return states;
        }

        public static string GetCode(string stateAbbr)
        {
            if (string.IsNullOrEmpty(stateAbbr))
                throw new ArgumentException("stateAbbr");

            var code = (from stateFips in _stateFIPS
                         where stateFips.Value.Key == stateAbbr.ToUpper() 
                         select stateFips.Key).FirstOrDefault();

            if (code == null)
                throw new ArgumentOutOfRangeException("stateAbbr");

            return code;
        }

        private static string FindCode(string code)
        {
            var result = (from stateFips in _stateFIPS
                        where stateFips.Key == code
                        select stateFips.Key).FirstOrDefault();

            return result;
        }

        private static KeyValuePair<string, string> FindState(string stateAbbr)
        {
            var result = (from stateFips in _stateFIPS
                        where stateFips.Value.Key == stateAbbr.ToUpper()
                        select stateFips.Value).FirstOrDefault();

            return result;
        }

        public static string GetStateFIPSForCMS()
        {
            return GetCode("CM");
        }

        public static IEnumerable<KeyValuePair<string, string>> GetStatesWithFIPSKey()
        {
            var states = new List<KeyValuePair<string, string>>();
            string key = string.Empty;
            string val = string.Empty;
            foreach (var entry in _stateFIPS)
            {
                key = entry.Key;
                val = entry.Value.Value;
                states.Add(new KeyValuePair<string, string>(key, val));
            }

                //states.Add(entry.Key, ((KeyValuePair<string, string>)entry.Value).Value);

            return states;
        }

        public static string GetStateName(string FIPSCode)
        {
            return GetState(FIPSCode).Value;
        }
    }
}