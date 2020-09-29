namespace Tetris
{
    public readonly struct Rotation
	{
        private readonly byte val;

        public static readonly Rotation None;
        public static readonly Rotation Right = new Rotation(1);
        public static readonly Rotation Uturn = new Rotation(2);
        public static readonly Rotation Left = new Rotation(3);

        private Rotation(int val) => this.val = (byte)(val & 3);

        public Rotation Rotate(int steps) => new Rotation(val + steps);

        public override string ToString() => new[] { "None", "Right", "U-turn", "Left" }[val];

        public static implicit operator int(Rotation r)=> r.val;
	}
}
