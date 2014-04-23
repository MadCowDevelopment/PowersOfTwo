namespace PowersOfTwo.Dto
{
    public class RunningGameDto
    {
        public string GroupName { get; private set; }
        public PlayerDto Player1 { get; private set; }
        public PlayerDto Player2 { get; private set; }

        public RunningGameDto(string groupName, PlayerDto player1, PlayerDto player2)
        {
            GroupName = groupName;
            Player1 = player1;
            Player2 = player2;
        }

        public override bool Equals(object obj)
        {
            var other = obj as RunningGameDto;
            if (other == null) return false;
            return GroupName == other.GroupName;
        }

        public override int GetHashCode()
        {
            return GroupName.GetHashCode();
        }
    }
}
