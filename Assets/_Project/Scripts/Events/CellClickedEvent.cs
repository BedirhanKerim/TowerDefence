namespace _Project.Scripts.Events
{
    public readonly struct CellClickedEvent : IGameEvent
    {
        public readonly int Row;
        public readonly int Column;

        public CellClickedEvent(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
