namespace DataComp.Training.Models.SearchCriteria
{
    public class UserSearchCriteria : SearchCriteria
    {
        public string FullName { get; set; }
        public bool? IsRemoved { get; set; }
    }

    public class PermissionCriteria
    {
        public string[] Positions { get; set; }
        public string[] Departments { get; set; }
    }
}
