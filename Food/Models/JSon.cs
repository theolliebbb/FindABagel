using System;
using System.Collections.Generic;
using SQLite;

namespace Food.Models
{
    public class JSon
    { }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class SearchMetadata
        {
            public string id { get; set; }
            public string status { get; set; }
            public string json_endpoint { get; set; }
            public string created_at { get; set; }
            public string processed_at { get; set; }
            public string google_maps_url { get; set; }
            public string raw_html_file { get; set; }
            public double total_time_taken { get; set; }
        }

        public class SearchParameters
        {
            public string engine { get; set; }
            public string type { get; set; }
            public string q { get; set; }
            public string ll { get; set; }
            public string google_domain { get; set; }
            public string hl { get; set; }
        }

        public class SearchInformation
        {
            public string local_results_state { get; set; }
            public string query_displayed { get; set; }
        }

        public class GpsCoordinates
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
        }

        public class ServiceOptions
        {
            public bool takeout { get; set; }
            public bool delivery { get; set; }
            public bool? dine_in { get; set; }
            public bool? curbside_pickup { get; set; }
        }

    public class LocalResult
    {

        public int position { get; set; }
        public string title { get; set; }
        public string place_id { get; set; }
        public string data_id { get; set; }
        public string data_cid { get; set; }
        public string reviews_link { get; set; }
        public string photos_link { get; set; }
        public GpsCoordinates gps_coordinates { get; set; }
        public string place_id_search { get; set; }
        public double rating { get; set; }
        public int reviews { get; set; }
        public string price { get; set; }
        public string type { get; set; }
        public string address { get; set; }
        public string open_state { get; set; }
        public string hours { get; set; }
        public string website { get; set; }
        public ServiceOptions service_options { get; set; }
        public string thumbnail { get; set; }
        public bool? unclaimed_listing { get; set; }
        public string description { get; set; }
        public string phone { get; set; }
    }
    public class LocalResult2
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int position { get; set; }
        public string title { get; set; }
        public string website { get; set; }
        public string place_id { get; set; }

        public double rating { get; set; }

        public string price { get; set; }

        public string address { get; set; }

        public string hours { get; set; }

        public string thumbnail { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string description { get; set; }
        public string phone { get; set; }
    }

    public class SerpapiPagination
        {
            public string next { get; set; }
        }

        public class Root
        {
            public SearchMetadata search_metadata { get; set; }
            public SearchParameters search_parameters { get; set; }
            public SearchInformation search_information { get; set; }
            public List<LocalResult> local_results { get; set; }
            public SerpapiPagination serpapi_pagination { get; set; }
        }


    }

