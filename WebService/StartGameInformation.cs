namespace WebService
{
    public class StartGameInformation
    {
        public string GroupName { get; private set; }
        public string Name { get; private set; }
        public int StartPoints { get; private set; }

        public StartGameInformation(string groupName, string name, int startPoints)
        {
            GroupName = groupName;
            Name = name;
            StartPoints = startPoints;
        }
    }
}