using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using System.Xml;
namespace ShiptalkLogic.BusinessObjects
{
    public class GoogleMapsService
    {
        private readonly string _apiKey;
        private readonly IWebProxy _webProxy;

        public GoogleMapsService(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException("apiKey");

            _apiKey = apiKey;
            _webProxy = WebRequest.GetSystemWebProxy();
            _webProxy.Credentials = CredentialCache.DefaultCredentials;
        }

        public LatLng GetLocation(string address)
        {
           // var url = string.Format("http://maps.google.com/maps/geo?output=xml&key={0}&q={1}", _apiKey, address);
            var url = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", address);
            var request = WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = -1;
            request.Proxy = _webProxy;

            try
            {
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        var geocodeResponse = new StreamReader(stream).ReadToEnd().DeserializeToType<GeocodeResponse>();

                        var lat = string.Empty;
                        var lng = string.Empty;
                        var zipCode = string.Empty;
                        var state = string.Empty;
                        var country = string.Empty;
                        var statusCode = string.Empty;
                        var type = string.Empty;
                        var accuracy = 0;

                        if (geocodeResponse.status == "REQUEST_DENIED" || geocodeResponse.status == "ZERO_RESULTS")
                        {
                            statusCode = "602";
                            return null;
                        }
                        else
                        {

                            var stateComp = geocodeResponse.result[0].address_component.Where(m => m.type[0].Value == "administrative_area_level_1").FirstOrDefault();
                            var countryComp = geocodeResponse.result[0].address_component.Where(m => m.type[0].Value == "country").FirstOrDefault();

                          

                            if (countryComp != null)
                                country = countryComp.short_name;

                            //type = geocodeResponse.result[0].address_component.
                            //country = geocodeResponse.result[0].address_component[3].short_name;

                            if (country == "US")
                            {

                                switch (geocodeResponse.result[0].geometry[0].location_type)
                                {
                                    case "ROOFTOP":
                                        accuracy = 9;
                                        break;
                                    case "RANGE_INTERPOLATED":
                                        accuracy = 8;
                                        break;
                                    case "GEOMETRIC_CENTER":
                                        accuracy = 6;
                                        break;
                                    case "APPROXIMATE":
                                        accuracy = 4;
                                        break;
                                }

                                if (accuracy > 3)
                                {
                                    lat = geocodeResponse.result[0].geometry[0].location[0].lat;
                                    lng = geocodeResponse.result[0].geometry[0].location[0].lng;
                                    if (stateComp != null)
                                        state = stateComp.short_name;
                                   
                                    return new LatLng("200", accuracy, lat, lng, state);
                                }
                                else
                                    return null;
                            }
                            else
                                return null;
                        }
                       
                    }
                }
            }
            catch (WebException)
            {
                return GetLocation(address);
            }
        }

        public string GetLocationRecepientZipCode(string address)
        {
            string url = string.Format("http://maps.google.com/maps/geo?output=csv&key={0}&q={1}", _apiKey, address);
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = -1;
            request.Proxy = _webProxy;

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        string[] csvResult = new StreamReader(stream).ReadToEnd().Split(',');

                        // cvs structure.
                        // [0] HTTP status code
                        // [1] accuracy
                        // [2] latitude
                        // [3] longitude

                        if ((csvResult.Length == 0) || (csvResult[0] != "200"))
                            return null;

                        return string.Format("{0}{1}{2}", csvResult[2], ",", csvResult[3]);
                    }
                }
            }
            catch (WebException)
            {
                return GetLocationRecepientZipCode(address);
            }
        }
    }
}
