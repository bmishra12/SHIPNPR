using System;

namespace ShiptalkLogic.BusinessObjects
{
    public sealed class LatLng
    {
        public LatLng()
        {
        }

        public LatLng(string statusCode, int accuracy, string lat, string lng, string state)
        {
            StatusCode = Convert.ToInt32(statusCode);
            Accuracy = Convert.ToInt32(accuracy);
            Lat = Convert.ToDecimal(lat);
            Lng = Convert.ToDecimal(lng);
            State = state;
        }       

        public LatLng(string statusCode, int accuracy, string lat, string lng, string zipcode, string RecipientZipCodelat,string RecipientZipCodeLng)
        {
            StatusCode = Convert.ToInt32(statusCode);
            Accuracy = Convert.ToInt32(accuracy);
            Lat = Convert.ToDecimal(lat);
            Lng = Convert.ToDecimal(lng);
            ZipCode = Convert.ToInt32(zipcode);
            RecipientZipCodeLatitude = Convert.ToDecimal(RecipientZipCodelat);
            RecipientZipCodeLongitude = Convert.ToDecimal(RecipientZipCodeLng);
        }

        public int StatusCode { get; set; }
        public int Accuracy { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public string State { get; set; }
        public decimal RecipientZipCodeLatitude { get; set; }
        public decimal RecipientZipCodeLongitude { get; set; }
        public int ZipCode { get; set; }
    }
}