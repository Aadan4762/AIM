using System;

namespace AIM.Models.Entities
{
    public class School
    {
        public Guid Id { get; set; }
        public string InstitutionName { get; set; }
        public string County { get; set; }
        public string SubCounty { get; set; }
        public string Zone { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string Size { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string TitleDeed { get; set; }
    }
}