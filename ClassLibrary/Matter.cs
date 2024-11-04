namespace ClassLibrary
{

    public class Matter
    {

        public string MatterCode { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }

        public Matter() 
        {
        
        }

        public Matter(string name, string matterCode, string departmentName)
        {

            MatterCode = matterCode;
            Name = name;
            DepartmentName = departmentName;

        }

        public override string ToString()
        {

            return Name + " - " + MatterCode + " - " + DepartmentName;
        
        }

    }

}
