using System.Collections.Generic;

namespace BadamApplicationAndForum.Data.Dtos.Professors
{
    public class ProfessorListingModel
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string EducationLevel { get; set; }
        public string Field { get; set; }
        public string Group { get; set; }
        public string ScopusLink { get; set; }
        public string ScholarLink { get; set; }
        public string OrcidLink { get; set; }
        public string PublonsLink { get; set; }
        public string WosLink { get; set; }
        public string ProfessorInterests { get; set; } = "";
        public string OragnizationalObligation { get; set; } = "";
        public string About { get; set; } = "";
        public string NormalName { get; set; }
    }
}