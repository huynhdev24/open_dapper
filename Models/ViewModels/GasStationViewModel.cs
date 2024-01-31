using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels
{
    public class GasStationViewModel
    {
        public List<string> GasStation { get; set; }
        public long GasStationId { get; set; }
        public string GasStationName { get; set; }
        public string Address { get; set; }
        public string OpeningTime { get; set; }
        public string GasTypes { get; set; }
        public long District { get; set; }
        public string DistrictName { get; set; }
        public Models.MDistrict DistrictView { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Rating { get; set; }
        public ICollection<GasStationFeedback> GasStationFeedback { get; set; }
        public DateTime FeedbackAt { get; set; }
        public ICollection<GasStationGasType> GasStationGasType { get; set; }
        public GasStationViewModel()
        {
            GasStationFeedback = new HashSet<GasStationFeedback>();
            GasStationGasType = new HashSet<GasStationGasType>();
            DistrictView = new MDistrict();
            DistrictView.DistrictId = District;
            DistrictView.DistrictName = DistrictName;
        }
    }
}
